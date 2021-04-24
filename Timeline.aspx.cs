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
        //int userid = Int32.Parse(HttpContext.Current.Session["UserId"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            pc1.PostId = 6;
            pc1.DataBind();
            //Response.Write("<script>alert('" + Session["UserId"].ToString() + "');</script>");
            //Response.Write("<script>alert('" + userid + "');</script>"); 
            //Console.Write(Session["UserId"]);

        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            Post post = new Post();
            post.Timestamp = DateTime.Now;
            post.Text = txtPostText.Text;
            //Util.FileUpload? maybe something else
            int imageSize = 0, result = 0;
            byte[] bytes = new byte[]{byte.MinValue};
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
                        
                        int postId = util.InsertPost((int)Session["UserId"], post.Text, imageData, DateTime.Now);
                        Response.Write("<script>alert('" + postId + "');</script>");
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

            }
            catch (Exception ex)
            {
                lblUploadStatus.Text = "Error ocurred: [" + ex.Message + "] cmd=" + result;
            }
            lblUploadStatus.Visible = true;



        }

        
    }
}