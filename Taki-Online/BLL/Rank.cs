using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BLL
{
    public class Rank
    {
        public int ID { get; private set; }
        public string name { get; private set; }
        public int lowestElo { get; private set; }

        public string GetRange()
        {
            return DAL.RankDal.GetRankRange(ID);
        }
        public Rank(int ID):this(DAL.RankDal.GetRank(ID))
        {
        }
        public Rank(DataRow dr)
        {
            this.ID = (int)dr["Rank ID"];
            this.name = dr["Rank Name"].ToString();
            this.lowestElo = (int)dr["Lowest Elo"];
        }
        public override string ToString()
        {
            return name;
        }
    }
}
