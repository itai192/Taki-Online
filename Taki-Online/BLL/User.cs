using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Data;
namespace BLL
{
    public class User
    {
        public string username { get; }
        public UserType type { get; }
        public string email { get; }
        public DateTime BirthDate { get; }
        public int level { get; }
        public int xp { get; }
        public string fName { get; }
        public string lName {get;}

        public User(string username, string password) : this(UserDal.SelectUsernameWithPassword(username, password))
        {

        }
        public User(string username, UserType type, string email, DateTime BirthDate, string fName, string lName,string password)
        {
            level = 1;
            xp = 0;
            this.BirthDate = BirthDate;
            this.fName = fName;
            this.lName = lName;
            this.type = type;
            this.username = username;
            DAL.UserDal.AddUser(email, password, (int)type, fName, lName, BirthDate,username);
        }
        public User(DataRow dr)
        {
            username = dr["username"].ToString();
            type = (UserType)dr["Type"];
            email = dr["Email"].ToString();
            BirthDate = (DateTime)dr["Date Of Birth"];
            level = (int)dr["Level"];
            xp = (int)dr["xp"];
            fName = dr["First Name"].ToString();
            lName = dr["Last Name"].ToString();
        }
    }
}
