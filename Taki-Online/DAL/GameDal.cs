using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class GameDal
    {
        public const string GAMEID = "[Game ID]", TIMEPLAYED = "[Time Played]", ACTIVITY = "Activity", GAMENAME = "Game Name", GAMETYPE="[Game Type]";
        public static int AddGame(string GameName)
        {
            return DalHelper.Insert(DalHelper.SimpleInsertQuery(Constants.GAMESTBL, new string[] { GameName }, new string[] { GameName }));
        }
        public static void ChangeGameActivity(int activity, int gameID)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.GAMESTBL,$"{ACTIVITY} = {activity}",$"{GAMEID}={gameID}"));
        }
        public static void ChangeGameName(string gameName, int gameID)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.GAMESTBL, $"{GAMENAME} = '{gameName}'", $"{GAMEID}={gameID}"));
        }
    }
}
