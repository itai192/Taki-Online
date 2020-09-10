using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class UserDal
    {
        /// <summary>
        /// adds a user
        /// </summary>
        /// <returns>user ID</returns>
        public static int AddUser(string email, string password, bool isManager)
        {
            string sql = $"INSERT into Users (Email, Password, IsManeger) VALUES '{email}', '{password}', isManager";
            return DalHelper.Insert(sql);
        }
    }
}
