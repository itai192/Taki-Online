using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taki_AssosiationUI.WebService;
namespace Taki_AssosiationUI
{
    public partial class Leaderboard : System.Web.UI.Page
    {
        public TakiWebService proxy;

        protected void Page_Load(object sender, EventArgs e)
        {
            proxy = (TakiWebService)Session["Proxy"];
            WSRank[] ranks = proxy.GetAllRanks();
            if (!IsPostBack)
            {
                LeaderboardGrid.DataSource = proxy.GetAllUsersInRank(ranks[0]);
                LeaderboardGrid.DataBind();
                RankDropDown.DataSource = proxy.GetAllRanks();
                RankDropDown.DataBind();
            }
        }

        protected void Leaderboard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                Response.Redirect($"~/UserDetails.aspx?username={e.CommandArgument}");
            }
        }

        protected void RankDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            WSRank rank = new WSRank();
            rank.ID = int.Parse(RankDropDown.SelectedValue);
            LeaderboardGrid.DataSource = proxy.GetAllUsersInRank(rank);
            LeaderboardGrid.DataBind();
        }
    }
}