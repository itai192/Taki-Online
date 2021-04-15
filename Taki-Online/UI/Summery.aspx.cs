using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.Custom_Controls;
using BLL;
namespace UI
{
    public partial class Summery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.Redirect("~/Home.aspx");
            User user = (User)Session["User"];
            try
            {
                if (!user.IsUserInGame(int.Parse(Request.QueryString["GameID"])))
                {
                    Response.Redirect("~/Home.aspx");
                }
            }
            catch
            {
                Response.Redirect("~/Home.aspx");
            }
            int gameID = int.Parse(Request.QueryString["GameID"]);
            UserStatsInGame stats = user.statsInGame(gameID);
            XpChangeLbl.Text = AddPlusIfPositive(stats.xpChanged);
            EloChangeLbl.Text = AddPlusIfPositive(stats.eloChanged);
            SummeryTbl.DataSource = BLL_Helper.FindUserStatsInGame(gameID);
            SummeryTbl.DataBind();
        }
        private string AddPlusIfPositive(int num)
        {
            if (num > 0)
            {
                return "+" + num;
            }
            return num.ToString();
        }

        protected void SummeryTbl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserCard uc = (UserCard)e.Row.FindControl("UserCard");
                User userToBind = new User(((UserStatsInGame)e.Row.DataItem).username);
                uc.user = userToBind;
                Label numberlbl = (Label)e.Row.FindControl("Numberlbl");
                numberlbl.Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }
}