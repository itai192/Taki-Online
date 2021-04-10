using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taki_AssosiationUI.WebService;
namespace Taki_AssosiationUI
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Proxy"]==null)
            {
                TakiWebService proxy = new TakiWebService();
                proxy.CookieContainer = new System.Net.CookieContainer();
                Session["Proxy"] = proxy;
            }
        }
    }
}