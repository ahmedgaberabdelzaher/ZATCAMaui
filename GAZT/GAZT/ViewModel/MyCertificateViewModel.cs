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
    public class MyCertificateViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnLoginButtonClicked { get; set; }
        public ICommand OnBellClicked { get; set; }
        public ICommand OnMyTaxPayerProfileClicked { get; set; }
        public ICommand OnHomeIconClicked { get; set; }
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

        public MyCertificateViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnBellClicked = new RelayCommand(async () =>
            {
                //_navigationService.NavigateTo(App.MyCertificate);

            });
            OnMyTaxPayerProfileClicked = new RelayCommand(async () =>
            {
                _navigationService.NavigateTo(App.TaxPayerProfileView);

            });
            OnHomeIconClicked = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });

        }

        #endregion

        #region Method


        #endregion
    }
}
