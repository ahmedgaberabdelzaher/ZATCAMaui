using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AboutUsPage
{
    public class AboutUsPageViewModel : BaseViewModel
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
        #region Constructor
        public AboutUsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        #endregion
    }
}
