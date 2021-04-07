using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data;
namespace DAL
{
    public class RankDal
    {
        /// <summary>
        /// gets all records that describe ranks
        /// </summary>
        public static DataTable GetAllRanks()
        {
            return DalHelper.SelectTable($"SELECT * FROM {Constants.RANKINGTBL}");
        }
        /// <summary>
        /// gets the range of elos of a certin rank
        /// </summary>
        public static string GetRankRange(int rankID)
        {
            if (DalHelper.IsExist($"SELECT Origin.[Rank ID] , MIN(Other.[Lowest Elo]) AS MaxELO, Origin.[Lowest Elo] FROM Ranking AS Origin, Ranking AS Other WHERE Other.[Lowest Elo] > Origin.[Lowest Elo] AND Origin.[Rank ID] = {rankID} GROUP BY Origin.[Rank ID], Origin.[Lowest Elo]"))
            {
                DataRow dr = DalHelper.SelectRow($"SELECT Origin.[Rank ID] , MIN(Other.[Lowest Elo]) AS MaxELO, Origin.[Lowest Elo] FROM Ranking AS Origin, Ranking AS Other WHERE Other.[Lowest Elo] > Origin.[Lowest Elo] AND Origin.[Rank ID] = {rankID} GROUP BY Origin.[Rank ID], Origin.[Lowest Elo]");
                return dr["Lowest Elo"].ToString() + " - " + dr["MaxElo"].ToString();
            }
            return GetRankLowestValue(rankID).ToString() + "+";
        }
        /// <summary>
        /// gets the lowest elo of a rank
        /// </summary>
        public static int GetRankLowestValue(int rankID)
        {
            return (int)GetRank(rankID)["Lowest Elo"];
        }
        /// <summary>
        /// returns a record that describes the rank
        /// </summary>
        public static DataRow GetRank(int rankID)
        {
            return DalHelper.SelectRow($"SELECT * FROM {Constants.RANKINGTBL} WHERE [Rank ID] = {rankID}");
        }

    }
}
