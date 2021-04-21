using DogeBookLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class EditProfile : System.Web.UI.Page
    {
        Utility util = new Utility();
        int userId = 1;
        string path = "https://localhost:44386/api/User/";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = userId;
            LoadUserInformation();
        }

        protected void LoadUserInformation()
        {

            WebRequest request = WebRequest.Create(path + "GetUserById/" + userId);
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

                ImgProfilePic.ImageUrl = util.ProfPicArrayToImage((int)Session["userId"]);

                LBio.Text = user.Bio;
                LInterests.Text = user.Interests;
                LCity.Text = user.City;
                LState.Text = user.State;
            }
        }

        //private string ProfPicArrayToImage(int userid)
        //{
            
        //    byte[] bytes = util.GetProfilePicture(userid);
        //    string imageUrl;
        //    if (bytes == null)
        //    {
        //        imageUrl = null;
        //    }
        //    else
        //    {
        //        imageUrl = "data:image/jpg;base64," + Convert.ToBase64String(bytes);

        //    }
        //    return imageUrl;
        //}


        protected void btnUploadProfilePicture_Click(object sender, EventArgs e)
        {
            Utility util = new Utility();
            int imageSize = 0, result = 0;
            String fileExtension, imageName;
            try
            {
                if (fuProfilePic.HasFile)
                {
                    imageSize = fuProfilePic.PostedFile.ContentLength;
                    byte[] imageData = new byte[imageSize];

                    fuProfilePic.PostedFile.InputStream.Read(imageData, 0, imageSize);
                    imageName = fuProfilePic.PostedFile.FileName;

                    fileExtension = imageName.Substring(imageName.LastIndexOf(".")).ToLower();

                    if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                    {
                        //Get userid from session
                        //use ajax or storeprocedure to put image data into TP_Users -> ProfilePicture
                        result = util.InsertProfilePicture(1, imageData);
                        lblUploadStatus.Text = "Image Uploaded successfully";
                    }
                    else
                    {
                        lblUploadStatus.Text = "Only jpg file formats supported.";
                    }
                }
                else
                {
                    lblUploadStatus.Text = "Plz upload the image!!!!";
                }
                lblUploadStatus.Visible = true;
            }
            catch(Exception ex)
            { 
                lblUploadStatus.Text = "Error ocurred: [" + ex.Message + "] cmd=" + result;
            }
        }
    }
}