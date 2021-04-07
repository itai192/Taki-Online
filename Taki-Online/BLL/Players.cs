﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using DAL;
//[assembly: InternalsVisibleTo("Card.cs")]
//[assembly: InternalsVisibleTo("Cards.cs")]
//[assembly: InternalsVisibleTo("Broadcasts.cs")]
namespace BLL
{
    public class Player
    {
        public bool GameEnded
        {
            get;
            internal set;
        }
        public User user
        {
            get;
            private set;
        }
        public int numberOfCards
        {
            get
            {
                return hand.Count;
            }
        }
        internal List<Card> hand;
        public Card leadingCard
        {
            get;
            internal set;
        }
        public bool order
        {
            get;
            internal set;
        }
        public int turn
        {
            get;
            internal set;
        }
        private List<SimplePlayer> _players;
        public List<SimplePlayer> players
        {
            get
            {
                return _players;
            }
        }
        private Queue<Game.IPlayerBroadcast> broadcastsToDo;
        private Game game;
        private SimplePlayer FindSimplePlayer(SimplePlayer player)
        {
            foreach (SimplePlayer pl in players)
            {
                if (pl.Equals(player))
                    return pl;
            }
            return null;
        }
        public List<Card> GetHand()
        {
            return new List<Card>(hand);
        }
        internal void AddACardToSimplePlayer(SimplePlayer player)
        {
            FindSimplePlayer(player).NumberOfCards++;
        }
        internal void SubtractACardFromSimplePlayer(SimplePlayer player)
        {
            FindSimplePlayer(player).NumberOfCards--;
        }
        internal void UpdatePlayerList(List<SimplePlayer> players)
        {
            this._players = players;
        }
        public bool HasCard(Card card)
        {
            return hand.Contains(card);
        }
        public bool HasUndoneBroadcasts()
        {
            return broadcastsToDo.Count > 0;
        }
        public Game.IPlayerBroadcast NextBroadcast()
        {
            if (HasUndoneBroadcasts())
                return broadcastsToDo.Peek();
            return null;
        }
        public void DoBroadcast()
        {
            if (HasUndoneBroadcasts())
            {
                Game.IPlayerBroadcast broadcastToDo = broadcastsToDo.Dequeue();
                broadcastToDo.DoBroadcast(this);
            }
        }
        internal Card TakeCard(Card card)
        {
            if (hand.Remove(card))
            {
                return card;
            }
            return null;
        }
        internal void AddCardToHand(Card c)
        {
            this.hand.Add(c);
        }
        internal void AddBroadcast(Game.IPlayerBroadcast broadcast)
        {
            broadcastsToDo.Enqueue(broadcast);
        }
        public SimplePlayer ToSimplePlayer()
        {
            return new SimplePlayer(this, user);
        }
        internal Player(Game game, User user)
        {
            this.user = user;
            this.game = game;
            broadcastsToDo = new Queue<Game.IPlayerBroadcast>();
            hand = new List<Card>();
            leadingCard = game.leadingCard;
            order = game.order;
        }
        public void DrawCards()
        {
            game.TryDoAction(new Action(ActionType.DrawCard, null, this));
        }
        public void putCard(Card card)
        {
            game.TryDoAction(new Action(ActionType.putCard, card, this));
        }
        public void invitePlayer(string recipiant)
        {
            try
            {
                GameInvitesDal.CreateGameInvite(user.username, recipiant, game.gameRoom.GameID);
            }
            catch(Exception e)
            {
                throw new Exception("Couldn't invite this user", e);
            }
        }
    }
    public class SimplePlayer
    {
        private Player player;
        public User user
        {
            get;
            private set;
        }
        public bool IsPlayer(Player player)
        {
            return this.player == player;
        }
        public SimplePlayer(Player p, User user)
        {
            this.user = user;
            player = p;
            NumberOfCards = p.numberOfCards;
        }
        public override bool Equals(object obj)
        {
            if (obj is SimplePlayer)
            {
                SimplePlayer other = (SimplePlayer)obj;
                return other.player == this.player;
            }
            return false;
        }
        public int NumberOfCards
        {
            get;
            set;
        }
    }
}
