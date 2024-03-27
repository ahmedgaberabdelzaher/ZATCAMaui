using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{

    public class TaxpayerSubsidyViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
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
        
        #endregion
        #region Constructor
        public TaxpayerSubsidyViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion
        #region Method
        #endregion
    }
}
