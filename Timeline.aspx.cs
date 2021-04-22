using DogeBookLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class Timeline : System.Web.UI.Page
    {
        Utility util = new Utility();
        protected void Page_Load(object sender, EventArgs e)
        {
            pc1.PostId = 3;
            pc1.DataBind();
            //Response.Write("<script>alert('" + Session["UserId"].ToString() + "');</script>");
            //Console.Write(Session["UserId"]);

        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            Post post = new Post();
            post.Timestamp = DateTime.Now.ToString();
            post.Text = txtPostText.Text;
            //Util.FileUpload? maybe something else
            post.ImageUrl = "";
            int imageSize = 0, result = 0;
            String fileExtension, imageName;
            try
            {
                if (fuPost.HasFile)
                {
                    imageSize = fuPost.PostedFile.ContentLength;
                    byte[] imageData = new byte[imageSize];

                    fuPost.PostedFile.InputStream.Read(imageData, 0, imageSize);
                    imageName = fuPost.PostedFile.FileName;

                    fileExtension = imageName.Substring(imageName.LastIndexOf(".")).ToLower();

                    if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                    {
                        //Get userid from session
                        //use ajax or storeprocedure to put image data into TP_Users -> ProfilePicture
                        //Insert ImageURL for POST
                        //result = util.InsertProfilePicture(1, imageData);
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
            catch (Exception ex)
            {
                lblUploadStatus.Text = "Error ocurred: [" + ex.Message + "] cmd=" + result;
            }

        }

        
    }
}