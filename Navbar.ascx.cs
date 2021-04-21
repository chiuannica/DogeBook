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
        int userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            // load the userId
            if (Session["UserId"] != null)
                userId = int.Parse(Session["UserId"].ToString());
            
        }
    }
}