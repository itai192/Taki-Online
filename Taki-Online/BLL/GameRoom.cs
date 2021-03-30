using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
namespace BLL
{
    class GameRoom
    {
        private static Dictionary<int,Game> games = new Dictionary<int,Game>();
        private string host;
        public string _gameName;
        private GameStatus status;
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
            this.host = host.username;
            this._gameName = gameName;
            status = GameStatus.Starting;
            GameID = GameDal.AddGame(gameName,this.host,(int)status);
            game = new Game();
            games.Add(GameID, game);
        }
        public GameRoom(int ID)
        {
            GameID = ID;
            UpdateObject();
            game = games[GameID];
        }
        public void UpdateObject()
        {
            DataRow dr = GameDal.FindGameByID(GameID);
            _gameName = dr[GameDal.GAMENAMEFLD].ToString();
            status = (GameStatus)dr[GameDal.ACTIVITFLD];
            host = dr[GameDal.HOSTFLD].ToString();
        }
    }
}
