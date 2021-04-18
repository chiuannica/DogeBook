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
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 1;
            //int userId = Session["UserId"];

            string path = "https://localhost:44386/api/User/";

            // Create an HTTP Web Request and get the HTTP Web Response from the server.

            WebRequest request = WebRequest.Create(path + userId);

            WebResponse response = request.GetResponse();



            // Read the data from the Web Response, which requires working with streams.

            Stream theDataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();

            reader.Close();

            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            User user = js.Deserialize<User>(data);

            if (user.FirstName != null)
            {
                LFirstName.Text = user.FirstName;
            }

        }
    }
}