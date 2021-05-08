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
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
namespace UI
{
    public class Global : System.Web.HttpApplication
    {
        public Timer dayTimer;
        public Timer SeasonTimer;
        public Season currentSeason;
        
        protected void Application_Start(object sender, EventArgs e)
        {
            
            Application["Users Online"] = 0;
            Application["Visitors Today"] = 0;
            Directory.SetCurrentDirectory(Server.MapPath("~"));
            string strong = Directory.GetCurrentDirectory();
            BLL.BLL_Helper.CreateDBHelperInDalHelper(ConfigurationManager.AppSettings["path"], ConfigurationManager.AppSettings["provider"]);
            dayTimer = new Timer();
            dayTimer.AutoReset = true;
            dayTimer.Interval = TimeSpan.FromDays(1).TotalMilliseconds;
            dayTimer.Start();
            dayTimer.Elapsed += ResetVisitorsToday;
            GC.KeepAlive(dayTimer);
            dayTimer.Start();
        }
        public void SetSeasonTimer()
        {
            if(SeasonTimer!=null)
            {
                SeasonTimer.Dispose();
                SeasonTimer = null;
            }
            currentSeason = BLL_Helper.GetCurrentSeason();
            TimeSpan timeToWait = currentSeason.EndDate-DateTime.Now;
            if (timeToWait.TotalMilliseconds > int.MaxValue)
            {
                SeasonTimer = new Timer(int.MaxValue);
                SeasonTimer.AutoReset = false;
                SeasonTimer.Elapsed += ResetTimer;
            }
            else
            {
                SeasonTimer = new Timer(timeToWait.TotalMilliseconds);
                SeasonTimer.AutoReset = false;
                SeasonTimer.Elapsed += StartNewSeason;
            }
        }
        public void StartNewSeason(object sender, ElapsedEventArgs e)
        {
            Season s = new Season(DateTime.Now, DateTime.Now.AddDays(60));
            List<User> users = BLL_Helper.GetAllUsers();
            foreach(User u in users)
            {
                u.SeasonStartRank(s.SeasonID, currentSeason.SeasonID);
            }
            currentSeason = s;
        }
        public void ResetTimer(object sender, ElapsedEventArgs e)
        {
            SetSeasonTimer();
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