using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
namespace BLL
{
    public class UserStatsInGame
    {
        public string username
        {
            get;
            private set;
        }
        public int cardsInHand
        {
            get;
            private set;
        }
        public bool hasWon
        {
            get;
            private set;
        }
        public int GameID
        {
            get;
            private set;
        }
        public int eloChanged
        {
            get;
            private set;
        }
        public int xpChanged
        {
            get;
            private set;
        }
        public UserStatsInGame(string user, int gameID):this(UsersInGamesDal.FindUserInGame(user,gameID))
        {

        }
        public UserStatsInGame(DataRow dr)
        {
            GameID = (int)dr[UsersInGamesDal.GAME];
            hasWon = (bool)dr["Has Won"];
            cardsInHand = (int)dr["Cards In Hand"];
            username = dr["User"].ToString();
            eloChanged = (int)dr["elo Added"];
            xpChanged = (int)dr["XP Added"];
        }
    }
}
