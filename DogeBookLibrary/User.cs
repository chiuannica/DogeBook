using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogeBookLibrary
{
    public class User
    {
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        //public byte[] ProfilePicture { get; set; }
        public String Bio { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Interests { get; set; }
        public String Verified { get; set; }

        public User() { }
    }
}
