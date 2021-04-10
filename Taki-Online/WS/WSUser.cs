using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System.Web.Services;
namespace WS
{
    public class WSUser
    {
        
        [System.Xml.Serialization.SoapElement("FirstName")]
        [DisplayName("First Name")]
        public string firstName{get; set;}
        [DisplayName("Last Name")]
        public string lastName{get; set;}
        [DisplayName("Username")]
        public string username { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("Rank")]
        public string rank{get; set;}
        public int level{get; set;}
        [DisplayName("Level")]
        public int xp{get; set;}
        [DisplayName("Elo")]
        public int elo{get; set;}
        [DisplayName("Birth Date")]
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