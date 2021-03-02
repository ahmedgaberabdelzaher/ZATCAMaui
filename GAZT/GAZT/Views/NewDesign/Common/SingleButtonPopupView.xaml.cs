using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Common
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleButtonPopupView : PopupPage
    {
        public SingleButtonPopupView(string buttonName, string message)
        {
            InitializeComponent();
            MessageText.Text = message;
            btnOK.Text = buttonName;
            SetLTR();
        }
        public SingleButtonPopupView(string buttonName, string message, string header)
        {
            InitializeComponent();
            MessageText.Text = message;
            btnOK.Text = buttonName;
            SetLTR();
            lblPopupHeader.Text = header;
        }

        private void OnOkayButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", true);
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnBackGroundClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<SingleButtonPopupView, bool>(this, "SingleButtonPopupBackgroundClickedResponse", true);
        }
    }
}