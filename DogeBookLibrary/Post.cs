using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogeBookLibrary
{
    public partial class Post
    {
        private int postId;
        private DateTime timestamp;
        private string text;
        private string image;

        public String Text { get => text; set => text = value; }
        public String Image { get => image; set => image = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public int PostId { get => postId; set => postId = value; }

        public Post()
        {
        }

    }
}