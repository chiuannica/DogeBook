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
        protected void Page_Load(object sender, EventArgs e)
        {
            // Read encrypted password from cookie
            if (!IsPostBack && Request.Cookies["LoginCookie"] != null)

            {
                HttpCookie myCookie = Request.Cookies["LoginCookie"];
                TxtEmail_SignIn.Text = myCookie.Values["Email"];
                TxtPassword_SignIn.Text = myCookie.Values["Password"];
                //lblDisplay.Text = "Data read from cookie.";
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

        protected void Btn_SignIn_Click(object sender, EventArgs e)
        {
            if (Validator())
            {
                DataSet loginData = util.LoginCheck(TxtEmail_SignIn.Text, TxtPassword_SignIn.Text);

                if (loginData != null && loginData.Tables.Count > 0 && loginData.Tables[0].Rows.Count > 0)
                {
                    if (RemeberChkBox.Checked)
                    {
                        //Store Cookie

                    }
                    else
                    {
                        //Delete Cookie

                    }

                    //if (int.Parse(dBConnect.GetField("Active", 0).ToString()) == 0)
                    //{
                    //    Errors.Text += "You have been banned!";
                    //    ErrorDiv.Visible = true;
                    //}
                    //else
                    //{
                    //    Session["UserId"] = dBConnect.GetField("UserId", 0);
                    //    Session["UserName"] = dBConnect.GetField("UserName", 0);
                    //    Session["Address"] = dBConnect.GetField("Address", 0);
                    //    Session["PhoneNumber"] = dBConnect.GetField("PhoneNumber", 0);
                    //    Session["CreatedEmailAddress"] = dBConnect.GetField("CreatedEmailAddress", 0);
                    //    Session["Avatar"] = dBConnect.GetField("Avatar", 0);
                    //    if (RemeberChkBox.Checked)
                    //    {
                    //        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    //        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                    //    }
                    //    else
                    //    {
                    //        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    //        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                    //    }
                    //    Response.Cookies["UserName"].Value = TxtEmail_SignIn.Text.Trim();
                    //    Response.Cookies["Password"].Value = TxtPassword_SignIn.Text.Trim();
                    //    //User
                    //    if ((dBConnect.GetField("Type", 0).ToString()).Equals("User"))
                    //    {
                    //        Response.Redirect("EmailClient.aspx");
                    //    }
                    //    //Admin
                    //    else
                    //    {
                    //        Response.Redirect("AdminManagement.aspx");
                    //    }
                    // }
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

        protected void Encrypt()
        {
          //  String user = TxtEmail_SignIn.Text;
          //  String plainTextPassword = TxtPassword_SignIn.Text;
          //  String encryptedPassword;
          //  UTF8Encoding encoder = new UTF8Encoding();      // used to convert bytes to characters, and back
          //  Byte[] textBytes;                               // stores the plain text data as bytes

          //  Perform Encryption
          //  -------------------
          //   Convert a string to a byte array, which will be used in the encryption process.

          //  textBytes = encoder.GetBytes(plainTextPassword);

          //  Create an instances of the encryption algorithm(Rinjdael AES) for the encryption to perform,
          // a memory stream used to store the encrypted data temporarily, and

          // a crypto stream that performs the encryption algorithm.


          //RijndaelManaged rmEncryption = new RijndaelManaged();
          //  MemoryStream myMemoryStream = new MemoryStream();
          //  CryptoStream myEncryptionStream = new CryptoStream(myMemoryStream, rmEncryption.CreateEncryptor(key, vector), CryptoStreamMode.Write);

          //  Use the crypto stream to perform the encryption on the plain text byte array.
          //  myEncryptionStream.Write(textBytes, 0, textBytes.Length);
          //  myEncryptionStream.FlushFinalBlock();



          //  Retrieve the encrypted data from the memory stream, and write it to a separate byte array.
          //  myMemoryStream.Position = 0;

          //  Byte[] encryptedBytes = new Byte[myMemoryStream.Length];

          //  myMemoryStream.Read(encryptedBytes, 0, encryptedBytes.Length);

          //  Close all the streams.
          //  myEncryptionStream.Close();
          //  myMemoryStream.Close();

          //  Convert the bytes to a string and display it.
          //  encryptedPassword = Convert.ToBase64String(encryptedBytes);
          //  lblDisplay.Text = encryptedPassword;
          //  divMethod.InnerText = "Encrypted Data:";



          //  Write encrypted password to a cookie
          //  HttpCookie myCookie = new HttpCookie("LoginCookie");
          //  myCookie.Values["Username"] = user;
          //  myCookie.Values["Password"] = encryptedPassword;
          //  myCookie.Expires = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day);
          //  Response.Cookies.Add(myCookie);
        }
        protected void Btn_CreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx");
        }

    }
}