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
            proxy = (TakiWebService)Session["Proxy"];
            if (proxy.IsSessionConnected())
            {
                Table1.Rows.Add(CreateLinkRow(@"..\Leaderboard.aspx", "Leaderboard"));
                Table1.Rows.Add(CreateButtonRow(LogOut, "Log Out"));
                Table1.Rows.Add(CreateLinkRow(@"..\Account.aspx", "Account"));
            }
            else
            {
                Table1.Rows.Add(CreateLinkRow(@"..\SignUp.aspx", "Sign up"));
                Table1.Rows.Add(CreateLinkRow(@"..\SignIn.aspx", "Sign in"));
            }
        }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected TableRow CreateLinkRow(string link, string text)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            Button btn = new Button();
            btn.PostBackUrl = link;
            btn.Text = text;
            btn.CssClass = "sideButton";
            cell.Controls.Add(btn);
            row.Cells.Add(cell);
            return row;
        }
        protected TableRow CreateButtonRow(EventHandler cmd, string text)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            Button btn = new Button();
            btn.Text = text;
            btn.CssClass = "sideButton";
            btn.Click += cmd;
            cell.Controls.Add(btn);
            row.Cells.Add(cell);
            return row;
        }
        protected void LogOut(object sender, EventArgs e)
        {
            proxy.CookieContainer=new System.Net.CookieContainer();
            Response.Redirect(@"~\Home.aspx");
        }
    }
}