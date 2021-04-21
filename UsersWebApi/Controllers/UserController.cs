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
                user.Bio = record["Bio"].ToString();
                user.City = record["City"].ToString();
                user.State = record["State"].ToString();
                user.Interests = record["Interests"].ToString();
                user.Verified = record["Verified"].ToString();
            }
            return user;
        }


        [HttpGet("SearchForUser/{searchTerm}")]
        public List<User> SearchForUser(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            DBConnect objDB = new DBConnect();

            string strSQL = "SELECT * FROM TP_Users " +
                            "WHERE LOWER(FirstName) LIKE '" + searchTerm + "' " +
                                "OR LOWER(LastName) LIKE '" + searchTerm + "' " +
                                "OR LOWER(City) LIKE '" + searchTerm + "' " +
                                "OR LOWER(State) LIKE '" + searchTerm + "' ";

            DataSet ds = objDB.GetDataSet(strSQL);

            List<User> users = new List<User>();

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
                    user.Bio = record["Bio"].ToString();
                    user.City = record["City"].ToString();
                    user.State = record["State"].ToString();
                    user.Interests = record["Interests"].ToString();
                    user.Verified = record["Verified"].ToString();
                    users.Add(user);
                }
            }
            return users;
        }

        [HttpGet("SearchForFriends/{userId}/{searchTerm}")]
        public List<User> SearchForFriends(int userId, string searchTerm)
        {

            List<User> users = GetFriendsByUserId(userId);

            searchTerm = searchTerm.ToLower();

            List<User> friends = new List<User>();


            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].FirstName.ToLower().Contains(searchTerm) || users[i].LastName.ToLower().Contains(searchTerm)
                    || users[i].City.ToLower().Contains(searchTerm) || users[i].State.ToLower().Contains(searchTerm))
                {
                    friends.Add(users[i]);
                }
            }
            return friends;
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

        // gets the friends of friends (excludes self)
        [HttpGet("GetFriendsOfFriends/{userId}")]
        public List<User> GetFriendsOfFriends(int userId)
        {
            // get friends
            List<User> friends = GetFriendsByUserId(userId);

            List<User> friendsOfFriends = new List<User>();
            
            // go through each friend and add their friends
            for (int i = 0; i < friends.Count; i++)
            {
                List<User> friendsOfFriend = GetFriendsByUserId(friends[i].UserId);

                for (int j = 0; j < friendsOfFriend.Count; j++)
                {
                    // don't add friend if it is the current user
                    if (friendsOfFriend[j].UserId != userId)
                    {
                        // set this as bio (not changing in the db, just for display)
                        friendsOfFriend[j].Bio = "Friend of " + friends[i].FirstName + " " + friends[i].LastName;
                        friendsOfFriends.Add(friendsOfFriend[j]);
                    }

                }
            }

            return friendsOfFriends;
        }


        // gets users who are not themselves or friends or friends of friends
        [HttpGet("GetUnrelatedUsers/{userId}")]
        public List<User> GetUnrelatedUsers(int userId)
        {
            List<User> everyone = GetAllUser();
            List<User> friends = GetFriendsByUserId(userId);
            List<User> friendsOfFriends = GetFriendsOfFriends(userId);

            List<User> unrelatedUsers = new List<User>();
            for (int i = 0; i < everyone.Count; i++)
            {
                bool isSelf = false;
                // don't add if adding the current user
                if  (everyone[i].UserId == userId)
                {
                    isSelf = true;
                }
                if (!isSelf)
                {
                    // don't add if they are a friend of a friend
                    bool isFriend = false;

                    for (int j = 0; j < friends.Count; j++)
                    {
                        if (everyone[i].UserId == friends[j].UserId)
                        {
                            isFriend = true;
                        }
                    }

                    if (!isFriend)
                    {
                        bool isFriendOfFriend = false;
                        for (int k = 0; k < friendsOfFriends.Count; k++)
                        {
                            if (everyone[i].UserId == friendsOfFriends[k].UserId)
                            {
                                isFriendOfFriend = true;
                            }
                        }
                        if (!isFriendOfFriend) // only add to unrelated users if not self, not a friend and not a friend's firend
                            unrelatedUsers.Add(everyone[i]);
                    }

                }
            }

            return unrelatedUsers;
        }

        // get all friends
        [HttpGet("GetAllUsers")]
        public List<User> GetAllUser()
        {
            // !! change to only get if verified
            DBConnect objDB = new DBConnect();

            string sqlString = "" +
                "SELECT * " +
                "FROM TP_Users ";
            DataSet ds = objDB.GetDataSet(sqlString);
            List<User> users = new List<User>();

            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow record = ds.Tables[0].Rows[i];

                    User user = new User();
                    user.UserId = int.Parse(record["UserId"].ToString());
                    user.FirstName = record["FirstName"].ToString();
                    user.LastName = record["LastName"].ToString();
                    user.Email = record["Email"].ToString();
                    user.Bio = record["Bio"].ToString();
                    user.City = record["City"].ToString();
                    user.State = record["State"].ToString();
                    user.Interests = record["Interests"].ToString();
                    user.Verified = record["Verified"].ToString();
                    users.Add(user);
                }
            }
            return users;
        }

        // gets everyone who isn't a friend
        [HttpGet("GetNonFriends/{userId}")]
        [HttpGet("GetNonFriendsByUserId/{userId}")]
        public List<User> GetNonFriends(int userId)
        {
            DBConnect objDB = new DBConnect();

            string sqlString = "" +
                "SELECT * " +
                "FROM TP_Users ";
            DataSet ds = objDB.GetDataSet(sqlString);
            List<User> users = new List<User>();

            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow record = ds.Tables[0].Rows[i];

                    int otherPersonId = int.Parse(record["UserId"].ToString());

                    // only add to list if they aren't friends
                    if (!AreFriends(userId, otherPersonId))
                    {
                        User user = new User();
                        user.UserId = otherPersonId;
                        user.FirstName = record["FirstName"].ToString();
                        user.LastName = record["LastName"].ToString();
                        user.Email = record["Email"].ToString();
                        user.Bio = record["Bio"].ToString();
                        user.City = record["City"].ToString();
                        user.State = record["State"].ToString();
                        user.Interests = record["Interests"].ToString();
                        user.Verified = record["Verified"].ToString();
                        users.Add(user);

                    }
                }
            }
            return users;
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

        [HttpPost("AddFriend")]
        public bool AddFriend([FromBody] FriendRequest friendRequest)
        {
            // add new request record
            // accept = 0 because pending
            DBConnect objDB = new DBConnect();
            string strSQL = "INSERT INTO TP_FriendRequests(Friend1Id, Friend2Id, Accept) " +
                            "VALUES(" + friendRequest.SenderId + ", " + friendRequest.ReceiverId + ", 0)";
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

    }





}
