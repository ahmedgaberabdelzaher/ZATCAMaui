using System;

using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(TransparentViewCellRenderer))]

namespace GAZT.iOS.CustomRenderer
{
    [Preserve(AllMembers = true)]
    public class TransparentViewCellRenderer : ViewCellRenderer
    {


        public override UITableViewCell GetCell(Cell pCell, UITableViewCell pReusableCell, UITableView pTableView)
        {
            UITableViewCell lCell = base.GetCell(pCell, pReusableCell, pTableView);
            if (lCell != null)
            {
                //lCell.BackgroundColor = UIColor.Clear;
                SetBackgroundColor(lCell, pCell, UIColor.White);
            }
            return lCell;
        }



    }

}

