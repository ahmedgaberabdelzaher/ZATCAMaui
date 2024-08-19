using AppDynamics.Agent;

using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
    
    public class NafathLoginPageViewModel : BaseViewModel
    {
    
        public int CurrentAttempt = 0;
        public NafathLoginPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            
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
            TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileAndUpdatePasswordAPICall(UserId);

            if (TPProfile != null)
            {
                

                App.TP = new TaxPayerProfile();
                App.TP = TPProfile;
                App.TP.userId = TPProfile.TIN;
                try
                {
                    if (App.LoginDataRetrieved != null)
                    {
                        if (App.TP != null)
                        {
                            App.TP.firstName = App.LoginDataRetrieved.NameFirst;
                            App.TP.lastName = App.LoginDataRetrieved.NameLast;
                            App.TP.organizationName = App.LoginDataRetrieved.NameOrg1;
                            App.TP.typeCheck = App.LoginDataRetrieved.TypeChk;
                        }

                    }

                }
                catch (Exception ex)
                {
                    
                    
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

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
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
                catch (Exception ex)
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

