using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    //class VATIndividualSignupTnCPageViewModel
    //{
    //}
    public class VATIndividualSignupTnCPageViewModel : ViewModelBase
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
                _webUrl = value;
                RaisePropertyChanged("WebUrl");
            }
        }
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
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
                _ischkTAndC = value;
                if (_ischkTAndC == true)
                {
                    IsButtonEnabled = true;
                    VerifyButtonDisableColor = Color.FromHex("#005e4b");
                }
                else
                {
                    IsButtonEnabled = false;
                    VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
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
        private Color _verifybuttonDisableColor = Color.FromHex("#9EA4A9");
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
        public VATIndividualSignupTnCPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            GoBackClick = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
            BackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
            OnSubmitClicked = new Command(async () =>
            {
                await Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
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
                            Device.BeginInvokeOnMainThread(() =>
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
                    catch (Exception ex)
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
