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
    public partial class Timeline : System.Web.UI.Page
    {
        Utility util = new Utility();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // redirect to login if the user if is null 
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            LoadTimeline();
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            Post post = new Post();
            post.Timestamp = DateTime.Now;
            post.Text = txtPostText.Text;
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
                    //lblUploadStatus.Text = "Plz upload the image!!!!";
                    int postId = util.InsertPost((int)Session["UserId"], post.Text, DateTime.Now);
                }

            }
            catch (Exception ex)
            {
                lblUploadStatus.Text = "Error ocurred: [" + ex.Message + "] cmd=" + result;
            }
            lblUploadStatus.Visible = true;



        }

        protected void LoadTimeline()
        {
            WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/GetTimeline/" + Session["UserId"]);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            int[] posts = js.Deserialize<int[]>(data);

            for(int i = posts.Length-1; i >= 0; i--)
            {
                PostControl pCtrl = (PostControl)LoadControl("PostControl.ascx");

                pCtrl.PostId = posts[i];
                pCtrl.DataBind();
                timeline.Controls.Add(pCtrl);
            }

        }

        protected void LoadComments()
        {
            //WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/GetComments/" + postid);
            //WebResponse response = request.GetResponse();

            //Stream theDataStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(theDataStream);

            //String data = reader.ReadToEnd();
            //reader.Close();
            //response.Close();

            //JavaScriptSerializer js = new JavaScriptSerializer();

            //DataSet postComments = js.Deserialize<DataSet>(data);

            //if (postComments != null && postComments.Tables.Count > 0 && postComments.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < postComments.Tables[0].Rows.Count; i++)
            //    {
            //        LiteralControl comment = new LiteralControl("<div class=\"col\">" + util.GetNameByUserId((int)postComments.Tables[0].Rows[i]["UserId"]) + "  </div>");
            //        commentSection.Controls.Add(comment);
            //    }


            //}
                

            }

    }
}