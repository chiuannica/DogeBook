using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DogeBookLibrary;

using System.Data;
using System.Data.SqlClient;

namespace UsersWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpGet]
        public String Get()
        {
            return "test ";
        }

        [HttpGet("GetUserById/{userId}")]
        public User GetUserById(int userId)
        {
            DBConnect objDB = new DBConnect();

            DataSet ds = objDB.GetDataSet("SELECT * FROM TP_Users WHERE UserId=" + userId);

            User user = new User();
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                user.UserId = int.Parse(record["UserId"].ToString());
                user.FirstName = record["FirstName"].ToString();
                user.LastName = record["LastName"].ToString();
                user.Email = record["Email"].ToString();
                //user.ProfilePicture = record["ProfilePicture"].ToString();
                user.Bio = record["Bio"].ToString();
                user.City = record["City"].ToString();
                user.State = record["State"].ToString();
                user.Interests = record["Interests"].ToString();
                user.Verified = record["Verified"].ToString();
            }
            return user;
        }

        [HttpGet("AreFriends/{userId}/{otherId}")]
        public bool AreFriends(int userId, int otherId)
        {
            DBConnect objDB = new DBConnect();

            string sqlString = "" +
                "SELECT rec.FriendRequestId " +
                "FROM TP_Users f1 INNER JOIN TP_FriendRequests rec " +
                    "ON f1.UserId=rec.Friend1Id " +
                    "INNER JOIN TP_Users f2 " +
                    "ON rec.Friend2Id=f2.UserId " +
                "WHERE f1.UserId=" + userId + " " +
                "AND f2.UserId=" + otherId + " " +
                "AND Accept=1";

            DataSet ds = objDB.GetDataSet(sqlString);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return true;
            }

            // try again with the ids swapped arround
            sqlString = "" +
                "SELECT rec.FriendRequestId " +
                "FROM TP_Users f1 INNER JOIN TP_FriendRequests rec " +
                    "ON f1.UserId=rec.Friend1Id " +
                    "INNER JOIN TP_Users f2 " +
                    "ON rec.Friend2Id=f2.UserId " +
                "WHERE f1.UserId=" + otherId + " " +
                "AND f2.UserId=" + userId + " " +
                "AND Accept=1";

            ds = objDB.GetDataSet(sqlString);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return true;
            }

            return false;
        }

        [HttpGet("GetFriends/{userId}")]
        [HttpGet("GetFriendsByUserId/{userId}")]
        public List<User> GetFriendsByUserId(int userId)
        {
            DBConnect objDB = new DBConnect();
            // get the friend 2 if the FriendReq has them as friend 1

            string sqlString = "" +
                "SELECT f2.UserId, f2.FirstName, f2.LastName, f2.Email, " +
                    "f2.ProfilePicture, f2.Bio, f2.City, " +
                    "f2.State, f2.Interests, f2.Verified " +
                "FROM TP_Users f1 INNER JOIN TP_FriendRequests rec " +
                    "ON f1.UserId=rec.Friend1Id " +
                    "INNER JOIN TP_Users f2 " +
                    "ON rec.Friend2Id=f2.UserId " +
                "WHERE f1.UserId=" + userId + " " +
                "AND Accept=1";
            DataSet ds = objDB.GetDataSet(sqlString);
            List<User> friends = new List<User>();

            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    User user = new User();
                    DataRow record = ds.Tables[0].Rows[i];
                    user.UserId = int.Parse(record["UserId"].ToString());
                    user.FirstName = record["FirstName"].ToString();
                    user.LastName = record["LastName"].ToString();
                    user.Email = record["Email"].ToString();
                    //user.ProfilePicture = record["ProfilePicture"].ToString();
                    user.Bio = record["Bio"].ToString();
                    user.City = record["City"].ToString();
                    user.State = record["State"].ToString();
                    user.Interests = record["Interests"].ToString();
                    user.Verified = record["Verified"].ToString();
                    friends.Add(user);
                }
            }

            // get the friend 1 if the FriendReq has them as friend 2
            sqlString = "SELECT f1.UserId, f1.FirstName, f1.LastName, f1.Email, " +
                    "f1.ProfilePicture, f1.Bio, f1.City, " +
                    "f1.State, f1.Interests, f1.Verified " +
                "FROM TP_Users f1 INNER JOIN TP_FriendRequests rec " +
                    "ON f1.UserId=rec.Friend1Id " +
                    "INNER JOIN TP_Users f2 " +
                    "ON rec.Friend2Id=f2.UserId " +
                "WHERE f2.UserId=" + userId + " " +
                "AND Accept=1";

            ds = objDB.GetDataSet(sqlString);
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    User user = new User();
                    DataRow record = ds.Tables[0].Rows[i];
                    user.UserId = int.Parse(record["UserId"].ToString());
                    user.FirstName = record["FirstName"].ToString();
                    user.LastName = record["LastName"].ToString();
                    user.Email = record["Email"].ToString();
                    //user.ProfilePicture = (byte[])record["ProfilePicture"];
                    user.Bio = record["Bio"].ToString();
                    user.City = record["City"].ToString();
                    user.State = record["State"].ToString();
                    user.Interests = record["Interests"].ToString();
                    user.Verified = record["Verified"].ToString();
                    friends.Add(user);
                }
            }
            return friends;
        }

        [HttpGet("GetFriendRequests/{userId}")]
        [HttpGet("GetFriendRequestsByUserId/{userId}")]
        public List<User> GetFriendRequestsByUserId(int userId)
        {
            DBConnect objDB = new DBConnect();

            // accept =0 because pending
            // the current user would be the receiver
            string sqlString = "" +
                "SELECT f1.UserId, f1.FirstName, f1.LastName, f1.Email, " +
                    "f1.ProfilePicture, f1.Bio, f1.City, " +
                    "f1.State, f1.Interests, f1.Verified " +
                "FROM TP_Users f1 INNER JOIN TP_FriendRequests rec " +
                    "ON f1.UserId=rec.Friend1Id " +
                    "INNER JOIN TP_Users f2 " +
                    "ON rec.Friend2Id=f2.UserId " +
                "WHERE f2.UserId=" + userId + " " +
                "AND rec.Accept=0";
            DataSet ds = objDB.GetDataSet(sqlString);
            List<User> friendRequests = new List<User>();

            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    User user = new User();
                    DataRow record = ds.Tables[0].Rows[i];
                    user.UserId = int.Parse(record["UserId"].ToString());
                    user.FirstName = record["FirstName"].ToString();
                    user.LastName = record["LastName"].ToString();
                    user.Email = record["Email"].ToString();
                    //user.ProfilePicture = (byte[])record["ProfilePicture"];
                    user.Bio = record["Bio"].ToString();
                    user.City = record["City"].ToString();
                    user.State = record["State"].ToString();
                    user.Interests = record["Interests"].ToString();
                    user.Verified = record["Verified"].ToString();
                    friendRequests.Add(user);
                }
            }
            return friendRequests;
        }


        [HttpGet("GetPostsByUserId/{userId}")]
        public String GetPostsByUserId(int userId)
        {
            // get all the posts by user id
            // change return to ArrayList of Posts 
            DBConnect objDB = new DBConnect();
            string sqlString = "SELECT * FROM TP_Posts WHERE UserId=" + userId;
            DataSet ds = objDB.GetDataSet(sqlString);

            // List<Post> posts = new List<Post>();

            String tempString = "";
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // Post post = new Post();
                    DataRow record = ds.Tables[0].Rows[i];

                    tempString += record["Text"].ToString() + "|| ";

                    // ...

                    // posts.Add(post);
                }
            }
            return "temp posts " + tempString;
        }

        [HttpPost("AddFriend/{senderId}/{receiverId}")]
        public bool AddFriend(int senderId, int receiverId)
        {
            // add new request record
            // accept = 0 because pending
            DBConnect objDB = new DBConnect();
            string strSQL = "INSERT INTO TP_FriendRequests(Friend1Id, Friend2Id, Accept) " +
                            "VALUES(" + senderId + ", " + receiverId + ", 0)";
            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }

        [HttpPut("DenyFriend/{friendRequestId}")]
        public bool DenyFriend(int friendRequestId)
        {
            // change accept to -1
            DBConnect objDB = new DBConnect();
            string strSQL = "UPDATE TP_FriendRequests " +
                            "SET Accept=-1 " +
                            "WHERE FriendRequestId=" + friendRequestId;
            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }

        // change friend request to accepted
        [HttpPut("AcceptFriend/{friendRequestId}")]
        public bool AcceptFriend(int friendRequestId)
        {
            // change accept to 1
            DBConnect objDB = new DBConnect();
            string strSQL = "UPDATE TP_FriendRequests " +
                            "SET Accept=1 " +
                            "WHERE FriendRequestId=" + friendRequestId;
            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }

        [HttpDelete("DeleteFriend/{friendRequestId}")]
        public bool DeleteFriend(int friendRequestId)
        {
            // delete friendrequest record
            DBConnect objDB = new DBConnect();
            string strSQL = "DELETE FROM TP_FriendRequests " +
                            "WHERE FriendRequestId=" + friendRequestId + " " +
                            "AND Accept=1 ";
            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }


        [HttpPut("DenyFriend/{receiverId}/{senderId}")]
        public bool DenyFriendWithUserIds(int receiverId, int senderId)
        {
            // change accept to -1
            DBConnect objDB = new DBConnect();
            string strSQL = "UPDATE TP_FriendRequests " +
                            "SET Accept=-1 " +
                            "WHERE Friend1Id=" + senderId + " " +
                            "AND Friend2Id=" + receiverId;

            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }

        // change friend request to accepted
        [HttpPut("AcceptFriend/{receiverId}/{senderId}")]
        public bool AcceptFriendWithUserIds(int receiverId, int senderId)
        {
            // change accept to 1
            DBConnect objDB = new DBConnect();
            string strSQL = "UPDATE TP_FriendRequests " +
                            "SET Accept=1 " +
                            "WHERE Friend1Id=" + senderId + " " +
                            "AND Friend2Id=" + receiverId;

            int result = objDB.DoUpdate(strSQL);

            if (result > 0)
                return true;
            return false;
        }

        [HttpDelete("DeleteFriend/{userId}/{friendId}")]
        public bool DeleteFriendWithUserIds(int userId, int friendId)
        {

            // delete this friend record
            DBConnect objDB = new DBConnect();
            string strSQL = "SELECT FriendRequestId " +
                            "FROM TP_FriendRequests " +
                            "WHERE Friend1Id=" + friendId + " " +
                            "AND Friend2Id=" + userId;

            DataSet ds = objDB.GetDataSet(strSQL);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                int friendRequestId = int.Parse(record["FriendRequestId"].ToString());

                bool result = DeleteFriend(friendRequestId);

                if (result == true)
                    return true;
            }

            // attempt again, swap around friendIds.
            strSQL = "SELECT FriendRequestId " +
                            "FROM TP_FriendRequests " +
                            "WHERE Friend1Id=" + userId + " " +
                            "AND Friend2Id=" + friendId;

            ds = objDB.GetDataSet(strSQL);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                int friendRequestId = int.Parse(record["FriendRequestId"].ToString());

                bool result = DeleteFriend(friendRequestId);

                if (result == true)
                    return true;
            }
            return false;
        }








        /*
         // DELETED because we aren't using FriendRecord table anymore
        // create a new friend record
        [HttpPost("AcceptFriend/{friendRequestId}")]
        public bool AcceptFriendPost(int friendRequestId)
        {

            DBConnect objDB = new DBConnect();
            string getSQL = "SELECT * " +
                            "FROM TP_FriendRequests " +
                            "WHERE FriendRequestId=" + friendRequestId;
            DataSet ds = objDB.GetDataSet(getSQL);


            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                string friend1Id = record["Friend1Id"].ToString(); 
                string friend2Id = record["Friend2Id"].ToString();

                string postSQL = "INSERT INTO TP_FriendRecords(Friend1Id, Friend2Id) " +
                                 "VALUES(" + friend1Id + ", " + friend2Id + ")";
                int result = objDB.DoUpdate(postSQL);

                if (result > 0)
                    return true;
            }
            return false;
        }
        */
    }





}
