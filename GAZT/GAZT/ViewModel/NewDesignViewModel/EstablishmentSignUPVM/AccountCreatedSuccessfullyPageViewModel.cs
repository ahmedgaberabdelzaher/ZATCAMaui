using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    [Preserve(AllMembers = true)]
    public class AccountCreatedSuccessfullyPageViewModel : ViewModelBase
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
        public AccountCreatedSuccessfullyPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
          
        }
        #endregion
    }
}
