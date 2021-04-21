using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        // CHANGE !!!
        int userId = -1;
        //userId = Session["UserId"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (userId == -1)
            {
                //Response.Redirect("Login.aspx");
            }
        }
    }
}