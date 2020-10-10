using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public enum UserType
    {
        User,
        Manager
    }
    public enum GameType
    {
        Competetive,
        Casual,
        Custom,
        PrivateCustom
    }
    public enum GameStatus
    {
        Starting,
        AlreadyStarted,
        Ended
    }
    public enum FriendRequestStatus
    {
        Unopened,
        Accepted,
        Declined
    }
    
}
