using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class NewTaxpayerProfileViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Properties
        private string _TINLabel;
        public string TINLabel
        {
            get { return _TINLabel; }
            set
            {
                _TINLabel = value;
                RaisePropertyChanged("TINLabel");
            }
        }

        private string _MobileNumber;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set
            {
                _MobileNumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }

        private string _EmailEntry;
        public string EmailEntry
        {
            get { return _EmailEntry; }
            set
            {
                _EmailEntry = value;
                RaisePropertyChanged("EmailEntry");
            }
        }

        private string _PasswordEntry;
        public string PasswordEntry
        {
            get { return _PasswordEntry; }
            set
            {
                _PasswordEntry = value;
                RaisePropertyChanged("PasswordEntry");
            }
        }

        private string _ShowHidePasswordImage;
        public string ShowHidePasswordImage
        {
            get { return _ShowHidePasswordImage; }
            set
            {
                _ShowHidePasswordImage = value;
                RaisePropertyChanged("ShowHidePasswordImage");
            }
        }
        #endregion

        public NewTaxpayerProfileViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null) { throw new ArgumentNullException("navigationService"); }
            _navigationService = navigationService;

            if (dialogService == null) { throw new ArgumentNullException("dialogService"); }
            _dialogService = dialogService;
        }
    }
}
