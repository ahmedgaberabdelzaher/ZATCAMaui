using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PrivacyAndPolicyPage
{
    public class PrivacyAndPolicyPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion
        #region Property

        private string _webUrl = string.Empty;
        public string WebUrl
        {
            get
            {
                return _webUrl;
            }
            set
            {
                _webUrl = value;
                RaisePropertyChanged("WebUrl");
            }
        }
        private bool _IsLoading = false;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        //IsLoading
        #endregion
        #region Commands
        public ICommand BackButtonClicked { get; private set; }

        #endregion
        #region Constructor
        public PrivacyAndPolicyPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            BackButtonClicked = new Command(() => navigateBack());

        }
        #endregion
        #region Method
        private void navigateBack()
        {
            _navigationService.GoBack();
        }
        #endregion
    }
}
