using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;
namespace UI
{
    public partial class Sign_up : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BuildYearSelect();
            }
            Validate();
        }
       public void CalanderValidation(object source, ServerValidateEventArgs args)
        {
            if (Calendar.SelectedDate == null|| Calendar.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0)|| DateTime.Today - Calendar.SelectedDate.AddYears(4)<=TimeSpan.Zero)
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
                Session["User"] = u;
                ProfilePictureUpload.UploadPhoto();
                Response.Redirect(@"~\Home.aspx");
            }
            catch(Exception ex)
            {
                error.Text = "couldn't sign up, " + ex.Message;
            }
        }
        protected void BuildYearSelect()
        {
            int year = DateTime.Now.Year;
            for(int i = year; i>year-150;i--)
            {
                YearSelect.Items.Add(new ListItem(i.ToString()));
            }
            YearSelect.Items.FindByText(year.ToString()).Selected = true;
        }

        

        protected void YearSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calendar.VisibleDate = new DateTime(int.Parse(YearSelect.SelectedValue), Calendar.SelectedDate.Month, 1);
            
        }
    }
}