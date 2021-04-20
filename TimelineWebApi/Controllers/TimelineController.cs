using System.Collections.Generic;
using DogeBookLibrary;
using Microsoft.AspNetCore.Mvc;

namespace TimelineWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TimelineController : Controller
    {
        // GET: api/Timeline
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Timeline/5
        [HttpGet("GetTimelineByUserId/{userId}")]
        public List<Post> GetTimeLineByUserId(int id)
        {
            List<Post> timeline = new List<Post>();
            //Get all your posts + all your friends' posts and sort by timestamp
            return timeline;
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
