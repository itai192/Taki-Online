using System;
using System.Data;
namespace DAL
{
    public class UserDal
    {
        //field names so it is easier to make queries
        public const string XPFLD = "XP", LEVELFLD = "[Level]", FIRSTNAMEFLD = "[First Name]", LASTNAMEFLD = "[Last Name]", BIRTHDATEFLD = "[Date Of Birth]", USERNAMEFLD = "Username", EMAILFLD = "Email", PASSWORDFLD = "[Password]", TYPEFLD = "Type", PICTUREFLD = "Picture";
        /// <summary>
        /// adds a user
        /// </summary>
        public static void AddUser(string email, string password, int type, string fName, string lName, DateTime bDate, string username)
        {
            string sql = DalHelper.SimpleInsertQuery(Constants.USERSTBL, new string[] { EMAILFLD, PASSWORDFLD, TYPEFLD, FIRSTNAMEFLD, LASTNAMEFLD, BIRTHDATEFLD, USERNAMEFLD }, new string[] { $"'{email}'", $"'{password}'", type.ToString(), $"'{fName}'", $"'{lName}'", bDate.ToOADate().ToString(), $"'{username}'" });
            if (!DalHelper.insertWithoutCreatingID(sql))
                throw new Exception("Couldn't Add User To db");
        }
        /// <summary>
        /// updates a user's xp in his record
        /// </summary>
        public static void UpdateUserXP(int xp, string username)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.USERSTBL, $"{XPFLD} = {xp}", $"{USERNAMEFLD} = '{username}' "));
        }
        /// <summary>
        /// updates a user's level in his record
        /// </summary>
        public static void UpdateUserLevel(int level, string username)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.USERSTBL, $"{LEVELFLD} = {level}", $"{USERNAMEFLD} = '{username}'"));
        }
        /// <summary>
        /// selects a user record
        /// </summary>
        public static DataRow SelectUser(string username)
        {
            return DalHelper.SelectRow($"SELECT * FROM {Constants.USERSTBL} WHERE {USERNAMEFLD}='{username}'");
        }
        /// <summary>
        /// selects a user that has given username and password
        /// </summary>
        public static DataRow SelectUsernameWithPassword(string username, string password)
        {
            return DalHelper.SelectRow($"SELECT * FROM {Constants.USERSTBL} WHERE {USERNAMEFLD}='{username}' AND {PASSWORDFLD}='{password}'");
        }
        /// <summary>
        /// updates a user's picture location in his record
        /// </summary>
        public static void UpdateUserPic(string username, string Picture)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.USERSTBL, new string[] { PICTUREFLD }, new string[] { $"'{Picture}'" }, $"{USERNAMEFLD} = '{username}'"));
        }
        /// <summary>
        /// checks whether a user with a given username exists
        /// </summary>
        public static bool ExistUsername(string username)
        {
            return DalHelper.IsExist($"SELECT * FROM {Constants.USERSTBL} WHERE {USERNAMEFLD} = '{username}'");
        }
        /// <summary>
        /// Updates a user's username
        /// </summary>
        public static void UpdateUsername(string oldUsername, string newUsername)
        {
            DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.USERSTBL, new string[] { USERNAMEFLD }, new string[] { $"'{newUsername}'" }, $"{USERNAMEFLD} = '{oldUsername}'"));
        }
        /// <summary>
        /// searches users which their usernames contain search term
        /// </summary>
        public static DataTable SearchUsername(string searchTerm)
        {
            return DalHelper.SelectTable($"SELECT * FROM {Constants.USERSTBL} WHERE {USERNAMEFLD} LIKE '%{searchTerm}%'");
        }
        /// <summary>
        /// a method that finds and returns the avarage date of birth among users
        /// </summary>
        public static DateTime AvarageBirthDate()
        {
            string sql = $"SELECT CDate(Avg({BIRTHDATEFLD})) as avgbirth FROM {Constants.USERSTBL}";
            return (DateTime)DalHelper.SelectRow(sql)["avgbirth"];
        }
        /// <summary>
        /// Returns the amount of users
        /// </summary>
        public static int UserCount()
        {
            return (int)DalHelper.SelectRow($"SELECT COUNT({USERNAMEFLD}) AS Amount FROM {Constants.USERSTBL}")["Amount"];
        }
    }
}
