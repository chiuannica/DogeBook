using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Web.Script.Serialization;  // needed for JSON serializers
using System.IO;                        // needed for Stream and Stream Reader
using System.Net;                       // needed for the Web Request
using DogeBookLibrary;

namespace DogeBook
{
    public partial class Profile : System.Web.UI.Page
    {

        int userId;
        string path = "https://localhost:44386/api/User/";
        Utility util = new Utility();

        protected void Page_Load(object sender, EventArgs e)
        {
            // load the userId
            userId = int.Parse(Session["UserId"].ToString());

            // load user information
            LoadUserInformation();
            // load friends
            LoadFriends();
        }
        protected void LoadUserInformation()
        {

            WebRequest request = WebRequest.Create(path + "GetUserById/"  + userId);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            User user = js.Deserialize<User>(data);

            if (user != null)
            {
                LFirstName.Text = user.FirstName;
                LLastName.Text = user.LastName;

                string imageUrl = util.ProfPicArrayToImage(userId);

                if (imageUrl == null || imageUrl == "")
                {
                    ImgProfilePic.ImageUrl = "https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg";
                } else
                {
                    ImgProfilePic.ImageUrl = imageUrl;
                }

                LBio.Text = user.Bio;
                LInterests.Text = user.Interests;
                LCity.Text = user.City + ", " ;
                LState.Text = user.State;
            }
        }
        protected void LoadFriends()
        {
            WebRequest request = WebRequest.Create(path + "GetFriendsByUserId/" + userId);
            WebResponse response = request.GetResponse();


            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();

            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            User[] friends = js.Deserialize<User[]>(data);


            for (int i = 0; i < friends.Length; i++)

            {
                FriendCard ctrl = (FriendCard)LoadControl("FriendCard.ascx");

                ctrl.FirstName = friends[i].FirstName.ToString();
                ctrl.LastName = friends[i].LastName.ToString();
                int friendId = int.Parse(friends[i].UserId.ToString());
                ctrl.UserId = friendId;

                // load default pic if there is no profile pic
                if (util.ProfPicArrayToImage((int)Session["userId"]) != "")
                {
                    ctrl.ImageUrl = util.ProfPicArrayToImage(friendId);
                }
                else
                {
                    ctrl.ImageUrl = "https://www.telegraph.co.uk/content/dam/technology/2021/01/28/Screenshot-2021-01-28-at-13-20-35_trans_NvBQzQNjv4BqEGKV9LrAqQtLUTT1Z0gJNRFI0o2dlzyIcL3Nvd0Rwgc.png";
                }


                ctrl.DataBind();



                // Add the control object to the WebForm's Controls collection

                FriendPanel.Controls.Add(ctrl);
            }
             // update the badge to show how many   
            LFriendsNumber.Text = friends.Length.ToString();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {

            string searchTerm = TBSearch.Text;
            SearchPanel.Visible = true;

            searchTerm = searchTerm.ToLower();


            WebRequest request = WebRequest.Create(path + "SearchForFriends/" + userId + "/" + searchTerm);
            WebResponse response = request.GetResponse();


            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();

            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            User[] friends = js.Deserialize<User[]>(data);

            for (int i = 0; i < friends.Length; i++)
            {
                // exclude if self 

                if (friends[i].UserId != userId)
                {

                    FriendCard ctrl = (FriendCard)LoadControl("FriendCard.ascx");

                    ctrl.FirstName = friends[i].FirstName.ToString();
                    ctrl.LastName = friends[i].LastName.ToString();


                    int friendId = int.Parse(friends[i].UserId.ToString());

                    ctrl.ImageUrl = util.ProfPicArrayToImage(friendId);

                    //ctrl.Bio = friends[i].Bio.ToString();
                    ctrl.UserId = friendId;

                    // bind data to ctrl
                    ctrl.DataBind();

                    // add to panel
                    SearchPanel.Controls.Add(ctrl);


                }
            }
            LSearchTitle.Text = "Search results for \"" + searchTerm + "\"";
            LSearchEmpty.Text = friends.Length.ToString();
        }

        protected void BtnEditProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx");
        }
    }
}