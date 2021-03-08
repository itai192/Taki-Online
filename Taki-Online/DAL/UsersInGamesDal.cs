
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
        public static string RANKSELECT = $"SELECT SUM({Constants.USERSINGAMESTBL}.{ELO})+{Constants.RANKINGHISTORYTBL}.[Season Start Elo] AS ELO, {Constants.RANKINGHISTORYTBL}.Season, {Constants.RANKINGHISTORYTBL}.[User], {Constants.RANKINGTBL}.[Rank Name], {Constants.RANKINGTBL}.[Rank ID], Max({Constants.RANKINGTBL}.[Lowest Elo]) AS Lowest";
        public static string RANKFROM = $"FROM {Constants.RANKINGTBL}, (Seasons INNER JOIN {Constants.RANKINGHISTORYTBL} ON {Constants.SEASONSTBL}.[Season ID] = {Constants.RANKINGHISTORYTBL}.Season) INNER JOIN ({Constants.GAMESTBL} INNER JOIN {Constants.USERSINGAMESTBL} ON {Constants.GAMESTBL}.[Game ID] = {Constants.USERSINGAMESTBL}.{GAME}) ON ({Constants.RANKINGHISTORYTBL}.User = {Constants.USERSINGAMESTBL}.{USERNAME}) AND ({Constants.GAMESTBL}.[Time Played] BETWEEN  {Constants.SEASONSTBL}.[End Date] AND {Constants.SEASONSTBL}.[Start Date]  )";
        public static string RANKGROUPBY = $"GROUP BY {Constants.RANKINGHISTORYTBL}.Season, {Constants.RANKINGHISTORYTBL}.[User], {Constants.RANKINGHISTORYTBL}.[Season Start Elo], {Constants.RANKINGHISTORYTBL}.[Rank Name], Ranking.[Rank ID]";
        public static string RANKHAVING = $"HAVING Max({Constants.RANKINGTBL}.[Lowest Elo])<SUM({Constants.USERSINGAMESTBL}.[elo Added])+{Constants.RANKINGHISTORYTBL}.[Season Start Elo]";
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
            return DalHelper.SelectRow($"{RANKSELECT} {RANKFROM} WHERE {Constants.RANKINGHISTORYTBL}.[User] = '{username}' AND {Constants.RANKINGHISTORYTBL}.Season = {season} {RANKGROUPBY} {RANKHAVING}");
        }
        public static DataTable FindAllPlayersInRankInSeason(int rankID, int season)
        {
            return DalHelper.SelectTable($"{RANKSELECT} {RANKFROM} WHERE {Constants.RANKINGTBL}.[Rank ID] = {rankID} AND {Constants.RANKINGHISTORYTBL}.Season = {season} {RANKGROUPBY} {RANKHAVING}");
        }
    }
}
