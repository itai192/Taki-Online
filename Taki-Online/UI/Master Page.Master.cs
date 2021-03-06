﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace UI
{
    public partial class Master_Page : System.Web.UI.MasterPage
    {
        delegate void command();
        protected void Page_Init(object sender, EventArgs e)
        {
            TableRow up = Table1.Rows[0];
            if (Session["User"] is User)
            {
                User u = (User)Session["User"];
                up.Cells.Add(CreateLinkCell(@"..\Ladders.aspx", "Ladders"));
                up.Cells.Add(CreateLinkCell(@"..\Play.aspx", "Play!"));
                if (u.type==UserType.Manager)
                {
                    up.Cells.Add(CreateLinkCell(@"..\Manager.aspx", "Manager"));
                }
                up.Cells.Add(CreateLinkCell(@"..\Account.aspx", "Account"));
                up.Cells.Add(CreateButtonCell(LogOut, "Log Out"));
                Username.Text = u.username;
                ProfilePicture.pictureName = u.picture;
                Userpanel.Visible = true;
            }
            else
            {
                up.Cells.Add(CreateLinkCell(@"..\Sign up.aspx", "Sign up"));
                up.Cells.Add(CreateLinkCell(@"..\Sign in.aspx", "Sign in"));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected TableCell CreateLinkCell(string link,string text)
        {
            TableCell cell = new TableCell();
            Button btn = new Button();
            btn.PostBackUrl = link;
            btn.Text = text;
            cell.Controls.Add(btn);
            return cell;
        }
        protected TableCell CreateButtonCell(EventHandler cmd, string text)
        {
            TableCell cell = new TableCell();
            Button btn = new Button();
            btn.Text = text;
            btn.Click += cmd;
            cell.Controls.Add(btn);
            return cell;
        }
        protected void LogOut(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(@"~\Home.aspx");
        }
    }
}