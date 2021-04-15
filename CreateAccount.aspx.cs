using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("test");

            //AccountManagementService.AccountManagement proxy = new AccountManagementService.AccountManagement();
            //double sum = proxy.Add(31212, 12);
            //Response.Write(sum);

            Response.Write("12");

        }

        protected string InputValidation()
        {
            string warning = "";
            if (TBEmail.Text == "")
            {
                warning += "Enter hubababa. <br>";
            } 
            if (TBFirstName.Text == "")
            {
                warning += "Enter a first name. <br> ";
            }
            if (TBLastName.Text == "")
            {
                warning += "Enter a last name. <br> ";
            }
            if (TBPassword.Text == "")
            {
                warning += "Enter a password. <br> ";
            }
            if (TBPassword.Text != TBConfirmPassword.Text)
            {
                warning += "Passwords do not match. <br> ";
            }
            return warning;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string warning = InputValidation();

            // check if email is used already

            if (warning != "")
                LblWarning.Visible = true;
                LblWarning.Text = warning;

            // insert new user record
        }
    }
}