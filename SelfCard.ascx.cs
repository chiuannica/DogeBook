using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class SelfCard : System.Web.UI.UserControl
    {
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public override void DataBind()
        {

            ImgFriend.ImageUrl = ImageUrl;
            LFriendFName.Text = FirstName;
            LFriendLName.Text = LastName;

            // if no picture, load default pic
            if (ImgFriend.ImageUrl == null || ImgFriend.ImageUrl == "")
            {
                ImgFriend.ImageUrl = "https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg";
            }

        }

        protected void BtnGoToProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
    }
}