﻿using System;

namespace DAL
{
    public class SesonsDal
    {
        //field names so it is easier to make queries
        public const string SEASONID = "[Season ID]", STARTDATE = "[Start Date]", ENDDATE = "[End Date]";
        /// <summary>
        /// inserts a new season and returns it's ID
        /// </summary>
        public static int CreateNewSeason(DateTime startDate, DateTime endDate)
        {
            return DalHelper.Insert(DalHelper.SimpleInsertQuery(Constants.SEASONSTBL,new string[] {STARTDATE,ENDDATE}, new string[] {startDate.ToOADate().ToString(), endDate.ToOADate().ToString() }));
        }
        /// <summary>
        /// gets current season ID
        /// </summary>
        public static int GetCurrentSeason()
        {
            return (int)DalHelper.SelectRow($"SELECT * FROM {Constants.SEASONSTBL} WHERE {DateTime.Now.ToOADate().ToString()} BETWEEN {STARTDATE} AND {ENDDATE}")["Season ID"];
        }
    }
}
