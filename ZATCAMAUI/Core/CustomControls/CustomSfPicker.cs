
using System.Runtime.CompilerServices;
using Syncfusion.Maui.Picker;

namespace ZATCAMAUI.Core.CustomControls
{

    public class CustomSfPicker : SfPicker
    {
       
        protected override void OnPropertyChanged(string propertyName = null)
        {
            this.FooterView.CancelButtonText = AppResources.CancelText;
            this.FooterView.OkButtonText = AppResources.OKText;
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            base.OnPropertyChanged(propertyName);
        }
        protected override void OnOkButtonClicked(EventArgs e)
        {
            this.IsOpen = false;
            base.OnOkButtonClicked(e);
        }

        protected override void OnCancelButtonClicked(EventArgs e)
        {
            this.IsOpen = false;
            base.OnCancelButtonClicked(e);
        }
    }
}
