using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    
    public class AccountCreatedSuccessfullyPageViewModel : BaseViewModel
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnLoginPageLinkClicked;
        #endregion
        #region Properties 
        private string _tINnumber;
        public string TINnumber
        {
            get
            {
                return _tINnumber;
            }
            set
            {
                if (_tINnumber == value) return;
                _tINnumber = value;

                RaisePropertyChanged("TINnumber");
            }
        }
        #endregion
        #region Constructor
        public AccountCreatedSuccessfullyPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

        }
        #endregion
    }
}
