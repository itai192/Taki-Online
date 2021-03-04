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
    //public interface IAbilityCard
    //{
    //    //to implement
    //}
    
    public class Reverse : Card
    {
        public Reverse(Color color) : base(color)
        {
        }

        public override bool CanPutOn(Card card)
        {
            return base.CanPutOn(card) || card is Reverse;
        }
        internal protected override void EndAbility(Game game)
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
        internal protected override void EndAbility(Game game)
        {
            game.NextTurn();
            base.EndAbility(game);
        }
    }
    public class NumberCard : Card, IGetCardText
    {
        const int MAXNUMBERCARD = 9;
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
        protected internal override Exception WhereCanPutCard()
        {
            return new Exception($"You can only put {this} on cards that are either {color.ToString("G")} or {value}");
        }
        public override string ToString()
        {
            return $"{color} {value}";
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
