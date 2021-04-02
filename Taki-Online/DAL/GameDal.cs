using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public class GameDal
    {
        public const string GAMEIDFLD = "[Game ID]", TIMEPLAYED = "[Time Played]", ACTIVITYFLD = "Activity", GAMENAMEFLD = "[Game Name]", ISPRIVATEFLD="[Is Private]", HOSTFLD ="Host";
        public static int AddGame(string gameName, string host,int activity)
        {
            return DalHelper.Insert(DalHelper.SimpleInsertQuery(Constants.GAMESTBL, new string[] { GAMENAMEFLD,HOSTFLD,ACTIVITYFLD }, new string[] { $"'{gameName}'", $"'{host}'",activity.ToString() }));
        }
        public static void ChangeGameActivity(int activity, int gameID)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.GAMESTBL,$"{ACTIVITYFLD} = {activity}",$"{GAMEIDFLD}={gameID}"));
        }
        public static void ChangeGameName(string gameName, int gameID)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.GAMESTBL, $"{GAMENAMEFLD} = '{gameName}'", $"{GAMEIDFLD}={gameID}"));
        }
        public static DataTable FindAllOpenGamesWithActivity(int activity)
        {
            return DalHelper.SelectTable($"SELECT * FROM {Constants.GAMESTBL} WHERE {ACTIVITYFLD} = {activity} AND {ISPRIVATEFLD}= FALSE");
        }
        public static bool IsGameExistWithNameAndActivity(string name, int activity)
        {
            return DalHelper.IsExist($"SELECT * FROM {Constants.GAMESTBL} WHERE {ACTIVITYFLD} = {activity} AND {GAMENAMEFLD} = '{name}'");
        }
        public static DataRow FindGameByID(int ID)
        {
            return DalHelper.SelectRow($"SELECT * FROM {Constants.GAMESTBL} WHERE {GAMEIDFLD} = {ID}");
        }
        public static DataTable SearchGame(string searchTerm,int activity)
        {
            return DalHelper.SelectTable($"SELECT * FROM {Constants.GAMESTBL} WHERE {GAMENAMEFLD} LIKE '%{searchTerm}%' AND {ACTIVITYFLD} = {activity}");
        }
    }
}
