using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI
{
    public partial class Master_Page : System.Web.UI.MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            TableRow up = Table1.Rows[0];
            if(Session["User"] is User)
            {

            }
            else
            {

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected TableCell CreateLinkCell(string link,string text)
        {
            TableCell cell = new TableCell();
            LinkButton btn = new LinkButton();
            btn.PostBackUrl = link;
            btn.Text = text;
            cell.Controls.Add(btn);
            return cell;
        }
    }
}