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
        public static int AddUser()
        {
            string sql = "";
            return DalHelper.Insert();
        }
    }
}
