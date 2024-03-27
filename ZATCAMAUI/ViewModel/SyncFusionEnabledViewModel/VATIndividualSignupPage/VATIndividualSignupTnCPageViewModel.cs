using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using System.Windows.Input;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{

    public class VATIndividualSignupTnCPageViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnSubmitClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand BackButtonClicked { get; set; }

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
                if (_webUrl == value) return;
                _webUrl = value;
                RaisePropertyChanged("WebUrl");
            }
        }
       
        private bool _ischkTAndC = false;
        public bool IschkTAndC
        {
            get
            {
                return _ischkTAndC;
            }
            set
            {
                if (_ischkTAndC == value) return;

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
                if (_isButtonEnabled == value) return;

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
                if (_verifybuttonDisableColor == value) return;

                _verifybuttonDisableColor = value;
                RaisePropertyChanged("VerifyButtonDisableColor");
            }
        }

        #endregion
        #region Constructor
        public VATIndividualSignupTnCPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            BackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
            OnSubmitClicked = new Command(async () =>
            {
                await Task.Run(() =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                        // IsLoading = false;
                    });

                });


                try
                {
                    await Task.Run(() =>
                    {
                        IsLoading = true;
                        // IsLoading = false;


                    });

                    if (IsButtonEnabled == true)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.NavigateTo(App.IndividualRegistrationPageView);
                            // IsLoading = false;
                        });

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
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    }
                }
                catch (Exception)
                {


                    await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                }
            });
        }
        #endregion
    }
}
