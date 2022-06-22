using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.DashBoardPageViewModel
{
    [Preserve(AllMembers = true)]
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
                RaisePropertyChanged("AppVersion");
            }
        }

        #endregion

        #region Constructor

        public TaxManagementPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
        }

        #endregion


        #region Methods

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
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
            _navigationService.GoBack();
        }

        #endregion
    }
}
