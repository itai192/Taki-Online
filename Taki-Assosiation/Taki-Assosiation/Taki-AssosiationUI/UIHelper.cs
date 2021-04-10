using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
namespace Taki_AssosiationUI
{
   
    public static class UIHelper
    {
        public static TableCell LableTableCell(string text)
        {
            TableCell cell = new TableCell();
            Label lbl = new Label();
            lbl.Text = text;
            cell.Controls.Add(lbl);
            return cell;
        }
    }
}