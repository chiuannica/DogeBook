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

        int userId = 1;
        string path = "https://localhost:44386/api/User/";

        //int userId = Session["UserId"];
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = userId;

            LoadUserInformation();
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

            //if (user != null)
            //{
            //    LFirstName.Text = user.FirstName;
            //    LLastName.Text = user.LastName;
                
            //    if (user.ProfilePicture != "")
            //    {
            //        ImgProfilePic.ImageUrl = user.ProfilePicture;
            //    }
            //    else
            //    {
            //        ImgProfilePic.ImageUrl = "https://www.telegraph.co.uk/content/dam/technology/2021/01/28/Screenshot-2021-01-28-at-13-20-35_trans_NvBQzQNjv4BqEGKV9LrAqQtLUTT1Z0gJNRFI0o2dlzyIcL3Nvd0Rwgc.png";
            //    }
            


            //    LBio.Text = user.Bio;
            //    LInterests.Text = user.Interests;
            //    LCity.Text = user.City;
            //    LState.Text = user.State;
            //}
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
                //ctrl.ImageUrl = friends[i].ProfilePicture.ToString();

                ctrl.UserId = int.Parse(friends[i].UserId.ToString());


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
                    //ctrl.ImageUrl = friends[i].ProfilePicture.ToString();
                    //ctrl.Bio = friends[i].Bio.ToString();
                    ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                    // bind data to ctrl
                    ctrl.DataBind();

                    // add to panel
                    SearchPanel.Controls.Add(ctrl);


                }
            }
            LSearchTitle.Text = "Search results for \"" + searchTerm + "\"";
            LSearchEmpty.Text = friends.Length.ToString();
        }
    }
}