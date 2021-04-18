using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogeBookLibrary
{
    public class FriendRequest
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public FriendRequest() { }
    }
}
