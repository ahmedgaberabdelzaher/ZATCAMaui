using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.SignUpTAndCPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class SignUpTAndCPageViewModel : ViewModelBase
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
                if(_ischkTAndC==true)
                {
                    IsButtonEnabled = true;
                    VerifyButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
                }
                else
                {
                    IsButtonEnabled = false;
                    VerifyButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
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
        private Color _verifybuttonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
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
        public SignUpTAndCPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
                        await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
