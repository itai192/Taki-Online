using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
namespace BLL
{
    public class Season
    {
        public int SeasonID
        {
            get;
            private set;
        }
        public DateTime StartDate
        {
            get;
            private set;
        }
        public DateTime EndDate
        {
            get;
            private set;
        }
        public Season(DateTime StartDate, DateTime EndDate)
        {
            DAL.SeasonsDal.CreateNewSeason(StartDate, EndDate);
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
        public Season(int ID):this(SeasonsDal.GetSeason(ID))
        {

        }
        public Season(DataRow dr)
        {
            this.SeasonID = (int)dr["Season ID"];
            this.StartDate = (DateTime)dr["Start Date"];
            this.EndDate = (DateTime)dr["End Date"];
        }
    }
}
