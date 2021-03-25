using System;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.UnlockAccount
{
    [Preserve(AllMembers = true)]
    public class UnlockAccountSuccessPageViewModel:ViewModelBase
    {

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

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
                RaisePropertyChanged("PasswordChangedSuccessfully");
            }
        }

        #region ConstructorF
        /// <summary>
        /// Initializes a new instance for the <see cref="UnlockAccountTINPageViewModel" /> class.
        /// </summary>
        public UnlockAccountSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
        }

        public void PopToRootPage()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                _navigationService.GoBack();
            });
        }

        #endregion
    }
}
