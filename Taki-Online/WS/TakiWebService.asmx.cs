using BLL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Services;
namespace WS
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TakiWebService : System.Web.Services.WebService
    {
        [WebMethod(enableSession:true)]
        public bool SignIn(string username, string password)
        {
            
            try
            {
                Session["User"] = new User(username, password);
                return true;
            }
            catch
            {
                return false;
            }
        }
        [WebMethod]
        public bool SignUp(string username, string password, string email, DateTime birthDate, string fName, string lName)
        {
            Regex regEmail = new Regex(@"^[^@]+@[^\.]+\..+$");
            Regex regWord = new Regex(@"^\w+$");
            Regex regPass = new Regex(@"[\S]{5,}");
            if (!regWord.IsMatch(username) || !regWord.IsMatch(fName) || !regWord.IsMatch(lName) || !regEmail.IsMatch(email) || !regPass.IsMatch(password) || DateTime.Today - birthDate.AddYears(10) <= TimeSpan.Zero)
            {
                return false;
            }
            try
            {
                User user = new User(username, UserType.User, email, birthDate, fName, lName, password);
                return true;
            }
            catch
            {
                return false;
            }
        }
        [WebMethod(enableSession: true)]
        public List<string> GetFriendNames()
        {
            if (Session["User"] == null)
                return null;
            User user = (User)Session["User"];
            return user.AcceptedFriends;
        }
        [WebMethod]
        public WSUser GetUserDetails(string username)
        {
            try
            {
                return new WSUser(username);
            }
            catch
            {
                return null;
            }
        }
        [WebMethod(enableSession: true)]
        public List<WSUser> GetFriends()
        {
            if(Session["User"]==null)
            {
                return null;
            }
            User user = (User)Session["User"];
            return UserListToWSUserList(BLL_Helper.UserListFromUsernameList(user.AcceptedFriends));
        }
        [WebMethod]
        public List<WSRank> GetAllRanks()
        {
            List<WSRank> ret = new List<WSRank>();
            List<Rank> ranks = BLL_Helper.GetAllRanks();
            foreach(Rank rank in ranks)
            {
                ret.Add(new WSRank(rank));
            }
            return RankListToWSRankList(BLL_Helper.GetAllRanks());
        }
        [WebMethod]
        public List<WSUser> GetAllUsersInRank(WSRank rank)
        {
            return UserListToWSUserList(BLL_Helper.GetAllUsersInRankThisSeason(rank.ID)); ; 
        }
        [WebMethod(enableSession:true)]
        public bool IsSessionConnected()
        {
            return Session["User"] != null;
        }
        [WebMethod(enableSession:true)]
        public WSUser GetConnectedUser()
        {
            if(Session["User"]==null)
            {
                return null;
            }
            return new WSUser((User)Session["User"]);
        }
        private static List<WSUser> UserListToWSUserList(List<User> users)
        {
            List<WSUser> ret = new List<WSUser>();
            foreach(User user in users)
            {
                ret.Add(new WSUser(user));
            }
            return ret;
        }
        private static List<WSRank> RankListToWSRankList(List<Rank> ranks)
        {
            List<WSRank> ret = new List<WSRank>();
            foreach (Rank rank in ranks)
            {
                ret.Add(new WSRank(rank));
            }
            return ret;
        }
        
    }
}
