using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.ComponentModel;
namespace UI.Custom_Controls
{
    public partial class Loading_Bar : System.Web.UI.UserControl
    {
        [Description("Color of outside"), Category("Data")]
        public Color OutColor { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Out.Style["Background-Color"] = OutColor.ToString();
        }
    }
}