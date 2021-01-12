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
        protected void Page_Init(object sender, EventArgs e)
        {
            
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Home.aspx");
            }
            User user = (User)Session["User"];
            FriendRequests.DataSource=user.UnopenedFriendRequests;
            FriendRequests.DataBind();
            Friends.DataSource = user.AcceptedFriends;
            Friends.DataBind();
            UsernameLbl.Text = user.username;
            Levellbl.Text = user.level.ToString();
            XpBar.progress = user.xp;
            XpBar.outOf = user.XPUntilNextLevel();
            ProfilePicture.pictureName = user.picture;
        }

        protected void FriendRequests_ItemCommand(object source, DataListCommandEventArgs e)
        {
            
        }

        protected void UploadPicBtn_Click(object sender, EventArgs e)
        {
            ProfilePictureUpload.UploadPhoto();
            Response.Redirect("~/Account.aspx");
        }

        
    }
}