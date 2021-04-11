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
        //dictionary containing all games in the key of their game id
        private static Dictionary<int,Game> games = new Dictionary<int,Game>();
        //the name of the host
        private string _host;
        //object of host
        public User host
        {
            get { return new User(_host); }
        }
        //the name of the game
        private string _gameName;
        //the status of the game
        private GameStatus _status;
        //users in the game
        public List<string> users
        {
            get
            {
                return BLL_Helper.DataTableToList<string>(UsersInGamesDal.FindUsersInGame(GameID),"User");
            }
        }
        //the status of the game
        public GameStatus status
        {
            get { return _status; }
            set
            {
                GameDal.ChangeGameActivity((int)value, GameID);
                if (value == GameStatus.Ended)
                    games.Remove(GameID);
                _status = value;
            }
        }
        //the name of the game
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
        //id of the game        
        public int GameID
        {
            get;
            private set;
        }
        //the game assosiated with the game room object
        private Game game
        {
            get
            {
                if(status==GameStatus.Ended)
                {
                    throw new Exception("this game has already ended");
                }
                try
                {
                    return games[GameID];
                }
                catch
                {
                    games.Add(GameID, new Game(this));
                    return games[GameID];
                }
            }
            set 
            {
                games.Add(GameID, value);
            }
        }
        /// <summary>
        /// adds a user to the game
        /// </summary>
        public Player AddUserToGame(User user)
        {
            UsersInGamesDal.AddUserToGame(user.username, this.GameID);
            return game.AddPlayer(user);
        }
        /// <summary>
        /// validates the name of the game
        /// </summary>
        public void ValidateName(string name)
        {
            if (BLL_Helper.IsGameWithNameStarting(name))
            {
                throw new Exception("A game with that name already exists");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("game needs to have a name");
            }
        }
        /// <summary>
        /// gameroom constructor
        /// </summary>
        public GameRoom(User host, string gameName)
        {
            ValidateName(gameName);
            this._host = host.username;
            this._gameName = gameName;
            _status = GameStatus.Starting;
            GameID = GameDal.AddGame(gameName,this._host,(int)status);
            game = new Game(this);
        }
        /// <summary>
        /// game room constructor which returns an already existing game room by game ID
        /// </summary>
        public GameRoom(int ID)
        {
            GameID = ID;
            try
            {
                UpdateRoom();
            }
            catch
            {
                throw new Exception("This game does not exist");
            }
        }
        /// <summary>
        /// constructs a game room using a data row
        /// </summary>
        public GameRoom(DataRow dr)
        {
            this.GameID = (int)dr["Game ID"];
            UpdateRoom(dr);
        }
        /// <summary>
        /// updates the game room information
        /// </summary>
        public void UpdateRoom()
        {
            DataRow dr = GameDal.FindGameByID(GameID);
            UpdateRoom(dr);
        }
        /// <summary>
        /// updates the game room information using a datarow
        /// </summary>
        public void UpdateRoom(DataRow dr)
        {
            _gameName = dr["Game Name"].ToString();
            status = (GameStatus)dr[GameDal.ACTIVITYFLD];
            _host = dr[GameDal.HOSTFLD].ToString();
        }
    }
}
