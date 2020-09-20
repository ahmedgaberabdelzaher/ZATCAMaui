using EGAZT.ViewModel.NewDesignViewModel.DashBoardPageViewModel;
using EGAZT.Views.NewDesign.VATDeRegistration;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
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
            ChangeFlowDirection();
            
        }

        public void ChangeFlowDirection()
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
        private void Label_MyProfile_Tapped(object sender, EventArgs e)
        {
            //         App.DisplayProgressView();

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayerProfilePageView);
            });
        }

        private async void Label_MyBills(object sender, EventArgs e)
        {
            //     App.DisplayProgressView();
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

            });

        }

        private async void Label_MyRetuns_Tapped(object sender, EventArgs e)
        {
            //       App.DisplayProgressView();   await Task.Run(() =>



            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
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
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

            });

        }

        private async void TinRegistrationDetails_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ZakatRegistrationDetailsListPageView);
            });
        }

        private async void VATRefundRequest_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });
        }

        private async void ChnageFillingPeriod_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });
        }

        private async void VATDeregistrationDetails_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});
            Device.BeginInvokeOnMainThread(() =>
            {
                PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage());
            });
        }

        private async void ZakatInstalmentPlan_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);

            });
        }

        private async void Vat_Review_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ObjectionsSelectionPageView);
            });
        }

        private async void ContractRelease_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
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
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);

            });
        }

        private void Aboutus_Tapped(object sender, EventArgs e)
        {
            //       App.DisplayProgressView();
            viewModel._navigationService.NavigateTo(App.AboutUsPageView);
        }

        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            //        App.DisplayProgressView();
            viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);

        }

        private void ChangeLanguage_Tapped(object sender, EventArgs e)
        {
            try
            {
                //        App.DisplayProgressView();
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

                OnAppearing();
            }
            catch (Exception ex)
            {

            }
        }
        private void OnSupportTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SupportPageView);
        }
        private async void Logout_Tapped(System.Object sender, System.EventArgs e)
        {
            //    App.DisplayProgressView();

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
        //private void OnRealStateTapped(object sender, EventArgs e)
        //{

        //}

        //private async void OnTaxEvasionTapped(object sender, EventArgs e)
        //{
        //    await Task.Run(() =>
        //    {
        //        //viewModel.IsLoading = true;

        //    });
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);

        //    });
        //}

        //private void OnFillingFrequencyTapped(object sender, EventArgs e)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

        //    });
        //}

        //private void OnVATRegVerTapped(object sender, EventArgs e)
        //{

        //}

        //private void OnReqForRullingTapped(object sender, EventArgs e)
        //{

        //}

        //private async void OnZakatTaxCertificateTapped(object sender, EventArgs e)
        //{
        //    await Task.Run(() =>
        //    {
        //        viewModel.IsLoading = true;

        //    });
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

        //    });
        //}

        //private void OnContractReleaseTapped(object sender, EventArgs e)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        viewModel._navigationService.NavigateTo(App.ContractReleaseListPageView);

        //    });
        //}

        //private async void OnVATRegDetailsTapped(object sender, EventArgs e)
        //{
        //    await Task.Run(() =>
        //    {
        //        //viewModel.IsLoading = true;

        //    });
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);

        //    });
        //}
        //private async void OnVATCertificateTapped(object sender, EventArgs e)
        //{
        //    await Task.Run(() =>
        //    {
        //        viewModel.IsLoading = true;

        //    });
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

        //    });
        //}

        //private async void OnLogOutTapped(object sender, EventArgs e)
        //{
        //    if (App.IsArabic)
        //    {
        //        var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZNo, AppResources.ZYes);
        //        if (!result)
        //        {
        //            App.TP = null;
        //            await viewModel.LogOut();
        //        }
        //    }
        //    else
        //    {
        //        var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
        //        if (result)
        //        {
        //            App.TP = null;
        //            await viewModel.LogOut();
        //        }
        //    }
        //}
    }
}