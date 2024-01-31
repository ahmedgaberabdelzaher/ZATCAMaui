using System.Globalization;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerProfile
{

    public partial class TaxpayerProfileSuccessPage : ContentPage
    {
        TaxpayerProfileSuccessViewModel viewModel;

        public TaxpayerProfileSuccessPage(int SuccessId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = App.Locator.TaxpayerProfileSuccessPage;
            BindingContext = viewModel;

            // * Update UI
            viewModel.TPProfileSuccessId = SuccessId;
            UpdateUI();
        }

        private void UpdateUI()
        {
            switch (viewModel.TPProfileSuccessId)
            {
                case 1:
                    viewModel.SuccessTitleLbl = AppResources.TPEmailUpdated;
                    viewModel.successCaptionLbl = AppResources.TPNewEmailUpDated;
                    viewModel.ButtonLabelText = AppResources.NDBacktoLogin;
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
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });
                if (App.TP != null)
                    App.TP = null;
                if (App.PreviousIsArabic)
                {
                    string langName = "ar-AE";
                    AppResources.Culture = new CultureInfo(langName);
                }
                else
                {
                    string langName = "en-US";
                    AppResources.Culture = new CultureInfo(langName);
                }

                try { await WebServiceManager.GAZTLogOff(); }
                catch { }

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
                catch (Exception)
                {



                }
                try
                {
                    if (viewModel.TPProfileSuccessId == 1)
                    {
                        
                        Navigation.PopToRootAsync();
                    }
                    else
                    {
                        var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(firstPageToRemove);

                        var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(secondPageToRemove);
                        viewModel._navigationService.GoBack();
                    }
                }
                catch (Exception)
                {



                }
            });
        }
    }
}
