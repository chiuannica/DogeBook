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

        //protected void LoadComments()
        //{
        //    WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/GetComments/" + postid);
        //    WebResponse response = request.GetResponse();

        //    Stream theDataStream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(theDataStream);

        //    String data = reader.ReadToEnd();
        //    reader.Close();
        //    response.Close();

        //    JavaScriptSerializer js = new JavaScriptSerializer();

        //    DataSet postComments = js.Deserialize<DataSet>(data);

        //    if(postComments != null && postComments.Tables.Count > 0 && postComments.Tables[0].Rows.Count > 0)
        //    {
        //        for(int i = 0; i < postComments.Tables[0].Rows.Count; i++)
        //        {
        //            LiteralControl comment = new LiteralControl("<div class=\"col\">" + util.GetNameByUserId((int)postComments.Tables[0].Rows[i]["UserId"]) + "  </div>");
        //            commentSection.Controls.Add(comment);
        //        }


        //    }


        //}

        protected void btnComment_Click(object sender, EventArgs e)
        {
            txtPostText.Text = "Thanks for clicking comment";
            if (commentTextBox.Visible)
            {
                commentTextBox.Visible = false;
            }
            else
            {
                commentTextBox.Visible = true;
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
            imgPostImage.ImageUrl = util.ByteArrayToImageUrl((byte[])row["Image"]);
            txtPostText.Text = row["Text"].ToString();
            lblTimestamp.Text = row["TimeStamp"].ToString();
            postid = (int)row["PostId"];
            hdnPostId.Value = postid.ToString();
            Boolean liked = util.CheckLike((int)Session["UserId"], postid);
            if (liked)
            {
                btnLike.InnerText = "Unlike";
            }

            lblLikes.Text += util.CountLikesOnPost(postid);
            //DataSet PostComments = util.GetCommentsForPost(postid);
            //if (PostComments.Tables[0].Rows.Count > 0)
            //{
            //    cmtAuthor.Text = util.GetNameByUserId((int)PostComments.Tables[0].Rows[0]["UserId"]);
            //    comment.Text = PostComments.Tables[0].Rows[0]["Text"].ToString();
            //}
            //LoadComments();

        }

        protected void btnLike_ServerClick(object sender, EventArgs e)
        {
            Boolean liked = util.CheckLike((int)Session["UserId"], postid);
            if (liked)
            {
                WebRequest request = WebRequest.Create("https://localhost:44305/api/Timeline/Unlike/" + Session["UserId"] +"/"+ postid);
                request.Method = "DELETE";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if(response.StatusCode == HttpStatusCode.OK)
                {
                     
                }
            }
            else
            {
                util.LikePost((int)Session["UserId"], postid);
            }

        }
    }
}