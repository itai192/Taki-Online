using System;

namespace DAL
{
    public class SesonsDal
    {
        public const string SEASONID = "[Season ID]", STARTDATE = "[Start Date]", ENDDATE = "[End Date]";
        public static int CreateNewSeason(DateTime startDate, DateTime endDate)
        {
            return DalHelper.Insert(DalHelper.SimpleInsertQuery(Constants.SEASONSTBL,new string[] {STARTDATE,ENDDATE}, new string[] {startDate.ToOADate().ToString(), endDate.ToOADate().ToString() }));
        }
        public static int GetCurrentSeason()
        {
            return (int)DalHelper.SelectRow($"SELECT * FROM {Constants.SEASONSTBL} WHERE {DateTime.Now.ToOADate().ToString()} BETWEEN {STARTDATE} AND {ENDDATE}")[SEASONID];
        }
    }
}
