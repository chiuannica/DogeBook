using DogeBookLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class OtherProfile : System.Web.UI.Page
    {

        int userId;
        string path = "https://localhost:44386/api/User/";
        Utility util = new Utility();
        int otherPersonId;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = int.Parse(Session["UserId"].ToString());
            otherPersonId = int.Parse(Session["OtherPersonId"].ToString());
            LoadUserInformation();
            LoadFriends();
        }

        protected void LoadUserInformation()
        {
            // load this other person's user
            WebRequest request = WebRequest.Create(path + "GetUserById/" + otherPersonId);
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

                string imageUrl = util.ProfPicArrayToImage(otherPersonId);

                if (imageUrl == null || imageUrl == "")
                {
                    ImgProfilePic.ImageUrl = "https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg";
                }
                else
                {
                    ImgProfilePic.ImageUrl = imageUrl;
                }

                LBio.Text = user.Bio;
                LInterests.Text = user.Interests;
                LCity.Text = user.City + ", ";
                LState.Text = user.State;
            }
        }

        protected void LoadFriends()
        {
            // load other person's friends
            WebRequest request = WebRequest.Create(path + "GetFriendsByUserId/" + otherPersonId);
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
                // if self 

                if (friends[i].UserId == userId)
                {
                    SelfCard ctrl = (SelfCard)LoadControl("SelfCard.ascx");

                    ctrl.FirstName = friends[i].FirstName.ToString();
                    ctrl.LastName = friends[i].LastName.ToString();

                    // if no profile pic, load default
                    if (util.ProfPicArrayToImage((int)Session["userId"]) == "")
                    {
                        ctrl.ImageUrl = "https://www.telegraph.co.uk/content/dam/technology/2021/01/28/Screenshot-2021-01-28-at-13-20-35_trans_NvBQzQNjv4BqEGKV9LrAqQtLUTT1Z0gJNRFI0o2dlzyIcL3Nvd0Rwgc.png";
                    }
                    else
                    {
                        ctrl.ImageUrl = util.ProfPicArrayToImage((int)Session["userId"]);
                    }
                    ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                    // bind data to ctrl
                    ctrl.DataBind();

                    // add to panel
                    FriendPanel.Controls.Add(ctrl);

                }
                else
                { // if not self


                    // check if friend and load friend card if theyre friends
                    bool areFriends = AreFriends(friends[i].UserId);

                    if (!areFriends)
                    {
                        NonFriendCard ctrl = (NonFriendCard)LoadControl("NonFriendCard.ascx");

                        ctrl.FirstName = friends[i].FirstName.ToString();
                        ctrl.LastName = friends[i].LastName.ToString();

                        // if no profile pic, load default
                        if (util.ProfPicArrayToImage((int)Session["userId"]) == "")
                        {
                            ctrl.ImageUrl = "https://www.telegraph.co.uk/content/dam/technology/2021/01/28/Screenshot-2021-01-28-at-13-20-35_trans_NvBQzQNjv4BqEGKV9LrAqQtLUTT1Z0gJNRFI0o2dlzyIcL3Nvd0Rwgc.png";
                        }
                        else
                        {
                            ctrl.ImageUrl = util.ProfPicArrayToImage((int)Session["userId"]);
                        }
                        ctrl.Description = friends[i].Bio.ToString();
                        ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                        // bind data to ctrl
                        ctrl.DataBind();

                        // add to panel
                        FriendPanel.Controls.Add(ctrl);
                    }
                    else
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

                        // bind data to ctrl
                        ctrl.DataBind();

                        // add to panel
                        FriendPanel.Controls.Add(ctrl);

                    }

                }
            }
            // update the badge to show how many   
            LFriendsNumber.Text = friends.Length.ToString();
        }

        protected bool AreFriends(int otherPersonId)
        {
            WebRequest request = WebRequest.Create(path + "AreFriends/" + userId + "/" + otherPersonId);

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();

            reader.Close();
            response.Close();



            JavaScriptSerializer js = new JavaScriptSerializer();

            bool areFriends = js.Deserialize<bool>(data);
            return areFriends;
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = TBSearch.Text;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LSearchTitle.Text = "Search results for \"" + searchTerm + "\"";
                LSearchEmpty.Text = "0";
                return;
            }

            SearchPanel.Visible = true;

            searchTerm = searchTerm.ToLower();


            WebRequest request = WebRequest.Create(path + "SearchForFriends/" + otherPersonId + "/" + searchTerm);
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
                // if self 

                if (friends[i].UserId == userId)
                {
                    SelfCard ctrl = (SelfCard)LoadControl("SelfCard.ascx");

                    ctrl.FirstName = friends[i].FirstName.ToString();
                    ctrl.LastName = friends[i].LastName.ToString();

                    // if no profile pic, load default
                    if (util.ProfPicArrayToImage((int)Session["userId"]) == "")
                    {
                        ctrl.ImageUrl = "https://www.telegraph.co.uk/content/dam/technology/2021/01/28/Screenshot-2021-01-28-at-13-20-35_trans_NvBQzQNjv4BqEGKV9LrAqQtLUTT1Z0gJNRFI0o2dlzyIcL3Nvd0Rwgc.png";
                    }
                    else
                    {
                        ctrl.ImageUrl = util.ProfPicArrayToImage((int)Session["userId"]);
                    }
                    ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                    // bind data to ctrl
                    ctrl.DataBind();

                    // add to panel
                    SearchPanel.Controls.Add(ctrl);

                }
                else { // if not self


                    // check if friend and load friend card if theyre friends
                    bool areFriends = AreFriends(friends[i].UserId);

                    if (!areFriends)
                    {
                        NonFriendCard ctrl = (NonFriendCard)LoadControl("NonFriendCard.ascx");

                        ctrl.FirstName = friends[i].FirstName.ToString();
                        ctrl.LastName = friends[i].LastName.ToString();

                        // if no profile pic, load default
                        if (util.ProfPicArrayToImage((int)Session["userId"]) == "")
                        {
                            ctrl.ImageUrl = "https://www.telegraph.co.uk/content/dam/technology/2021/01/28/Screenshot-2021-01-28-at-13-20-35_trans_NvBQzQNjv4BqEGKV9LrAqQtLUTT1Z0gJNRFI0o2dlzyIcL3Nvd0Rwgc.png";
                        }
                        else
                        {
                            ctrl.ImageUrl = util.ProfPicArrayToImage((int)Session["userId"]);
                        }
                        ctrl.Description = friends[i].Bio.ToString();
                        ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                        // bind data to ctrl
                        ctrl.DataBind();

                        // add to panel
                        SearchPanel.Controls.Add(ctrl);
                    }
                    else
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

                        // bind data to ctrl
                        ctrl.DataBind();

                        // add to panel
                        SearchPanel.Controls.Add(ctrl);

                    }

                }
            }
            LSearchTitle.Text = "Search results for \"" + searchTerm + "\"";
            LSearchEmpty.Text = friends.Length.ToString();
        }
    }
}