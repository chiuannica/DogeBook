using DogeBookLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class BrowseFriends : System.Web.UI.Page
    {
        string path = "https://localhost:44386/api/User/";
        int userId;
        Utility util = new Utility();
        string defaultImgUrl  = "https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // redirect to login if the user if is null 
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            } 
            // load the userId
            userId = int.Parse(Session["UserId"].ToString());

            // load the cards for each section
            List<int> friends = LoadFriends();
            List<int> nonFriends = LoadUsers("GetUnrelatedUsers/", NonFriendPanel);
            List<int> friendsOfFriends = LoadUsers("GetFriendsOfFriends/", FriendsOfFriendsPanel);

            // show labels
            LFriendsEmpty.Visible = true;
            LFriendOfFriendsEmpty.Visible = true;
            LNonFriendsEmpty.Visible = true;

            // show how many there are
            LFriendsNumber.Text = friends.Count.ToString();
            // if 0, hide buttons and show message
            if (friends.Count == 0)
            {
                BtnFriends.Visible = false;
                BtnFriendsHide.Visible = false;

                LFriendsEmpty.Visible = true;
                LFriendsEmpty.Text = "You have no friends. ";
            }

            LFriendOfFriendsNumber.Text = friendsOfFriends.Count.ToString();
            if (friendsOfFriends.Count == 0)
            {
                BtnFriendOfFriends.Visible = false;
                BtnFriendOfFriendsHide.Visible = false;


                LFriendOfFriendsEmpty.Visible = true;
                LFriendOfFriendsEmpty.Text = "You have no friend of friends. ";
            }

            LNonFriendsNumber.Text = nonFriends.Count.ToString();
            if (nonFriends.Count == 0)
            {
                BtnAll.Visible = false;
                BtnAllHide.Visible = false;

                LNonFriendsEmpty.Visible = true;
                LNonFriendsEmpty.Text = "There are no other users. ";
            }
        }
        protected string GetProfilePicture(int userId)
        {
            string profilePicture = util.ProfPicArrayToImage(userId);
            if (profilePicture == "" || profilePicture == null)
            {
                profilePicture = defaultImgUrl;
            }
            else
            {
                profilePicture = util.ProfPicArrayToImage(userId);
            }
            return profilePicture;
        }

        protected List<int> LoadUsers(string extension, Panel panel)
        {
            WebRequest request = WebRequest.Create(path + extension  + userId);
            WebResponse response = request.GetResponse();


            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();

            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            User[] friends = js.Deserialize<User[]>(data);

            List<int> UserIds = new List<int>();

            for (int i = 0; i < friends.Length; i++)
            {
                // skip loading the currently logged in user
                if (friends[i].UserId != userId)
                {
                    // skip if this user is already loaded
                    if (!UserIds.Contains(friends[i].UserId))
                    {
                        // check if friend and load friend card if theyre friends
                        bool areFriends = AreFriends(friends[i].UserId);

                        // if not friends, load the not friends card
                        if (!areFriends)
                        {
                            NonFriendCard ctrl = (NonFriendCard)LoadControl("NonFriendCard.ascx");

                            ctrl.FirstName = friends[i].FirstName.ToString();
                            ctrl.LastName = friends[i].LastName.ToString();

                            int friendId = int.Parse(friends[i].UserId.ToString());
                            ctrl.UserId = friendId;
                            // if no profile pic, load default

                            string profilePicture = GetProfilePicture(friendId);
                            ctrl.ImageUrl = profilePicture;

                            ctrl.Description = friends[i].Bio.ToString();
                            ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                            // bind data to ctrl
                            ctrl.DataBind();

                            UserIds.Add(friends[i].UserId);
                            // add to panel
                            panel.Controls.Add(ctrl);
                        }
                        else
                        {
                            FriendCard ctrl = (FriendCard)LoadControl("FriendCard.ascx");

                            ctrl.FirstName = friends[i].FirstName.ToString();
                            ctrl.LastName = friends[i].LastName.ToString();

                            int friendId = int.Parse(friends[i].UserId.ToString());
                            ctrl.UserId = friendId;

                            // load default pic if there is no profile pic
                            string profilePicture = GetProfilePicture(friendId);
                            ctrl.ImageUrl = profilePicture;
                            ctrl.Description = friends[i].Bio.ToString() + " and you";


                            // bind data to ctrl
                            ctrl.DataBind();

                            UserIds.Add(friends[i].UserId);

                            // add to panel
                            panel.Controls.Add(ctrl);
                        }
                    }
                }
                
            }
            return UserIds;
        }

        protected List<int> LoadFriends()
        {
            string extension = "GetFriends/";
            WebRequest request = WebRequest.Create(path + extension + userId);
            WebResponse response = request.GetResponse();


            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();

            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            User[] friends = js.Deserialize<User[]>(data);

            List<int> UserIds  = new List<int>();
            for (int i = 0; i < friends.Length; i++)
            {
                // create card and add data
                FriendCard ctrl = (FriendCard)LoadControl("FriendCard.ascx");

                ctrl.FirstName = friends[i].FirstName.ToString();
                ctrl.LastName = friends[i].LastName.ToString();
                // get friend id
                int friendId = int.Parse(friends[i].UserId.ToString());
                ctrl.UserId = friendId;

                // load default pic if there is no profile pic
                string profilePicture = GetProfilePicture(friendId);
                ctrl.ImageUrl = profilePicture;

                ctrl.Description = "Friend";

                // bind data to ctrl
                ctrl.DataBind();

                // add ctrl to list of all ctrls
                UserIds.Add(friends[i].UserId);


                // add to panel
                FriendsPanel.Controls.Add(ctrl);
            }
            return UserIds;
        }



        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = TBSearch.Text;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LSearchTitle.Text = "Search results for \"" + searchTerm + "\"";
                LSearchNumber.Text = "0";
                return;
            }

            SearchPanel.Visible = true;

            searchTerm = searchTerm.ToLower();


            WebRequest request = WebRequest.Create(path + "SearchForUser/" + searchTerm);
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
                // if self, load self card
                if (friends[i].UserId == userId)
                {
                    SelfCard ctrl = (SelfCard)LoadControl("SelfCard.ascx");

                    ctrl.FirstName = friends[i].FirstName.ToString();
                    ctrl.LastName = friends[i].LastName.ToString();

                    int friendId = int.Parse(friends[i].UserId.ToString());
                    ctrl.UserId = friendId;

                    string profilePicture = GetProfilePicture(friendId);
                    ctrl.ImageUrl = profilePicture;

                    // bind data to ctrl
                    ctrl.DataBind();

                    // add to panel
                    SearchPanel.Controls.Add(ctrl);
                }
                else // not self
                {
                    // check if friend and load friend card if theyre friends
                    bool areFriends = AreFriends(friends[i].UserId);

                    // if not friends, load the not friends card
                    if (!areFriends)
                    {
                        NonFriendCard ctrl = (NonFriendCard)LoadControl("NonFriendCard.ascx");

                        ctrl.FirstName = friends[i].FirstName.ToString();
                        ctrl.LastName = friends[i].LastName.ToString();

                        int friendId = int.Parse(friends[i].UserId.ToString());
                        ctrl.UserId = friendId;
                        // if no profile pic, load default
                        string profilePicture = GetProfilePicture(friendId);
                        ctrl.ImageUrl = profilePicture;

                        ctrl.Description = friends[i].Bio.ToString();
                        ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                        // bind data to ctrl
                        ctrl.DataBind();

                        // add to panel
                        SearchPanel.Controls.Add(ctrl);
                    } else
                    {
                        FriendCard ctrl = (FriendCard)LoadControl("FriendCard.ascx");

                        ctrl.FirstName = friends[i].FirstName.ToString();
                        ctrl.LastName = friends[i].LastName.ToString();

                        int friendId = int.Parse(friends[i].UserId.ToString());
                        ctrl.UserId = friendId;

                        // load default pic if there is no profile pic
                        string profilePicture = GetProfilePicture(friendId);
                        ctrl.ImageUrl = profilePicture;

                        // bind data to ctrl
                        ctrl.DataBind();

                        // add to panel
                        SearchPanel.Controls.Add(ctrl);
                    }
                }
            }
            LSearchTitle.Text = "Search results for \"" + searchTerm + "\""; 
            LSearchNumber.Text = friends.Length.ToString();
        }


        protected bool AreFriends(int otherPersonId)
        {
            userId = int.Parse(Session["UserId"].ToString());
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



        protected void BtnFriends_Click(object sender, EventArgs e)
        {
            // show the panel, show the hide button, hide the show button, hide empty msg
            BtnFriends.Visible = false;
            BtnFriendsHide.Visible = true;

            FriendsPanel.Visible = true;
            LFriendsEmpty.Visible = false;
        }

        protected void BtnFriendOfFriends_Click(object sender, EventArgs e)
        {
            // show the panel, show the hide button, hide the show button, hide empty msg
            BtnFriendOfFriends.Visible = false;
            BtnFriendOfFriendsHide.Visible = true;

            FriendsOfFriendsPanel.Visible = true;
            LFriendOfFriendsEmpty.Visible = false;
        }

        protected void BtnAll_Click(object sender, EventArgs e)
        {
            // show the panel, show the hide button, hide the show button, hide empty msg
            BtnAll.Visible = false;
            BtnAllHide.Visible = true;

            NonFriendPanel.Visible = true;
            LNonFriendsEmpty.Visible = false;
        }

        protected void BtnFriendsHide_Click(object sender, EventArgs e)
        {
            // hide the friends, hide the hide button, show the show button
            BtnFriends.Visible = true;
            BtnFriendsHide.Visible = false;
            FriendsPanel.Visible = false;
            // show a message saying this is hidden
            LFriendsEmpty.Visible = true;
            LFriendsEmpty.Text = "Friends are hidden. ";
        }

        protected void BtnFriendOfFriendsHide_Click(object sender, EventArgs e)
        {
            // hide the friends, hide the hide button, show the show button
            BtnFriendOfFriends.Visible = true;
            BtnFriendOfFriendsHide.Visible = false;
            FriendsOfFriendsPanel.Visible = false;
            // show a message saying this is hidden
            LFriendOfFriendsEmpty.Visible = true;
            LFriendOfFriendsEmpty.Text = "Friends of friends are hidden. ";
        }

        protected void BtnAllHide_Click(object sender, EventArgs e)
        {
            // hide the friends, hide the hide button, show the show button
            BtnAll.Visible = true;
            BtnAllHide.Visible = false;
            NonFriendPanel.Visible = false;
            // show a message saying this is hidden
            LNonFriendsEmpty.Visible = true;
            LNonFriendsEmpty.Text = "Friends of friends are hidden. ";
        }
    }
}