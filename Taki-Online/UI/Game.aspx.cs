<<<<<<< Updated upstream
﻿using System;
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
            if(Session["Player"]==null)
            {
                Session["Player"] = game.AddPlayer();
                Response.Redirect("Game.aspx");
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
        }

        protected void Timer_Tick(object sender, EventArgs e)
        {
            DoUpdate();
        }
        public void DoUpdate()
        {
            if (player.HasUndoneBroadcasts())
            {
                BLL.Game.IPlayerBroadcast broadcast = player.NextBroadcast();
                player.DoBroadcast();
            }
            Pile.card = player.leadingCard;
            Pile.LoadCard();
        }
    }
=======
﻿using System;
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
            UpdatePanel1.Update();
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

        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {
            game = (BLL.Game)Application["Game"];
            Card.card = new BLL.NumberCard(Color.red, 4);
            Pile.card = game.leadingCard;
            Deck.card = new BLL.Reverse(Color.yellow);
            Card2.card = new BLL.NumberCard(Color.yellow, 9);
            Card.Click += TryUse;
            Card2.Click += TryUse;
            
        }
    }
>>>>>>> Stashed changes
}