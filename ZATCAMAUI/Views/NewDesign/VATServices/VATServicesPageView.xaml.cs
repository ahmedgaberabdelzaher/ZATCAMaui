using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATServicesPageViewModel;
using ZATCAMAUI.Views.NewDesign.VATDeRegistration;

namespace ZATCAMAUI.Views.NewDesign.VATServices
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATServicesPageView : ContentPage
    {
        #region Variable
        VATServicesPageViewModel viewModel;
        #endregion
        public VATServicesPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATServicesPageView;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                //FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                //FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnBackTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        private void VATRegistration_Details_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATRegistration_Details_Tapped", "VAT Registration Details eService");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationDisplayDetails);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void VATRefundRequest_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void ChnageFillingPeriod_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "ChangeFilingPeriod_Tapped", "Change Filing Period eService");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void VATDeregistrationDetails_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATDeregistrationDetails_Tapped", "VAT Deregistration Request eService");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage(false));
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void VATReactivation_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() => viewModel.IsLoading = true);
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATReactivation_Tapped", "VAT Reactivation eService");
            App.VATType = PageExecutionType.Reactivation;
            MainThread.BeginInvokeOnMainThread(() => viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView));
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void VATAment_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() => viewModel.IsLoading = true);
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATAmend_Tapped", "VAT Amendment eService");
            App.VATType = PageExecutionType.Amend;
            viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
    }
}