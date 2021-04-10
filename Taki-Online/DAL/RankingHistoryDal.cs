using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class RankingHistoryDal
    {
        //field names so it is easier to make queries
        public const string USERFLD = "[User]", SEASONFLD = "Season", ELOFLD = "[Season Start Elo]";
        /// <summary>
        /// inserts a record of user start elo in seasons to rankingHistory table
        /// </summary>
        public static void InsertRankHistory(string username, int season, int elo)
        {
            DalHelper.Insert(DalHelper.SimpleInsertQuery(Constants.RANKINGHISTORYTBL, new string[] { USERFLD, SEASONFLD, ELOFLD }, new string[] { $"'{username}'", season.ToString(), elo.ToString() }));
        }
    }
}
