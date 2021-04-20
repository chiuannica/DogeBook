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

        protected void Page_Load(object sender, EventArgs e)
        {
            // !!! CHANGE
            // userId = Session["UserId"];
            Session["UserID"] = 1;
            userId = 1;

            // load the cards for each section
            List<FriendCard> friends = LoadFriends();
            List<NonFriendCard> nonFriends = LoadUsers("GetUnrelatedUsers/", NonFriendPanel);
            List<NonFriendCard> friendsOfFriends = LoadUsers("GetFriendsOfFriends/", FriendsOfFriendsPanel);

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



        protected List<NonFriendCard> LoadUsers(string extension, Panel panel)
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

            List<NonFriendCard> ctrls = new List<NonFriendCard>();
            for (int i = 0; i < friends.Length; i++)
            {
                // create card and add data
                NonFriendCard ctrl = (NonFriendCard)LoadControl("NonFriendCard.ascx");

                ctrl.FirstName = friends[i].FirstName.ToString();
                ctrl.LastName = friends[i].LastName.ToString();
                ctrl.ImageUrl = friends[i].ProfilePicture.ToString();
                ctrl.Bio = friends[i].Bio.ToString();
                ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                // bind data to ctrl
                ctrl.DataBind();
                
                // add ctrl to list of all ctrls
                ctrls.Add(ctrl);

                // add to panel
                panel.Controls.Add(ctrl);
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
                ctrl.ImageUrl = friends[i].ProfilePicture.ToString();
                //ctrl.Bio = friends[i].Bio.ToString();
                ctrl.UserId = int.Parse(friends[i].UserId.ToString());

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
            SearchPanel.Controls.Clear();

            string searchTerm = TBSearch.Text;
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
                // check if friend and load friend card if theyre friends


                NonFriendCard ctrl = (NonFriendCard)LoadControl("NonFriendCard.ascx");

                ctrl.FirstName = friends[i].FirstName.ToString();
                ctrl.LastName = friends[i].LastName.ToString();
                ctrl.ImageUrl = friends[i].ProfilePicture.ToString();
                ctrl.Bio = friends[i].Bio.ToString();
                ctrl.UserId = int.Parse(friends[i].UserId.ToString());

                // bind data to ctrl
                ctrl.DataBind();

                // add to panel
                SearchPanel.Controls.Add(ctrl);
            }




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