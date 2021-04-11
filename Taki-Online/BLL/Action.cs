using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public class Action
    {
        //the action type
        public ActionType type { get; }
        //card assosiated with the action
        public Card card { get; }
        //Player who did the action
        public Player player { get; }
        /// <summary>
        /// action constructor
        /// </summary>
        public Action(ActionType type, Card card, Player player)
        {
            this.type = type;
            this.card = card;
            this.player = player;
        }
    }
}
