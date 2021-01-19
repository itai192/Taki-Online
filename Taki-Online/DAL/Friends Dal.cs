using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public class Friends_Dal
    {
        public const string SENDERFLD = "Sender", RECIPIANTFLD = "Recipiant", REQUESTSTATUSEFLD = "[Request status]", TIMESENTFLD = "[Time Sent]";
        
        public static bool FriendRequestExists(string username1, string username2)
        {
            return DalHelper.IsExist($"SELECT * FROM {Constants.FRIENDREQUESTSTBL} WHERE ({RECIPIANTFLD} = '{username1}' AND {SENDERFLD} = '{username2}') OR ({RECIPIANTFLD} = '{username2}' AND {SENDERFLD} = '{username1}')");
        }
        public static void AddFriend(string sender, string recipiant)
        {
            DalHelper.insertWithoutCreatingID(DalHelper.SimpleInsertQuery(Constants.FRIENDREQUESTSTBL, new string[] { SENDERFLD, RECIPIANTFLD }, new string[] { "'" + sender + "'", "'" + recipiant + "'" }));
        }
        public static void ChangeStatus(string sender, string recipiant, int status)
        {
            if (DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.FRIENDREQUESTSTBL, new string[] { REQUESTSTATUSEFLD }, new string[] { status.ToString() }, $"{RECIPIANTFLD}='{recipiant}' AND {SENDERFLD}='{sender}'")) == 0)
                throw new Exception("Didn't Change any status");
        }
        static readonly string FriendRequestSQL = $"SELECT * FROM " + Constants.FRIENDREQUESTSTBL;
        public static DataTable AllFriendRequestsOfUser(string username)
        {
            return DalHelper.SelectTable(FriendRequestSQL+$" WHERE {RECIPIANTFLD} = '{username}' OR {SENDERFLD} = '{username}'") ;
        }
        public static DataTable AllSentFriendRequestsOfUser(string username)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {SENDERFLD} = '{username}'");
        }
        public static DataTable AllRecievedFriendRequestsOfUser(string username)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {RECIPIANTFLD} = '{username}'");
        }
        public static DataTable FriendRequestsWithStatus(string username, int status)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {REQUESTSTATUSEFLD} = {status} AND ({RECIPIANTFLD} = '{username}' OR {SENDERFLD} = '{username}')");
        }
        public static DataTable FriendRequestsWithStatusSent(string username, int status)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {REQUESTSTATUSEFLD} = {status} AND {SENDERFLD} = '{username}'");
        }
        public static DataTable FriendRequestsWithStatusRecieved(string username, int status)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {REQUESTSTATUSEFLD} = {status} AND {RECIPIANTFLD} = '{username}'");
        }
        //public static DataTable
    }
}
