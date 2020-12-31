using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
namespace UI.Custom_Controls
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        public string pictureName;
        public Unit height;
        public Unit width;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(pictureName))
            {
                ProfilePicture.ImageUrl = ConfigurationManager.AppSettings["userPics"] + ConfigurationManager.AppSettings["defaultPic"];
            }
            else
            {
                ProfilePicture.ImageUrl = ConfigurationManager.AppSettings["userPics"] + pictureName;
            }
            ProfilePicture.Width = width;
            ProfilePicture.Height = height;
        }
    }
}