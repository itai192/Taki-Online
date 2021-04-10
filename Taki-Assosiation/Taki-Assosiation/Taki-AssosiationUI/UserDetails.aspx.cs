using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taki_AssosiationUI.WebService;
namespace Taki_AssosiationUI
{
    public partial class UserDetails : System.Web.UI.Page
    {
        public TakiWebService proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            proxy = (TakiWebService)Session["Proxy"];
            if (Request.QueryString["username"] != null)
            {
                string username = Request.QueryString["username"];
                try
                {
                    UserDetailsTable.user = proxy.GetUserDetails(username);
                }
                catch
                {
                    ErrorLbl.Text = "This User Doesn't Exist";
                }
            }
        }
    }
}