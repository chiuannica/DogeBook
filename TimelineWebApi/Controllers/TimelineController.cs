using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Microsoft.AspNetCore.Mvc;
using DogeBookLibrary;
using System.Data;

namespace TimelineWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TimelineController : Controller
    {
        // GET: api/Timeline
        [HttpGet ("GetTimeline/{userId}")]
        public int[] GetTimeline(int userId)
        {
            //string path = "https://localhost:44386/api/User/";
            //WebRequest request = WebRequest.Create(path + "GetFriends/" + userId);
            //WebResponse response = request.GetResponse();

            //Stream theDataStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(theDataStream);

            //String data = reader.ReadToEnd();
            //reader.Close();
            //response.Close();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //User[] friends = js.Deserialize<User[]>(data);

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
            List<int> timelineUsers = new List<int>();
            timelineUsers.Add(userId);

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
                    timelineUsers.Add(user.UserId);
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
                    timelineUsers.Add(user.UserId);
                }
            }

            //return Array.ConvertAll(friends, x => x.ToString());
            //Okay now friends and userId from the param is a list of all users
            //Which i need to grab the posts from and return in a list of posts
            //bada bing bada boom
            //
            List<Post> tempTimeline = new List<Post>();
            
            foreach (int user in timelineUsers)
            {
                sqlString = "SELECT * FROM TP_Posts WHERE UserId=" + user;
                ds = objDB.GetDataSet(sqlString);
                //String tempString = "User: " + userId;
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Post post = new Post();
                        DataRow record = ds.Tables[0].Rows[i];
                        post.PostId = (int)record["PostId"];
                        //tempString += "Post text:  " + record["Text"].ToString() + Environment.NewLine +
                        //              "PostId: " + record["PostId"].ToString() + Environment.NewLine;
                        post.Timestamp = (DateTime)record["Timestamp"];
                        // ...
                        
                        tempTimeline.Add(post);
                    }
                    //tempTimeline.Add(tempString);
                }
            }
            tempTimeline.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
            int[] timeline = new int[tempTimeline.Count];
            int r = 0;
            foreach (Post post in tempTimeline)
            {
                timeline[r] = post.PostId;
                r++;
            }

            return timeline;
        }

        // GET: api/Timeline/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "" + id;
        }

        // POST: api/Timeline
        [HttpPost("LikePost/{UserId}/{PostId}")]
        public int LikePost(int userId, int postId)
        {

            Utility util = new Utility();
            return util.LikePost(userId, postId);
        }

        // POST: api/Timeline
        [HttpPost("MakeComment/{UserId}/{PostId}/{Text}")]
        public int MakeComment(int userId, int postId, string text)
        {

            Utility util = new Utility();
            return util.MakeComment(postId, userId, text);
        }

        // PUT: api/Timeline/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
