using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Data;
using System.IO;
namespace BLL
{
    public class User
    {
        const int InitailXpNeeded=50;
        public string username { get;  }
        public UserType type { get; }
        public string email { get; }
        public DateTime BirthDate { get; }
        private int _level;
        private string _picture;
        public int level { get { return _level; } 
            set 
            {
                //update level
            } 
        }
        private int _xp;
        public int xp 
        {
            get
            {
                return _xp;
            }
            set
            {
                _xp = xp;
                LevelUpIfCan();
                //update
            }
        }
        public string fName { get; }
        public string lName {get;}
        public string picture {
            get
            {
                return _picture;
            }
            set
            {
                
            }
                }
        public List<string> AcceptedFriends
        {
            get
            {
                List<string> l1 = BLL_Helper.DataTableToList<string>(DAL.Friends_Dal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Accepted), Friends_Dal.SENDERFLD );
                List<string> l2 = BLL_Helper.DataTableToList<string>(DAL.Friends_Dal.FriendRequestsWithStatusSent(username, (int)FriendRequestStatus.Accepted), Friends_Dal.RECIPIANTFLD);
                return BLL_Helper.UniteLists(l1, l2);
            }
        }
        public void LevelUpIfCan()
        {
            int xpuntill = XPUntilNextLevel();
            if (_xp >= xpuntill)
            { 
                level++;
                xp-=xpuntill;
            }
        }
        public int XPUntilNextLevel()
        {
            if (level == 1)
                return InitailXpNeeded;
            return (int)(InitailXpNeeded * Math.Pow(2,level-2));
        }
        public List<string> DeclinedFriends
        {
            get
            {
                return BLL_Helper.DataTableToList<string>(DAL.Friends_Dal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Declined),Friends_Dal.SENDERFLD);
            }
        }
        public List<string> UnopenedFriendRequests
        {
            get { return BLL_Helper.DataTableToList<string>(DAL.Friends_Dal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Unopened), Friends_Dal.SENDERFLD); }
        }
        public User(string username) : this(UserDal.SelectUser(username))
        {

        }
        public User(string username, string password) : this(UserDal.SelectUsernameWithPassword(username, password))
        {

        }
        public void AddFriend(string username)
        {
            //later
        }
        public User(string username, UserType type, string email, DateTime BirthDate, string fName, string lName,string password)
        {
            level = 1;
            _xp = 0;
            this.BirthDate = BirthDate;
            this.fName = fName;
            this.lName = lName;
            this.type = type;
            this.username = username;
            DAL.UserDal.AddUser(email, password, (int)type, fName, lName, BirthDate,username);
        }
        
        public User(DataRow dr)
        {
            username = dr[UserDal.USERNAMEFLD].ToString();
            type = (UserType)dr[UserDal.TYPEFLD];
            email = dr[UserDal.EMAILFLD].ToString();
            BirthDate = (DateTime)dr["Date Of Birth"];
            level = (int)dr[UserDal.LEVELFLD];
            _xp = (int)dr[UserDal.XPFLD];
            fName = dr["First Name"].ToString();
            lName = dr["Last Name"].ToString();
            picture = dr[UserDal.PICTUREFLD].ToString();
        }
    }
}
