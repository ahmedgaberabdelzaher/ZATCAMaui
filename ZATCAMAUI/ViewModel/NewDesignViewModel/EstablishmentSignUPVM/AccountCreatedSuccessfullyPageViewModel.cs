

using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    
    public class AccountCreatedSuccessfullyPageViewModel : BaseViewModel
    {
        #region Veriables
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

                OnPropertyChanged("TINnumber");
            }
        }
        #endregion
        #region Constructor
        public AccountCreatedSuccessfullyPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }
        #endregion
    }
}
