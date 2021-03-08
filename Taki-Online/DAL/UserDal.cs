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
        //field names
        public const string XPFLD = "XP", LEVELFLD = "Level", FIRSTNAMEFLD = "[First Name]", LASTNAMEFLD = "[Last Name]", BIRTHDATEFLD = "[Date Of Birth]", USERNAMEFLD = "Username", EMAILFLD = "Email", PASSWORDFLD = "[Password]", TYPEFLD = "Type", PICTUREFLD = "Picture";
        /// <summary>
        /// adds a user
        /// </summary>
        /// <returns>user ID</returns>
        public static void AddUser(string email, string password, int type, string fName, string lName, DateTime bDate,string username)
        {
            string sql = DalHelper.SimpleInsertQuery(Constants.USERSTBL, new string[] { EMAILFLD,PASSWORDFLD,TYPEFLD,FIRSTNAMEFLD,LASTNAMEFLD,BIRTHDATEFLD,USERNAMEFLD } , new string[] { $"'{email}'", $"'{password}'", type.ToString(), $"'{fName}'", $"'{lName}'", bDate.ToOADate().ToString(), $"'{username}'" });
            if (!DalHelper.insertWithoutCreatingID(sql))
                throw new Exception("Couldn't Add User To db");
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
            return DalHelper.SelectRow($"SELECT * FROM {Constants.USERSTBL} WHERE {USERNAMEFLD}='{username}'");
        }
        public static DataRow SelectUsernameWithPassword(string username, string password)
        {
            return DalHelper.SelectRow($"SELECT * FROM {Constants.USERSTBL} WHERE {USERNAMEFLD}='{username}' AND {PASSWORDFLD}='{password}'");
        }
        public static void UpdateUserPic(string username, string Picture)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.USERSTBL, new string[] { PICTUREFLD }, new string[] { $"'{Picture}'" }, $"{USERNAMEFLD} = '{username}'"));
        }
        public static bool ExistUsername(string username)
        {
            return DalHelper.IsExist($"SELECT * FROM {Constants.USERSTBL} WHERE {USERNAMEFLD} = '{username}'");
        }
        public static void UpdateUsername(string oldUsername, string newUsername)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.USERSTBL, new string[] {USERNAMEFLD}, new string[] {$"'{newUsername}'" }, $"{USERNAMEFLD} = '{oldUsername}'" ));
        }
    }
}
