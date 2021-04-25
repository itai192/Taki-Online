using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BLL;
using System.Configuration;
using System.IO;
using System.Timers;
namespace UI
{
    public class Global : System.Web.HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {
            
            Application["Users Online"] = 0;
            Application["Visitors Today"] = 0;
            Directory.SetCurrentDirectory(Server.MapPath("~"));
            string strong = Directory.GetCurrentDirectory();
            BLL.BLL_Helper.CreateDBHelperInDalHelper(ConfigurationManager.AppSettings["path"], ConfigurationManager.AppSettings["provider"]);
            Timer dayTimer = new Timer();
            dayTimer.AutoReset = true;
            GC.KeepAlive(dayTimer);
            dayTimer.Start();
            dayTimer.Elapsed += ResetVisitorsToday;
            dayTimer.Start();
        }
        public void ResetVisitorsToday(object sender, ElapsedEventArgs e)
        {
            Application["Visitors Today"] = 0;
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["Users Online"] =(int)Application["Users Online"] + 1;
            Application["Visitors Today"] = (int)Application["Visitors Today"] + 1;
            Application.UnLock();
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
            Application.Lock();
            Application["Users Online"] = (int)Application["Users Online"] - 1;
            Application.UnLock();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}