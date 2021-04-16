using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogeBook
{
    public partial class Post
    {
        public Post()
        {

        }

        public Post(string text, string imageUrl, string timestamp)
        {
            Text = text;
            ImageUrl = imageUrl;
            Timestamp = timestamp;
        }

        private String Text { get; set; }
        private String ImageUrl{ get; set; }
        private String Timestamp{ get; set; }
    }
}