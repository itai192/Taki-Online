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
        public const string RECIPIANTFLD = "Recipient", SENDERFLD = "Sender", GAMEIDFLD="[Game ID]";
        public static void CreateGameInvite(string sender, string recipiant, int gameID)
        {
            DalHelper.insertWithoutCreatingID(DalHelper.SimpleInsertQuery(Constants.FRIENDREQUESTSTBL, new string[] { RECIPIANTFLD, SENDERFLD, GAMEIDFLD }, new string[] { $"'{recipiant}'", $"'{sender}'", gameID.ToString() }));
        }
        public static DataTable FindInvitesToRecipiantWhereGameWithActivity(string recipiant, int activity )
        {
            return DalHelper.SelectTable($"SELECT {Constants.GAMEINVITESTBL}.* FROM Games INNER JOIN [Game Invitations] ON Games.[Game ID] = [Game Invitations].[Game ID] WHERE Games.Activity = {activity}");
        }
    }
}
