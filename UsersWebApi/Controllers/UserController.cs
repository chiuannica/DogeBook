using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpGet]
        public String Get()
        {
            return "test";
        }
        
        [HttpGet("GetUserById/(userId)")]
        public String GetUserById(int userId)
        {
            // get user by Id
            // change return to User after you make User class
            return "temp";
        }
        
        [HttpGet("GetFriendsByUserId/(userId)")]
        public String GetFriendsByUserId(int userId)
        {
            // get friends by user id
            // change return to ArrayList of Users 
            return "temp";
        }

        [HttpGet("GetPostsByUserId/(userId)")]
        public String GetPostsByUserId(int userId)
        {
            // get all the posts by user id
            // change return to ArrayList of Posts 
            return "temp";
        }

        [HttpPost("AddFriend")]
        public String AddFriend(int senderId, int receiverId)
        {
            // make a record in friendrequest table for this
            return "temp";
        }
        [HttpPut("AcceptFriend")]
        public String AcceptFriend(int friendRequestId)
        {
            // change request to be accepted
            // also add a friend record
            return "temp";
        }

    }
}
