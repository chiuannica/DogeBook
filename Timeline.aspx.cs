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
        protected void Page_Load(object sender, EventArgs e)
        {
            PostControl1.PostText = "Page Load!";

            //PostControl pCtrl = (PostControl)LoadControl("PostControl.ascx");
            //pCtrl.AuthorImage = "";
            //pCtrl.PostAuthor = "";
            //pCtrl.PostImage = "";
            //pCtrl.PostText = "";
            //pCtrl.PostTimestamp = DateTime.Now.ToString();

            //Form.Controls.Add(pCtrl);

        }
    }
}