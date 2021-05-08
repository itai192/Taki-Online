using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
namespace BLL
{
    /// <summary>
    /// a class that describes a game season
    /// </summary>
    public class Season
    {
        // season id
        public int SeasonID
        {
            get;
            private set;
        }
        // start date of season
        public DateTime StartDate
        {
            get;
            private set;
        }
        // end date of season
        public DateTime EndDate
        {
            get;
            private set;
        }
        /// <summary>
        /// season constructor, adds a new season
        /// </summary>
        public Season(DateTime StartDate, DateTime EndDate)
        {
            this.SeasonID = DAL.SeasonsDal.CreateNewSeason(StartDate, EndDate);
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
        /// <summary>
        /// season constructor, gets season information from ID
        /// </summary>
        public Season(int ID):this(SeasonsDal.GetSeason(ID))
        {

        }
        /// <summary>
        /// season constructor, gets season information from data row
        /// </summary>
        public Season(DataRow dr)
        {
            this.SeasonID = (int)dr["Season ID"];
            this.StartDate = (DateTime)dr["Start Date"];
            this.EndDate = (DateTime)dr["End Date"];
        }
    }
}
