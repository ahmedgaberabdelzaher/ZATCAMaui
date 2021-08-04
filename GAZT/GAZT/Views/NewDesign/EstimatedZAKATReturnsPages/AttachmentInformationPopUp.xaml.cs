using System;
using EGAZT.ViewModel.NewDesignViewModel;
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

            if (InfromatationText.Text.Equals(AppResources.ZZZZReturnUnderReviewAddAttachments))
            {
                MessagingCenter.Send<App, string>
                    ((App)Xamarin.Forms.Application.Current, "OnlyAddAttachments", "add vat attachments");

            }
        }

             private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

    }
}
