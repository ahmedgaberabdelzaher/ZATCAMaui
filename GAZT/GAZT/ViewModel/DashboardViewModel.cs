using System;
using System;
using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
using GAZT.Models;

namespace GAZT
{
    public class DashboardViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnMyCertificateClicked { get; set; }
        public ICommand OnBellClicked { get; set; }
        public ICommand OnMyTaxPayerProfileClicked { get; set; }

        

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

        public DashboardViewModel(INavigationService navigationService, IDialogService dialogService)
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
          
            OnMyCertificateClicked = new Command(() =>
            {
             _navigationService.NavigateTo(App.MyCertificate);

            });
            OnBellClicked = new Command(async () =>
            {
                //_navigationService.NavigateTo(App.MyCertificate);

            });
            OnMyTaxPayerProfileClicked = new Command(async () =>
            {
               _navigationService.NavigateTo(App.TaxPayerProfileView);

            });

            _navigationService.GoBack();
        }

        #endregion

        #region Method


        #endregion
    }
}
