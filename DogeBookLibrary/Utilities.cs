using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DogeBookLibrary
{
    public class Utility
    {
        DBConnect dBConnect = new DBConnect();
        SqlCommand myCommandObj = new SqlCommand();

        public DataSet LoginCheck(String email, String password)
        {

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_LoginCheck";
            myCommandObj.Parameters.Clear();

            SqlParameter inputEmail = new SqlParameter("@email", email);
            inputEmail.Direction = ParameterDirection.Input;
            myCommandObj.Parameters.Add(inputEmail);

            SqlParameter inputPassword = new SqlParameter("@password", password);
            inputPassword.Direction = ParameterDirection.Input;
            myCommandObj.Parameters.Add(inputPassword);

            DataSet loginData = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            return loginData;
        }

        protected HttpCookie Encrypt(String email, String password)
        {
            Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
            Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

            String encryptedPassword;
            UTF8Encoding encoder = new UTF8Encoding();      // used to convert bytes to characters, and back
            Byte[] textBytes;                               // stores the plain text data as bytes

            // Perform Encryption
            //-------------------
            // Convert a string to a byte array, which will be used in the encryption process.

            textBytes = encoder.GetBytes(password);

            // Create an instances of the encryption algorithm (Rinjdael AES) for the encryption to perform,
            // a memory stream used to store the encrypted data temporarily, and
            // a crypto stream that performs the encryption algorithm.

            RijndaelManaged rmEncryption = new RijndaelManaged();
            MemoryStream myMemoryStream = new MemoryStream();
            CryptoStream myEncryptionStream = new CryptoStream(myMemoryStream, rmEncryption.CreateEncryptor(key, vector), CryptoStreamMode.Write);

            // Use the crypto stream to perform the encryption on the plain text byte array.
            myEncryptionStream.Write(textBytes, 0, textBytes.Length);
            myEncryptionStream.FlushFinalBlock();



            // Retrieve the encrypted data from the memory stream, and write it to a separate byte array.
            myMemoryStream.Position = 0;

            Byte[] encryptedBytes = new Byte[myMemoryStream.Length];

            myMemoryStream.Read(encryptedBytes, 0, encryptedBytes.Length);

            // Close all the streams.
            myEncryptionStream.Close();
            myMemoryStream.Close();

            // Convert the bytes to a string and display it.
            encryptedPassword = Convert.ToBase64String(encryptedBytes);

            //lblDisplay.Text = encryptedPassword;
            //divMethod.InnerText = "Encrypted Data:";



            // Write encrypted password to a cookie
            HttpCookie myCookie = new HttpCookie("LoginCookie");
            myCookie.Values["Username"] = email;
            myCookie.Values["Password"] = encryptedPassword;
            myCookie.Expires = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day);

            //Response.Cookies.Add(myCookie);

            //Options -> return HttpCookie
            //Option -> SOAP WEB SERVICE???

            return myCookie;
        }

        protected void btnDecrypt_Click(object sender, EventArgs e)

        {
           // Read encrypted password from cookie
           // if (Request.Cookies["LoginCookie"] != null)
           // {

           //     HttpCookie myCookie = Request.Cookies["LoginCookie"];

           //     String encryptedPassword = myCookie.Values["Password"];
           //     Byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);
           //     Byte[] textBytes;
           //     String plainTextPassword;
           //     UTF8Encoding encoder = new UTF8Encoding();
           //     Perform Decryption
           // -------------------
           //  Create an instances of the decryption algorithm(Rinjdael AES) for the encryption to perform,
           // a memory stream used to store the decrypted data temporarily, and

           // a crypto stream that performs the decryption algorithm.

           //RijndaelManaged rmEncryption = new RijndaelManaged();
           //     MemoryStream myMemoryStream = new MemoryStream();
           //     CryptoStream myDecryptionStream = new CryptoStream(myMemoryStream, rmEncryption.CreateDecryptor(key, vector), CryptoStreamMode.Write);

           //     Use the crypto stream to perform the decryption on the encrypted data in the byte array.
           // myDecryptionStream.Write(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
           //     myDecryptionStream.FlushFinalBlock();

           //     Retrieve the decrypted data from the memory stream, and write it to a separate byte array.
           // myMemoryStream.Position = 0;
           //     textBytes = new Byte[myMemoryStream.Length];
           //     myMemoryStream.Read(textBytes, 0, textBytes.Length);

           //     Close all the streams.
           // myDecryptionStream.Close();
           //     myMemoryStream.Close();



           //     Convert the bytes to a string and display it.
           // plainTextPassword = encoder.GetString(textBytes);

           //     lblDisplay.Text = plainTextPassword;
           //     txtPassword.Text = plainTextPassword;
           //     divMethod.InnerText = "Decrypted Data:";
            }



        }
}

