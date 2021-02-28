using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Game
    {
        public abstract class Card
        {
            public Color color { get; private protected set; }
            public Card()
            {
                this.color = Color.none;
            }
            public Card(Color color)
            {
                this.color = color;
            }
            private protected virtual bool CanPutOn(Card card)//if you can put card, on this
            {
                if (card.color == Color.none)
                {
                    return true;
                }
                return this.color == card.color;
            }
            private protected virtual bool CanBePutOn(Card card)
            {
                return card.CanPutOn(this);
            }
            private protected virtual void EndAbility(Game game)
            {
                game.ChangeActiveCard(null);
                game.NextTurn();
            }
            private protected void StartAbility(Game game)
            {
                this.EndAbility(game);
            }
            private protected virtual void ProcessPlayerAction(Game game, Action a)
            {
                
            }
        }
    }
    
}
