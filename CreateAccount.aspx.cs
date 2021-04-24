using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DogeBookLibrary;

namespace DogeBook
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };
        public AccountManagementService.AccountManagement proxy = new AccountManagementService.AccountManagement();

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected bool EmailUsed(string email)
        {
            return proxy.EmailUsed(email);
        }

        protected string InputValidation()
        {
            string warning = "";
            
            if (string.IsNullOrWhiteSpace(TBEmail.Text))
            {
                warning += "Enter email. <br>";
            }
            else
            {
                // check if this email has been used before
                if (EmailUsed(TBEmail.Text))
                {
                    warning += "Email is already used. <br>";
                }

            } 
            if (string.IsNullOrWhiteSpace(TBFirstName.Text))
            {
                warning += "Enter a first name. <br> ";
            }
            if (string.IsNullOrWhiteSpace(TBLastName.Text))
            {
                warning += "Enter a last name. <br> ";
            }
            if (string.IsNullOrWhiteSpace(TBPassword.Text))
            {
                warning += "Enter a password. <br> ";
            }
            if (TBPassword.Text != TBConfirmPassword.Text)
            {
                warning += "Passwords do not match. <br> ";
            }
            if (string.IsNullOrWhiteSpace(TBSecurityQuestion1.Text))
            {
                warning += "Enter an answer for security question 1. <br> ";
            }
            if (string.IsNullOrWhiteSpace(TBSecurityQuestion2.Text))
            {
                warning += "Enter an answer for security question 2. <br> ";
            }
            if (string.IsNullOrWhiteSpace(TBSecurityQuestion3.Text))
            {
                warning += "Enter an answer for security question 3. <br> ";
            }

            return warning;
        }
        protected bool CreateAccountFromUserInput()
        {
            string firstName = TBFirstName.Text;
            string lastName = TBLastName.Text;
            string email = TBEmail.Text;
            string password = TBPassword.Text;
            if (proxy.CreateAccount(firstName, lastName, email, password))
            {
                return true;
            }
            return false;
        }
        protected bool SendVerificationEmail()
        {
            string toEmail = TBEmail.Text;
            string fromEmail = "tui30639@temple.edu";
            string subject = "Verify your DogeBook Account";
            string body = "Go to ____/VerifyAccount.aspx";
            string cc = "";
            string bcc = "";

            // create email object
            Email verificationEmail = new Email();
            // send the email
            bool sent = verificationEmail.SendMail(toEmail, fromEmail, subject, body, cc, bcc);
            //bool sent = true;
            return sent;
        }
        protected bool AddSecurityQuestions()
        {
            string email = TBEmail.Text;
            // get security question and answer
            string question1 = DDLSecurityQuestion1.SelectedValue.ToString();
            string answer1 = TBSecurityQuestion1.Text;

            string question2 = DDLSecurityQuestion2.SelectedValue;
            string answer2 = TBSecurityQuestion2.Text;

            string question3 = DDLSecurityQuestion3.SelectedValue;
            string answer3 = TBSecurityQuestion3.Text;



            // get user id 
            int userId = proxy.GetUserIdFromEmail(email);

            if (userId == -1) // if no userId, can't find user Id
            {
                return false;
            }


            // insert security questions 

            bool insert1 = AddSecurityQuestion(userId, question1, answer1);
            bool insert2 = AddSecurityQuestion(userId, question2, answer2);
            bool insert3 = AddSecurityQuestion(userId, question3, answer3);

            // if all inserted
            if (insert1 && insert2 && insert3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool AddSecurityQuestion(int userId, string question, string answer)
        {
            return proxy.AddSecurityQuestion(userId, question, answer);
        }


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string warning = InputValidation();

            // if there is a warning
            if (warning != "")
            {
                LblWarning.Visible = true;
                LblWarning.Text = warning;
                return;
            }

            // no warning, create an account
            bool createdAccount = CreateAccountFromUserInput();

            // send an email to validate
            //bool sentVerification = SendVerificationEmail();
            bool sentVerification = true;

            // add security questions
            bool addedSecurityQuestions = AddSecurityQuestions();

            // if successfully created account and sent email
            if (createdAccount && sentVerification && addedSecurityQuestions)
            {
                if (RemeberChkBox.Checked)
                {
                    String plainTextEmail = TBEmail.Text;
                    String plainTextPassword = TBPassword.Text;
                    String encryptedEmail;
                    String encryptedPassword;

                    UTF8Encoding encoder = new UTF8Encoding();
                    Byte[] emailBytes;
                    Byte[] passwordBytes;

                    emailBytes = encoder.GetBytes(plainTextEmail);
                    passwordBytes = encoder.GetBytes(plainTextPassword);

                    RijndaelManaged rmEncryption = new RijndaelManaged();
                    MemoryStream memStream = new MemoryStream();
                    CryptoStream encryptionStream = new CryptoStream(memStream, rmEncryption.CreateEncryptor(key, vector), CryptoStreamMode.Write);

                    //Store Cookie
                    //Email
                    encryptionStream.Write(emailBytes, 0, emailBytes.Length);
                    encryptionStream.FlushFinalBlock();

                    memStream.Position = 0;
                    Byte[] encryptedEmailBytes = new byte[memStream.Length];
                    memStream.Read(encryptedEmailBytes, 0, encryptedEmailBytes.Length);

                    encryptionStream.Close();
                    memStream.Close();

                    //password
                    memStream = new MemoryStream();
                    encryptionStream = new CryptoStream(memStream, rmEncryption.CreateEncryptor(key, vector), CryptoStreamMode.Write);

                    encryptionStream.Write(passwordBytes, 0, passwordBytes.Length);
                    encryptionStream.FlushFinalBlock();

                    memStream.Position = 0;
                    Byte[] encryptedPasswordBytes = new byte[memStream.Length];
                    memStream.Read(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
                    encryptedEmail = Convert.ToBase64String(encryptedEmailBytes);
                    encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);

                    HttpCookie myCookie = new HttpCookie("LoginCookie");
                    myCookie.Values["Email"] = encryptedEmail;
                    myCookie.Values["Password"] = encryptedPassword;
                    myCookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(myCookie);
                }
                CreatedAccountSuccessfully();
            }
            else
            {
                CreatedAccountFailed();
            }
            return;
        }

        protected void CreatedAccountSuccessfully()
        {
            LblWarning.Visible = false;
            LblSuccess.Visible = true;
            LblSuccess.Text = "Account created successfully. <br/> Check your email to verify your account";
            BtnRedirectToLogin.Visible = true;
        }
        protected void CreatedAccountFailed()
        {
            LblSuccess.Visible = false;
            LblWarning.Visible = true;
            LblWarning.Text = "Failed to create account.";
        }

        protected void BtnRedirectToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}