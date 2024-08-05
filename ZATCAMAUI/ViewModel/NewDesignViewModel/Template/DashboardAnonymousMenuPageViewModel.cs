

using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Template
{ 
    public class DashboardAnonymousMenuPageViewModel : BaseViewModel
    {
        private string _appVersion = App.AppVersion;
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                _appVersion = value;
                OnPropertyChanged("AppVersion");
            }
        }

        public DashboardAnonymousMenuPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }
    }
}
