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
        private string _username;
        public string username { 
            get
            {
                return _username;
            }
            set
            {
                UserDal.UpdateUsername(_username, value);
                _username = value;
            }
        }
        public Rank rank
        {
            get
            {
                try
                {
                    return new Rank((int)DAL.UsersInGamesDal.FindPlayerRankInSeason(DAL.SesonsDal.GetCurrentSeason(), username)["Rank ID"]);
                }
                catch
                {
                    return new Rank((int)DAL.UsersInGamesDal.FindPlayerStartRankInSeason(DAL.SesonsDal.GetCurrentSeason(), username)["Rank ID"]);
                }
            }
        }
        public int elo { get
            {
                try
                {
                    int elo = (int)(double)UsersInGamesDal.FindPlayerRankInSeason(DAL.SesonsDal.GetCurrentSeason(), username)["ELO"];
                    return elo;
                }
                catch
                {
                    int elo = (int)UsersInGamesDal.FindPlayerStartRankInSeason(DAL.SesonsDal.GetCurrentSeason(), username)["ELO"];
                    return elo;
                }
                
            }
        }
        public UserType type { get; }
        public string email { get; }
        public DateTime BirthDate { get; }
        private int _level;
        private string _picture;
        public int level { get { return _level; } 
            set 
            {
                _level = value;
                DAL.UserDal.UpdateUserLevel(_level, username);
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
                _xp = value;
                LevelUpIfCan();
                DAL.UserDal.UpdateUserXP(_xp,username);
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
                DAL.UserDal.UpdateUserPic(username, value);
                _picture = value;
            }
        }
        public List<string> AcceptedFriends
        {
            get
            {
                List<string> l1 = BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Accepted), FriendsDal.SENDERFLD );
                List<string> l2 = BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusSent(username, (int)FriendRequestStatus.Accepted), FriendsDal.RECIPIANTFLD);
                return BLL_Helper.UniteLists(l1, l2);
            }
        }
        public void LevelUpIfCan()
        {
            int xpuntill = XPUntilNextLevel();
            if (_xp >= xpuntill)
            { 
                level++;
                _xp-=xpuntill;
            }
        }
        public int XPUntilNextLevel()
        {
            return (int)(InitailXpNeeded * Math.Pow(2,level-1));
        }
        /// <summary>
        /// users that this user has declined friend requests from
        /// </summary>
        public List<string> DeclinedFriends
        {
            get
            {
                return BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Declined),FriendsDal.SENDERFLD);
            }
        }
        public List<string> UnopenedFriendRequests
        {
            get { return BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Unopened), FriendsDal.SENDERFLD); }
        }
        public List<string> UnopenedSentFriendRequests
        {
            get { return BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusSent(username, (int)FriendRequestStatus.Unopened), FriendsDal.RECIPIANTFLD); }
        }
        public List<GameInvite> activeGameInvites
        {
            get
            {
                List<GameInvite> invites = new List<GameInvite>();
                DataTable dt = GameInvitesDal.FindInvitesToRecipiantWhereGameWithActivity(username, (int)GameStatus.Starting);
                foreach(DataRow dr in dt.Rows)
                {
                    invites.Add(new GameInvite(dr));
                }
                return invites;
            }
        }
        public User(string username) : this(UserDal.SelectUser(username))
        {

        }
        public User(string username, string password) : this(UserDal.SelectUsernameWithPassword(username, password))
        {

        }
        public bool IsUserInGame(int gameID)
        {
            return DAL.UsersInGamesDal.IsUserInGame(username, gameID);
        }
        public string AddFriend(string username)
        {
            if (DeclinedFriends.Contains(username))
            {
                FriendsDal.ChangeStatus(username, this.username, (int)FriendRequestStatus.Accepted);
                return "Your'e now friends with"+username;
            }
            else if (!FriendsDal.FriendRequestExists(username, this.username))
            {
                FriendsDal.AddFriend(this.username, username);
                return "Your friend request has been sent successfuly to "+username;
            }
            return "there already exists a friend request between you and" + username;
            
        }
        public void AcceptFriendRequestFrom(string username)
        {
            if (UnopenedFriendRequests.Contains(username))
                FriendsDal.ChangeStatus(username, this.username, (int)FriendRequestStatus.Accepted);
            else
                throw new Exception("You Have No Friend Invitation From This Friend");
        }
        public void DeclineFriendRequestFrom(string username)
        {
            if (UnopenedFriendRequests.Contains(username))
                FriendsDal.ChangeStatus(username, this.username, (int)FriendRequestStatus.Declined);
            else
                throw new Exception("You Have No Friend Invitation From This Friend");
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
            _username = dr[UserDal.USERNAMEFLD].ToString();
            type = (UserType)dr[UserDal.TYPEFLD];
            email = dr[UserDal.EMAILFLD].ToString();
            BirthDate = (DateTime)dr["Date Of Birth"];
            _level = (int)dr[UserDal.LEVELFLD];
            _xp = (int)dr[UserDal.XPFLD];
            fName = dr["First Name"].ToString();
            lName = dr["Last Name"].ToString();
            _picture = dr[UserDal.PICTUREFLD].ToString();
        }
        public UserStatsInGame statsInGame(int gameID)
        {
            return new UserStatsInGame(username, gameID);
        }
        public override bool Equals(object obj)
        {
            return obj is User && ((User)obj).username==this.username;
        }
        public override string ToString()
        {
            return username;
        }
        public int HowManyGamesWon()
        {
            return UsersInGamesDal.HowManyGamesUserWonOrLost(username,true);
        }
        public int HowManyGamesLost()
        {
            return UsersInGamesDal.HowManyGamesUserWonOrLost(username, false);
        }
    }
}
