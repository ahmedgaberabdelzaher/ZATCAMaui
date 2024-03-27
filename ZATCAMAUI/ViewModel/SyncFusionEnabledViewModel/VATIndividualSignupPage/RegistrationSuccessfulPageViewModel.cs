using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{

    public class RegistrationSuccessfulPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand btn_LoginScreen { get; set; }

        private bool isGulf;
        public bool IsGulf
        {
            get
            {
                return isGulf;
            }
            set
            {
                isGulf = value;



                RaisePropertyChanged("IsGulf");
            }
        }
        private bool isCitizen;
        public bool IsCitizen
        {
            get
            {
                return isCitizen;
            }
            set
            {
                isCitizen = value;



                RaisePropertyChanged("IsCitizen");
            }
        }

        private string _tINnumber;
        public string TINnumber
        {
            get
            {
                return _tINnumber;
            }
            set
            {
                _tINnumber = value;

                RaisePropertyChanged("TINnumber");
            }
        }
        public RegistrationSuccessfulPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            btn_LoginScreen = new Command(() =>
            {
                _navigationService.NavigateTo($"/{App.SFLoginPageView}", App.SFLandingPageView);
            });
        }


    }
}