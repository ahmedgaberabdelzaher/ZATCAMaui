using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.ViewModel.NewViewModel
{
    public class CreateGaztAccountPageViewModel : ViewModelBase
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Properties
        private string _txtEmailAddress = string.Empty;
        public string TxtEmailAddress
        {
            get
            {
                return _txtEmailAddress;
            }
            set
            {
                _txtEmailAddress = value;
                RaisePropertyChanged("SelectedFilter");
            }
        }

        private string _txtEmailCode = string.Empty;
        public string TxtEmailCode
        {
            get
            {
                return _txtEmailCode;
            }
            set
            {
                _txtEmailCode = value;
                RaisePropertyChanged("TxtEmailCode");
            }
        }

        private string _txtMobileNumber = string.Empty;
        public string TxtMobileNumber
        {
            get
            {
                return _txtMobileNumber;
            }
            set
            {
                _txtMobileNumber = value;
                RaisePropertyChanged("TxtMobileNumber");
            }
        }

        private string _txtMobileNumberCode = string.Empty;
        public string TxtMobileNumberCode
        {
            get
            {
                return _txtMobileNumberCode;
            }
            set
            {
                _txtMobileNumberCode = value;
                RaisePropertyChanged("TxtMobileNumberCode");
            }
        }
        private string _txtPassword = string.Empty;
        public string TxtPassword
        {
            get
            {
                return _txtPassword;
            }
            set
            {
                _txtPassword = value;
                RaisePropertyChanged("TxtPassword");
            }
        }

        private string _txtConfirmPassword = string.Empty;
        public string TxtConfirmPassword
        {
            get
            {
                return _txtConfirmPassword;
            }
            set
            {
                _txtConfirmPassword = value;
                RaisePropertyChanged("TxtConfirmPassword");
            }
        }

    
        #endregion

        #region Constructor

        #endregion

        #region Methods

        #endregion
    }
}
