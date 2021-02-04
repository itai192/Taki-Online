using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using UI.Custom_Controls;

namespace UI
{
    public partial class CSS_Control_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.card = new BLL.NumberCard(Color.red, 4);
            Pile.card = new BLL.NumberCard(Color.yellow,5);
            Deck.card = new BLL.Reverse(Color.yellow);
            Card.Click += Use;
        }
        protected void Use(object sender, CardEventArgs e)
        {
            ((UI.Custom_Controls.Card)sender).PutCard();
        }
        
    }
}