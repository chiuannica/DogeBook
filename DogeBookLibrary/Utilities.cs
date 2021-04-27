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

        public string ProfPicArrayToImage(byte[] image)
        {
            throw new NotImplementedException();
        }

        public int GetUserIdByEmail(String email)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetUserIdByEmail";
            myCommandObj.Parameters.Clear();

            SqlParameter inputEmail = new SqlParameter("@Email", email);
            inputEmail.Direction = ParameterDirection.Input;
            myCommandObj.Parameters.Add(inputEmail);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return (int)ds.Tables[0].Rows[0]["UserId"];
            }
            else
            {
                return -1;
            }

        }


        public DataSet PostControlDatabind(int postId)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_PostControlDataBind";
            myCommandObj.Parameters.Clear();

            SqlParameter inputPostId = new SqlParameter("@PostId", postId);
            inputPostId.Direction = ParameterDirection.Input;
            myCommandObj.Parameters.Add(inputPostId);

            DataSet postData = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            return postData;
        }

        public int InsertProfilePicture(int userId, byte[] bytes)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_InsertProfilePicture";
            myCommandObj.Parameters.Clear();

            myCommandObj.Parameters.AddWithValue("@UserId", userId);
            myCommandObj.Parameters.AddWithValue("@ProfilePicture", bytes);
            return dBConnect.DoUpdateUsingCmdObj(myCommandObj);
        }
        // int userId, string text, byte[] bytes, DateTime timestamp
        public int InsertPost(int userId, string text, byte[] bytes, DateTime timestamp)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_InsertPost";
            myCommandObj.Parameters.Clear();

            myCommandObj.Parameters.AddWithValue("@UserId", userId);
            myCommandObj.Parameters.AddWithValue("@Text", text);
            myCommandObj.Parameters.AddWithValue("@Image", bytes);
            myCommandObj.Parameters.AddWithValue("@Timestamp", timestamp);
            //Get Output, return postid
            SqlParameter postId = new SqlParameter("@PostId", DbType.Int32);
            postId.Direction = ParameterDirection.Output;
            myCommandObj.Parameters.Add(postId);

            dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            return int.Parse(myCommandObj.Parameters["@PostId"].Value.ToString());
        }

        public int InsertPost(int userId, string text, DateTime timestamp)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_InsertPost";
            myCommandObj.Parameters.Clear();

            myCommandObj.Parameters.AddWithValue("@UserId", userId);
            myCommandObj.Parameters.AddWithValue("@Text", text);
            myCommandObj.Parameters.AddWithValue("@Timestamp", timestamp);
            //Get Output, return postid
            SqlParameter postId = new SqlParameter("@PostId", DbType.Int32);
            postId.Direction = ParameterDirection.Output;
            myCommandObj.Parameters.Add(postId);

            dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            return int.Parse(myCommandObj.Parameters["@PostId"].Value.ToString());
        }

        public Byte[] GetProfilePicture(int userId)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetProfilePicture";
            myCommandObj.Parameters.Clear();

            myCommandObj.Parameters.AddWithValue("@UserId", userId);
            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ProfilePicture"] != DBNull.Value)
                {
                    return (byte[])ds.Tables[0].Rows[0]["ProfilePicture"];
                }
            }
            return null;
        }

        public string ProfPicArrayToImage(int userid)
        {

            byte[] bytes = GetProfilePicture(userid);
            string imageUrl;
            if (bytes == null)
            {
                imageUrl = null;
            }
            else
            {
                imageUrl = "data:image/jpg;base64," + Convert.ToBase64String(bytes);

            }
            return imageUrl;
        }

        public string ByteArrayToImageUrl(byte[] bytes)
        {
            string imageUrl;
            if (bytes == null)
            {
                imageUrl = null;
            }
            else
            {
                imageUrl = "data:image/jpg;base64," + Convert.ToBase64String(bytes);

            }
            return imageUrl;
        }

        public int LikePost(int userId, int postId)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_LikePost";
            myCommandObj.Parameters.Clear();

            myCommandObj.Parameters.AddWithValue("@UserId", userId);
            myCommandObj.Parameters.AddWithValue("@PostId", postId);

            return dBConnect.DoUpdateUsingCmdObj(myCommandObj);
        }

        public int MakeComment(int postId, int userId, string text)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_MakeComment";
            myCommandObj.Parameters.Clear();

            myCommandObj.Parameters.AddWithValue("@UserId", userId);
            myCommandObj.Parameters.AddWithValue("@PostId", postId);
            myCommandObj.Parameters.AddWithValue("@Text", text);
            return dBConnect.DoUpdateUsingCmdObj(myCommandObj);
        }

        public DataSet GetCommentsForPost(int postId)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetCommentsForPost";
            myCommandObj.Parameters.Clear();
            myCommandObj.Parameters.AddWithValue("@PostId", postId);

            return dBConnect.GetDataSetUsingCmdObj(myCommandObj);
        }

        public String GetNameByUserId(int userId)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetNameByUserId";
            myCommandObj.Parameters.Clear();
            myCommandObj.Parameters.AddWithValue("@UserId", userId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            return ds.Tables[0].Rows[0]["FirstName"].ToString() + " " + ds.Tables[0].Rows[0]["LastName"];
        }


        public int CountLikesOnPost(int postId)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_CountLikesOnPost";
            myCommandObj.Parameters.Clear();
            myCommandObj.Parameters.AddWithValue("@PostId", postId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            return (int)ds.Tables[0].Rows[0]["count_likes"];
        }

        public Boolean CheckLike(int userId, int postId)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_CheckLike";
            myCommandObj.Parameters.Clear();
            myCommandObj.Parameters.AddWithValue("@PostId", postId);
            myCommandObj.Parameters.AddWithValue("@UserId", userId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Unlike(int userId, int postId)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_Unlike";
            myCommandObj.Parameters.Clear();
            myCommandObj.Parameters.AddWithValue("@PostId", postId);
            myCommandObj.Parameters.AddWithValue("@UserId", userId);

            dBConnect.DoUpdateUsingCmdObj(myCommandObj);
        }

        public void UpdatePostText(int postId, string text)
        {
            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_UpdatePostText";
            myCommandObj.Parameters.Clear();
            myCommandObj.Parameters.AddWithValue("@PostId", postId);
            myCommandObj.Parameters.AddWithValue("@Text", text);

            dBConnect.DoUpdateUsingCmdObj(myCommandObj);
        }

    }
}

