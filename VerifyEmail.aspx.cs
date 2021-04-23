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
        public AccountManagementService.AccountManagement proxy = new AccountManagementService.AccountManagement();
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

            string email = TBEmail.Text;
            // if nothing in the textbox
            if (string.IsNullOrWhiteSpace(email))
            {
                warning = "Enter your email. ";
                LblWarning.Visible = true;
                LblWarning.Text = warning;
                return;
            }
            int userId = proxy.GetUserIdFromEmail(email);
            // if there is no email found 
            if (userId < 0)
            {
                warning = "Email not found. ";
                LblWarning.Visible = true;
                LblWarning.Text = warning;
                return;
            } else // if there is a userId for this email
            {
                // hide warning
                LblWarning.Visible = false;

                // Verify account
                bool verified = proxy.VerifyAccount(userId);

                // if verified successfully
                if (verified)
                {
                    // show success message
                    LblSuccess.Visible = true;
                    LblSuccess.Text = warning;
                    // show go to login button
                    BtnRedirectToLogin.Visible = true; 
                } else // not verified
                {
                    warning = "Failed to verify email. ";
                    LblWarning.Visible = true;
                    LblWarning.Text = warning;
                }
            }


            return;
        }
    }
}