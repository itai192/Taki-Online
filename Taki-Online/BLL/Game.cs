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
        private Color color;

        public virtual bool CanPutOn(Card card)//if you can put card, on this
        {
            if(card.color==Color.none)
            {
                return true;
            }
            return this.color == card.color;
        }
        public virtual void Play(Game game)
        { 
        }
    }
    public class Player
    {
        private List<Card> hand;
        
        public void Draw(Game game)
        {
            int drawAmount = 1 + game.penelty;
            game.penelty = 0;
            for (int i = 0; i < drawAmount; i++)
            {
                hand.Add(game.TakeCardFromDeck());
            }
        }
    }

}
