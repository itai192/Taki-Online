using System;
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
    /// <summary>
    /// player class, what's being used to interact with the game
    /// </summary>
    public class Player
    {
        //game id of assosiated game
        public int GameID
        {
            get
            {
                return game.gameRoom.GameID;
            }
        }
        //whether game has ended
        public bool GameEnded
        {
            get;
            internal set;
        }
        //user asosiated with a game
        public User user
        {
            get;
            private set;
        }
        //number of cards in player hand
        public int numberOfCards
        {
            get
            {
                return hand.Count;
            }
        }
        //the list of cards describing the hand
        internal List<Card> hand;
        //the leading card in the game, updates asyncronously to the game
        public Card leadingCard
        {
            get;
            internal set;
        }
        //the order of the game, updates asyncronously to the game
        public bool order
        {
            get;
            internal set;
        }
        //the turn of the game, updates asyncronously to the game
        public int turn
        {
            get;
            internal set;
        }
        //a list of all other players in order
        private List<SimplePlayer> _players;
        //a list of all other players in order
        public List<SimplePlayer> players
        {
            get
            {
                return _players;
            }
        }
        //a queue of broadcasts to do
        private Queue<Game.IPlayerBroadcast> broadcastsToDo;
        //the game assosiated with the game
        private Game game;
        /// <summary>
        /// finds and returns the simple player in the player list, equal to player
        /// </summary>
        private SimplePlayer FindSimplePlayer(SimplePlayer player)
        {
            foreach (SimplePlayer pl in players)
            {
                if (pl.Equals(player))
                    return pl;
            }
            return null;
        }
        /// <summary>
        /// gets the hand of the player
        /// </summary>
        public List<Card> GetHand()
        {
            return new List<Card>(hand);
        }
        /// <summary>
        /// adds a card to a simple player
        /// </summary>
        internal void AddACardToSimplePlayer(SimplePlayer player)
        {
            FindSimplePlayer(player).NumberOfCards++;
        }
        /// <summary>
        /// subtracts a card to a simple player
        /// </summary>
        internal void SubtractACardFromSimplePlayer(SimplePlayer player)
        {
            FindSimplePlayer(player).NumberOfCards--;
        }
        /// <summary>
        /// updates the simple player list
        /// </summary>
        internal void UpdatePlayerList(List<SimplePlayer> players)
        {
            this._players = players;
        }
        /// <summary>
        /// checks whether player has a card
        /// </summary>
        public bool HasCard(Card card)
        {
            return hand.Contains(card);
        }
        /// <summary>
        /// returns whether player has broadcasts to do
        /// </summary>
        public bool HasUndoneBroadcasts()
        {
            return broadcastsToDo.Count > 0;
        }
        /// <summary>
        /// returns the broadcast that will execute next
        /// </summary>
        public Game.IPlayerBroadcast NextBroadcast()
        {
            if (HasUndoneBroadcasts())
                return broadcastsToDo.Peek();
            return null;
        }
        /// <summary>
        /// does broadcast and removes it from broadcast queue
        /// </summary>
        public void DoBroadcast()
        {
            if (HasUndoneBroadcasts())
            {
                Game.IPlayerBroadcast broadcastToDo = broadcastsToDo.Dequeue();
                broadcastToDo.DoBroadcast(this);
            }
        }
        /// <summary>
        /// Tries to take a card
        /// </summary>
        internal Card TakeCard(Card card)
        {
            if (hand.Remove(card))
            {
                return card;
            }
            return null;
        }
        /// <summary>
        /// adds a card to player's hand
        /// </summary>
        internal void AddCardToHand(Card c)
        {
            this.hand.Add(c);
        }
        /// <summary>
        /// adds a broadcast to broadcast queue
        /// </summary>
        internal void AddBroadcast(Game.IPlayerBroadcast broadcast)
        {
            broadcastsToDo.Enqueue(broadcast);
        }
        /// <summary>
        /// create a simple player based of this player
        /// </summary>
        public SimplePlayer ToSimplePlayer()
        {
            return new SimplePlayer(this, user);
        }
        /// <summary>
        /// player constructor
        /// </summary>
        internal Player(Game game, User user)
        {
            this.user = user;
            this.game = game;
            broadcastsToDo = new Queue<Game.IPlayerBroadcast>();
            hand = new List<Card>();
            leadingCard = game.leadingCard;
            order = game.order;
        }
        /// <summary>
        /// tries to draw cards from deck
        /// </summary>
        public void DrawCards()
        {
            game.TryDoAction(new Action(ActionType.DrawCard, null, this));
        }
        /// <summary>
        /// tries to put card
        /// </summary>
        public void putCard(Card card)
        {
            game.TryDoAction(new Action(ActionType.putCard, card, this));
        }
        /// <summary>
        /// invites another user to the game
        /// </summary>
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
        /// <summary>
        /// equals method, checks if users are equal
        /// </summary>
        public override bool Equals(object obj)
        {
            if(obj is Player)
            {
                Player other = (Player)obj;
                return other.user.Equals(this.user);
            }
            return false;
        }
    }
    /// <summary>
    /// a simpler player class, to elcapsulate player
    /// </summary>
    public class SimplePlayer
    {
        //the player object assosiated with this simple player
        private Player player;
        //user assosiated with simple player;
        public User user
        {
            get;
            private set;
        }
        //checks whether the player is described in this simple player
        public bool IsPlayer(Player player)
        {
            return this.player.Equals(player);
        }
        /// <summary>
        /// simple player constructor
        /// </summary>
        public SimplePlayer(Player p, User user)
        {
            this.user = user;
            player = p;
            NumberOfCards = p.numberOfCards;
        }
        /// <summary>
        /// equals override, checks if players are equal
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is SimplePlayer)
            {
                SimplePlayer other = (SimplePlayer)obj;
                return other.player.Equals(this.player);
            }
            return false;
        }
        /// <summary>
        /// the number of cards the simple player has
        /// </summary>
        public int NumberOfCards
        {
            get;
            set;
        }
    }
}
