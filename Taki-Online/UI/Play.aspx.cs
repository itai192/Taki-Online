using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI
{
    public partial class Play : System.Web.UI.Page
    {
        public string searchTerm;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewState["SearchTerm"] = string.Empty;
            }
            searchTerm = ViewState["SearchTerm"].ToString();
        }
        protected void Page_LoadComplete()
        {
            Games.DataSource = BLL_Helper.SearchStartingGameRoom(searchTerm);
            Games.DataBind();
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            ViewState["SearchTerm"] = SearchText.Text;
            searchTerm = SearchText.Text;
        }

        protected void Games_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Games.PageIndex = e.NewPageIndex;
        }

        protected void Games_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Join")
            {
                GameRoom gr = (GameRoom)Games.Rows[(int)e.CommandArgument].DataItem;
            }
        }
    }
}