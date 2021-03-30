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
        private string host;
        private string gameName;
        private GameStatus status;
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
        public GameRoom(User host, string gameName)
        {
            if(BLL_Helper.IsGameWithNameStarting(gameName))
            {
                throw new Exception("A game with that name already exists");
            }
            this.host = host.username;
            this.gameName = gameName;
            status = GameStatus.Starting;
            GameID = GameDal.AddGame(gameName,this.host,(int)status);
            game = new Game();
        }
        public GameRoom(int ID)
        {
            GameID = ID;
            UpdateObject();
        }
        public void UpdateObject()
        {
            DataRow dr = GameDal.FindGameByID(GameID);
            gameName = dr[GameDal.GAMENAMEFLD].ToString();
            status = (GameStatus)dr[GameDal.ACTIVITFLD];
            host = dr[GameDal.HOSTFLD].ToString();
        }
    }
}
