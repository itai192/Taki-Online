using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public class Action
    {
        public ActionType type { get; }
        public Card card { get; }
        public Player player { get; }
        public Action(ActionType type, Card card, Player player)
        {
            this.type = type;
            this.card = card;
            this.player = player;
        }
    }
}
