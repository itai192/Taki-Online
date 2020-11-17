using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if(Session["User"]==null)
            {
                Response.Redirect("~/Home.aspx");
            }
            User user = (User)Session["User"];
            F1.DataSource = user.AcceptedFriends;
            F2.DataSource = user.AcceptedFriends;
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            F1.DataBind();
            F2.DataBind();
        }

        protected void F1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}