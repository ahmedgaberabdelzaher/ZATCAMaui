

using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{

    public class RegistrationSuccessfulPageViewModel : BaseViewModel
    {
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



                OnPropertyChanged("IsGulf");
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



                OnPropertyChanged("IsCitizen");
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

                OnPropertyChanged("TINnumber");
            }
        }
        public RegistrationSuccessfulPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            btn_LoginScreen = new Command(() =>
            {
                _navigationService.NavigateTo($"/{App.SFLoginPageView}", App.SFLandingPageView);
            });
        }


    }
}