using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public class UserDal
    {
        /// <summary>
        /// adds a user
        /// </summary>
        /// <returns>user ID</returns>
        public static void AddUser(string email, string password, int type, string fName, string lName, DateTime bDate,string username)
        {
            string sql = DalHelper.SimpleInsertQuery("Users", new string[] { "Email","[Password]","Type","[First Name]","[Last Name]","[Date Of Birth]","Username" } , new string[] { $"'{email}'", $"'{password}'", type.ToString(), $"'{fName}'", $"'{lName}'", bDate.ToOADate().ToString(), $"'{username}'" });
            DalHelper.insertWithoutCreatingID(sql);
        }
        /* static bool UpdateUser(string email, string password, bool isManager,int ID)
        {
            string sql = DalHelper.SimpleUpdateQuery("Users", new string[] { "Email", "Password", "IsManager" }, new string[] { $"'{email}'", $"'{password}'", isManager.ToString()},$"ID={ID}");
            return DalHelper.Update(sql)==1;
        }
        To Fix
        */
        public static DataRow SelectUser(string username)
        {
            return DalHelper.SelectRow($"SELECT * FROM Users WHERE ID='{username}'");
        }
        public static DataRow SelectUsernameWithPassword(string username, string password)
        {
            return DalHelper.SelectRow($"SELECT * FROM Users WHERE Username='{username}' AND [Password]='{password}'");
        }
    }
}
