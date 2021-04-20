using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class FriendCard : System.Web.UI.UserControl 
    { 
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }

        string path = "https://localhost:44386/api/User/";


        protected void Page_Load(object sender, EventArgs e)
        {
            // default pic
            ImgFriend.ImageUrl = "https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg";
        }
        public override void DataBind()
        {
            ImgFriend.ImageUrl = ImageUrl;
            LFriendFName.Text = FirstName;
            LFriendLName.Text = LastName;
        }


        protected void BtnGoToProfile_Click(object sender, EventArgs e)
        {
            Session["OtherPersonId"] = UserId.ToString();
            //Response.Redirect();
        }

        protected void BtnDeleteFriend_Click(object sender, EventArgs e)
        {
            // this is the user that is logged in
            int userId = (int)Session["UserId"];
            // the userId of the friend card, which is the friend's user id
            int friendId = UserId;

            try
            {

                WebRequest request = WebRequest.Create(path + "DeleteFriend/" + userId + "/" + friendId);

                request.Method = "DELETE";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string responseCode = response.StatusCode.ToString();

                if (responseCode == "OK")
                    LblDisplay.Text = FirstName + " " + LastName + " was removed from your friend list";
                else
                    LblDisplay.Text = "A problem occurred. ";
            }
            catch (Exception ex)

            {
                LblDisplay.Text = "Error: " + ex.Message;
            }
            LblDisplay.Visible = true;

        }
    }
}