using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogeBookLibrary
{
    public partial class Post
    {
        private string timestamp;
        private string text;
        private string imageUrl;

        public String Text { get => text; set => text = value; }
        public String ImageUrl { get => imageUrl; set => imageUrl = value; }
        public String Timestamp { get => timestamp; set => timestamp = value; }

        public Post()
        {
        }

    }
}