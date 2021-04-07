
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public class UsersInGamesDal
    {
        //a query that selects every player in each season and it's elo score, and rank,
        public static string FULLSELECT = "SELECT R2.[Rank ID], R2.[Rank Name], T2.Lowest, T2.[User], T2.Season, T2.ELO FROM Ranking AS R2 INNER JOIN(SELECT T.ELO, T.Season, T.[User], Max(R1.[Lowest Elo]) AS Lowest FROM Ranking AS R1 INNER JOIN (SELECT ((IIF(ISNULL(Sum([Users in Games].[elo Added])),0, Sum([Users in Games].[elo Added])))+[Ranking History].[Season Start Elo]) AS ELO, [Ranking History].Season, [Ranking History].User FROM (Seasons INNER JOIN [Ranking History] ON Seasons.[Season ID] = [Ranking History].Season) LEFT JOIN (Games RIGHT JOIN [Users in Games] ON Games.[Game ID] = [Users in Games].Game) ON [Ranking History].User = [Users in Games].User GROUP BY [Ranking History].Season, [Ranking History].User, [Ranking History].[Season Start Elo]) AS T ON R1.[Lowest Elo]< T.ELO GROUP BY T.ELO, T.Season, T.[User]) AS T2 ON T2.Lowest = R2.[Lowest Elo]";
        //part of the query that orders it by elo score
        public static string FULLORDER = "ORDER BY T2.ELO DESC";
        //a query that selects the rank and elo from season start
        public static string STARTSELECT = "SELECT R2.[Rank ID], R2.[Rank Name], T2.Lowest, T2.[User], T2.Season, T2.ELO FROM Ranking AS R2 INNER JOIN (SELECT T.ELO, T.Season, T.[User], Max(R1.[Lowest Elo]) AS Lowest FROM Ranking AS R1 INNER JOIN (SELECT[Ranking History].[Season Start Elo] AS ELO, [Ranking History].Season, [Ranking History].[User] FROM[Ranking History]) AS T ON R1.[Lowest Elo] < T.ELO GROUP BY T.ELO, T.Season, T.[User]) AS T2 ON T2.Lowest = R2.[Lowest Elo]";
        //names of fields as constants so it would be easier to make queries
        public const string USERNAME = "[User]", GAME = "Game", FINALSCORE = "[Final Score]", XP = "[XP Added]", ELO = "[Elo Added]",HASWON = "[Has Won]",CARDSINHAND = "[Cards In Hand]";
        /// <summary>
        /// adds a user to users in games table
        /// </summary>
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
        /// <summary>
        /// updates a record of a user in a game
        /// </summary>
        public static void UpdateUserInGame(string username, int GameID, int xpAdded,int eloAdded,  bool HasWon,int CardsInHand)
        {
            try
            {
                DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.USERSINGAMESTBL, new string[] { XP, ELO, HASWON,CARDSINHAND }, new string[] { xpAdded.ToString(), eloAdded.ToString(), HasWon.ToString() }, $"{USERNAME} = '{username}' AND {GAME} = {GameID}"));
            }
            catch(Exception e)
            {
                throw new Exception("Couldn't update user",e);
            }
        }
        /// <summary>
        /// a method finds a player's rank(and elo) in a given season
        /// </summary>
        public static DataRow FindPlayerRankInSeason(int season, string username)
        {
            return DalHelper.SelectRow($"{FULLSELECT} WHERE {Constants.RANKINGHISTORYTBL}.[User] = '{username}' AND {Constants.RANKINGHISTORYTBL}.Season = {season}");
        }
        /// <summary>
        /// a method that find's all player's in a given rank in a given season
        /// </summary>
        public static DataTable FindAllPlayersInRankInSeason(int rankID, int season)
        {
            return DalHelper.SelectTable($"{FULLSELECT} WHERE R2.[Rank ID]= {rankID} AND T2.Season = {season} {FULLORDER}");
        }
        /// <summary>
        /// finds the start rank (and elo) of a player in a season
        /// </summary>
        public static DataRow FindPlayerStartRankInSeason(int season, string username)
        {
            return DalHelper.SelectRow($"{STARTSELECT} WHERE {Constants.RANKINGHISTORYTBL}.[User] = '{username}' AND {Constants.RANKINGHISTORYTBL}.Season = {season}");
        }
        /// <summary>
        /// finds the records of users in a specific game
        /// </summary>
        public static DataTable FindUsersInGame(int gameID)
        {
            return DalHelper.SelectTable($"SELECT * FROM {Constants.USERSINGAMESTBL} WHERE {GAME} = {gameID}");
        }
    }
}
