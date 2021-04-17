﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DogeBookLibrary;

using System.Data;
using System.Data.SqlClient;

namespace TimelineWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TimelineController : ControllerBase
    {
        // GET: api/Timeline
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Timeline/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
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
