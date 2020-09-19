using EGAZT.ViewModel.NewDesignViewModel.DashBoardPageViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxManagementPageView : ContentPage
    {
        TaxManagementPageViewModel viewModel;
        public TaxManagementPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxManagementPageView;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnRealStateTapped(object sender, EventArgs e)
        {

        }

        private async void OnTaxEvasionTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                //viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);

            });
        }

        private void OnFillingFrequencyTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });
        }

        private void OnVATRegVerTapped(object sender, EventArgs e)
        {

        }

        private void OnReqForRullingTapped(object sender, EventArgs e)
        {

        }

        private async void OnZakatTaxCertificateTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

            });
        }

        private void OnContractReleaseTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ContractReleaseListPageView);

            });
        }

        private async void OnVATRegDetailsTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                //viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);

            });
        }
        private async void OnVATCertificateTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

            });
        }

        private async void OnLogOutTapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZNo, AppResources.ZYes);
                if (!result)
                {
                    App.TP = null;
                    await viewModel.LogOut();
                }
            }
            else
            {
                var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
                if (result)
                {
                    App.TP = null;
                    await viewModel.LogOut();
                }
            }
        }
    }
}