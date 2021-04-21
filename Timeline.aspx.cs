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
            
        }

        
    }
}