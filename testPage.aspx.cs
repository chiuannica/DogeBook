using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeBook
{
    public partial class testPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("test");

            AccountManagementService.AccountManagement proxy = new AccountManagementService.AccountManagement();
            double sum = proxy.Add(31212, 12);
            Response.Write(sum);

            bool insert1 = proxy.AddSecurityQuestion(0, "thing", "thang");
            Response.Write(insert1);
            proxy.AddSecurityQuestion(0, "thing", "thang34234");


        }
    }
}