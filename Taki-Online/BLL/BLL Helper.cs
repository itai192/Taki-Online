using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
namespace BLL
{
    public static class BLL_Helper
    {
        /// <summary>
        /// returns a list of how many days were played in the last 30 days, where index 0 is today and 29 is 29 days ago
        /// </summary>
        public static List<int> GamesPlayedInMonth()
        {
            List<int> Games = new List<int>();
            for (int i = 0; i < 30; i++)
            {
                DateTime today = DateTime.Today.Subtract(TimeSpan.FromDays(i));
                DateTime tommorow = today.AddDays(1);
                Games.Add(GameDal.GamesPlayedBetweenDates(today,tommorow));
            }
            return Games;
        }
        /// <summary>
        /// gets a season object describing current season
        /// </summary>
        public static Season GetCurrentSeason()
        {
            return new Season(SeasonsDal.GetCurrentSeason());
        }
        /// <summary>
        /// Gets a datatable of all players whose rank this season is corresponding to the rank id
        /// </summary>
        public static DataTable GetAllPlayersInRankThisSeasonDataTable(int rankID)
        {
            return UsersInGamesDal.FindAllPlayersInRankInSeason(rankID, DAL.SeasonsDal.GetCurrentSeason());
        }
        /// <summary>
        /// gets all rank objects
        /// </summary>
        public static List<Rank> GetAllRanks()
        {
            DataTable dt = RankDal.GetAllRanks();
            List<Rank> ranks = new List<Rank>();
            foreach (DataRow dr in dt.Rows)
            {
                ranks.Add(new Rank(dr));
            }
            return ranks;
        }
        /// <summary>
        /// Sets the source and provider of the dal helper
        /// </summary>
        public static void SetSourceAndProvider(string source, string provider)
        {
            DalHelper.SetProvider(provider);
            DalHelper.SetSource(source);
        }
        /// <summary>
        /// Creates the db helper needed in dal helper
        /// </summary>
        public static void CreateDBHelperInDalHelper(string source, string provider)
        {
            SetSourceAndProvider(source, provider);
            DalHelper.CreateDBHelper();
        }
        /// <summary>
        /// a method which takes a data table and field in that table, and turns all values in that field into a list of type T
        /// </summary>
        public static List<T> DataTableToList<T>(DataTable dt, string field)
        {
            List<T> l = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                l.Add(dr.Field<T>(field));
            }
            return l;
        }
        /// <summary>
        /// a method which takes two lists of type T and unites them into one list
        /// </summary>
        public static List<T> UniteLists<T>(List<T> l1, List<T> l2)
        {
            List<T> l = new List<T>();
            l.AddRange(l1);
            l.AddRange(l2);
            return l;
        }
        /// <summary>
        /// a method which checks and returns whether a user exists
        /// </summary>
        public static bool UserExists(string username)
        {
            return UserDal.ExistUsername(username);
        }
        /// <summary>
        /// A method which turns a data table of user details and turns it into a list of user objects
        /// </summary>
        public static List<User> UserListFromDataTable(DataTable dt)
        {
            return UserListFromUsernameList(DataTableToList<string>(dt, DAL.UserDal.USERNAMEFLD));
        }
        /// <summary>
        /// A methods which creates and returns a user list from a username list
        /// </summary>
        public static List<User> UserListFromUsernameList(List<string> usernames)
        {
            List<User> ret = new List<User>();
            foreach (string name in usernames)
            {
                ret.Add(new User(name));
            }
            return ret;
        }
        /// <summary>
        /// a method which finds and returns a list of all users which contain the search term
        /// </summary>
        public static List<User> SearchUser(string searchTerm)
        {
            return UserListFromDataTable(DAL.UserDal.SearchUsername(searchTerm));
        }
        /// <summary>
        /// a method which checks and returns if there exists a game with a name that is in the starting status
        /// </summary>
        public static bool IsGameWithNameStarting(string name)
        {
            return DAL.GameDal.IsGameExistWithNameAndActivity(name, (int)GameStatus.Starting);
        }
        /// <summary>
        /// a methods which finds and returns a list of all game rooms with starting status which their name contain the search term
        /// </summary>
        public static List<GameRoom> SearchStartingGameRoom(string searchTerm)
        {
            return GameRoomTableToGameRoomList(GameDal.SearchGame(searchTerm, (int)GameStatus.Starting));
        }
        /// <summary>
        /// A method which takes a data table of game rooms and returns a list of the game rooms as objects
        /// </summary>
        public static List<GameRoom> GameRoomTableToGameRoomList(DataTable dt)
        {
            List<GameRoom> ret = new List<GameRoom>();
            foreach(DataRow dr in dt.Rows)
            {
                ret.Add(new GameRoom(dr));
            }
            return ret;
        }
        /// <summary>
        /// a method which returns the statitics object of a user in a game
        /// </summary>
        public static List<UserStatsInGame> FindUserStatsInGame(int gameID)
        {
            DataTable dt = UsersInGamesDal.FindUsersInGame(gameID);
            List<UserStatsInGame> ret = new List<UserStatsInGame>();
            foreach(DataRow dr in dt.Rows)
            {
                ret.Add(new UserStatsInGame(dr));
            }
            return ret;
        }
        /// <summary>
        /// gets all players whose rank this season is corresponding to the rank id
        /// </summary>
        public static List<User> GetAllUsersInRankThisSeason(int rankID)
        {
            List<string> usernames = DataTableToList<string>(GetAllPlayersInRankThisSeasonDataTable(rankID),"User");
            List<User> users = new List<User>();
            foreach(string username in usernames)
            {
                users.Add(new User(username));
            }
            return users;
        }
        /// <summary>
        /// gets the avarage age of a user
        /// </summary>
        public static int GetAvarageAge()
        {
            return (int)(DateTime.Today - UserDal.AvarageBirthDate()).TotalDays / 365;
        }
        public static int GetUserAmount()
        {
            return UserDal.UserCount();
        }
    }
}
