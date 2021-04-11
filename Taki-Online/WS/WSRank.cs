using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
namespace WS
{
    public class WSRank
    {
        //rank id
        public int ID { get; set; }
        //rank name
        public string name { get; set; }
        //rank lowest elo
        public int lowestElo { get; set; }
        //rank range
        public string range { get; set; }

        /// <summary>
        /// empty constructor
        /// </summary>
        public WSRank()
        {

        }
        /// <summary>
        /// constructs wsrank using rank
        /// </summary>
        public WSRank(Rank rank)
        {
            ID = rank.ID;
            name = rank.name;
            lowestElo = rank.lowestElo;
            range = rank.GetRange();
        }
        /// <summary>
        /// constructs wsrank using rank ID
        /// </summary>
        public WSRank(int ID) : this(new Rank(ID))
        {

        }
    }
}