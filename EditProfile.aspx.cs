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
        AccountManagementService.AccountManagement proxy;

        string path = "https://localhost:44386/api/User/";

        int userId;
        User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // redirect to login if the user if is null 
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                userId = int.Parse(Session["UserId"].ToString());
                proxy = new AccountManagementService.AccountManagement();
                LoadUserInformation();
            }
        }
        protected User GetUser()
        {
            userId = int.Parse(Session["UserId"].ToString());
            WebRequest request = WebRequest.Create(path + "GetUserById/" + userId);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            user = js.Deserialize<User>(data);
            return user;
        }

        protected void LoadUserInformation()
        {
            user = GetUser();

            if (user != null)
            {
                LFirstName.Text = user.FirstName;
                LLastName.Text = user.LastName;

                string imageUrl = util.ProfPicArrayToImage(userId);
                if (imageUrl == null || imageUrl == "")
                {
                    ImgProfilePic.ImageUrl = "https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg";
                }
                else
                {
                    ImgProfilePic.ImageUrl = imageUrl;
                }

                TBBio.Text = user.Bio;
                TBInterests.Text = user.Interests;
                TBCity.Text = user.City;
                TBState.Text = user.State;
            }
        }



        protected void btnUploadProfilePicture_Click(object sender, EventArgs e)
        {
            UpdateProfilePicture();
        }
        protected bool UpdateProfilePicture()
        {
            Utility util = new Utility();
            int imageSize = 0, result = 0;
            String fileExtension, imageName;
            bool updatedProfilePicture = false;
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
                        result = util.InsertProfilePicture(userId, imageData);
                        lblUploadStatus.Text = "Image uploaded successfully";

                        updatedProfilePicture = true;
                    }
                    else
                    {
                        lblUploadStatus.Text = "Only jpg file formats supported.";
                    }
                }
                else
                {
                    lblUploadStatus.Text = "Please select the image before uploading.";
                }
                lblUploadStatus.Visible = true;
            }
            catch (Exception ex)
            {
                lblUploadStatus.Text = "Error ocurred: [" + ex.Message + "] cmd=" + result;
            }
            // reload user information
            LoadUserInformation();
            return updatedProfilePicture;
        }

        protected void BtnUpdateProfile_Click(object sender, EventArgs e)
        {
            bool updated = true;
            userId = int.Parse(Session["UserId"].ToString());
            // if there is a new picture, update picture
            if (fuProfilePic.HasFile)
            {
                updated = updated && UpdateProfilePicture();
            }
            updated = updated && UpdateBio(userId, TBBio.Text);
            updated = updated && UpdateInterests(userId, TBInterests.Text);
            updated = updated && UpdateCity(userId, TBCity.Text);
            updated = updated && UpdateState(userId, TBState.Text);
            
            // reload profile
            LoadUserInformation();

            // display message if the profile was updated or not
            LUpdateProfile.Visible = true;
            if (updated)
                LUpdateProfile.Text = "Your profile was updated.";
            else
                LUpdateProfile.Text = "A problem occurred. Your profile was not updated.";
        }


        public bool UpdateBio(int userId, string content)
        {
            proxy = new AccountManagementService.AccountManagement();
            return proxy.UpdateBio(userId, content);
        }
        public bool UpdateInterests(int userId, string content)
        {
            proxy = new AccountManagementService.AccountManagement();
            return proxy.UpdateInterests(userId, content);
        }
        public bool UpdateCity(int userId, string content)
        {
            proxy = new AccountManagementService.AccountManagement();
            return proxy.UpdateCity(userId, content);
        }
        public bool UpdateState(int userId, string content)
        {
            proxy = new AccountManagementService.AccountManagement();
            return proxy.UpdateState(userId, content);
        }

        protected void BtnBackToProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
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
    }
}