using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
namespace BLL
{
    public interface IGetCardText
    {
        string GetCardText();
    }
    public enum Color
    {
        none,
        red,
        green,
        blue,
        yellow
    }
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
    public abstract class Card
    {
        public Color color { get; }
        public Card()
        {
            this.color=Color.none;
        }
        public Card(Color color)
        {
            this.color=color;
        }
        public virtual bool CanPutOn(Card card)//if you can put card, on this
        {
            if(card.color==Color.none)
            {
                return true;
            }
            return this.color == card.color;
        }
        public virtual bool CanBePutOn(Card card)
        {
            return card.CanPutOn(this);
        }
        public virtual void EndAbility(Game game)
        {
            game.ChangeActiveCard(null);
            game.NextTurn();
        }
        public virtual void StartAbility(Game game)
        {
            this.EndAbility(game);
        }
        public virtual void AddCardAbility(Game game, Card card)
        {
            
        }
    }
    public class Player
    {
        private List<Card> hand;
        private Queue<Action> actionsToDo;
        private Game game;
        public void DrawCards(Game game)
        {
            int drawAmount = 1 + game.penelty;
            game.penelty = 0;
            for (int i = 0; i < drawAmount; i++)
            {
                hand.Add(game.TakeCardFromDeck());
            }
        }
        public void putCard(int index)
        {
            
        }
    }
    public class Reverse:Card
    {
        public Reverse(Color color):base(color){
        }

        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card)||card is Reverse;
        }
        public override void EndAbility(Game game)
        {
            game.order=!game.order;
            base.EndAbility(game);
        }
    }
    public class Stop:Card
    {
        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card)||card is Stop;
        }
        public override void EndAbility(Game game)
        {
            game.NextTurn();
            base.EndAbility(game);
        }
    }
    public class NumberCard:Card, IGetCardText
    {
        public string GetCardText()
        {
            return value.ToString();
        }
        private int value;
        public NumberCard(Color color, int value):base(color)
        {
            this.value=value;
        }
        public override bool CanPutOn(Card card)
        {
            bool sameNum = (card is NumberCard) ? ((NumberCard)card).value==this.value:false;
            return sameNum||base.CanPutOn(card);
        }
    }
    public class PlusTwo:Card
    {
        //
    }
    public class Taki:Card
    {
        //
    }
    public class King:Card
    {
        //
    }
    public class ColorChanger:Card
    {
        //
    }
}
