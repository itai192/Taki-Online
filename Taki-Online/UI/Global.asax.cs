using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BLL;
using System.Configuration;
using System.IO;
namespace UI
{
    public class Global : System.Web.HttpApplication
    {
        public BLL.Game game;
        protected void Application_Start(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(Server.MapPath("~"));
            string strong = Directory.GetCurrentDirectory();
            BLL.BLL_Helper.CreateDBHelperInDalHelper(ConfigurationManager.AppSettings["path"], ConfigurationManager.AppSettings["provider"]);
            BLL.Game game = new BLL.Game();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}