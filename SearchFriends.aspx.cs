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
    public partial class SearchFriends : System.Web.UI.Page
    {
        string path = "https://localhost:44386/api/User/";
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // !!!
            // userId = Session["UserId"];
            userId = 1;
        }
        protected void LoadNonFriends()
        {


            WebRequest request = WebRequest.Create(path + "GetNonFriends/" + userId);
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
                NonFriendCard ctrl = (NonFriendCard)LoadControl("NonFriendCard.ascx");

                ctrl.FirstName = friends[i].FirstName.ToString();
                ctrl.LastName = friends[i].LastName.ToString();
                ctrl.ImageUrl = friends[i].ProfilePicture.ToString();

                ctrl.UserId = int.Parse(friends[i].UserId.ToString());


                ctrl.DataBind();

                //Panel1.Controls.Add(ctrl);


            }
        }
    }
}