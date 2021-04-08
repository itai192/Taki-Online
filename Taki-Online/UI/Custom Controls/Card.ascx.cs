using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Configuration;
using System.ComponentModel;
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
        [Browsable(true)]
        public event CardEventHandler Click;
        private WebControl control;
        public bool IsButton
        {
            get; set;
        } = false;
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
        protected Panel CreatePanelCard(string text, Color c)
        {
            Panel panel = new Panel();
            Label lbl = new Label();
            lbl.Text = text;
            
            panel.CssClass = "Card " + colorToColorName(c);
            panel.Controls.Add(lbl);
            return panel;
        }
        protected Image CreateImageCard(string name, Color c)
        {
            Image img = new Image();
            img.ImageUrl = GetCardImagePath(name, c);
            img.CssClass = "Card";
            return img;
        }
        public void PutCard()
        {
            IsButton = false;
            LoadCard();
            control.CssClass += " Put";
        }
        public void DrawCard()
        {
            LoadCard();
            control.CssClass += " Draw";
        }
        public void LoadCard()
        {
            if (card is IGetCardText || card == null)
            {
                Controls.Clear();
                IGetCardText c = (IGetCardText)card;
                if (this.IsButton)
                {
                    if (c != null)
                        control = CreateButtonCard(c.GetCardText(), card.color);
                    else
                        control = CreateButtonCard(string.Empty, Color.none);
                }
                else
                {
                    if (c != null)
                        control = CreatePanelCard(c.GetCardText(), card.color);
                    else
                        control = CreatePanelCard(string.Empty, Color.none);
                }
            }
            else
            {
                string cardName = card.GetType().Name;
                Color c = card.color;
                if (this.IsButton)
                {
                    control = CreateImageButtonCard(cardName, c);
                }
                else
                {
                    control = CreateImageCard(cardName, c);
                }
            }
            this.Controls.Add(control);
            control.ClientIDMode = ClientIDMode.Static;
            control.ID = this.ID;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCard();
        }
        public string GetCardImagePath(string name,Color c)
        {
            string path = ConfigurationManager.AppSettings["Cards"];
            string folder = ConfigurationManager.AppSettings[name];
            string ColorPath = @"\" + colorToColorName(c);
            return path + folder + ColorPath+".svg";
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