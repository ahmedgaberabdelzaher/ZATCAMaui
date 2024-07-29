using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Mopups.Services;
using System.Windows.Input;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.SignUpTAndCPage
{

    public class SignUpTAndCPageViewModel : BaseViewModel
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnSubmitClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        #endregion
        #region Properties
        private bool _ischkTAndC = false;
        public bool IschkTAndC
        {
            get
            {
                return _ischkTAndC;
            }
            set
            {
                _ischkTAndC = value;
                if (_ischkTAndC == true)
                {
                    IsButtonEnabled = true;
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                }
                else
                {
                    IsButtonEnabled = false;
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                }
                RaisePropertyChanged("IschkTAndC");
            }
        }
        private bool _isButtonEnabled = false;
        public bool IsButtonEnabled
        {
            get
            {
                return _isButtonEnabled;
            }
            set
            {
                _isButtonEnabled = value;
                RaisePropertyChanged("IsButtonEnabled");
            }
        }
        private Color _verifybuttonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
        public Color VerifyButtonDisableColor
        {
            get
            {
                return _verifybuttonDisableColor;
            }
            set
            {
                _verifybuttonDisableColor = value;
                RaisePropertyChanged("VerifyButtonDisableColor");
            }
        }
        #endregion
        #region Constructor
        public SignUpTAndCPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            OnSubmitClicked = new Command(async () =>
            {
                try
                {
                    if (IsButtonEnabled == true)
                    {
                        _navigationService.NavigateTo(App.SignUpFormPageView);
                    }
                    else
                    {
                        PopUp popUp = new PopUp();
                        popUp.Message = AppResources.ZZPleaseselecttermsandconditions;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                    }
                }
                catch (Exception)
                {


                }
            });
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion
    }
}
