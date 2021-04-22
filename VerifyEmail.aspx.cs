using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }

        protected void BtnRedirectToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            string warning = "";

            // if there is a warning
            if (string.IsNullOrWhiteSpace(TBEmail.Text))
            {
                warning = "Enter your email. ";
                LblWarning.Visible = true;
                LblWarning.Text = warning;
                return;
            }
            return;
        }
    }
}