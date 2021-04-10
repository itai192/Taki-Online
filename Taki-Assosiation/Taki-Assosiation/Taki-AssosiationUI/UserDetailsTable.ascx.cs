﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taki_AssosiationUI.WebService;
using System.Reflection;
namespace Taki_AssosiationUI
{
    public partial class UserDetailsTable : System.Web.UI.UserControl
    {
        private WSUser _user;
        public WSUser user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                LoadUser();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void LoadUser()
        {
            Table tbl = new Table();
            Type user = _user.GetType();
            PropertyInfo[] properties = user.GetProperties();
            foreach(PropertyInfo prop in properties)
            {
                TableRow row = new TableRow();
                row.Cells.Add(UIHelper.LableTableCell(prop.Name));
                //if date
                if(prop.PropertyType.IsEquivalentTo(typeof(DateTime)))
                {
                    DateTime date = (DateTime)prop.GetValue(user);
                    row.Cells.Add(UIHelper.LableTableCell(date.ToShortDateString()));
                }
                else
                {
                    row.Cells.Add(UIHelper.LableTableCell(prop.GetValue(user).ToString()));
                }
                tbl.Rows.Add(row);
            }
            Controls.Add(tbl);
        }
        
    }
}