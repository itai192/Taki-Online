﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class User
    {
        public int id { get; }
        public bool isManager { get; }
        public string email { get; }
        public User(string email, string password)
        {
            this.email = email;
        }
    }
}
