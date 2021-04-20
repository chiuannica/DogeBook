using DogeBookLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class PostControl : System.Web.UI.UserControl
    {
        private int postid;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            lblPostText.Text = "Thanks for clicking comment";
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
                return lblPostText.Text;
            }
            set
            {
                lblPostText.Text = value;
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
            imgPostImage.ImageUrl = row["ImageUrl"].ToString();
            lblPostText.Text = row["Text"].ToString();
            lblTimestamp.Text = row["TimeStamp"].ToString();

        }
    }
}