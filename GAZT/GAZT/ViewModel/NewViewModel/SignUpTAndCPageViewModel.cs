using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class SignUpTAndCPageViewModel : ViewModelBase
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnSubmitClicked { get; set; }
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
                }
                else
                {
                    IsButtonEnabled = false;
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
                _navigationService.NavigateTo(App.SignUpFormPageView);
             

            });

        }
        #endregion
    }
}
