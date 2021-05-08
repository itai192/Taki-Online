using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Data;
using System.IO;
namespace BLL
{
    /// <summary>
    /// a class describing a user
    /// </summary>
    public class User
    {
        //xp needed to pass level
        const int InitailXpNeeded=50;
        //initial elo of a user
        const int InitailElo = 200;
        //username
        private string _username;
        //username property
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
        //User's rank this season
        public Rank rank
        {
            get
            {
                try
                {
                    return new Rank((int)DAL.UsersInGamesDal.FindPlayerRankInSeason(DAL.SeasonsDal.GetCurrentSeason(), username)["Rank ID"]);
                }
                catch
                {
                    return new Rank((int)DAL.UsersInGamesDal.FindPlayerStartRankInSeason(DAL.SeasonsDal.GetCurrentSeason(), username)["Rank ID"]);
                }
            }
        }
        //User's elo this season
        public int elo { get
            {
                try
                {
                    int elo = (int)(double)UsersInGamesDal.FindPlayerRankInSeason(DAL.SeasonsDal.GetCurrentSeason(), username)["ELO"];
                    return elo;
                }
                catch
                {
                    int elo = (int)UsersInGamesDal.FindPlayerStartRankInSeason(DAL.SeasonsDal.GetCurrentSeason(), username)["ELO"];
                    return elo;
                }
            }
        }
        //the user's type
        public UserType type { get; }
        //user's email
        public string email { get; }
        //user's birthdate
        public DateTime BirthDate { get; }
        //user's level
        private int _level;
        //user's picture
        private string _picture;
        //user's level
        public int level {
            get { return _level; } 
            set 
            {
                _level = value;
                DAL.UserDal.UpdateUserLevel(_level, username);
            } 
        }
        //user's expiriance level
        private int _xp;
        //user's expiriance level
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
                UserDal.UpdateUserXP(_xp,username);
            }
        }
        //user's first name
        public string fName { get; }
        //user's last name
        public string lName {get;}
        //user's picture profile path
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
        //user's accepted friends list
        public List<string> AcceptedFriends
        {
            get
            {
                List<string> l1 = BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Accepted), FriendsDal.SENDERFLD );
                List<string> l2 = BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusSent(username, (int)FriendRequestStatus.Accepted), FriendsDal.RECIPIANTFLD);
                return BLL_Helper.UniteLists(l1, l2);
            }
        }
        /// <summary>
        /// Returns The users elo at season
        /// </summary>
        public int GetUserEloAtSeason(int seasonID)
        {
            try
            {
                int elo = (int)(double)UsersInGamesDal.FindPlayerRankInSeason(seasonID, username)["ELO"];
                return elo;
            }
            catch
            {
                int elo = (int)UsersInGamesDal.FindPlayerStartRankInSeason(seasonID, username)["ELO"];
                return elo;
            }
        }
        /// <summary>
        /// Returns The users beginning elo at season
        /// </summary>
        public int GetUserBeginningEloAtSeason(int seasonID)
        {
            int elo = (int)UsersInGamesDal.FindPlayerStartRankInSeason(seasonID, username)["ELO"];
            return elo;
        }
        /// <summary>
        /// Creates a new record in Rank History for new season based on old season
        /// </summary>
        public void SeasonStartRank(int newSeason, int oldSeason)
        {
            int elo = this.GetUserEloAtSeason(oldSeason);
            int startElo = this.GetUserBeginningEloAtSeason(oldSeason);
            RankingHistoryDal.InsertRankHistory(username, newSeason, ((elo + startElo) / 2));

        }
        /// <summary>
        /// levels user up if he can level up
        /// </summary>
        public void LevelUpIfCan()
        {
            int xpuntill = XPUntilNextLevel();
            if (_xp >= xpuntill)
            { 
                level++;
                _xp-=xpuntill;
            }
        }
        /// <summary>
        /// xp needed to reach next level, doubles every level
        /// </summary>
        public int XPUntilNextLevel()
        {
            return (int)(InitailXpNeeded * Math.Pow(2,level-1));
        }
        // users that this user has declined friend requests from
        public List<string> DeclinedFriends
        {
            get
            {
                return BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Declined),FriendsDal.SENDERFLD);
            }
        }
        //names of users which have requested to be friends and not answered yet
        public List<string> UnopenedFriendRequests
        {
            get { return BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusRecieved(username, (int)FriendRequestStatus.Unopened), FriendsDal.SENDERFLD); }
        }
        //names of users which ypu have requested to be friends with and not answered yet
        public List<string> UnopenedSentFriendRequests
        {
            get { return BLL_Helper.DataTableToList<string>(DAL.FriendsDal.FriendRequestsWithStatusSent(username, (int)FriendRequestStatus.Unopened), FriendsDal.RECIPIANTFLD); }
        }
        //a list of game invites
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
        /// <summary>
        /// user constructor, using a user's username
        /// </summary>
        public User(string username) : this(UserDal.SelectUser(username))
        {

        }

        /// <summary>
        /// user constructor, using a user's username and password
        /// </summary>
        public User(string username, string password) : this(UserDal.SelectUsernameWithPassword(username, password))
        {

        }
        /// <summary>
        /// returns whether a user is or was in a game with game id
        /// </summary>
        public bool IsUserInGame(int gameID)
        {
            return DAL.UsersInGamesDal.IsUserInGame(username, gameID);
        }
        /// <summary>
        /// tries to send a friend request and returns a textual response
        /// </summary>
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
        /// <summary>
        /// tries to accept a friend request from user with username
        /// </summary>
        public void AcceptFriendRequestFrom(string username)
        {
            if (UnopenedFriendRequests.Contains(username))
                FriendsDal.ChangeStatus(username, this.username, (int)FriendRequestStatus.Accepted);
            else
                throw new Exception("You Have No Friend Invitation From This Friend");
        }
        /// <summary>
        /// tries to decline a friend request from user with username
        /// </summary>
        public void DeclineFriendRequestFrom(string username)
        {
            if (UnopenedFriendRequests.Contains(username))
                FriendsDal.ChangeStatus(username, this.username, (int)FriendRequestStatus.Declined);
            else
                throw new Exception("You Have No Friend Invitation From This Friend");
        }
        /// <summary>
        /// user constructor which inserts a new user into user table using details
        /// </summary>
        public User(string username, UserType type, string email, DateTime BirthDate, string fName, string lName,string password)
        {
            _level = 1;
            _xp = 0;
            this.BirthDate = BirthDate;
            this.fName = fName;
            this.lName = lName;
            this.type = type;
            this.username = username;
            if(BLL_Helper.UserExists(username))
            {
                throw new Exception("A user with that username already exists");
            }
            DAL.UserDal.AddUser(email, password, (int)type, fName, lName, BirthDate,username);
            RankingHistoryDal.InsertRankHistory(username, BLL_Helper.GetCurrentSeason().SeasonID,InitailElo);
        }
        /// <summary>
        /// user constructor that uses data row as data source
        /// </summary>
        public User(DataRow dr)
        {
            _username = dr[UserDal.USERNAMEFLD].ToString();
            type = (UserType)dr[UserDal.TYPEFLD];
            email = dr[UserDal.EMAILFLD].ToString();
            BirthDate = (DateTime)dr["Date Of Birth"];
            _level = (int)dr["Level"];
            _xp = (int)dr[UserDal.XPFLD];
            fName = dr["First Name"].ToString();
            lName = dr["Last Name"].ToString();
            _picture = dr[UserDal.PICTUREFLD].ToString();
        }
        /// <summary>
        /// statistics of user in game
        /// </summary>
        public UserStatsInGame statsInGame(int gameID)
        {
            return new UserStatsInGame(username, gameID);
        }
        /// <summary>
        /// returns whether user equals to other user, using users' usernames
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is User && ((User)obj).username==this.username;
        }
        /// <summary>
        /// to string method, returns username
        /// </summary>
        public override string ToString()
        {
            return username;
        }
        /// <summary>
        /// returns how many games this user has won
        /// </summary>
        public int HowManyGamesWon()
        {
            return UsersInGamesDal.HowManyGamesUserWonOrLost(username,true);
        }
        /// <summary>
        /// returns how many games this user has lost
        /// </summary>
        public int HowManyGamesLost()
        {
            return UsersInGamesDal.HowManyGamesUserWonOrLost(username, false);
        }
    }
}
