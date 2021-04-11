using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taki_AssosiationUI.WebService;
namespace Taki_AssosiationUI
{
    public partial class SignUp : System.Web.UI.Page
    {
        public TakiWebService proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            proxy = (TakiWebService)Session["Proxy"];
        }

        protected void signup(object sender, EventArgs e)
        {
            if(!proxy.SignUp(Username.Text,Password.Text,email.Text,Calendar.SelectedDate,FName.Text,LName.Text))
            {
                error.Text = "Couldn't sign up";
            }
            else
            {
                proxy.SignIn(Username.Text, Password.Text);
                Response.Redirect("~/Account.aspx");
            }
        }
    }
}