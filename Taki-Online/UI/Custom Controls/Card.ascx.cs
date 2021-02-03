using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Configuration;
namespace UI.Custom_Controls
{
    public class CardEventArgs:EventArgs
    {
        public BLL.Card card { get; set; }
    }
    public partial class Card : System.Web.UI.UserControl
    {
        public BLL.Card card { get; set; }
        public delegate void CardEventHandler(object sender, CardEventArgs e);
        public event EventHandler Click;

        
        protected Button CreateButtonCard(string text, Color c)
        {
            Button button = new Button();
            button.Text = text;
            button.CssClass = "Card " + colorToColorName(c);
            button.Click += OnClick;
            return button;
        }
        protected ImageButton CreateImageButtonCard(string name, Color c)
        {
            ImageButton imgbtn = new ImageButton();
            imgbtn.ImageUrl = GetCardImagePath(name, c);
            imgbtn.Click += OnClick;
            imgbtn.CssClass = "Card";
            return imgbtn;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (card != null)
            {
                if (card is IGetCardText)
                {
                    IGetCardText c = (IGetCardText)card;
                    this.Controls.Add(CreateButtonCard(c.GetCardText(), card.color));
                }
                else
                {
                    string cardName = typeof(Card).Name;
                    Color c = card.color;
                    this.Controls.Add(CreateImageButtonCard(cardName, c));
                }
            }
        }
        public string GetCardImagePath(string name,Color c)
        {
            string path = ConfigurationManager.AppSettings["Cards"];
            string folder = ConfigurationManager.AppSettings[name];
            string ColorPath = @"\" + colorToColorName(c);
            return path + folder + ColorPath;
        }
        
        public string colorToColorName(Color c)
        {
            switch(c)
            {
                case Color.none:
                {
                    return "Default";
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
                    return "Default";
            }
        }

        protected void OnClick(object sender, ImageClickEventArgs e)
        {
            OnClick(sender, (EventArgs)e);
        }
        protected void OnClick(object sender, EventArgs e)
        {
            CardEventArgs ea = new CardEventArgs();
            ea.card = card;
            if(Click!=null)
                Click.Invoke(this, ea);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        
    }
}