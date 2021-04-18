using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class NonFriendCard : System.Web.UI.UserControl
    {
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public int UserId { get; set; }

        string path = "https://localhost:44386/api/User/";

        protected void Page_Load(object sender, EventArgs e)
        {
            ImgFriend.ImageUrl = "https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg";

        }
        public override void DataBind()
        {
            ImgFriend.ImageUrl = ImageUrl;
            LFriendFName.Text = FirstName;
            LFriendLName.Text = LastName;
            LBio.Text = Bio;
        }


        protected void BtnAddFriend_Click(object sender, EventArgs e)
        {

        }

        protected void BtnGoToProfile_Click(object sender, EventArgs e)
        {

        }
    }
}