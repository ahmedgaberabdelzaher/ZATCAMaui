using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATRegistrationMenuPopUp : PopupPage
    {
        public VATRegistrationMenuPopUp()
        {
            InitializeComponent();
        }

        private async void OnAttachmentTapped(object sender, EventArgs e)
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            await attach.TranslateTo(0, 0, 0, Easing.BounceOut);
            VATRegistrationDetails vatReg = null;

            await MopupService.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void CloseTapped(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }
    }
}