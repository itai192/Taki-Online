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
        
        public Color outColor = Color.Blue;
        public Color inColor = Color.Green;
        private int _outOf = 1;
        private int _progress = 1;
        public int outOf 
        { get
            {
                return _outOf;
            }
            set {
                if (value < progress)
                    _outOf = progress;
                else
                    _outOf = value;

            }
        }
        public int progress 
        { get { return _progress; } 
            set {
                if (value > outOf || value < 0)
                    _progress = outOf;
                else
                    _progress = value;
                } 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Out.Style["background-color"] = outColor.Name;
            In.Style["background-color"] = inColor.Name;
            In.Style["width"] = (((double)progress / (double)outOf) * 100).ToString() + "%";
            ProgressLbl.Text = $"{progress}/{outOf}";
        }
    }
}