using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class SesonsDal
    {
        public const string SEASONID = "[Season ID]", STARTDAT = "[Start Date]", ENDDATE = "[End Date]";
        public static int CreateNewSeason(DateTime startDate, DateTime endDate)
        {
            return DalHelper.Insert(DalHelper.SimpleInsert)
        }
    }
}
