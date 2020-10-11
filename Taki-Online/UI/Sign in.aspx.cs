using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI
{
    public partial class Sign_in : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void SignIn(object sender, EventArgs e)
        {
            try
            {
                Session["User"] = new User(username.Text, password.Text);
                Response.Redirect(@"~\Home.aspx");
            }
            catch
            {
                Error.Text = "Couldn't Log In Please Try again";
            }
        }
    }
}