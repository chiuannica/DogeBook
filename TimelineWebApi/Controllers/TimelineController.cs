using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Microsoft.AspNetCore.Mvc;
using DogeBookLibrary;

namespace TimelineWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TimelineController : Controller
    {
        // GET: api/Timeline
        [HttpGet ("GetTimeline/{userId}")]
        public IEnumerable<string> GetTimeline(int userId)
        {
            string path = "https://localhost:44386/api/User/";
            WebRequest request = WebRequest.Create(path + "GetFriends/" + userId);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            User[] friends = js.Deserialize<User[]>(data);

            return Array.ConvertAll(friends, x => x.ToString());
        }

        // GET: api/Timeline/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "" + id;
        }

        // POST: api/Timeline
        [HttpPost]
        public void Post([FromBody] string value)
        {

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
