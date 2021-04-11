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
        //first name of user
        public string firstName{get; set;}
        //last name of user
        public string lastName{get; set;}
        //username of user
        public string username { get; set; }
        //email of user
        public string email { get; set; }
        //rank name of user
        public string rank{get; set;}
        //level of user
        public int level{get; set;}
        //xp of user
        public int xp{get; set;}
        //elo of user
        public int elo { get; set; }
        //birth date of users
        public DateTime birthDate{get; set;}
        /// <summary>
        /// empty constructor
        /// </summary>
        public WSUser()
        { 
        }
        /// <summary>
        /// constructor using user
        /// </summary>
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
        /// <summary>
        /// constructor using username
        /// </summary>
        public WSUser(string username) : this(new User(username))
        { }
    }
}