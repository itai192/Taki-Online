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
        BLL.Player player;
        protected void Page_Load(object sender, EventArgs e)
        {
            game = (BLL.Game)Application["Game"];
            if(!IsPostBack)
            {
                Session["Player"] = game.AddPlayer();
            }
            player = (BLL.Player)Session["Player"];
            Card.card = new BLL.NumberCard(Color.red, 4);
            Pile.card = player.leadingCard;
            Deck.card =null;
            Deck.IsButton = true;
            Deck.Click += TryTake;
            Card2.card = new BLL.NumberCard(Color.yellow, 9);
            Card.Click += TryUse;
            Card2.Click += TryUse;
        }
        protected void TryTake(object sender, CardEventArgs e)
        {

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