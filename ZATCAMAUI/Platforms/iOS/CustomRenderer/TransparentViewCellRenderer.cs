using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using UIKit;
using ZATCAMAUI.Platforms.iOS.CustomRenderer;

namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
    public class TransparentViewCellRenderer : ViewCellRenderer
    {


        public override UITableViewCell GetCell(Cell pCell, UITableViewCell pReusableCell, UITableView pTableView)
        {
            UITableViewCell lCell = base.GetCell(pCell, pReusableCell, pTableView);
            if (lCell != null)
            {
                SetBackgroundColor(lCell, pCell, UIColor.White);
            }
            return lCell;
        }
    }
}
