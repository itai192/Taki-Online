using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace UI.Custom_Controls
{
    public class CardEventArgs:EventArgs
    {
        public Card card { get; set; }
    }
    public partial class Card : System.Web.UI.UserControl
    {
        public Card card;
        public delegate void CardEventHandler(object sender, CardEventArgs e);
        public event CardEventHandler Click;
        protected void Page_Init(object sender, EventArgs e)
        {
            //if()
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Text= "Ta" + Environment.NewLine + "ki"; 
        }
        
        public string colorToColorName(Color c)
        {
            switch(c)
            {
                case Color.none:
                {
                    return "default";
                }
                case Color.red:
                {
                    return "Red";
                }
                case Color.green:
                {
                    return "Green";
                }
                case Color.blue:
                {
                    return "Blue";
                }
                case Color.yellow:
                {
                    return "Yellow";
                }
                default:
                    return "";
            }
        }

        protected void OnClick(object sender, ImageClickEventArgs e)
        {
            CardEventArgs ea = new CardEventArgs();
            ea.card = card;
            Click.Invoke(this, ea);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
    }
}