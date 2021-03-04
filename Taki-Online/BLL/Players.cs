using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
//[assembly: InternalsVisibleTo("Card.cs")]
//[assembly: InternalsVisibleTo("Cards.cs")]
[assembly: InternalsVisibleTo("Broadcasts.cs")]
namespace BLL
{
    public class Player
    {
        //private User user;
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
        private List<SimplePlayer> players;
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
            FindSimplePlayer(player).NumberOfCards++;
        }
        internal void UpdatePlayerList(List<SimplePlayer> players)
        {
            this.players = players;
        }
        public bool HasCard(Card card)
        {
            return hand.Contains(card);
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
            return new SimplePlayer(this);
        }
        internal Player(Game game)
        {
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
    }
    public class SimplePlayer
    {
        private Player player;
        public bool IsPlayer(Player player)
        {
            return this.player == player;
        }
        public SimplePlayer(Player p)
        {
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
