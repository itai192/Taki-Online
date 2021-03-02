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
        public class ChangeTurnBroadcast:IPlayerBroadcast
        {
            private int turn;
            public void DoBroadcast(Player player)
            {
                GamePlayer p = (GamePlayer)player;
                p.turn = turn;
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
                GamePlayer p = (GamePlayer)player;
                p.order=order;
            }
        }
    }
}
