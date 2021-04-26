using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using DogeBookLibrary;

using System.Data;
using System.Data.SqlClient;

namespace AccountManagementSOAPApi
{
    /// <summary>
    /// Summary description for AccountManagement
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AccountManagement : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public double Add(double a, double b)
        {
            return a + b;
        }

        public string hashPassword(string password)
        {
            return password;
        }


        // check if the email has been used already when creating account
        [WebMethod]
        public bool EmailUsed(string email)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetEmailFromEmail";
            myCommandObj.Parameters.Clear();

            SqlParameter inputEmail = new SqlParameter("@email", email);
            inputEmail.Direction = ParameterDirection.Input;
            myCommandObj.Parameters.Add(inputEmail);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        // get userId from email for when adding security questions
        [WebMethod]
        public int GetUserIdFromEmail(string email)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetUserIdByEmail";
            myCommandObj.Parameters.Clear();

            SqlParameter inputEmail = new SqlParameter("@Email", email);
            inputEmail.Direction = ParameterDirection.Input;
            myCommandObj.Parameters.Add(inputEmail);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                return int.Parse(record["UserId"].ToString());
            }
            return -1;
        }

        // create account with info, verified = 0 
        [WebMethod]
        public bool CreateAccount(string firstName, string lastName, string email, string password)
        {
            // hash the password
            string hashedPassword = hashPassword(password);

            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_InsertCreateAccount";
            myCommandObj.Parameters.Clear();

            SqlParameter inputFirstName = new SqlParameter("@firstName", firstName);
            SqlParameter inputLastName = new SqlParameter("@lastName", lastName);
            SqlParameter inputEmail = new SqlParameter("@email", email);
            SqlParameter inputPassword = new SqlParameter("@password", hashedPassword);

            // NOTE FOR BRUCE:
            // usually verified is set to 0 and changed later,
            // but locally the email sending the link for verification doesn't work
            // for this reason the user is automatically verified. 
            SqlParameter inputVerified = new SqlParameter("@verified", 1);

            myCommandObj.Parameters.Add(inputFirstName);
            myCommandObj.Parameters.Add(inputLastName);
            myCommandObj.Parameters.Add(inputEmail);
            myCommandObj.Parameters.Add(inputPassword);
            myCommandObj.Parameters.Add(inputVerified);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }

        /*
         SECURITY QUESTION
         */
        [WebMethod]
        public bool AddSecurityQuestion(int userId, string securityQuestion, string answer)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_InsertSecurityQuestion";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            SqlParameter inputSecurityQuestion = new SqlParameter("@securityQuestion", securityQuestion);
            SqlParameter inputAnswer = new SqlParameter("@answer", answer);

            myCommandObj.Parameters.Add(inputUserId);
            myCommandObj.Parameters.Add(inputSecurityQuestion);
            myCommandObj.Parameters.Add(inputAnswer);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        // set verified to be 1 after confirming with email
        [WebMethod]
        public bool VerifyAccount(int userId)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_VerifyAccount";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);

            myCommandObj.Parameters.Add(inputUserId);
            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }
        [WebMethod]
        public bool ChangePassword(int userId, string password)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_ChangePassword";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            SqlParameter inputPassword = new SqlParameter("@password", password);

            myCommandObj.Parameters.Add(inputUserId);
            myCommandObj.Parameters.Add(inputPassword);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }

        [WebMethod]
        public List<Question> GetSecurityQuestions(int userId)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetSecurityQuestionsByUserId";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            myCommandObj.Parameters.Add(inputUserId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            List<Question> questions = new List<Question>();

            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Question question = new Question();
                    DataRow record = ds.Tables[0].Rows[i];

                    question.SecurityQuestionId = int.Parse(record["SecurityQuestionId"].ToString());
                    question.UserId = int.Parse(record["UserId"].ToString());
                    question.SecurityQuestion = record["SecurityQuestion"].ToString();
                    question.Answer = record["Answer"].ToString();
                    questions.Add(question);
                }
            }
            return questions;
        }

        /*
        [WebMethod]
        public bool CheckSecurityQuestion(int securityQuestionId, string userAnswer)
        {
            DBConnect objDB = new DBConnect();


            // loop through all three questions
            string strSQL = "SELECT * " +
                            "FROM TP_SecurityQuestions " +
                            "WHERE SecurityQuestionId=" + securityQuestionId;
            DataSet ds = objDB.GetDataSet(strSQL);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                string realAnswer = record["Answer"].ToString();

                if (realAnswer.ToLower() == userAnswer.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        */
        /*
         UPDATE PROFILE
         */
        // gets called by other methods
        [WebMethod]
        public bool UpdateBio(int userId, string content)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_UpdateBio";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            SqlParameter inputContent = new SqlParameter("@content", content);

            myCommandObj.Parameters.Add(inputUserId);
            myCommandObj.Parameters.Add(inputContent);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }

        [WebMethod]
        public bool UpdateInterests(int userId, string content)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_UpdateInterests";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            SqlParameter inputContent = new SqlParameter("@content", content);

            myCommandObj.Parameters.Add(inputUserId);
            myCommandObj.Parameters.Add(inputContent);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }
        
        [WebMethod]
        public bool UpdateCity(int userId, string content)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_UpdateCity";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            SqlParameter inputContent = new SqlParameter("@content", content);

            myCommandObj.Parameters.Add(inputUserId);
            myCommandObj.Parameters.Add(inputContent);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }
        
        [WebMethod]
        public bool UpdateState(int userId, string content)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_UpdateState";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            SqlParameter inputContent = new SqlParameter("@content", content);

            myCommandObj.Parameters.Add(inputUserId);
            myCommandObj.Parameters.Add(inputContent);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }
    }
}
