using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;     // needed for the encryption classes
using System.IO;                        // needed for the MemoryStream
using System.Text;                      // needed for the UTF8 encoding
using System.Net;                       // needed for the cookie
using System.Data.SqlClient;
using System.Data;
using DogeBookLibrary;

namespace DogeBook
{
    public partial class Login : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        SqlCommand myCommandObj = new SqlCommand();
        Utility util = new Utility();
        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

        protected void Page_Load(object sender, EventArgs e)
        {
            // Read encrypted password from cookie
            if (!IsPostBack && Request.Cookies["LoginCookie"] != null)
            {
                // clear the session userid
                // redirect to login if the user if is null 
                Session["UserId"] = null;

                HttpCookie myCookie = Request.Cookies["LoginCookie"];
                String encryptedEmail = myCookie.Values["Email"];
                String encryptedPassword = myCookie.Values["Password"];

                Byte[] encryptedEmailBytes = Convert.FromBase64String(encryptedEmail);
                Byte[] emailBytes;
                String plainTextEmail;

                UTF8Encoding encoder = new UTF8Encoding();

                RijndaelManaged rEncrypt = new RijndaelManaged();
                MemoryStream ms = new MemoryStream();
                CryptoStream decryptStream = new CryptoStream(ms, rEncrypt.CreateDecryptor(key, vector), CryptoStreamMode.Write);

                //Email
                decryptStream.Write(encryptedEmailBytes, 0, encryptedEmailBytes.Length);
                decryptStream.FlushFinalBlock();

                ms.Position = 0;
                emailBytes = new Byte[ms.Length];
                ms.Read(emailBytes, 0, emailBytes.Length);

                decryptStream.Close();
                ms.Close();

                plainTextEmail = encoder.GetString(emailBytes);
                TxtEmail_SignIn.Text = plainTextEmail;
                //Password
                Byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);
                Byte[] passwordBytes;
                String plainTextPassword;

                ms = new MemoryStream();
                decryptStream = new CryptoStream(ms, rEncrypt.CreateDecryptor(key, vector), CryptoStreamMode.Write);

                decryptStream.Write(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
                decryptStream.FlushFinalBlock();

                ms.Position = 0;
                passwordBytes = new Byte[ms.Length];
                ms.Read(passwordBytes, 0, passwordBytes.Length);

                decryptStream.Close();
                ms.Close();

                plainTextPassword = encoder.GetString(passwordBytes);
                TxtPassword_SignIn.Attributes.Add("value", plainTextPassword);
                //TxtPassword_SignIn.Text = plainTextPassword;
                RemeberChkBox.Checked = true;


            }
        }

        //CreateAccount -> Ask to remember me? -> Store LoginCookie
        //Login -> AutoFill from LoginCookie
        //Login -> Log in succesfully && Remember Me Checked -> Override (Store) or do nothing to cookie?
        //Login -> Log in succesfully && Remember Me unchecked -> delete cookie
        //-> make sure remember me is checked if there is a cookie and unchecked if not
        //Login -> Log in fail && Remember Me Checked -> Don't Override or do anything to cookie
        //Login -> Log in fail && Remember Me Unchecked -> Don't delete cookie
        //

        //makse session["userid"] the user's id

        protected void Btn_SignIn_Click(object sender, EventArgs e)
        {
            if (Validator())
            {
                DataSet loginData = util.LoginCheck(TxtEmail_SignIn.Text, TxtPassword_SignIn.Text);

                if (loginData != null && loginData.Tables.Count > 0 && loginData.Tables[0].Rows.Count > 0)
                {
                    Errors.Text = "Successful sign in!";



                    if (RemeberChkBox.Checked)
                    {
                        String plainTextEmail = TxtEmail_SignIn.Text;
                        String plainTextPassword = TxtPassword_SignIn.Text;
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
                    else
                    {
                        //Delete Cookie
                        //Response.Cookies.Remove("LoginCookie");
                        HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["LoginCookie"];
                        HttpContext.Current.Response.Cookies.Remove("LoginCookie");
                        if (currentUserCookie != null)
                        {
                            currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                            currentUserCookie.Value = null;
                            HttpContext.Current.Response.SetCookie(currentUserCookie);
                        }
                    }

                    Session["UserId"] = util.GetUserIdByEmail(TxtEmail_SignIn.Text);
                    Response.Redirect("Timeline.aspx");
                }
                else
                {
                    Errors.Text = "Invalid Email or Password!";
                    ErrorDiv.Visible = true;
                }

            }
        }
        private Boolean Validator()
        {
            Boolean isValid = true;

            if (TxtEmail_SignIn.Text == "")
            {
                isValid = false;
                Errors.Text += "Missing Email!";
            }
            if (TxtPassword_SignIn.Text == "")
            {
                isValid = false;
                Errors.Text += "Missing Password!";
            }
            return isValid;
        }


        protected void Btn_CreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx");
        }

    }
}