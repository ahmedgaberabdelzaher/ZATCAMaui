using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    public partial class TaxpayerProfileSuccessPage : ContentPage
    {
        TaxpayerProfileSuccessViewModel viewModel;

        public TaxpayerProfileSuccessPage(int SuccessId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = App.Locator.TaxpayerProfileSuccessPage;
            this.BindingContext = viewModel;

            // * Update UI
            viewModel.TPProfileSuccessId = SuccessId;
            UpdateUI();

            // * Need to update - Taxpayer Profile Data
        }

        private void UpdateUI()
        {
            switch (viewModel.TPProfileSuccessId)
            {
                case 1:
                    viewModel.SuccessTitleLbl = AppResources.TPEmailUpdated;
                    viewModel.successCaptionLbl = AppResources.TPNewEmailUpDated;
                   viewModel.ButtonLabelText=  AppResources.NDBacktoLogin;
                    break;
                case 2:
                    viewModel.SuccessTitleLbl = AppResources.TPMobileUpdate;
                    viewModel.successCaptionLbl = AppResources.TPSuccessMobileUpdated;
                    viewModel.ButtonLabelText = AppResources.NDBacktoLogin;
                    break;
                case 3:
                    viewModel.SuccessTitleLbl = AppResources.TPPasswordUpdate;
                    viewModel.successCaptionLbl = AppResources.NewPasswordUpdatedSuccessfully;
                    viewModel.ButtonLabelText = AppResources.NDBacktoLogin;
                    break;
                default:
                    break;
            }
        }

        void OnGoToProfileLblTapped(object sender, EventArgs args)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (viewModel.TPProfileSuccessId != 4) // Need to check
                {
                    await Task.Run(() =>
                    {
                        App.DisplayProgressView();
                    });
                    if (App.TP != null)
                        App.TP = null;
                    if (App.PreviousIsArabic)
                    {
                        String langName = "ar-AE";
                        AppResources.Culture = new CultureInfo(langName);
                    }
                    else
                    {
                        String langName = "en-US";
                        AppResources.Culture = new CultureInfo(langName);
                    }

                    try
                    {
                        await WebServiceManager.GAZTLogOff();
                    }
                    catch
                    {

                    }

                    await Task.Run(() =>
                    {
                        App.HideProgressView();
                    });

                    App.IsLogOut = true;
                    App.IsLoginCalled = false;
                    App.IsSamlApiCalledAndroid = false;

                    try
                    {
                        App.httpClientHandler = new HttpClientHandler();
                        App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                        App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
                    }
                    catch (Exception ex)
                    {

                    }
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                }
                else { viewModel._navigationService.GoBack(); }
            });
        }
    }
}
