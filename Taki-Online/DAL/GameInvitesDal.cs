using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public class GameInvitesDal
    {
        //field names so it is easier to make queries
        public const string RECIPIANTFLD = "Recipient", SENDERFLD = "Sender", GAMEIDFLD="[Game ID]";
        /// <summary>
        /// inserts a new game invitaition to recipiant from sender to game with game id
        /// </summary>
        public static void CreateGameInvite(string sender, string recipiant, int gameID)
        {
            DalHelper.insertWithoutCreatingID(DalHelper.SimpleInsertQuery(Constants.GAMEINVITESTBL, new string[] { RECIPIANTFLD, SENDERFLD, GAMEIDFLD }, new string[] { $"'{recipiant}'", $"'{sender}'", gameID.ToString() }));
        }
        /// <summary>
        /// finds game invitations to recipiant where game has specific activity status
        /// </summary>
        public static DataTable FindInvitesToRecipiantWhereGameWithActivity(string recipiant, int activity )
        {
            return DalHelper.SelectTable($"SELECT {Constants.GAMEINVITESTBL}.* FROM Games INNER JOIN [Game Invitations] ON Games.[Game ID] = [Game Invitations].[Game ID] WHERE Games.Activity = {activity}");
        }
    }
}
