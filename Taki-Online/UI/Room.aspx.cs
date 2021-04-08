using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI
{
    public partial class Room : System.Web.UI.Page
    {
        public User user;
        public GameRoom room;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Home.aspx");
            }
            user = (User)Session["User"];
            try
            {
                room = new GameRoom(int.Parse(Request.QueryString["gameId"]));
                if(room.status!=GameStatus.Starting&& !IsPostBack)
                {
                    throw new Exception("Can't join this game");
                }
            }
            catch
            {
                Response.Redirect("~/Home.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["Player"] == null)
            {
                Session["Player"] = room.AddUserToGame(user);
            }
            if(!user.Equals(room.host)||room.users.Count<2)
            {
                HostPanel.Visible = false;
            }
            else
            {
                HostPanel.Visible = true;
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            Update();
        }
        protected void Update()
        {
            room.UpdateRoom();
            if(room.status==GameStatus.AlreadyStarted)
            {
                Response.Redirect("~/Game.aspx");
            }
            Players.DataSource = BLL_Helper.UserListFromUsernameList(room.users);
            Players.DataBind();
            FriendsToInvite.DataSource = BLL_Helper.UserListFromUsernameList(user.AcceptedFriends);
            FriendsToInvite.DataBind();
        }

        protected void FriendsToInvite_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            User friend = (User)e.Item.DataItem;
            if (room.users.Contains(friend.username))
            {
                Button invitebtn=(Button)e.Item.FindControl("InviteBtn");
                invitebtn.Enabled = false;
            }
        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            room.status = GameStatus.AlreadyStarted;
        }

        protected void FriendsToInvite_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Invite")
            {
                string friend = e.CommandArgument.ToString();
                try
                {
                    Player player = (Player)Session["Player"];
                    player.invitePlayer(friend);
                }
                catch(Exception ex)
                {
                    errorLbl.Text = ex.Message;
                }
            }
        }
    }
}