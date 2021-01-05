using EGAZT.Enums;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.ViewModel.NewDesignViewModel.VATServicesPageViewModel;
using EGAZT.Views.NewDesign.VATDeRegistration;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATServices
{
    [Preserve(AllMembers = true)]
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
            SetLTR();
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnBackTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        private void VATRegistration_Details_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATRegistration_Details_Tapped", "VAT Registration Details eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationDisplayDetails);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void VATRefundRequest_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void ChnageFillingPeriod_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "ChangeFilingPeriod_Tapped", "Change Filing Period eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void VATDeregistrationDetails_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATDeregistrationDetails_Tapped", "VAT Deregistration Request eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage());
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void VATReactivation_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() => viewModel.IsLoading = true);
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATReactivation_Tapped", "VAT Reactivation eService");
            App.VATType = PageExecutionType.Reactivation;
            Device.BeginInvokeOnMainThread(() => viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView));
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