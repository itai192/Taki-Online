using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
namespace BLL
{
    
    public enum ActionType
    {
        putCard,
        DrawCard,
        endAction
    }
    public class Action
    {
        public ActionType type { get; }
        public Card card { get; }
        public Player player { get; }
        public Action()
        { }
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
        private int turn;//turn
        private Card activeCard;
        public Card leadingCard
        {
            get { return pile.Peek(); }//temporery
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
        public void TryDoAction()
        {
            //ToImplement
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
        public void putCard(int index)
        {
            
        }
    }
    
}
