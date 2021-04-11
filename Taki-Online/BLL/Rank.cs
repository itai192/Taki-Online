using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BLL
{
    /// <summary>
    /// class used to describe ranks
    /// </summary>
    public class Rank
    {
        //id of the rank
        public int ID { get; private set; }
        //name of the rank
        public string name { get; private set; }
        //lowest elo in the rank
        public int lowestElo { get; private set; }
        /// <summary>
        /// returns a string describing the range of elo ratings in the rank
        /// </summary>
        public string GetRange()
        {
            return DAL.RankDal.GetRankRange(ID);
        }
        /// <summary>
        /// rank constructor by rank ID
        /// </summary
        public Rank(int ID):this(DAL.RankDal.GetRank(ID))
        {
        }
        /// <summary>
        /// rank constructor from datarow from ranks table
        /// </summary>
        public Rank(DataRow dr)
        {
            this.ID = (int)dr["Rank ID"];
            this.name = dr["Rank Name"].ToString();
            this.lowestElo = (int)dr["Lowest Elo"];
        }
        /// <summary>
        /// to string method returns names
        /// </summary>
        public override string ToString()
        {
            return name;
        }
    }
}
