using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
namespace BLL
{
    public class GameInvite
    {
        //game id
        public int gameID
        {
            get;
            private set;
        }
        //sender of game invite
        public string sender
        {
            get;
            private set;
        }
        //recipiant of game invite
        public string recipiant
        {
            get;
            private set;
        }
        //game room assosiated with game invitation
        public GameRoom gameRoom
        {
            get
            {
                return new GameRoom(gameID);
            }
        }
        /// <summary>
        /// a constructor for game invite object
        /// </summary>
        public GameInvite(int gameID, string sender, string recipiant)
        {
            this.gameID = gameID;
            this.sender = sender;
            this.recipiant = recipiant;
        }
        /// <summary>
        /// a constructor for game invite object from data row in game invites table
        /// </summary>
        public GameInvite(DataRow dr) : this((int)dr["Game ID"], dr[GameInvitesDal.SENDERFLD].ToString(), dr[GameInvitesDal.RECIPIANTFLD].ToString())
        {

        }

    }
}
