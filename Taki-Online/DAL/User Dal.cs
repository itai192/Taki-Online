using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
            string sql = DalHelper.SimpleInsertQuery("Users", new string[] { "Email","Password","IsManager" } , new string[] { $"'{email}'",$"'{password}'",isManager.ToString()});
            return DalHelper.Insert(sql);
        }
        public static bool UpdateUser(string email, string password, bool isManager,int ID)
        {
            string sql = DalHelper.SimpleUpdateQuery("Users", new string[] { "Email", "Password", "IsManager" }, new string[] { $"'{email}'", $"'{password}'", isManager.ToString()},$"ID={ID}");
            return DalHelper.Update(sql)==1;
        }
        public static DataRow SelectUser(int ID)
        {
            return DalHelper.SelectRow($"SELECT * FROM Users WHERE ID={ID}");
        }

    }
}
