using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportMobilePage_ViewModel
{
    public class TaxEvasionReportMobilePageViewModel : ViewModelBase
    {
        public ICommand BackButtonClicked { get; set; }
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand VerifyCommand { get; set; }
        //VerifyCommand
        private bool _isVerifyEnable = false;
        public bool IsVerifyEnable
        {
            get
            {
                return _isVerifyEnable;
            }
            set
            {
                _isVerifyEnable = value;
                this.RaisePropertyChanged("IsVerifyEnable");
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
                this.RaisePropertyChanged("IsLoading");
            }
        }
        private string _mobileNumber;
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;         
                this.RaisePropertyChanged("MobileNumber");
            }
        }
        public TaxEvasionReportMobilePageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            this.BackButtonClicked = new Command(this.BackButtonClick);
            this.VerifyCommand = new Command(this.VerifyCommandClick);
        }
        public void BackButtonClick()
        {
            _navigationService.GoBack();
        }
        public void VerifyCommandClick()
        {
            ComingToOTPVerificationScreenFromAndNavigatingTo tesmobnoscreen = new ComingToOTPVerificationScreenFromAndNavigatingTo();
            tesmobnoscreen.tes = "1";
            tesmobnoscreen._ComingToOTPVerificationScreenFrom = ComingToOTPVerificationScreenFrom.IsTes;
;
            if (MobileNumber.Length== 8)
            {
                tesmobnoscreen.MobileNumber = MobileNumber;
                _navigationService.NavigateTo(App.OTPPageView, tesmobnoscreen);
               // WebServiceManager.GetOtpVerification();
            }
//_navigationService.NavigateTo();
        }
    }
}
