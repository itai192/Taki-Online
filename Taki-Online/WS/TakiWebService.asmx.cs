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
        
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
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
        [WebMethod(enableSession: true)]
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

    }
}
