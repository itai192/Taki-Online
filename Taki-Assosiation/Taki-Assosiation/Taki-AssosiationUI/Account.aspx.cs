using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taki_AssosiationUI.WebService;
namespace Taki_AssosiationUI
{
    public partial class Account : System.Web.UI.Page
    {
        public TakiWebService proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            proxy = (TakiWebService)Session["Proxy"];
            if(!proxy.IsSessionConnected())
            {
                Response.Redirect("~/Home.aspx");
            }
            UserDetailsTable.user = proxy.GetConnectedUser();
        }

    }
}