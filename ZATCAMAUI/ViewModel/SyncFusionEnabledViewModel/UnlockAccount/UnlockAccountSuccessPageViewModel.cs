

using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.UnlockAccount
{
    public class UnlockAccountSuccessPageViewModel : BaseViewModel
    {


        private string _passwordChangedSuccessfully;
        public string PasswordChangedSuccessfully
        {
            get
            {
                return _passwordChangedSuccessfully;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value == _passwordChangedSuccessfully) return;
                _passwordChangedSuccessfully = value;
                OnPropertyChanged("PasswordChangedSuccessfully");
            }
        }

        #region ConstructorF
        /// <summary>
        /// Initializes a new instance for the <see cref="UnlockAccountTINPageViewModel" /> class.
        /// </summary>
        public UnlockAccountSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        public void PopToRootPage()
        {
           MainThread.BeginInvokeOnMainThread( () =>
            {
                _navigationService.GoBack();
            });
        }

        #endregion
    }
}
