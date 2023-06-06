using System;
using GalaSoft.MvvmLight.Views;
using EGAZT.Models.EnumModels;
using Xamarin.Forms.Internals;
using GalaSoft.MvvmLight;
using GAZT.Manager;
using GAZT.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using AppDynamics.Agent;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
	public class NafathLoginPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public int CurrentAttempt = 0;
        public NafathLoginPageViewModel(INavigationService navigationService, IDialogService dialogService)

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
        }

        public bool IsLoading { get; internal set; }

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
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

            }

            String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
            String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

            await Task.Run(() =>
            {
                try
                {

                    App.IsLoginCalled = false;
                    App.ArePreLoginLangCookiesSet = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                        App.HasToRefreshLoaderOnDashboard = true;
                    });

                   
                }
                catch (GAZTInternetException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

            });

        }
    }
}

