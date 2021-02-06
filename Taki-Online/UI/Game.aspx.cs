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
    public partial class Game : System.Web.UI.Page
    {
        BLL.Game game;
        protected void Page_Load(object sender, EventArgs e)
        {
            game = (BLL.Game)Application["Game"];
            Card.card = new BLL.NumberCard(Color.red, 4);
            Pile.card = game.leadingCard;
            Deck.card = new BLL.Reverse(Color.yellow);
            Card2.card = new BLL.NumberCard(Color.yellow, 9);
            Card.Click += TryUse;
            Card2.Click += TryUse;
            
        }
        protected void TryUse(object sender, CardEventArgs e)
        {
            if (e.card.CanBePutOn(game.leadingCard))
            {
                Statuslbl.Text = "Good";
                ((UI.Custom_Controls.Card)sender).PutCard();
            }
            else
                Statuslbl.Text = "Can't put that card";
        }

       
    }
}