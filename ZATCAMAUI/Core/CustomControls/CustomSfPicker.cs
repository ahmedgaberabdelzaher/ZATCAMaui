
using Syncfusion.Maui.Picker;

namespace ZATCAMAUI.Core.CustomControls
{

    public class CustomSfPicker : SfPicker
    {
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
