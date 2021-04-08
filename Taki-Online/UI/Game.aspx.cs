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
        public bool gameEnded;
        public BLL.Game game;
        public BLL.Player player;
        public User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"]==null)
            {
                Response.Redirect("~/Home.aspx");
            }
            user = (User)Session["User"];
            game = (BLL.Game)Application["Game"];
            player = (BLL.Player)Session["Player"];
            if (player.GameEnded)
            {
                Session["Player"] = null;
                Response.Redirect($"~/Summery.aspx?gameId={player.GameID}");
            }
            Pile.card = player.leadingCard;
            Deck.card =null;
            Deck.IsButton = true;
            Deck.Click += TryDraw;
            Hand.DataSource = player.GetHand();
            Hand.DataBind();
            Users.DataSource = player.players;
            Users.DataBind();
        }
        
        protected void TryDraw(object sender, CardEventArgs e)
        {
            try
            {
                player.DrawCards();
                DoUpdate();
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
                DoUpdate();
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
            card.LoadCard();
        }

        protected void Timer_Tick(object sender, EventArgs e)
        {
            DoUpdate();
        }
        public void DoUpdate()
        {
            gameEnded = false;
            if (player.HasUndoneBroadcasts())
            {
                BLL.Game.IPlayerBroadcast broadcast = player.NextBroadcast();
                player.DoBroadcast();
            }
            if(player.GameEnded)
            {
                gameEnded = true;
            }
            Pile.card = player.leadingCard;
            Pile.LoadCard();
            Users.DataSource = player.players;
            Users.DataBind();
        }

        protected void Users_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item|| e.Item.ItemType ==ListItemType.AlternatingItem)
            {
                SimplePlayer toBind = (SimplePlayer)e.Item.DataItem;
                PlayerCard uc = (PlayerCard)e.Item.FindControl("PlayerCard");
                uc.isSelected = false;
                if (player.players[player.turn].Equals(toBind))
                {
                    uc.isSelected = true;
                }
                uc.UpdateUser();
            }
        }
    }
}