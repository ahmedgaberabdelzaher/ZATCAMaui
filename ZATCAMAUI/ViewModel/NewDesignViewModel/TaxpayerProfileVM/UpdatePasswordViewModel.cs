


using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    
    public class UpdatePasswordViewModel : BaseViewModel
    {
        #region Variable
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
                OnPropertyChanged("CurrentPasswordEntry");
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
                OnPropertyChanged("NewPasswordEntry");
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
                OnPropertyChanged("ConfirmPasswordEntry");
            }
        }
        #endregion

        public UpdatePasswordViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
           
        }
    }
}
