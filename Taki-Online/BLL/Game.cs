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
        private int penelty;
        private bool isOpenTaki;

    }
    public abstract class Card
    {
        private Color color;

        public abstract bool CanPutOn(Card card);
        public abstract 
    }
    public class Player
    {
        private List<Card> hand;
        public void Draw(Game game)
        {

        }
    }
}
