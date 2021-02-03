using GalaSoft.MvvmLight.Views;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
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
                RaisePropertyChanged("AppVersion");
            }
        }

        public DashboardAnonymousMenuPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }
    }
}
