using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class NewTaxpayerProfileViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public string UpdatedMobileNumber = string.Empty;
        public string UpdatedEmail = string.Empty;
        #endregion

        #region Properties

        private string _TPProfileNameLbl;
        public string TPProfileNameLbl
        {
            get { return _TPProfileNameLbl; }
            set
            {
                _TPProfileNameLbl = value;
                RaisePropertyChanged("TPProfileNameLbl");
            }
        }

        private string _TINLabel;
        public string TINLabel
        {
            get { return _TINLabel; }
            set
            {
                _TINLabel = value;
                RaisePropertyChanged("TINLabel");
            }
        }

        private string _MobileNumber;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set
            {
                _MobileNumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }

        private string _EmailEntry;
        public string EmailEntry
        {
            get { return _EmailEntry; }
            set
            {
                _EmailEntry = value;
                RaisePropertyChanged("EmailEntry");
            }
        }

        private string _PasswordEntry;
        public string PasswordEntry
        {
            get { return _PasswordEntry; }
            set
            {
                _PasswordEntry = value;
                RaisePropertyChanged("PasswordEntry");
            }
        }

        private string _ShowHidePasswordImage;
        public string ShowHidePasswordImage
        {
            get { return _ShowHidePasswordImage; }
            set
            {
                _ShowHidePasswordImage = value;
                RaisePropertyChanged("ShowHidePasswordImage");
            }
        }
        #endregion

        public NewTaxpayerProfileViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null) { throw new ArgumentNullException("navigationService"); }
            _navigationService = navigationService;

            if (dialogService == null) { throw new ArgumentNullException("dialogService"); }
            _dialogService = dialogService;
        }

        public async Task<TaxPayerProfile> GetTPProfileData()
        {
            /*await Task.Run(() =>
            {
                IsLoading = true;
            });*/

            TaxPayerProfile APIResponse = null;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                await Task.Run(async () =>
                {
                    APIResponse = WebServiceManager.SFGAZTGetTaxPayerProfile(App.TP.Tin, lang);
                });

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception : ", ex.Message);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                });
            }

            return APIResponse;
        }
    }
}
