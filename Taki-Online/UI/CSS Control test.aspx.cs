using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace UI
{
    public partial class CSS_Control_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.card = new BLL.NumberCard(Color.red, 4);
        }
        
    }
}