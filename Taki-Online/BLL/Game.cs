using System;
using System.Collections.Generic;
using System.Collections;
namespace BLL
{
    public enum Color
    {
        none,
        red,
        green,
        blue,
        yellow
    }
    public class Game
    {
        private Stack<Card> deck;
        private Stack<Card> pile;
        private List<Player> players;
        private int turn;//turn
        public bool order { get; set; }//positive or negative relative to the order
        public int penelty {get; set;}//extra cards when you draw
        private bool isOpenTaki;
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
        private void Reshuffle()
        {

        }

    }
    public abstract class Card
    {
        protected Color color;
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
        public virtual void Play(Game game)
        { 
        }
    }
    public class Player
    {
        private List<Card> hand;
        
        public void DrawCards(Game game)
        {
            int drawAmount = 1 + game.penelty;
            game.penelty = 0;
            for (int i = 0; i < drawAmount; i++)
            {
                hand.Add(game.TakeCardFromDeck());
            }
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
        public override void Play(Game game)
        {
            game.order=!game.order;
        }
    }
    public class NumberCard:Card
    {
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
        
    }
    public class Taki:Card
    {

    }

}
