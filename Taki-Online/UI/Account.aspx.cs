using System;
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
        private List<string> DeclinedFriends;
        private List<string> UnopenedFriendRequests;
        private List<string> AcceptedFriends;
        private List<string> UnopenedSentFriendRequests;
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
            UnopenedFriendRequests = user.UnopenedFriendRequests;
            AcceptedFriends = user.AcceptedFriends;
            DeclinedFriends = user.DeclinedFriends;
            UnopenedSentFriendRequests = user.UnopenedSentFriendRequests;
            Levellbl.Text = user.level.ToString();
            XpBar.outOf = user.XPUntilNextLevel();
            XpBar.progress = user.xp;
            EloLbl.Text = user.elo.ToString();
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            UsernameLbl.Text = user.username;
            ProfilePicture.pictureName = user.picture;
            FriendRequests.DataSource = BLL_Helper.UserListFromUsernameList(user.UnopenedFriendRequests);
            FriendRequests.DataBind();
            Friends.DataSource = BLL_Helper.UserListFromUsernameList(user.AcceptedFriends);
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
        

        protected void GridSearchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                User us = (User)e.Row.DataItem;
                Button button = (Button)e.Row.FindControl("AddFriendBtn");
                if (AcceptedFriends.Contains(us.username) || UnopenedFriendRequests.Contains(us.username)||UnopenedSentFriendRequests.Contains(us.username))
                {
                    button.Enabled = false;
                }
        }
    }

        protected void SearchFriendsBtn_Click(object sender, EventArgs e)
        {
            ViewState["SearchTerm"] = FindFriendTxtBox.Text;
            UserSearchBind();
        }

        

        protected void UserSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UserSearch.PageIndex=e.NewPageIndex;
            UserSearchBind();
        }
        protected void UserSearchBind()
        {
            List<User> results = BLL_Helper.SearchUser(ViewState["SearchTerm"].ToString());
            results.Remove(user);
            UserSearch.DataSource = results;
            UserSearch.DataBind();
        }

        protected void UserSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddFriend")
                user.AddFriend(e.CommandArgument.ToString());
            UnopenedSentFriendRequests = user.UnopenedSentFriendRequests;
            UserSearchBind();
        }
    }
}