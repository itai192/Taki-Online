using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI;
using BLL;
namespace UI.Custom_Controls
{
    public partial class ProfilePictureUpload : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void uploadPhoto()
        {
            if(ProfilePictureFileUpload.HasFile&& Session["User"] is User)
            {
                User user = (User)Session["User"];
                string picture = UIHelper.SavePhoto(ProfilePictureFileUpload.PostedFile, user.username );
                user.picture = picture;
            }
        }
    }
}