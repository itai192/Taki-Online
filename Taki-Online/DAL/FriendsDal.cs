using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public class FriendsDal
    {
        //field names so it is easier to make queries
        public const string SENDERFLD = "Sender", RECIPIANTFLD = "Recipiant", REQUESTSTATUSEFLD = "[Request status]", TIMESENTFLD = "[Time Sent]";
        //a friend request query begginning that was useful to make a constant (readonly)
        public static readonly string FriendRequestSQL = $"SELECT * FROM " + Constants.FRIENDREQUESTSTBL;
        /// <summary>
        /// return's whether a friend request between two users exists
        /// </summary>
        public static bool FriendRequestExists(string username1, string username2)
        {
            return DalHelper.IsExist($"SELECT * FROM {Constants.FRIENDREQUESTSTBL} WHERE ({RECIPIANTFLD} = '{username1}' AND {SENDERFLD} = '{username2}') OR ({RECIPIANTFLD} = '{username2}' AND {SENDERFLD} = '{username1}')");
        }
        /// <summary>
        /// Creates a new friend request from sender to recipiant
        /// </summary>
        public static void AddFriend(string sender, string recipiant)
        {
            DalHelper.insertWithoutCreatingID(DalHelper.SimpleInsertQuery(Constants.FRIENDREQUESTSTBL, new string[] { SENDERFLD, RECIPIANTFLD }, new string[] { "'" + sender + "'", "'" + recipiant + "'" }));
        }
        /// <summary>
        /// changes the status of a friend request between sender and recipiant
        /// </summary>
        public static void ChangeStatus(string sender, string recipiant, int status)
        {
            if (DalHelper.Update(DalHelper.SimpleUpdateQuery(Constants.FRIENDREQUESTSTBL, new string[] { REQUESTSTATUSEFLD }, new string[] { status.ToString() }, $"{RECIPIANTFLD}='{recipiant}' AND {SENDERFLD}='{sender}'")) == 0)
                throw new Exception("Didn't Change any status");
        }
        /// <summary>
        /// returns all friend request records assosiated with a user
        /// </summary>
        public static DataTable AllFriendRequestsOfUser(string username)
        {
            return DalHelper.SelectTable(FriendRequestSQL+$" WHERE {RECIPIANTFLD} = '{username}' OR {SENDERFLD} = '{username}'") ;
        }
        /// <summary>
        /// returns all records of friend requests that a user has sent
        /// </summary>
        public static DataTable AllSentFriendRequestsOfUser(string username)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {SENDERFLD} = '{username}'");
        }
        /// <summary>
        /// returns all records of friend requests that a user has recieved
        /// </summary>
        public static DataTable AllRecievedFriendRequestsOfUser(string username)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {RECIPIANTFLD} = '{username}'");
        }
        /// <summary>
        /// returns all records of friend requests with specific status a user is assosiated with
        /// </summary>
        public static DataTable FriendRequestsWithStatus(string username, int status)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {REQUESTSTATUSEFLD} = {status} AND ({RECIPIANTFLD} = '{username}' OR {SENDERFLD} = '{username}')");
        }
        /// <summary>
        ///  returns all records of friend requests with specific status a user has sent
        /// </summary>
        public static DataTable FriendRequestsWithStatusSent(string username, int status)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {REQUESTSTATUSEFLD} = {status} AND {SENDERFLD} = '{username}'");
        }
        /// <summary>
        ///  returns all records of friend requests with specific status a user has recieved
        /// </summary>
        public static DataTable FriendRequestsWithStatusRecieved(string username, int status)
        {
            return DalHelper.SelectTable(FriendRequestSQL + $" WHERE {REQUESTSTATUSEFLD} = {status} AND {RECIPIANTFLD} = '{username}'");
        }
    }
}
