
using System.Windows.Input;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ContactUsPage
{

    public class ContactUsPageViewModel : BaseViewModel
    {
        public ICommand GoBackClick { get; set; }


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

        private string _Url;
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
                OnPropertyChanged();
            }
        }


        public ContactUsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            Url = PageSettings.GetContactUsUrl();
        }
    }
}
