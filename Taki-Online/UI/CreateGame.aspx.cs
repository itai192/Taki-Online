using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI
{
    public partial class CreateGame : System.Web.UI.Page
    {
        public User user; 
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Home.aspx");
            }
            user = (User)Session["User"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateGameBtn_Click(object sender, EventArgs e)
        {
            try
            {
                GameRoom gr = new GameRoom(user, GameNameTxt.Text);
                Response.Redirect($"~/Room.aspx?gameId={gr.GameID}");
            }
            catch(Exception ex)
            {
                ErorLbl.Text = ex.Message;
            }
        }
    }
}