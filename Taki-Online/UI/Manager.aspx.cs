using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI
{
    public partial class Manager : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Home.aspx");
            }
            User user = (User)Session["User"];
            if (user.type != UserType.Manager)
            {
                Response.Redirect("~/Home.aspx");
            }
            OnilneLbl.Text = Application["Users Online"].ToString();
            VisitorsTodayLbl.Text = Application["Visitors Today"].ToString();
            UserAmount.Text = BLL_Helper.GetUserAmount().ToString();
            List<int> games = BLL_Helper.GamesPlayedInMonth();
            for(int i = 0; i<games.Count;i++)
            {
                GamesInDates.Series[0].Points.AddXY(DateTime.Today.AddDays(-i), games[i]);
            }
            List<Rank> ranks = BLL_Helper.GetAllRanks();
            foreach(Rank rank in ranks)
            {
                int amount = BLL_Helper.GetAllUsersInRankThisSeason(rank.ID).Count;
                LeagueDistrobution.Series[0].Points.AddXY(rank.name, amount);
            }
        }
        
    }
}