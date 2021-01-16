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
            FriendRequests.DataSource = user.UnopenedFriendRequests;
            FriendRequests.DataBind();
            Friends.DataSource = user.AcceptedFriends;
            Friends.DataBind();
            Levellbl.Text = user.level.ToString();
            XpBar.outOf = user.XPUntilNextLevel();
            XpBar.progress = user.xp;
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            UsernameLbl.Text = user.username;
            ProfilePicture.pictureName = user.picture;
        }

        protected void FriendRequests_ItemCommand(object source, DataListCommandEventArgs e)
        {
            
        }
        

        protected void UploadPicBtn_Click(object sender, EventArgs e)
        {
            ProfilePictureUpload.UploadPhoto();
        }

        protected void ChngUsername_Click(object sender, EventArgs e)
        {
            user.username = ChngUsernameTxt.Text;
            user.picture = UIHelper.RenamePhoto(user.picture, user.username);
        }

        protected void UsernameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (BLL_Helper.UserExists(args.Value))
                args.IsValid = false;
        }
    }
}