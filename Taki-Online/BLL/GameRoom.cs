using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class GameRoom
    {
        private string host;
        private string gameName;
        private GameStatus status;
        private int GameID;

        public GameRoom(User host, string gameName)
        {
            if(BLL_Helper.IsGameWithNameStarting(gameName))
            {
                throw new Exception("A game with that name already exists");
            }
            this.host = host.username;
            this.gameName = gameName;
            status = GameStatus.Starting;
            GameID = DAL.GameDal.AddGame(gameName,this.host,(int)status);
        }
    }
}
