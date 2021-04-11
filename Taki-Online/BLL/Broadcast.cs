using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class Game
    {
        /// <summary>
        /// an interface that defines a broadcast sent from game to 
        /// players that tell the player object what happened in the game
        /// used to help make the game asyncronous
        /// </summary>
        public interface IPlayerBroadcast
        {
            /// <summary>
            /// a method which excecutes the broadcast over the player
            /// </summary>
            void DoBroadcast(Player player);
        }
        /// <summary>
        /// a brodcast which changes the turn of the taki game
        /// implements the IPlayerBroadcast interface
        /// </summary>
        public class ChangeTurnBroadcast : IPlayerBroadcast
        {
            //describes the turn to go to
            private int turn;
            /// <summary>
            /// changes player's turn to specified turn
            /// </summary>
            public void DoBroadcast(Player player)
            {
                player.turn = turn;
            }
            /// <summary>
            /// a simple constructor for the changeturn broadcast
            /// </summary>
            public ChangeTurnBroadcast(int turn)
            {
                this.turn = turn;
            }
        }
        /// <summary>
        /// a brodcast which changes the turn order of the taki game
        /// implements the IPlayerBroadcast interface
        /// </summary>
        public class ChangeOrderBroadcast : IPlayerBroadcast
        {
            //the order of the game(true means to add to the list and false to subtruct)
            private bool order;
            /// <summary>
            /// a simple constructor for the changeorder broadcast
            /// </summary>
            public ChangeOrderBroadcast(bool order)
            {
                this.order = order;
            }
            /// <summary>
            /// changes player's order to specified order
            /// </summary>
            public void DoBroadcast(Player player)
            { 
                player.order = order;
            }
        }
        /// <summary>
        /// a brodcast which describes a player's action
        /// implements the IPlayerBroadcast interface
        /// </summary>
        public class ActionBroadcast : IPlayerBroadcast
        {
            // the action describing the action a player has done
            public Action action;
            //a simple player object describing the player who did the action
            public SimplePlayer player
            {
                get { return action.player.ToSimplePlayer(); }
            }
            //the type of the action done
            public ActionType type
            {
                get { return action.type; }
            }
            /// <summary>
            /// a simple constructor for the action broadcast
            /// </summary>
            public ActionBroadcast(Action action)
            {
                this.action = action;
            }
            /// <summary>
            /// does the action described in the action object
            /// </summary>
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
        /// <summary>
        /// a brodcast which tells the player the game has ended
        /// implements the IPlayerBroadcast interface
        /// </summary>
        public class EndGameBroadcast:IPlayerBroadcast
        {
            /// <summary>
            /// an empty constructor for the end game broadcast
            /// </summary>
            public EndGameBroadcast()
            {

            }
            /// <summary>
            /// changes the players gameended property to true
            /// </summary>
            public void DoBroadcast(Player player)
            {
                player.GameEnded = true;
            }
        }
    }
}
