using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
namespace BLL
{
    
    public enum ActionType
    {
        putCard,
        DrawCard
    }
    public class Action
    {
        public ActionType type { get; }
        public Card card { get; }
        public Player player { get; }
        public Action(ActionType type, Card card, Player player)
        {
            this.type = type;
            this.card = card;
            this.player = player;
        }
    }
    public class Game
    {
        private Stack<Card> deck;
        private Stack<Card> pile;
        private List<Player> players;
        public int turn { get; private set; }
        private Card activeCard;
        public Card leadingCard
        {
            get { return pile.Peek(); }//temporery
        }
        public Player GetPlayerTurn()
        {
            return players[turn];
        }
        public bool order { get; set; }//positive or negative relative to the order
        public int penelty {get; set;}//extra cards when you draw
        public void ChangeActiveCard(Card card)
        {
            activeCard = card;
            if(card!=null)
            {
                activeCard.StartAbility(this);
            }
        }
        public Game()
        {
            order = true;
            this.players= new List<Player>();
            activeCard = null;
            turn = 0;
            pile = new Stack<Card>();
            foreach(Color c in Enum.GetValues(typeof(Color)))
            {
                if(c!=Color.none)
                {
                    for(int i = 1;i<=9;i++)
                    {
                        Card card = new NumberCard(c, i);

                    }
                }
            }
            pile.Push(new BLL.NumberCard(Color.yellow, 5));//temporery
        }
        public Card TakeCardFromDeck()
        {
            if(deck.Count==0)
            {
                Reshuffle();
            }
            return deck.Pop();
        }
        public void NextTurn()
        {
            turn = (order ? (turn + 1) : (turn - 1 + players.Count)) % players.Count;
        }
        private void BroadcastAction(Action action)
        {
            Action lessInfoAction=null;//for actions which players don't need to know about
            if(action.type==ActionType.DrawCard)
            {
                lessInfoAction = new Action(action.type, null, action.player);
            }
            foreach(Player p in players)
            {
                if (action.type == ActionType.DrawCard && p!= action.player)
                    p.AddAction(lessInfoAction);
                else
                {
                    p.AddAction(action);
                }
            }
        }
        public void TryDoAction(Action a)
        {
            
        }
        private void Reshuffle()
        {
            Card top = pile.Pop();
            List<Card> list = new List<Card>();
            
            while(pile.Count!=0)
            {
                list.Add(pile.Pop());
            }

            foreach(Card c in list)
            {
                deck.Push(c);
            }
            ShuffleDeck();
        }
        private void ShuffleDeck()
        {
            List<Card> l = new List<Card>();
            while(deck.Count !=0)
            {
                l.Add(deck.Pop());
            }
            Random random = new Random();
            while (l.Count!=0)
            {
                int num = random.Next(l.Count);
                deck.Push(l[num]);
                l.RemoveAt(num);
            }
        }
        public void AddPlayer()
        {

        }
    }
    public class Player
    {
        private List<Card> hand;
        private Queue<Action> actionsToDo;
        private Game game;
        //public void DrawCards(Game game)
        //{
        //    int drawAmount = 1 + game.penelty;
        //    game.penelty = 0;
        //    for (int i = 0; i < drawAmount; i++)
        //    {
        //        hand.Add(game.TakeCardFromDeck());
        //    }
        //}
        public void AddAction(Action action)
        {
            actionsToDo.Enqueue(action);
        }
        public void putCard()
        {
            
        }
    }
    
}
