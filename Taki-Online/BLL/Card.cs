using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Game.cs")]
namespace BLL
{

    /// <summary>
    /// an abstract class describing a taki card
    /// </summary>
    public abstract class Card
    {
        //the card's color
        public Color color { get; protected set; }
        /// <summary>
        /// an empty constructor for a card,
        /// for inheritence purposes
        /// </summary>
        public Card()
        {
            this.color = Color.none;
        }
        /// <summary>
        /// an empty constructor for a card which takes the color of the card,
        /// for inheritence purposes
        /// </summary>
        public Card(Color color)
        {
            this.color = color;
        }
        /// <summary>
        /// a method which checks if a card can be put on this card
        /// </summary>
        public virtual bool CanPutOn(Card card)
        {
            if (card.color == Color.none)
            {
                return true;
            }
            return this.color == card.color;
        }
        /// <summary>
        /// a method which checks if this card can be put on a card
        /// </summary>
        public virtual bool CanBePutOn(Card card)
        {
            return card.CanPutOn(this);
        }
        /// <summary>
        /// a virtual method that describes the end ablity of a card
        /// </summary>
        internal protected virtual void EndAbility(Game game)
        {
            game.ClearActiveCard();
            game.NextTurn();
        }
        /// <summary>
        /// a virtual method that describes the start ablity of a card
        /// </summary>
        internal protected virtual void StartAbility(Game game)
        {
            this.EndAbility(game);
        }
        /// <summary>
        /// an exception describing where this card can be put
        /// </summary>
        internal protected virtual Exception WhereCanPutCard()
        {
            return new Exception($"You can put {this} only on Cards of the color {color.ToString("G")}");
        }
        /// <summary>
        /// a method which processes player action
        /// </summary>
        internal protected virtual void ProcessPlayerAction(Game game, Action a)
        {
            
        }
    }
}
