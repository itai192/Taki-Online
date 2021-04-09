using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
namespace WS
{
    public class WSUser
    {
        public string firstName{get; set;}
        public string lastName{get; set;}
        public string username{get; set;}
        public string email{get; set;}
        public string rank{get; set;}
        public int level{get; set;}
        public int xp{get; set;}
        public int elo{get; set;}
        public DateTime birthDate{get; set;}
        public WSUser()
        { 
        }

        public WSUser(User user)
        {
            firstName = user.fName;
            lastName = user.lName;
            username = user.username;
            email = user.email;
            rank = user.rank.name;
            level = user.level;
            xp = user.level;
            elo = user.elo;
            birthDate = user.BirthDate;
        }
        public WSUser(string username) : this(new User(username))
        { }
    }
}