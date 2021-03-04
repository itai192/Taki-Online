using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Game.cs")]
namespace BLL
{


    public abstract class Card
    {
        public Color color { get; protected set; }
        public Card()
        {
            this.color = Color.none;
        }
        public Card(Color color)
        {
            this.color = color;
        }
        public virtual bool CanPutOn(Card card)//if you can put card, on this
        {
            if (card.color == Color.none)
            {
                return true;
            }
            return this.color == card.color;
        }
        public virtual bool CanBePutOn(Card card)
        {
            return card.CanPutOn(this);
        }
        internal protected virtual void EndAbility(Game game)
        {
            game.ClearActiveCard();
            game.NextTurn();
        }
        internal protected virtual void StartAbility(Game game)
        {
            this.EndAbility(game);
        }
        internal protected virtual Exception WhereCanPutCard()
        {
            return new Exception($"You can put {this} only on Cards of the color {color.ToString("G")}");
        }
        internal protected virtual void ProcessPlayerAction(Game game, Action a)
        {
            
        }
    }
}
