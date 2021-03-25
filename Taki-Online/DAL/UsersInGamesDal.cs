
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
        public static string FULLSELECT = "SELECT R2.[Rank ID], R2.[Rank Name], T2.Lowest, T2.[User], T2.Season, T2.ELO FROM Ranking AS R2 INNER JOIN(SELECT T.ELO, T.Season, T.[User], Max(R1.[Lowest Elo]) AS Lowest FROM Ranking AS R1 INNER JOIN (SELECT SUM([Users in Games].[elo Added])+[Ranking History].[Season Start Elo] AS ELO, [Ranking History].Season, [Ranking History].[User] FROM(Seasons INNER JOIN[Ranking History] ON Seasons.[Season ID] = [Ranking History].Season) INNER JOIN(Games INNER JOIN[Users in Games] ON Games.[Game ID] = [Users in Games].Game) ON([Ranking History].User = [Users in Games].User) AND(Games.[Time Played] BETWEEN  Seasons.[End Date] AND Seasons.[Start Date] ) GROUP BY[Ranking History].Season, [Ranking History].[User], [Ranking History].[Season Start Elo]) AS T ON R1.[Lowest Elo]< T.ELO GROUP BY T.ELO, T.Season, T.[User]) AS T2 ON T2.Lowest = R2.[Lowest Elo]";
        public static string FULLORDER = "ORDER BY T2.ELO DESC";
        public static string RANKSELECT = $"SELECT SUM({Constants.USERSINGAMESTBL}.{ELO})+{Constants.RANKINGHISTORYTBL}.[Season Start Elo] AS ELO, {Constants.RANKINGHISTORYTBL}.Season, {Constants.RANKINGHISTORYTBL}.[User]";
        public static string RANKFROM = $"FROM (Seasons INNER JOIN {Constants.RANKINGHISTORYTBL} ON {Constants.SEASONSTBL}.[Season ID] = {Constants.RANKINGHISTORYTBL}.Season) INNER JOIN ({Constants.GAMESTBL} INNER JOIN {Constants.USERSINGAMESTBL} ON {Constants.GAMESTBL}.[Game ID] = {Constants.USERSINGAMESTBL}.{GAME}) ON ({Constants.RANKINGHISTORYTBL}.User = {Constants.USERSINGAMESTBL}.{USERNAME}) AND ({Constants.GAMESTBL}.[Time Played] BETWEEN  {Constants.SEASONSTBL}.[End Date] AND {Constants.SEASONSTBL}.[Start Date]  )";
        public static string RANKGROUPBY = $"GROUP BY {Constants.RANKINGHISTORYTBL}.Season, {Constants.RANKINGHISTORYTBL}.[User], {Constants.RANKINGHISTORYTBL}.[Season Start Elo]";
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
        public static DataRow FindPlayerRankInSeason(int season, string username)
        {
            return DalHelper.SelectRow($"{FULLSELECT} WHERE {Constants.RANKINGHISTORYTBL}.[User] = '{username}' AND {Constants.RANKINGHISTORYTBL}.Season = {season}");
        }
        public static DataTable FindAllPlayersInRankInSeason(int rankID, int season)
        {
            return DalHelper.SelectTable($"{FULLSELECT} WHERE R2.[Rank ID]= {rankID} AND T2.Season = {season} {FULLORDER}");
        }
    }
}
