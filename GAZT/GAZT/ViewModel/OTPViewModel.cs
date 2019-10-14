using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
namespace GAZT
{
    public class OTPViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnLoginButtonClicked { get; set; }
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

     
        #endregion

        #region Constructor

        public OTPViewModel(INavigationService navigationService, IDialogService dialogService)
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
                // _navigationService.NavigateTo(App.LoginView);

            });

        }

        #endregion

        #region Method

        public void OnPageLoad()
        {
        }
        #endregion
    }
}
