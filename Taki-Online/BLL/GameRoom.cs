using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
namespace BLL
{
    public class GameRoom
    {
        private static Dictionary<int,Game> games = new Dictionary<int,Game>();
        private string _host;
        public User host
        {
            get { return new User(_host); }
        }
        public string _gameName;
        private GameStatus _status;
        public List<string> users
        {
            get
            {
                return BLL_Helper.DataTableToList<string>(UsersInGamesDal.FindUsersInGame(GameID),"User");
            }
        }
        public GameStatus status
        {
            get { return _status; }
            set
            {
                GameDal.ChangeGameActivity((int)value, GameID);
                _status = value;
            }
        }
        public string gameName
        {
            get
            {
                return _gameName;
            }
            set
            {
                ValidateName(value);
                GameDal.ChangeGameName(value, GameID);
                _gameName = value;
            }
        }
        
        public int GameID
        {
            get;
            private set;
        }
        private Game game;
        public Player AddUserToGame(User user)
        {
            UsersInGamesDal.AddUserToGame(user.username, this.GameID);
            return game.AddPlayer(user);

        }
        public void ValidateName(string name)
        {
            if (BLL_Helper.IsGameWithNameStarting(gameName))
            {
                throw new Exception("A game with that name already exists");
            }
            if (string.IsNullOrWhiteSpace(gameName))
            {
                throw new Exception("game needs to have a name");
            }
        }
        public GameRoom(User host, string gameName)
        {
            ValidateName(gameName);
            this._host = host.username;
            this._gameName = gameName;
            _status = GameStatus.Starting;
            GameID = GameDal.AddGame(gameName,this._host,(int)status);
            game = new Game();
            games.Add(GameID, game);
        }
        public GameRoom(int ID)
        {
            GameID = ID;
            UpdateRoom();
            if(status!=GameStatus.Ended)
                game = games[GameID];
        }
        public GameRoom(DataRow dr)
        {
            this.GameID = (int)dr["Game ID"];
            UpdateObject(dr);
            //if (status != GameStatus.Ended)
            //    game = games[GameID];
            //to fix
        }
        public void UpdateRoom()
        {
            DataRow dr = GameDal.FindGameByID(GameID);
            UpdateObject(dr);
        }
        public void UpdateObject(DataRow dr)
        {
            _gameName = dr["Game Name"].ToString();
            status = (GameStatus)dr[GameDal.ACTIVITYFLD];
            _host = dr[GameDal.HOSTFLD].ToString();
        }
    }
}
