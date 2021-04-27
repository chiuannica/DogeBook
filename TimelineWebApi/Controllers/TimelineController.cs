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
        [HttpGet("GetTimeline/{userId}")]
        public int[] GetTimeline(int userId)
        {

            Utility util = new Utility();
            // get the friend 2 if the FriendReq has them as friend 1

            List<int> timelineUsers = new List<int>();
            timelineUsers.Add(userId);
            DataSet ds =  util.GetFriendsFromUserId(userId);
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
                ds = util.GetPostsFromUserId(user);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Post post = new Post();
                        DataRow record = ds.Tables[0].Rows[i];
                        post.PostId = (int)record["PostId"];
                        post.Timestamp = (DateTime)record["Timestamp"];
                        tempTimeline.Add(post);
                    }
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
        [HttpGet("GetComments/{postid}")]
        public List<String> GetComments(int postId)
        {
            Utility util = new Utility();
            DataSet ds = util.GetCommentsForPost(postId);
            List<String> comments = new List<String>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    comments.Add(util.GetNameByUserId((int)row["UserId"]) + ": " + row["Text"].ToString());
                }
            }
            return comments;
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
        [HttpPut("UpdatePostText/{PostId}/{Text}")]
        public void UpdatePostText(int PostId, string Text)
        {
            Utility util = new Utility();
            util.UpdatePostText(PostId, Text);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Unlike/{UserId}/{PostId}")]
        public void Unlike(int userid, int postid)
        {
            Utility util = new Utility();
            util.Unlike(userid, postid);

        }
        [HttpDelete("DeletePost/{PostId}")]
        public void DeletePost(int postid)
        {
            Utility util = new Utility();
            util.DeletePost(postid);
        }
    }
}
