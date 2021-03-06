﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
namespace UI
{
    public partial class Ladders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                RanksDropDown.DataSource = BLL_Helper.GetAllRanks();
                RanksDropDown.DataBind();
            }
            Leaderboard.DataSource = BLL_Helper.GetAllPlayersInRankThisSeasonDataTable(int.Parse(RanksDropDown.SelectedValue));
            Leaderboard.DataBind();
        }

        protected void Leaderboard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}