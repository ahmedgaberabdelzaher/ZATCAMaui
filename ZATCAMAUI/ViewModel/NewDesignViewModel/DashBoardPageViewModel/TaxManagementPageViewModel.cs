
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.DashBoardPageViewModel
{

    public class TaxManagementPageViewModel : BaseViewModel
    {
        public ICommand OnBackButtonClicked { get; set; }

        private string _appVersion = App.AppVersion;
        #region Property

        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                _appVersion = value;
                OnPropertyChanged("AppVersion");
            }
        }

        #endregion

        #region Constructor

        public TaxManagementPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }

        #endregion


        #region Methods

        public async Task LogOut()
        {
            try
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

                await WebServiceManager.GAZTLogOff();

                App.HideProgressView();

                App.IsLogOut = true;
                App.IsLoginCalled = false;
                App.IsSamlApiCalledAndroid = false;

                App.httpClientHandler = new HttpClientHandler();
                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();

                _navigationService.GoBack();
            }
            catch (GAZTNetworkConnectivityIssueException)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.NetworkConnectivityIssue, true, _navigationService);
            }
            catch (InternetException)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.ZZInternetConnectionMessage, false);
            }
            catch (Exception)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.Somethingwentwrong, false);
            }
            finally
            {
                App.HideProgressView();
            }
        }

        #endregion
    }
}
