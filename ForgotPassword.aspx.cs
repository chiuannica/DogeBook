using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DogeBookLibrary;

namespace DogeBook
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        AccountManagementService.AccountManagement proxy;
        AccountManagementService.Question[] questionsFromDb;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                proxy = new AccountManagementService.AccountManagement();
            }
        }

        public void BtnGetSecurityQuestions_Click(object sender, EventArgs e)
        {
            // check if email is empty
            if (TBEmail.Text == "")
            {
                LblWarning.Text = "Enter email. <br>";
                LblWarning.Visible = true;
                return;
            }
            else
            {
                LblWarning.Visible = false;
            }

            // get security questions
            GetSecurityQuestions();

            // bind to repeater
            RSecurityQuestions.DataSource = questionsFromDb;
            RSecurityQuestions.DataBind();

            BtnSubmitAns.Visible = true;
        }
        protected void GetSecurityQuestions()
        {
            // !!!!! Loading questions doesn't work
            // get the security questions
            string email = TBEmail.Text;
            int userId = proxy.GetUserIdFromEmail(email);

            // load questions
            questionsFromDb = proxy.GetSecurityQuestions(userId);
        }


        protected string CheckSecurityQuestionAnsEmpty()
        {
            string warning = "";
            for (int i = 0; i < RSecurityQuestions.Items.Count; i++)
            {
                TextBox TBAnswer = (TextBox)RSecurityQuestions.Items[i].FindControl("TBAnswer");
                string answer = TBAnswer.Text;
                if (answer == "")
                {
                    warning += "Answer question " + (i+1) + ".<br/>";
                }
            }
            return warning;
        }
        protected bool CheckSecurityQuestionCorrect()
        {
            for (int i = 0; i < RSecurityQuestions.Items.Count; i++)
            {
                TextBox TBAnswer = (TextBox)RSecurityQuestions.Items[i].FindControl("TBAnswer");
                string answer = TBAnswer.Text;

                Label labelRealAnswer = (Label)RSecurityQuestions.Items[i].FindControl("LAnswer");
                string realAnswer = labelRealAnswer.Text;

                if (answer != realAnswer)
                {
                    return false;
                }
            }
            return true;
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
            bool correctAnswer = CheckSecurityQuestionCorrect();
            if (correctAnswer)
            {
                LblWarning.Visible = false;

                LPassword.Visible = true;
                TBPassword.Visible = true;

                LConfirmPassword.Visible = true;
                TBConfirmPassword.Visible = true;

                BtnCreateNewPass.Visible = true;
            } else
            { // not correct
                LblWarning.Text = "Your answers are not all correct";
                LblWarning.Visible = true;
            }
        }

        protected void BtnCreateNewPass_Click(object sender, EventArgs e)
        {
            LblSuccess.Visible = false;
            LblWarning.Visible = false;

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
                ChangePassword();

                LblSuccess.Text = "Your password was changed successfully";
                LblSuccess.Visible = true;
            }
        }
        protected bool ChangePassword()
        {
            // get email and password from textbox
            string email = TBEmail.Text;
            string password = TBPassword.Text;
            // get userId
            int userId = proxy.GetUserIdFromEmail(email);
            return proxy.ChangePassword(userId, password);
        }

        
    }
}