using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class Game
    {

        public interface IPlayerBroadcast
        {
            void DoBroadcast(Player player);
        }
        public class ChangeTurnBroadcast : IPlayerBroadcast
        {
            private int turn;
            public void DoBroadcast(Player player)
            {
                player.turn = turn;
            }
            public ChangeTurnBroadcast(int turn)
            {
                this.turn = turn;
            }
        }
        public class ChangeOrderBroadcast : IPlayerBroadcast
        {
            private bool order;
            public ChangeOrderBroadcast(bool order)
            {
                this.order = order;
            }
            public void DoBroadcast(Player player)
            { 
                player.order = order;
            }
        }
        public class ActionBroadcast : IPlayerBroadcast
        {
            public Action action;
            public SimplePlayer player
            {
                get { return action.player.ToSimplePlayer(); }
            }
            public ActionType type
            {
                get { return action.type; }
            }
            public ActionBroadcast(Action action)
            {
                this.action = action;
            }
            public void DoBroadcast(Player player)
            {
                if(action.type==ActionType.putCard)
                {
                    player.SubtractACardFromSimplePlayer(this.player);
                    player.leadingCard = action.card;
                }
                if (action.type == ActionType.DrawCard)
                {
                    player.AddACardToSimplePlayer(this.player);
                }
            }
        }
        public class EndGameBroadcast:IPlayerBroadcast
        {
            public EndGameBroadcast()
            {

            }
            public void DoBroadcast(Player player)
            {
                player.GameEnded = true;
            }
        }
    }
}
