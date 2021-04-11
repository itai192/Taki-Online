using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// an interface that describes cards which you can get their text
    /// </summary>
    public interface IGetCardText
    {
        /// <summary>
        /// a method that returns the card's text
        /// </summary>
        string GetCardText();
    }
    /// <summary>
    /// a card class describing the change direction or reverse card.
    /// </summary>
    public class Reverse : Card
    {
        /// <summary>
        /// a constructor for the reverse class
        /// </summary>
        public Reverse(Color color) : base(color)
        {
        }
        /// <summary>
        /// a method which returns whether you can put a card on this reverse card
        /// </summary>
        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card) || card is Reverse;
        }
        /// <summary>
        /// the end ability of reverse card, which changes direction of the game
        /// </summary>
        internal protected override void EndAbility(Game game)
        {
            game.order = !game.order;
            base.EndAbility(game);
        }
    }
    /// <summary>
    /// a class describing the stop card
    /// </summary>
    public class Stop : Card
    {
        /// <summary>
        /// a method which returns whether you can put a card on this stop card
        /// </summary>
        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card) || card is Stop;
        }
        /// <summary>
        /// the end ability of stop card, which changes skips a player's turn
        /// </summary>
        internal protected override void EndAbility(Game game)
        {
            game.NextTurn();
            base.EndAbility(game);
        }
    }
    /// <summary>
    /// a class describing the number card
    /// </summary>
    public class NumberCard : Card, IGetCardText
    {
        //the maximum number of a number card
        const int MAXNUMBERCARD = 9;
        /// <summary>
        /// the get card text method, implements the IGetCardText
        /// </summary>
        public string GetCardText()
        {
            return value.ToString();
        }
        // the number on the card
        private int value;
        /// <summary>
        /// constructor for the number card
        /// </summary>
        public NumberCard(Color color, int value) : base(color)
        {
            this.value = value;
        }
        /// <summary>
        /// a method which returns whether you can put a card on this number card, same number or same color
        /// </summary>
        public override bool CanPutOn(Card card)
        {
            bool sameNum = (card is NumberCard) ? ((NumberCard)card).value == this.value : false;
            //same number or same color
            return sameNum || base.CanPutOn(card);
        }
        /// <summary>
        /// an exception describing where this card can be put in it's messege
        /// </summary>
        protected internal override Exception WhereCanPutCard()
        {
            return new Exception($"You can only put {this} on cards that are either {color.ToString("G")} or {value}");
        }
        /// <summary>
        /// to string method returns the color and value of the card
        /// </summary>
        public override string ToString()
        {
            return $"{color} {value}";
        }
    }
    //not implemented classes
    public class PlusTwo : Card
    {
        //
    }
    public class Taki : Card
    {
        //
    }
    public class King : Card
    {
        //
    }
    public class ColorChanger : Card
    {
        //
    }
}
