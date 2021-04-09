using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Taki_AssosiationUI
{
    public partial class Sign_In : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebService.TakiWebService proxy = new WebService.TakiWebService();
            proxy.CookieContainer = new System.Net.CookieContainer();
        }
    }
}