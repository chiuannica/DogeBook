using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnGetSecurityQuestions_Click(object sender, EventArgs e)
        {
            if (TBEmail.Text == "")
            {
                LblWarning.Text = "Enter email. <br>";
                LblWarning.Visible = true;
                return;
            } else
            {
                LblWarning.Visible = false;
            }

            LSecurityQuestion1Ans.Visible = true;
            TBSecurityQuestion1Ans.Visible = true;

            LSecurityQuestion2Ans.Visible = true;
            TBSecurityQuestion2Ans.Visible = true;

            LSecurityQuestion3Ans.Visible = true;
            TBSecurityQuestion3Ans.Visible = true;

            BtnSubmitAns.Visible = true;
        }
        protected string CheckSecurityQuestionAnsEmpty()
        {
            string warning = "";
            if (TBSecurityQuestion1Ans.Text == "")
            {
                warning += "Enter answer for the first security question. <br>";
            }
            if (TBSecurityQuestion2Ans.Text == "")
            {
                warning += "Enter answer for the second security question. <br>";
            }
            if (TBSecurityQuestion3Ans.Text == "")
            {
                warning += "Enter answer for the third security question. <br>";
            }
            return warning;
        }

        protected void BtnSubmitAns_Click(object sender, EventArgs e)
        {
            string warning = CheckSecurityQuestionAnsEmpty();
            if (warning != "")
            {
                LblWarning.Text = warning;
                LblWarning.Visible = true;
                return;
            }
            LblWarning.Visible = false;
            
            // check if the answers are correct

            LPassword.Visible = true;
            TBPassword.Visible = true;
                
            LConfirmPassword.Visible = true;
            TBConfirmPassword.Visible = true;

            BtnCreateNewPass.Visible = true;
        }

        protected void BtnCreateNewPass_Click(object sender, EventArgs e)
        {
            LblSuccess.Visible = false;

            string warning = "";
            if (TBPassword.Text == "")
            {
                warning += "Enter password. <br> ";
            }
            if (TBConfirmPassword.Text == "")
            {
                warning += "Confirm password. <br> ";
            }
            if (TBPassword.Text != TBConfirmPassword.Text)
            {
                warning += "Passwords do not match. <br> ";
            }

            if (warning != "")
            {
                LblWarning.Text = warning;
                LblWarning.Visible = true;
                return;
            }
            else
            {
                // change the password

                LblSuccess.Text = "Your password was changed successfully";
                LblSuccess.Visible = true;
            }
        }
    }
}