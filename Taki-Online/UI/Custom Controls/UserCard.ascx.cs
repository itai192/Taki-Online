using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI.Custom_Controls
{
    public partial class UserCard : System.Web.UI.UserControl
    {
        private User _user;

        public User user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                UpdateUser();
            }
        }
        public void UpdateUser()
        {
            ProfilePicture.pictureName = _user.picture;
            UsernameLbl.Text = user.username;
            LevelLbl.Text = user.level.ToString();
            RankLbl.Text = user.rank.name;
        }
    }
}