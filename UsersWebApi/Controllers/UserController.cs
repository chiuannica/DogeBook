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
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetUserByUserId";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            myCommandObj.Parameters.Add(inputUserId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);
            
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
            List<User> users = new List<User>();

            if (searchTerm == null || searchTerm == "")
            {
                return users;
            }
            
            searchTerm = searchTerm.ToLower();

            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_SearchForUser";
            myCommandObj.Parameters.Clear();

            SqlParameter inputSearchTerm = new SqlParameter("@searchTerm", searchTerm);
            inputSearchTerm.Direction = ParameterDirection.Input;
            myCommandObj.Parameters.Add(inputSearchTerm);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

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
            List<User> friends = new List<User>();

            if (searchTerm == null || searchTerm == "")
            {
                return friends;
            }
            List<User> users = GetFriendsByUserId(userId);

            searchTerm = searchTerm.ToLower();


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

            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_AreFriends";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            myCommandObj.Parameters.Add(inputUserId);

            SqlParameter inputOtherId = new SqlParameter("@otherId", otherId);
            myCommandObj.Parameters.Add(inputOtherId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);
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
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetFriendsFromUserId";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            myCommandObj.Parameters.Add(inputUserId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

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
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetAllUsers";
            myCommandObj.Parameters.Clear();

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

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
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetAllUsers";
            myCommandObj.Parameters.Clear();

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

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
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetFriendRequestsByUserId";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            myCommandObj.Parameters.Add(inputUserId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

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
            
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetPostsFromUserId";
            myCommandObj.Parameters.Clear();

            SqlParameter inputUserId = new SqlParameter("@userId", userId);
            myCommandObj.Parameters.Add(inputUserId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

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
        // check if there is already a friend request sent 
        [HttpGet("GetFriendRequest/{receiverId}/{senderId}")]
        public bool GetFriendRequest(int receiverId, int senderId)
        {

            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_GetFriendRequest";
            myCommandObj.Parameters.Clear();

            SqlParameter inputSenderId = new SqlParameter("@senderId", senderId);
            SqlParameter inputReceiverId = new SqlParameter("@receiverId", receiverId);

            myCommandObj.Parameters.Add(inputSenderId);
            myCommandObj.Parameters.Add(inputReceiverId);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            return false;
        }



        [HttpPost("AddFriend")]
        public bool AddFriend([FromBody] FriendRequest friendRequest)
        {
            // add new request record
            // accept = 0 because pending

            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_AddFriend";
            myCommandObj.Parameters.Clear();

            SqlParameter inputSenderId = new SqlParameter("@senderId", friendRequest.SenderId);
            SqlParameter inputReceiverId = new SqlParameter("@receiverId", friendRequest.ReceiverId);

            myCommandObj.Parameters.Add(inputSenderId);
            myCommandObj.Parameters.Add(inputReceiverId);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }


        [HttpPut("DenyFriend/{receiverId}/{senderId}")]
        public bool DenyFriendWithUserIds(int receiverId, int senderId)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_DenyFriendWithUserIds";
            myCommandObj.Parameters.Clear();

            SqlParameter inputSenderId = new SqlParameter("@senderId", senderId);
            SqlParameter inputReceiverId = new SqlParameter("@receiverId", receiverId);

            myCommandObj.Parameters.Add(inputSenderId);
            myCommandObj.Parameters.Add(inputReceiverId);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }

        // change friend request to accepted
        [HttpPut("AcceptFriend/{receiverId}/{senderId}")]
        public bool AcceptFriendWithUserIds(int receiverId, int senderId)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_AcceptFriendWithUserIds";
            myCommandObj.Parameters.Clear();

            SqlParameter inputSenderId = new SqlParameter("@senderId", senderId);
            SqlParameter inputReceiverId = new SqlParameter("@receiverId", receiverId);

            myCommandObj.Parameters.Add(inputSenderId);
            myCommandObj.Parameters.Add(inputReceiverId);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }

        [HttpDelete("DeleteFriend/{friendRequestId}")]
        public bool DeleteFriend(int friendRequestId)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_DeleteFriendRequestWithFriendRequestId";
            myCommandObj.Parameters.Clear();

            SqlParameter inputFriendRequestId = new SqlParameter("@friendRequestId", friendRequestId);

            myCommandObj.Parameters.Add(inputFriendRequestId);

            int result = dBConnect.DoUpdateUsingCmdObj(myCommandObj);

            if (result > 0)
                return true;
            return false;
        }

        [HttpDelete("DeleteFriend/{userId}/{friendId}")]
        public bool DeleteFriendWithUserIds(int userId, int friendId)
        {

            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_DeleteFriendWithUserIds";
            myCommandObj.Parameters.Clear();
            
            SqlParameter inputUserId1 = new SqlParameter("@userId1", friendId);
            SqlParameter inputUserId2 = new SqlParameter("@userId2", userId);

            myCommandObj.Parameters.Add(inputUserId1);
            myCommandObj.Parameters.Add(inputUserId2);

            DataSet ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                int friendRequestId = int.Parse(record["FriendRequestId"].ToString());

                bool result = DeleteFriend(friendRequestId);

                if (result == true)
                    return true;
            }

            myCommandObj = new SqlCommand();

            myCommandObj.CommandType = CommandType.StoredProcedure;
            myCommandObj.CommandText = "TP_DeleteFriendWithUserIds";
            myCommandObj.Parameters.Clear();

            inputUserId1 = new SqlParameter("@userId1", userId);
            inputUserId2 = new SqlParameter("@userId2", friendId);

            myCommandObj.Parameters.Add(inputUserId1);
            myCommandObj.Parameters.Add(inputUserId2);

            ds = dBConnect.GetDataSetUsingCmdObj(myCommandObj);

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
