using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
namespace WS
{
    public class WSRank
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int lowestElo { get; set; }
        public string range { get; set; }

        public WSRank()
        {

        }
        public WSRank(Rank rank)
        {
            ID = rank.ID;
            name = rank.name;
            lowestElo = rank.lowestElo;
            range = rank.GetRange();
        }
        public WSRank(int ID) : this(new Rank(ID))
        {

        }
    }
}