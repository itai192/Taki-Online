using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class Game
    {
        private class GamePlayer : Player
        {
            public GamePlayer(Game game) : base(game)
            {
            }
            public bool HasCard(Card card)
            {
                return hand.Contains(card);
            }
            public Card TakeCard(Card card)
            {
                if(hand.Remove(card))
                {
                    return card;
                }
                return null;
            }
            public void AddCardToHand(Card c)
            {
                this.hand.Add(c);
            }
            public void AddAction(Action action)
            {
                actionsToDo.Enqueue(action);
            }
        }
    }
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
        protected List<Card> hand;
        protected Queue<Action> actionsToDo;
        protected Game game;
        public Player(Game game)
        {
            this.game = game;
            actionsToDo = new Queue<Action>();
            hand = new List<Card>();
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
