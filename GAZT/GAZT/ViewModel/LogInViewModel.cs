using System;
using System;
using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
namespace GAZT
{
    public class LogInViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnLoginButtonClicked { get; set; }
        public ICommand OnOnLanguageClickClicked { get; set; }

        #endregion

        #region Property
        //private bool _isLoading;

        //public bool IsLoading
        //{
        //    get
        //    {
        //        return _isLoading;
        //    }
        //    set
        //    {
        //        _isLoading = value;
        //        RaisePropertyChanged("IsLoading");
        //    }
        //}

        private bool _phoneVisibility;

        public bool PhoneVisibility
        {
            get
            {
                return _phoneVisibility;
            }
            set
            {
                _phoneVisibility = value;
                RaisePropertyChanged("PhoneVisibility");
            }
        }

        private bool _tabletVisibility;

        public bool TabletVisibility
        {
            get
            {
                return _tabletVisibility;
            }
            set
            {
                _tabletVisibility = value;
                RaisePropertyChanged("TabletVisibility");
            }
        }

        #endregion

        #region Constructor

        public LogInViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnLoginButtonClicked = new RelayCommand(async () =>
            {
                bool IsComingFromSearch = true;
                _navigationService.NavigateTo(App.OTPView);

            });

            OnLoginButtonClicked = new RelayCommand(async () =>
            {
                bool IsNavigatingFromLogin = true;
                _navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

            });

          
        }

        #endregion

        #region Method
        
        public void OnPageLoad()
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {

                TabletVisibility = false;
                PhoneVisibility = true;
            }
            else
            {
                TabletVisibility = true;
                PhoneVisibility = false;
            }
        }
        #endregion
    }
}
