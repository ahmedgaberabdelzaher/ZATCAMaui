using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [Preserve(AllMembers = true)]
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                // * Passing success id's : Mobile - 2 ; Email - 1 ; Password - 3
                /*if (viewModel.TPProfileSuccessId == 1 || viewModel.TPProfileSuccessId == 3)
                {*/
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
                catch (Exception) { }
                try
                {
                    if (viewModel.TPProfileSuccessId == 1)
                    {
                        //var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        //Navigation.RemovePage(firstPageToRemove);

                        //var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        //Navigation.RemovePage(secondPageToRemove);

                        //var thirdPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        //Navigation.RemovePage(thirdPageToRemove);
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
                    //viewModel._navigationService.GoBack();
                }
                catch (Exception) { }
            });
        }
    }
}
