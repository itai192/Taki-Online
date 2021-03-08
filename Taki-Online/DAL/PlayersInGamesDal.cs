
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class PlayersInGamesDal
    {
        public const string USERNAME = "[User]", GAME = "Game", FINALSCORE = "[Final Score]", XP = "[XP Added]", ELO = "[Elo Added]";

        public static void AddUserToGame(string username, int GameID)
        {
            try
            {
                DalHelper.insertWithoutCreatingID(DalHelper.SimpleInsertQuery(Constants.USERSINGAMESTBL,new string[] {USERNAME,GAME},new string[]{$"'{username}'",GameID.ToString() }));
            }
            catch(Exception e)
            {
                throw new Exception("Couldn't add user To Game",e);
            }
        }
        public static void UpdateUserInGame(string username, int GameID, int xpAdded,int eloAdded, int score  )
        {
            try
            {
                DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.USERSINGAMESTBL,new string[] {XP,ELO,FINALSCORE}, new string[] { xpAdded.ToString(),eloAdded.ToString(),score.ToString()},$"{USERNAME} = '{username}' AND {GAME} = {GameID}"));
            }
            catch(Exception e)
            {
                throw new Exception("Couldn't update user",e);
            }
        }
        public static int FindPlayerEloInSeason(int season, string username)
        {
            DalHelper.SelectRow("");
        }
    }
}
