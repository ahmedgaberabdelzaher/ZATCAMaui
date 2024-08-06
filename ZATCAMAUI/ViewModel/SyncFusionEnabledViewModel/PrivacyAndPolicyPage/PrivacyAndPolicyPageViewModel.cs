

using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PrivacyAndPolicyPage
{
    public class PrivacyAndPolicyPageViewModel : BaseViewModel
    {
        #region Variable
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
                OnPropertyChanged("WebUrl");
            }
        }
       
        #endregion
        #region Commands
        public ICommand BackButtonClicked { get; private set; }

        #endregion
        #region Constructor
        public PrivacyAndPolicyPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

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
