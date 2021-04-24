using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DogeBookLibrary;

namespace DogeBook
{
    public partial class NonFriendCard : System.Web.UI.UserControl
    {
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        string path = "https://localhost:44386/api/User/";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ImgFriend.ImageUrl = "https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg";
            }
        }
        public override void DataBind()
        {
            ImgFriend.ImageUrl = ImageUrl;
            LFriendFName.Text = FirstName;
            LFriendLName.Text = LastName;
            LDescription.Text = Description;
        }


        protected void BtnAddFriend_Click(object sender, EventArgs e)
        {
            // this is the user that is logged in
            int userId = (int)Session["UserId"];
            // the userId of the friend card, which is the friend's user id
            int friendId = UserId;

            // check if there has already been a friend request
            

            WebRequest request = WebRequest.Create(path + "GetFriendRequest/" + friendId + "/"+ userId);
            WebResponse response = request.GetResponse();


            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();

            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            bool alreadySentFriendRequest = js.Deserialize<bool>(data);

            if (alreadySentFriendRequest)
            {
                LblDisplay.Text = "Already added";
                LblDisplay.Visible = true;

                return;
            }



            FriendRequest friendRequest = new FriendRequest();

            friendRequest.SenderId = userId;
            friendRequest.ReceiverId = friendId;
            js = new JavaScriptSerializer();

            string jsonFriendRequest = js.Serialize(friendRequest);

            try
            {
                request = WebRequest.Create(path + "AddFriend/");

                request.Method = "POST";
                request.ContentLength = jsonFriendRequest.Length;
                request.ContentType = "application/json";


                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonFriendRequest);
                writer.Flush();
                writer.Close();

                response = request.GetResponse();
                theDataStream = response.GetResponseStream();
                reader = new StreamReader(theDataStream);
                data = reader.ReadToEnd();

                reader.Close();
                response.Close();

                if (data == "true")
                    LblDisplay.Text = FirstName + " " + LastName + " was added";
                else
                    LblDisplay.Text = "A problem occurred. ";
            }
            catch (Exception ex)

            {
                LblDisplay.Text = "Error: " + ex.Message;
            }
            LblDisplay.Visible = true;

        }

        protected void BtnGoToProfile_Click(object sender, EventArgs e)
        {
            Session["OtherPersonId"] = UserId.ToString();
            Response.Redirect("OtherProfile.aspx");
        }
    }
}