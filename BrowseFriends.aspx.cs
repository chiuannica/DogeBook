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
            // !!!
            // userId = Session["UserId"];
            userId = 1;

            List<NonFriendCard> nonFriends = LoadUsers("GetNonFriends/", NonFriendPanel);
            List<NonFriendCard> friendsOfFriends = LoadUsers("GetFriendsOfFriends/", FriendsOfFriendsPanel);

            // hide view buttons

            BtnFriendOfFriends.Visible = false;
            BtnAll.Visible = false;


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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchPanel.Controls.Clear();

            string searchTerm = TBSearch.Text;
            SearchPanel.Visible = true;




            WebRequest request = WebRequest.Create(path + "SearchForUser/" + userId);
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