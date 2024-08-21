using Mopups.Pages;
using Mopups.Services;
using System.Globalization;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.NewDesignViewModel.DashBoardPageViewModel;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPopUpPage : PopupPage
    {
        GAZTNewDesignDashBoardPageViewModel viewModel;
        public InfoPopUpPage()
        {
            viewModel = App.Locator.InfoPopUpPage;
            this.BindingContext = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZNo, AppResources.ZYes);
                if (!result)
                {
                    App.TP = null;
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PopAsync();
                        await LogOutFromPopup();
                    });
                }
            }
            else
            {
                var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
                if (result)
                {
                    App.TP = null;
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PopAsync();
                        await LogOutFromPopup();
                    });
                }
            }
        }

        public async Task LogOutFromPopup()
        {
            App.DisplayProgressView();
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

            try
            {
                await WebServiceManager.GAZTLogOff();
            }
            catch
            {

            }

            App.HideProgressView();

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
            viewModel._navigationService.GoBack();

        }

    }
}