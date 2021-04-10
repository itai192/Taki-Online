using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taki_AssosiationUI.WebService;
namespace Taki_AssosiationUI
{
    public partial class Sign_In : System.Web.UI.Page
    {
        public TakiWebService proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            proxy = (TakiWebService)Session["Proxy"];
        }

        protected void SignIn(object sender, EventArgs e)
        {
            if(proxy.SignIn(username.Text,password.Text))
            {
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                Error.Text = "Password and username don't match";
            }
        }
    }
}