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
        [WebMethod(enableSession:true)]
        public string AddFriend(WSUser user)
        {
            if (Session["User"] == null)
                return "You are not connected";
            User ThisUser = (User)Session["User"];
            return ThisUser.AddFriend(user.username);
        }
        [WebMethod(enableSession: true)]
        public bool AcceptFriend(WSUser user)
        {
            if (Session["User"] == null)
                return false;
            try
            {
                User ThisUser = (User)Session["User"];
                ThisUser.AcceptFriendRequestFrom(user.username);
                return true;
            }
            catch
            {
                return false;
            }
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
            return ret;
        }
        [WebMethod]
        public List<WSUser> GetAllUsersInRank(WSRank rank)
        {
            List<User> users = BLL_Helper.GetAllUsersInRankThisSeason(rank.ID);
            List<WSUser> wsUsers = new List<WSUser>();
            foreach(User user in users)
            {
                wsUsers.Add(new WSUser(user));
            }
            return wsUsers; 
        }
    }
}
