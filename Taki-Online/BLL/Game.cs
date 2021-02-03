﻿using System;
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
    public class Game
    {
        private Stack<Card> deck;
        private Stack<Card> pile;
        private List<Player> players;
        private int turn;//turn
        public bool order { get; set; }//positive or negative relative to the order
        public int penelty {get; set;}//extra cards when you draw
        
        public Game(List<Player> players)
        {
            this.players= new List<Player>(players);
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
        public void PrevTurn()
        {
            turn = (!order ? (turn + 1) : (turn - 1 + players.Count)) % players.Count;
        }
        private void Reshuffle()
        {
            //to implement
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
        public virtual void Ability(Game game)
        {
        }
    }
    public abstract class Player
    {
        private List<Card> hand;
        
        /*public void DrawCards(Game game)
        {
            int drawAmount = 1 + game.penelty;
            game.penelty = 0;
            for (int i = 0; i < drawAmount; i++)
            {
                hand.Add(game.TakeCardFromDeck());
            }
        }*/
        public abstract Card DoTurn(Game game, String nullCardText);
    }
    public class Reverse:Card
    {
        public Reverse(Color color):base(color){
        }

        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card)||card is Reverse;
        }
        public override void Ability(Game game)
        {
            game.order=!game.order;
        }
    }
    public class Stop:Card
    {
        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card)||card is Stop;
        }
        public override void Ability(Game game)
        {
            game.NextTurn();
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
        
    }
    public class King:Card
    {
    }
    public class ColorChanger:Card
    {
        //
    }
}
