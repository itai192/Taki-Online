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
        public static Season GetCurrentSeason()
        {
            return new Season(SeasonsDal.GetCurrentSeason());
        }
        public static DataTable GetAllPlayersInRankThisSeason(int rankID)
        {
            return UsersInGamesDal.FindAllPlayersInRankInSeason(rankID, DAL.SeasonsDal.GetCurrentSeason());
        }
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
        public static void SetSourceAndProvider(string source, string provider)
        {
            DalHelper.SetProvider(provider);
            DalHelper.SetSource(source);
        }
        public static void CreateDBHelperInDalHelper(string source, string provider)
        {
            SetSourceAndProvider(source, provider);
            DalHelper.CreateDBHelper();
        }
        public static List<T> DataTableToList<T>(DataTable dt, string field)
        {
            List<T> l = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                l.Add(dr.Field<T>(field));
            }
            return l;
        }
        public static List<T> UniteLists<T>(List<T> l1, List<T> l2)
        {
            List<T> l = new List<T>();
            l.AddRange(l1);
            l.AddRange(l2);
            return l;
        }
        public static bool UserExists(string username)
        {
            return UserDal.ExistUsername(username);
        }
        public static List<User> UserListFromDataTable(DataTable dt)
        {
            return UserListFromUsernameList(DataTableToList<string>(dt, DAL.UserDal.USERNAMEFLD));
        }
        public static List<User> UserListFromUsernameList(List<string> usernames)
        {
            List<User> ret = new List<User>();
            foreach (string name in usernames)
            {
                ret.Add(new User(name));
            }
            return ret;
        }
        public static List<User> SearchUser(string searchTerm)
        {
            return UserListFromDataTable(DAL.UserDal.SearchUsername(searchTerm));
        }
        public static bool IsGameWithNameStarting(string name)
        {
            return DAL.GameDal.IsGameExistWithNameAndActivity(name, (int)GameStatus.Starting);
        }
        public static List<GameRoom> SearchStartingGameRoom(string searchTerm)
        {
            return GameRoomTableToGameRoomList(GameDal.SearchGame(searchTerm, (int)GameStatus.Starting));
        }
        public static List<GameRoom> GameRoomTableToGameRoomList(DataTable dt)
        {
            List<GameRoom> ret = new List<GameRoom>();
            foreach(DataRow dr in dt.Rows)
            {
                ret.Add(new GameRoom(dr));
            }
            return ret;
        }
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
        public static List<User> GetAllUsersInRankThisSeason(int rankID)
        {
            List<string> usernames = DataTableToList<string>(GetAllPlayersInRankThisSeason(rankID),"User");
            List<User> users = new List<User>();
            foreach(string username in usernames)
            {
                users.Add(new User(username));
            }
            return users;
        }
        public static int GetAvarageAge()
        {
            return (int)(DateTime.Today - UserDal.AvarageBirthDate()).TotalDays / 365;
        }
    }
}
