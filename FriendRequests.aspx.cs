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
    public partial class FriendRequests : System.Web.UI.Page
    {
        string path = "https://localhost:44386/api/User/";
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // load the userId
                userId = int.Parse(Session["UserId"].ToString());

                LoadFriendRequests();
                LNumFriendRequests.Visible = true;

                if (RFriendRequests.Items.Count == 0)
                {
                    LNumFriendRequests.Text = "You have no friend requests.";
                } else if (RFriendRequests.Items.Count == 1)
                {
                    LNumFriendRequests.Text = "You have 1 friend request.";
                }
                else 
                {
                    LNumFriendRequests.Text = "You have "+ RFriendRequests.Items.Count.ToString() + "  friend requests.";
                }
            
            }

        }

        protected void LoadFriendRequests()
        {
            string extension = "GetFriendRequests/";
            WebRequest request = WebRequest.Create(path + extension + userId);
            WebResponse response = request.GetResponse();


            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();

            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            User[] friendRequests = js.Deserialize<User[]>(data);

            RFriendRequests.DataSource = friendRequests;
            RFriendRequests.DataBind();

        }

        protected void RFriendRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            // get the person's id
            int rowIndex = e.Item.ItemIndex;
            Label LUserID = (Label)RFriendRequests.Items[rowIndex].FindControl("LUserId");
            String otherPersonId = LUserID.Text;

            Label LFirstName = (Label)RFriendRequests.Items[rowIndex].FindControl("LFirstName");
            Label LLastName = (Label)RFriendRequests.Items[rowIndex].FindControl("LLastName");

            String otherPersonName = LFirstName.Text + " " + LLastName.Text;


            
            // if accept
            string extension = "";
            string responseCode = "";
            string userAction = "";
            if (e.CommandName == "Accept")
            {
                userAction = "accepted";
                extension = "AcceptFriend/";
            } else // if deny
            {
                userAction = "denied";
                extension = "DenyFriend/";
            }

            try
            {
                string requestString = path + extension + userId + "/" + otherPersonId;
                WebRequest request = WebRequest.Create(requestString);

                request.Method = "PUT";
                request.ContentLength = 0;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                responseCode = response.StatusCode.ToString();

                response.Close();
            }
            catch (Exception ex)
            {
                LMessage.Text = "Error: " + ex.Message;
            }


            if (responseCode == "OK")
                LMessage.Text = "The friend request from " + otherPersonName + " was " + userAction;
            else
                LMessage.Text = "A problem occurred. The friend request was not " + userAction;

            LMessage.Visible = true;

            LoadFriendRequests();
        }
    }
}