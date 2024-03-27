using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight;
using AppDynamics.Agent;
using System.Windows.Input;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Exceptions;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Nafat
{
    public class NafathLoginPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBackButtonClicked { get; set; }
        public int CurrentAttempt = 0;
        public NafathLoginPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)

        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            OnBackButtonClicked = new  Command(() =>
            {
                _navigationService.GoBack();
            });
        }

        public async Task LoginCompletedInWebView()
        {
            string response = string.Empty;
            string UserId = App.LoginDataRetrieved.TIN;

            Instrumentation.SetUserData("user_id", UserId);

            //string lang = "E";
            string language = UtilityManager.GetLanguageParameter();

            //if (App.IsArabic == true)
            //    lang = "AR";

            string _currentAttempts = CurrentAttempt.ToString();
            string languag = UtilityManager.GetLanguageParameter();

            // * OLD TP PROFILE API
            //TaxPayerProfile TPProfile = WebServiceManager.SFGAZTGetTaxPayerProfile(UserId, lang);

            // * NEW TP PROFILE API
            TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileDataAPICall(UserId);

            if (TPProfile != null)
            {
                App.TP = new TaxPayerProfile();
                App.TP = TPProfile;
                App.TP.Userid = TPProfile.Tin;
                try
                {
                    if (App.LoginDataRetrieved != null)
                    {
                        if (App.TP != null)
                        {
                            App.TP.NameFirst = App.LoginDataRetrieved.NameFirst;
                            App.TP.NameLast = App.LoginDataRetrieved.NameLast;
                            App.TP.NameOrg1 = App.LoginDataRetrieved.NameOrg1;
                            App.TP.TypeChk = App.LoginDataRetrieved.TypeChk;
                        }

                    }

                }
                catch (Exception)
                {


                }

            }

            string OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
            string OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

            await Task.Run(() =>
            {
                try
                {

                    App.IsLoginCalled = false;
                    App.ArePreLoginLangCookiesSet = false;

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView, false);
                        App.HasToRefreshLoaderOnDashboard = true;
                    });


                }
                catch (GAZTInternetException)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }
                catch (Exception)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }

            });

        }
    }
}

