using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    [Preserve(AllMembers = true)]
    public partial class AttachmentInformationPopUp : PopupPage
    {
        public delegate void OnDoneDelegate();
        public OnDoneDelegate OnDone { get; set; } = null;

        public AttachmentInformationPopUp(string infromationText)
        {
            InitializeComponent();
            if (infromationText == "Payer cancelled transaction")
            {
                paymentCancelInfo.IsVisible = true;
                if (App.IsArabic)
                {
                    paymentCancelInfo.Text = AppResources.PaymentCancelInfo;
                }
                else
                {
                    paymentCancelInfo.Text = AppResources.PaymentCancelInfo;
                }
            }
            InfromatationText.Text = infromationText;
            SetLTR();
        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            OnDone?.Invoke();
            PopupNavigation.Instance.PopAsync();
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                InfromatationText.HorizontalOptions = LayoutOptions.StartAndExpand;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                InfromatationText.HorizontalOptions = LayoutOptions.StartAndExpand;
            }
        }

    }
}
