using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    
    public class UpdatePasswordViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Properties
        

        private string _CurrentPasswordEntry;
        public string CurrentPasswordEntry
        {
            get
            {
                return _CurrentPasswordEntry;
            }
            set
            {
                if (_CurrentPasswordEntry == value) return;

                _CurrentPasswordEntry = value;
                RaisePropertyChanged("CurrentPasswordEntry");
            }
        }

        private string _NewPasswordEntry;
        public string NewPasswordEntry
        {
            get
            {
                return _NewPasswordEntry;
            }
            set
            {
                if (_NewPasswordEntry == value) return;

                _NewPasswordEntry = value;
                RaisePropertyChanged("NewPasswordEntry");
            }
        }

        private string _ConfirmPasswordEntry;
        public string ConfirmPasswordEntry
        {
            get
            {
                return _ConfirmPasswordEntry;
            }
            set
            {
                if (_ConfirmPasswordEntry == value) return;

                _ConfirmPasswordEntry = value;
                RaisePropertyChanged("ConfirmPasswordEntry");
            }
        }
        #endregion

        public UpdatePasswordViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null) { throw new ArgumentNullException("navigationService"); }
            _navigationService = navigationService;

            if (dialogService == null) { throw new ArgumentNullException("dialogService"); }
            _dialogService = dialogService;
        }
    }
}
