using System.Data;
using System;
namespace DAL
{
    public class GameDal
    {
        //field names so it is easier to make queries
        public const string GAMEIDFLD = "[Game ID]", TIMEPLAYED = "[Time Played]", ACTIVITYFLD = "Activity", GAMENAMEFLD = "[Game Name]", ISPRIVATEFLD="[Is Private]", HOSTFLD ="Host";
        /// <summary>
        /// inserts a game and returns it's id
        /// </summary>
        public static int AddGame(string gameName, string host,int activity)
        {
            return DalHelper.Insert(DalHelper.SimpleInsertQuery(Constants.GAMESTBL, new string[] { GAMENAMEFLD,HOSTFLD,ACTIVITYFLD }, new string[] { $"'{gameName}'", $"'{host}'",activity.ToString() }));
        }
        /// <summary>
        /// changes a game's activity status
        /// </summary>
        public static void ChangeGameActivity(int activity, int gameID)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.GAMESTBL,$"{ACTIVITYFLD} = {activity}",$"{GAMEIDFLD}={gameID}"));
        }
        /// <summary>
        /// Changes a game's name
        /// </summary>
        public static void ChangeGameName(string gameName, int gameID)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.GAMESTBL, $"{GAMENAMEFLD} = '{gameName}'", $"{GAMEIDFLD}={gameID}"));
        }
        /// <summary>
        /// finds all games with specific activity
        /// </summary>
        public static DataTable FindAllGamesWithActivity(int activity)
        {
            return DalHelper.SelectTable($"SELECT * FROM {Constants.GAMESTBL} WHERE {ACTIVITYFLD} = {activity} AND {ISPRIVATEFLD}= FALSE");
        }
        /// <summary>
        /// finds if a game with given name exists with a given activity
        /// </summary>
        public static bool IsGameExistWithNameAndActivity(string name, int activity)
        {
            return DalHelper.IsExist($"SELECT * FROM {Constants.GAMESTBL} WHERE {ACTIVITYFLD} = {activity} AND {GAMENAMEFLD} = '{name}'");
        }
        /// <summary>
        /// Finds a game with specific ID and returns it's record
        /// </summary>
        public static DataRow FindGameByID(int ID)
        {
            return DalHelper.SelectRow($"SELECT * FROM {Constants.GAMESTBL} WHERE {GAMEIDFLD} = {ID}");
        }
        /// <summary>
        /// searches a game with name that contains search term and also has given activity status
        /// </summary>
        public static DataTable SearchGame(string searchTerm,int activity)
        {
            return DalHelper.SelectTable($"SELECT * FROM {Constants.GAMESTBL} WHERE {GAMENAMEFLD} LIKE '%{searchTerm}%' AND {ACTIVITYFLD} = {activity}");
        }
        /// <summary>
        /// returns how many games were played in a certin date range
        /// </summary>
        public static int GamesPlayedBetweenDates(DateTime start, DateTime end)
        {
            string sql = $"SELECT Count({GAMEIDFLD}) as amount FROM {Constants.GAMESTBL} as games WHERE {TIMEPLAYED} BETWEEN {start.ToOADate()} AND {end.ToOADate()}";
            return (int)DalHelper.SelectRow(sql)["amount"];
        }
    }
}
