using EGAZT.ViewModel.NewDesignViewModel.ZakatRejectionPopUpViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatRejectPopUp
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatRejectionReasonPopupPageView : PopupPage
    {
        public ZakatRejectionReasonPopupViewModel viewModel;
        public delegate void OnSelectDelegate(string item);
        public OnSelectDelegate OnSelect { get; set; } = null;
        public ZakatRejectionReasonPopupPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatRejectionReasonPopupPageView;
            this.BindingContext = viewModel;
            viewModel.RejectReasonText = string.Empty;
            SetLTR();
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }

        private async void CancelButtonClicked(object sender, System.EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "RejectCancelled", "No");
            OnSelect?.Invoke("No");
            await PopupNavigation.Instance.PopAsync();
        }

        private async void RejectButtonClicked(object sender, System.EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "Reject", viewModel.RejectReasonText);
            OnSelect?.Invoke("Yes");
            await PopupNavigation.Instance.PopAsync();
        }
    }
}