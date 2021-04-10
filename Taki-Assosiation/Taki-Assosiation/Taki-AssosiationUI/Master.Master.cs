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
        TakiWebService proxy;
        delegate void command();
        protected void Page_Init(object sender, EventArgs e)
        {
            if(Session["Proxy"]==null)
            {
                proxy = new TakiWebService();
                proxy.CookieContainer = new System.Net.CookieContainer();
                Session["Proxy"] = proxy;
            }
            proxy= (TakiWebService)Session["Proxy"];
            TableRow up = Table1.Rows[0];
            if (proxy.IsSessionConnected())
            {
                up.Cells.Add(CreateLinkCell(@"..\Leaderboard.aspx", "Leaderboard"));
                up.Cells.Add(CreateButtonCell(LogOut, "Log Out"));
                up.Cells.Add(CreateLinkCell(@"..\Account.aspx", "Account"));
            }
            else
            {
                up.Cells.Add(CreateLinkCell(@"..\SignUp.aspx", "Sign up"));
                up.Cells.Add(CreateLinkCell(@"..\SignIn.aspx", "Sign in"));
            }
        }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected TableCell CreateLinkCell(string link, string text)
        {
            TableCell cell = new TableCell();
            Button btn = new Button();
            btn.PostBackUrl = link;
            btn.Text = text;
            cell.Controls.Add(btn);
            return cell;
        }
        protected TableCell CreateButtonCell(EventHandler cmd, string text)
        {
            TableCell cell = new TableCell();
            Button btn = new Button();
            btn.Text = text;
            btn.Click += cmd;
            cell.Controls.Add(btn);
            return cell;
        }
        protected void LogOut(object sender, EventArgs e)
        {
            proxy.CookieContainer=new System.Net.CookieContainer();
            Response.Redirect(@"~\Home.aspx");
        }
    }
}