using DogeBookLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class PostControl : System.Web.UI.UserControl
    {
        private int postid;
        Utility util = new Utility();
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void LoadComments()
        {
            WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/GetComments/" + postid);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);

            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();

            List<String> postComments = js.Deserialize<List<String>>(data);
            commentSection.Controls.Clear();
            if (postComments != null && postComments.Count > 0)
            {
                for (int i = 0; i < postComments.Count; i++)
                {
                    //panel or placeholder(server side container - server side code)
                    Label comment = new Label();
                    comment.Text = postComments[i].ToString();
                    commentSection.Controls.Add(comment);
                }
            }
        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            if (pnlCommentToggle.Visible)
            {
                pnlCommentToggle.Visible = false;
            }
            else
            {
                pnlCommentToggle.Visible = true;
            }
        }

        [Category("Misc")]
        public int PostId
        {
            get { return postid; }
            set { postid = value; }
        }

        public String PostText
        {
            get
            {
                return txtPostText.Text;
            }
            set
            {
                txtPostText.Text = value;
            }
        }

        public String PostAuthor
        {
            get
            {
                return lblAuthor.Text;
            }
            set
            {
                lblAuthor.Text = value;
            }
        }

        public String PostTimestamp
        {
            get
            {
                return lblTimestamp.Text;
            }
            set
            {
                lblTimestamp.Text = value;
            }
        }

        public String AuthorImage
        {
            get
            {
                return imgAuthor.ImageUrl;
            }
            set
            {
                imgAuthor.ImageUrl = value;
            }
        }

        public String PostImage
        {
            get
            {
                return imgPostImage.ImageUrl;
            }

            set
            {
                imgPostImage.ImageUrl = value;
            }
        }

        public override void DataBind()
        {
            Utility util = new Utility();
            DataSet postData = util.PostControlDatabind(PostId);
            DataRow row = postData.Tables[0].Rows[0];
            lblAuthor.Text = row["FirstName"].ToString() + " " + row["LastName"].ToString();
            imgAuthor.ImageUrl = util.ProfPicArrayToImage((int)row["UserId"]);
            if (!DBNull.Value.Equals(row["Image"]))
            {
                imgPostImage.ImageUrl = util.ByteArrayToImageUrl((byte[])row["Image"]);
            }
            else
            {
                imgPostImage.Visible = false;
            }

            txtPostText.Text = row["Text"].ToString();
            lblTimestamp.Text = row["TimeStamp"].ToString();
            postid = (int)row["PostId"];
            hdnPostId.Value = postid.ToString();
            Boolean liked = util.CheckLike((int)Session["UserId"], postid);
            if (liked)
            {
                btnLike.InnerHtml = "<i class=\"fas fa-thumbs-down\"></i>&nbsp Unlike";
            }
            if ((int)Session["UserId"] == (int)row["UserId"])
            {
                btnEdit.Visible = true;
                btnDeletePost.Visible = true;
            }
            lblLikes.Text = "<i class=\"fas fa-paw\"></i>" + util.CountLikesOnPost(postid);
            LoadComments();

        }

        protected void btnLike_ServerClick(object sender, EventArgs e)
        {
            Boolean liked = util.CheckLike((int)Session["UserId"], postid);
            if (liked)
            {
                WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/Unlike/" + Session["UserId"] + "/" + postid);
                request.Method = "DELETE";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    btnLike.InnerHtml = "<i class=\"fas fa-paw\"></i>&nbsp Like";
                }
            }
            else
            {
                WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/LikePost/" + Session["UserId"] + "/" + postid);
                request.Method = "POST";
                request.ContentLength = 0;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    btnLike.InnerHtml = "<i class=\"fas fa-thumbs-down\"></i>&nbsp Unlike";
                }

            }
            DataBind();
        }

        protected void btnPostComment_ServerClick(object sender, EventArgs e)
        {
            util.MakeComment(postid, (int)Session["UserId"], txtComment.Text);
            DataBind();
            txtComment.Text = "";
        }

        protected void btnUpdatePostText_ServerClick(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/UpdatePostText/" + postid + "/" + txtPostText.Text);
            request.Method = "PUT";
            request.ContentLength = 0;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                DataBind();
                btnUpdatePostText.Visible = false;
                txtPostText.ReadOnly = true;
            }

        }

        protected void btnEdit_ServerClick(object sender, EventArgs e)
        {
            btnUpdatePostText.Visible = true;
            txtPostText.ReadOnly = false;
            txtPostText.Focus();
        }

        protected void btnDeletePost_ServerClick(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/DeletePost/" + postid);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }

        }
    }
}