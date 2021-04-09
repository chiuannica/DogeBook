using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogeBook
{
    public class User
    {
        public User() { }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String ProfilePicture { get; set; }
        public String Bio { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Interests { get; set; }
    }
}