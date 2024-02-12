using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Globalization;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class VATRegistrationSuccessfullPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public VATRegistrationSuccessfullPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;

        }
        public async Task LogOut()
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
            catch (Exception)
            {


            }
            _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
        }
    }
}
