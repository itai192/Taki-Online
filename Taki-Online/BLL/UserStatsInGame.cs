using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
namespace BLL
{
    /// <summary>
    /// describes game information about user
    /// </summary>
    public class UserStatsInGame
    {
        //username of statistic
        public string username
        {
            get;
            private set;
        }
        //how many cards user has in hand
        public int cardsInHand
        {
            get;
            private set;
        }
        //has user won
        public bool hasWon
        {
            get;
            private set;
        }
        //game ID of game
        public int GameID
        {
            get;
            private set;
        }
        //the change in elo of user
        public int eloChanged
        {
            get;
            private set;
        }
        //the change in xp of user
        public int xpChanged
        {
            get;
            private set;
        }
        /// <summary>
        /// a constructor that finds user in game statistics
        /// </summary>
        public UserStatsInGame(string user, int gameID):this(UsersInGamesDal.FindUserInGame(user,gameID))
        {

        }
        /// <summary>
        /// a constructor that uses data row as data source
        /// </summary>
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
