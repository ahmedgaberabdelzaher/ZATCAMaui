

using Mopups.Services;
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{

    public class VATIndividualSignupTnCPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand OnSubmitClicked { get; set; }

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
                OnPropertyChanged("WebUrl");
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
                OnPropertyChanged("IschkTAndC");
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
                OnPropertyChanged("IsButtonEnabled");
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
                OnPropertyChanged("VerifyButtonDisableColor");
            }
        }

        #endregion
        #region Constructor
        public VATIndividualSignupTnCPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
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
                        await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
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
