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
        public int gameID
        {
            get;
            private set;
        }
        public string sender
        {
            get;
            private set;
        }
        public string recipiant
        {
            get;
            private set;
        }
        public GameInvite(int gameID, string sender, string recipiant)
        {
            this.gameID = gameID;
            this.sender = sender;
            this.recipiant = recipiant;
        }
        public GameInvite(DataRow dr) : this((int)dr["Game ID"], dr[GameInvitesDal.SENDERFLD].ToString(), dr[GameInvitesDal.RECIPIANTFLD].ToString())
        {

        }

    }
}
