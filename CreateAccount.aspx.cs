using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        public AccountManagementService.AccountManagement proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            proxy = new AccountManagementService.AccountManagement();
        }
        protected bool EmailUsed(string email)
        {
            return proxy.EmailUsed(email);
        }

        protected string InputValidation()
        {
            string warning = "";
            
            if (TBEmail.Text == "")
            {
                warning += "Enter email. <br>";
            } else { 
                // check if this email has been used before
                if (EmailUsed(TBEmail.Text))
                {
                    warning += "Email is already used. <br>";
                }
            } 
            if (TBFirstName.Text == "")
            {
                warning += "Enter a first name. <br> ";
            }
            if (TBLastName.Text == "")
            {
                warning += "Enter a last name. <br> ";
            }
            if (TBPassword.Text == "")
            {
                warning += "Enter a password. <br> ";
            }
            if (TBPassword.Text != TBConfirmPassword.Text)
            {
                warning += "Passwords do not match. <br> ";
            }
            if (TBSecurityQuestion1.Text == "")
            {
                warning += "Enter an answer for security question 1. <br> ";
            }
            if (TBSecurityQuestion2.Text == "")
            {
                warning += "Enter an answer for security question 2. <br> ";
            }
            if (TBSecurityQuestion3.Text == "")
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
            // !!!important fix me
            string email = TBEmail.Text;

            return true;
        }
        protected bool AddSecurityQuestions()
        {
            string email = TBEmail.Text;
            // get security question and answer
            string question1 = DDLSecurityQuestion1.SelectedValue;
            string answer1 = TBSecurityQuestion1.Text;

            string question2 = DDLSecurityQuestion1.SelectedValue;
            string answer2 = TBSecurityQuestion2.Text;

            string question3 = DDLSecurityQuestion1.SelectedValue;
            string answer3 = TBSecurityQuestion3.Text;


            // DELETE
            Response.Write(question1);
            Response.Write(answer1);
            Response.Write(email);


            // get user id 
            int userId = proxy.GetUserIdFromEmail(email);

            if (userId == -1) // if no userId, can't find user Id
            {
                return false;
            }


            // insert security questions 

            // THIS DON'T WORK !!!!!!!
            bool insert1 = proxy.AddSecurityQuestion(userId, question1, answer1);
            bool insert2 = proxy.AddSecurityQuestion(userId, question2, answer2);
            bool insert3 = proxy.AddSecurityQuestion(userId, question3, answer3);

            // if all inserted
            if (insert1 && insert2 && insert3)
            {
                return true;
            } else 
            {
                return false;
            }
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
            //!!!! FIX
            //bool createdAccount = CreateAccountFromUserInput();
            bool createdAccount = true;

            // send an email to validate
            bool sentVerification = SendVerificationEmail();

            // add security questions
            bool addedSecurityQuestions = AddSecurityQuestions();

            // if successfully created account and sent email
            if (createdAccount && sentVerification && addedSecurityQuestions) 
            {
                CreatedAccountSuccessfully();
            } else
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
        }
        protected void CreatedAccountFailed()
        {
            LblSuccess.Visible = false;
            LblWarning.Visible = true;
            LblWarning.Text = "Failed to create account.";
        }
    }
}