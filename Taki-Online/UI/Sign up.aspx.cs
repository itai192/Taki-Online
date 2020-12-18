using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI
{
    public partial class Sign_up : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Validate();
        }
       public void CalanderValidation(object source, ServerValidateEventArgs args)
        {
            if (Calendar.SelectedDate == null|| Calendar.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0))
                args.IsValid = false;
            else
            {
                args.IsValid = true;
            }
        }
        public void signup(object sender, EventArgs e)
        {
            try
            {
                BLL.User u = new User(Username.Text, UserType.User, email.Text, Calendar.SelectedDate, FName.Text, LName.Text,Password.Text);
                //SavePhoto(Photo.PostedFile,);
                Session["User"] = u;
                Response.Redirect(@"~\Home.aspx");
            }
            catch(Exception ex)
            {
                error.Text = "couldn't sign up, " +ex.Message;
            }
        }
        public void SavePhoto(HttpPostedFile file, string name)
        {
            string path = @"";
        }
    }
}