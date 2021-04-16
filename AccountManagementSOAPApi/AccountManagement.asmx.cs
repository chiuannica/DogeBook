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

        [WebMethod]
        public string hashPassword(string password)
        {
            // hash stuff
            return password;
        }

        [WebMethod]
        public User Login(string email, string password)
        {

            DBConnect objDB = new DBConnect();

            // HASH the password here
            string hashedPassword = hashPassword(password);

            string strSQL = "SELECT * " +
                            "FROM TP_Users " +
                            "WHERE Email='" + email + "' " +
                            "AND Password='" + hashedPassword + "'";
            DataSet ds = objDB.GetDataSet(strSQL);

            User user = new User();
            user.UserId = -1;
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                user.UserId = int.Parse(record["UserId"].ToString());
                user.FirstName = record["FirstName"].ToString();
                user.LastName = record["LastName"].ToString();
                user.Email = record["Email"].ToString();
                user.ProfilePicture = record["ProfilePicture"].ToString();
                user.Bio = record["Bio"].ToString();
                user.City = record["City"].ToString();
                user.State = record["State"].ToString();
                user.Interests = record["Interests"].ToString();
                user.Verified = record["Verified"].ToString();
            }

            return user;
        }

        [WebMethod]
        public bool EmailUsed(string email)
        {
            DBConnect objDB = new DBConnect();

            string strSQL = "SELECT Email " +
                            "FROM TP_Users " +
                            "WHERE Email='" + email + "' ";
            DataSet ds = objDB.GetDataSet(strSQL);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        public int GetUserIdFromEmail(string email)
        {
            DBConnect objDB = new DBConnect();

            string strSQL = "SELECT UserId " +
                            "FROM TP_Users " +
                            "WHERE Email='" + email + "' ";
            DataSet ds = objDB.GetDataSet(strSQL);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                return int.Parse(record["UserId"].ToString());
            }
            return -1;
        }

        [WebMethod]
        public bool CreateAccount(string firstName, string lastName, string email, string password)
        {
            DBConnect objDB = new DBConnect();

            // hash the password
            string hashedPassword = hashPassword(password);

            string strSQL = "INSERT INTO TP_Users(FirstName, LastName, Email, Password) " +
                            "VALUES('" + firstName + "', '" + lastName + "', '" + email + "', '" + password + "')";
            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }


        [WebMethod]
        public bool AddSecurityQuestion(int userId, string question, string answer)
        {
            DBConnect objDB = new DBConnect();

            string strSQL = "INSERT INTO TP_SecurityQuestions(UserId, SecurityQuestion, Answer) " +
                            "VALUES(" + userId + ", '" + question + "', '" + answer + "')";
            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }

        [WebMethod]
        public bool VerifyAccount(int userId)
        {
            DBConnect objDB = new DBConnect();
            string strSQL = "UPDATE TP_Users " +
                            "SET Verified=1 " +
                            "WHERE UserId=" + userId;
            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }
        [WebMethod]
        public bool ChangePassword(int userId, string password)
        {
            DBConnect objDB = new DBConnect();
            string strSQL = "UPDATE TP_Users " +
                            "SET Password=" + password + " " +
                            "WHERE UserId=" + userId;
            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }

        [WebMethod]
        public List<Question> GetSecurityQuestions(int userId)
        {
            DBConnect objDB = new DBConnect();

            string strSQL = "SELECT * " +
                            "FROM TP_SecurityQuestions " +
                            "WHERE UserId=" + userId;
            DataSet ds = objDB.GetDataSet(strSQL);

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
    }
}
