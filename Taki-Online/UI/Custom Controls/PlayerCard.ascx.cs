using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI.Custom_Controls
{
    public partial class PlayerCard : System.Web.UI.UserControl
    {
        private SimplePlayer _player;
        public bool isSelected { get; set; } = false;

        public SimplePlayer player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
                UpdateUser();
            }
        }
        public void UpdateUser()
        {
            ProfilePicture.pictureName = _player.user.picture;
            UsernameLbl.Text = player.user.username;
            CardsLbl.Text = player.NumberOfCards.ToString();
            if (isSelected)
            {
                UserPanel.CssClass = "Selected";
            }
            else
            {
                UserPanel.CssClass = null;
            }
        }
    }
}