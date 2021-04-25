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
            // load the userId
            userId = int.Parse(Session["UserId"].ToString());

            // load the cards for each section
            List<FriendCard> friends = LoadFriends();
            List<object> nonFriends = LoadUsers("GetUnrelatedUsers/", NonFriendPanel);
            List<object> friendsOfFriends = LoadUsers("GetFriendsOfFriends/", FriendsOfFriendsPanel);

            // show labels
            LFriendsEmpty.Visible = true;
            LFriendOfFriendsEmpty.Visible = true;
            LNonFriendsEmpty.Visible = true;

            // if 0, hide buttons
            // show how many there are
            LFriendsEmpty.Text = friends.Count.ToString();
            if (friends.Count == 0)
            {
                BtnFriends.Visible = false;
                BtnFriendsHide.Visible = false;
            } 

            LFriendOfFriendsEmpty.Text = friendsOfFriends.Count.ToString();
            if (friendsOfFriends.Count == 0)
            {
                BtnFriendOfFriends.Visible = false;
                BtnFriendOfFriendsHide.Visible = false;
            }

            LNonFriendsEmpty.Text = nonFriends.Count.ToString();
            if (nonFriends.Count == 0)
            {
                BtnAll.Visible = false;
                BtnAllHide.Visible = false;
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

        protected List<object> LoadUsers(string extension, Panel panel)
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

            List<object> ctrls = new List<object>();

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

                    // load default pic if there is no profile pic
                    if (util.ProfPicArrayToImage(friendId) != "")
                    {
                        ctrl.ImageUrl = util.ProfPicArrayToImage(friendId);
                    }
                    else
                    {
                        ctrl.ImageUrl = defaultImgUrl;
                    }

                    // bind data to ctrl
                    ctrl.DataBind();

                    ctrls.Add(ctrl);
                    // add to panel
                    panel.Controls.Add(ctrl);
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

                        ctrls.Add(ctrl);
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

                        // bind data to ctrl
                        ctrl.DataBind();

                        ctrls.Add(ctrl);
                        // add to panel
                        panel.Controls.Add(ctrl);
                    }
                }
                
            }
            return ctrls;
        }

        protected List<FriendCard> LoadFriends()
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

            List<FriendCard> ctrls = new List<FriendCard>();
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
                ctrls.Add(ctrl);

                // add to panel
                FriendsPanel.Controls.Add(ctrl);
            }
            return ctrls;
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
            LSearchEmpty.Text = friends.Length.ToString();
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

            BtnFriends.Visible = false;
            BtnFriendsHide.Visible = true;

            FriendsPanel.Visible = true;
        }

        protected void BtnFriendOfFriends_Click(object sender, EventArgs e)
        {
            BtnFriendOfFriends.Visible = false;
            BtnFriendOfFriendsHide.Visible = true;

            FriendsOfFriendsPanel.Visible = true;
        }

        protected void BtnAll_Click(object sender, EventArgs e)
        {
            BtnAll.Visible = false;
            BtnAllHide.Visible = true;

            NonFriendPanel.Visible = true;

        }

        protected void BtnFriendsHide_Click(object sender, EventArgs e)
        {

            BtnFriends.Visible = true;
            BtnFriendsHide.Visible = false;

            FriendsPanel.Visible = false;
        }

        protected void BtnFriendOfFriendsHide_Click(object sender, EventArgs e)
        {
            BtnFriendOfFriends.Visible = true;
            BtnFriendOfFriendsHide.Visible = false;

            FriendsOfFriendsPanel.Visible = false;
        }

        protected void BtnAllHide_Click(object sender, EventArgs e)
        {

            BtnAll.Visible = true;
            BtnAllHide.Visible = false;

            NonFriendPanel.Visible = false;
        }

    }
}