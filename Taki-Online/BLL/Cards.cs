using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    
    public class Reverse : Card
    {
        public Reverse(Color color) : base(color)
        {
        }

        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card) || card is Reverse;
        }
        public override void EndAbility(Game game)
        {
            game.order = !game.order;
            base.EndAbility(game);
        }
    }
    public class Stop : Card
    {
        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card) || card is Stop;
        }
        public override void EndAbility(Game game)
        {
            game.NextTurn();
            base.EndAbility(game);
        }
    }
    public class NumberCard : Card, IGetCardText
    {
        public string GetCardText()
        {
            return value.ToString();
        }
        private int value;
        public NumberCard(Color color, int value) : base(color)
        {
            this.value = value;
        }
        public override bool CanPutOn(Card card)
        {
            bool sameNum = (card is NumberCard) ? ((NumberCard)card).value == this.value : false;
            return sameNum || base.CanPutOn(card);
        }
    }
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
