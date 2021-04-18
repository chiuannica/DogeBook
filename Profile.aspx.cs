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
        //int userId = Session["UserId"];
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUserInformation();
            LoadFriends();
        }
        protected void LoadUserInformation()
        {
            string path = "https://localhost:44386/api/User/GetUserById/";

            WebRequest request = WebRequest.Create(path + userId);
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
                
                if (user.ProfilePicture != "")
                {
                    ImgProfilePic.ImageUrl = user.ProfilePicture;
                }
                else
                {
                    ImgProfilePic.ImageUrl = "https://www.telegraph.co.uk/content/dam/technology/2021/01/28/Screenshot-2021-01-28-at-13-20-35_trans_NvBQzQNjv4BqEGKV9LrAqQtLUTT1Z0gJNRFI0o2dlzyIcL3Nvd0Rwgc.png";
                }
            


                LBio.Text = user.Bio;
                LInterests.Text = user.Interests;
                LCity.Text = user.City;
                LState.Text = user.State;
            }
        }
        protected void LoadFriends()
        {
            string path = "https://localhost:44386/api/User/GetFriendsByUserId/";

            WebRequest request = WebRequest.Create(path + userId);
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
                ctrl.ImageUrl = friends[i].ProfilePicture.ToString();

                ctrl.UserId = int.Parse(friends[i].UserId.ToString());


                ctrl.DataBind();



                // Add the control object to the WebForm's Controls collection

                FriendPanel.Controls.Add(ctrl);

            }

            //gvTeams.DataSource = teams;

            //.DataBind();
        }
    }
}