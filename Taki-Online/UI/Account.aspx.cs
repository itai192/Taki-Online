﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BLL;
using UI.Custom_Controls;
namespace UI
{
    public partial class Account : System.Web.UI.Page
    {
        private User user;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Home.aspx");
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["User"];
            Levellbl.Text = user.level.ToString();
            XpBar.outOf = user.XPUntilNextLevel();
            XpBar.progress = user.xp;
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            UsernameLbl.Text = user.username;
            ProfilePicture.pictureName = user.picture;
            FriendRequests.DataSource = user.UnopenedFriendRequests;
            FriendRequests.DataBind();
            Friends.DataSource = user.AcceptedFriends;
            Friends.DataBind();
        }

        protected void FriendRequests_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Accept":
                        user.AcceptFriendRequestFrom(e.CommandArgument.ToString());
                        break;
                    case "Decline":
                        user.DeclineFriendRequestFrom(e.CommandArgument.ToString());
                        break;
                }
            }
            catch(Exception ex)
            {
                FriendRequestErrorLbl.Text = ex.Message;
                FriendRequestErrorLbl.Visible = true;
            }
        }
        

        protected void UploadPicBtn_Click(object sender, EventArgs e)
        {
            ProfilePictureUpload.UploadPhoto();
        }

        protected void ChngUsername_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                user.username = ChngUsernameTxt.Text;
                user.picture = UIHelper.RenamePhoto(user.picture, user.username);
            }
        }

        protected void UsernameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (BLL_Helper.UserExists(args.Value))
                args.IsValid = false;
        }
        protected void FriendValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!BLL_Helper.UserExists(args.Value))
                args.IsValid = false;
        }
        protected void AddFriend(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    FriendAddMsg.Text = user.AddFriend(AddFriendTxt.Text);
                }
                catch (Exception ex)
                {
                    FriendAddMsg.Text = ex.Message;
                }
            }
        }

    }
}