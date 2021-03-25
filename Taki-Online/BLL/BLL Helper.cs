﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
namespace BLL
{
    public class BLL_Helper
    {
        public static DataTable GetAllPlayersInRankThisSeason(int rankID)
        {
            return DAL.UsersInGamesDal.FindAllPlayersInRankInSeason(rankID, DAL.SesonsDal.GetCurrentSeason());
        }
        public static List<Rank> GetAllRanks()
        {
            DataTable dt = RankDal.GetAllRanks();
            List<Rank> ranks = new List<Rank>();
            foreach (DataRow dr in dt.Rows)
            {
                ranks.Add(new Rank(dr));
            }
            return ranks;
        }
        public static void SetSourceAndProvider(string source, string provider)
        {
            DalHelper.SetProvider(provider);
            DalHelper.SetSource(source);
        }
        public static void CreateDBHelperInDalHelper(string source, string provider)
        {
            SetSourceAndProvider(source, provider);
            DalHelper.CreateDBHelper();
        }
        public static List<T> DataTableToList<T>(DataTable dt, string field)
        {
            List<T> l = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                l.Add(dr.Field<T>(field));
            }
            return l;
        }
        public static List<T> UniteLists<T>(List<T> l1, List<T> l2)
        {
            List<T> l = new List<T>();
            l.AddRange(l1);
            l.AddRange(l2);
            return l;
        }
        public static bool UserExists(string username)
        {
            return UserDal.ExistUsername(username);
        }
        public static List<User> UserListFromDataTable(DataTable dt)
        {
            List<User> ret = new List<User>();
            List<string> usernames = DataTableToList<string>(dt, DAL.UserDal.USERNAMEFLD);
            foreach(string name in usernames)
            {
                ret.Add(new User(name));
            }
            return ret;
        }
        public static List<User> SearchUser(string searchTerm)
        {
            return UserListFromDataTable(DAL.UserDal.SearchUsername(searchTerm));
        }
    }
}
