using RGPopup.Maui.Services;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.DashBoardPageViewModel;
using ZATCAMAUI.Views.NewDesign.VATDeRegistration;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages
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
            ChangeFlowDirection();
            viewModel.IsLoading = false;
        }

        public void ChangeFlowDirection()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void Label_MyProfile_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayerProfilePageView);
            });
        }

        private async void Label_MyBills(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            MainThread.BeginInvokeOnMainThread(() =>
            {
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
            });
        }

        private async void Label_MyRetuns_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 4);
            });
        }

        private async void TaxpayerCertificate_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

            });

        }

        private void TinRegistrationDetails_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ZakatRegistrationDetailsListPageView);
            });
        }

        private void VATRefundRequest_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });
        }

        private void ChnageFillingPeriod_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });
        }

        private void VATDeregistrationDetails_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage(false));
            });
        }

        private void ZakatInstalmentPlan_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);

            });
        }

        private void Vat_Review_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ObjectionsSelectionPageView);
            });
        }

        private void ContractRelease_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ContractReleaseListPageView);

            });
        }

        private void OnApplicationStatus_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
        }

        private async void TaxEvasion_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);

            });
        }

        private void Aboutus_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.AboutUsPageView);
        }

        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);

        }

        private void ChangeLanguage_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (App.IsArabic)
                {
                    App.IsArabic = false;
                    App.changeFontFamily(App.appObj);
                    ChangeFlowDirection();
                    var vUpdatedPage = new TaxManagementPageView();
                    Navigation.InsertPageBefore(vUpdatedPage, this);
                    Navigation.PopAsync();

                    App.HasToRefreshLoaderOnDashboard = true;
                }
                else
                {
                    App.IsArabic = true;
                    App.changeFontFamily(App.appObj);
                    ChangeFlowDirection();
                    var vUpdatedPage = new TaxManagementPageView();
                    Navigation.InsertPageBefore(vUpdatedPage, this);
                    Navigation.PopAsync();

                    App.HasToRefreshLoaderOnDashboard = true;
                }
            }
            catch (Exception)
            {


            }
        }
        private void OnSupportTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SupportPageView);
        }
        private async void Logout_Tapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                var result = await DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZNo, AppResources.ZYes);
                if (!result)
                {
                    App.TP = null;
                    await viewModel.LogOut();
                }
            }
            else
            {
                var result = await DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
                if (result)
                {
                    App.TP = null;
                    await viewModel.LogOut();
                }
            }
        }
    }
}