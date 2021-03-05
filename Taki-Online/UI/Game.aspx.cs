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
            Pile.card = player.leadingCard;
            Deck.card =null;
            Deck.IsButton = true;
            Deck.Click += TryDraw;
            Hand.DataSource = player.GetHand();
            Hand.DataBind();
        }
        
        protected void TryDraw(object sender, CardEventArgs e)
        {
            try
            {
                player.DrawCards();
            }
            catch(Exception ex)
            {
                Statuslbl.Text = ex.Message;
            }
            
        }
        protected void TryUse(object sender, CardEventArgs e)
        {
            try
            {
                player.putCard(e.card);
                ((UI.Custom_Controls.Card)sender).PutCard();
            }
            catch(Exception ex)
            {
                Statuslbl.Text = ex.Message;
            }
            
        }

        protected void Hand_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            UI.Custom_Controls.Card card = (UI.Custom_Controls.Card) e.Item.FindControl("Card");
            card.Click += TryUse;
            card.card = (BLL.Card)e.Item.DataItem;
        }

        protected void Timer_Tick(object sender, EventArgs e)
        {
            if (player.HasUndoneBroadcasts())
            {
                BLL.Game.IPlayerBroadcast broadcast = player.NextBroadcast();
                player.DoBroadcast();
            }
            Pile.card = player.leadingCard;
        }
    }
}