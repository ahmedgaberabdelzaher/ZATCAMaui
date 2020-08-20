using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class UpdatePasswordViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Properties
        private bool _IsLoading = false;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        private string _CurrentPasswordEntry;
        public string CurrentPasswordEntry
        {
            get
            {
                return _CurrentPasswordEntry;
            }
            set
            {
                _CurrentPasswordEntry = value;
                RaisePropertyChanged("CurrentPasswordEntry");
            }
        }

        private string _NewPasswordEntry;
        public string NewPasswordEntry
        {
            get
            {
                return _NewPasswordEntry;
            }
            set
            {
                _NewPasswordEntry = value;
                RaisePropertyChanged("NewPasswordEntry");
            }
        }

        private string _ConfirmPasswordEntry;
        public string ConfirmPasswordEntry
        {
            get
            {
                return _ConfirmPasswordEntry;
            }
            set
            {
                _ConfirmPasswordEntry = value;
                RaisePropertyChanged("ConfirmPasswordEntry");
            }
        }
        #endregion

        public UpdatePasswordViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null) { throw new ArgumentNullException("navigationService"); }
            _navigationService = navigationService;

            if (dialogService == null) { throw new ArgumentNullException("dialogService"); }
            _dialogService = dialogService;
        }

        public async Task<bool> ChangePassword()
        {
            /*await Task.Run(() =>
            {
                IsLoading = true;
            });*/

            IsLoading = true;
            bool APIResponse = false;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                APIResponse = await WebServiceManager.GAZTValidateAndChangePassword(lang,
                                                                                    App.TP.Tin,
                                                                                    CurrentPasswordEntry,
                                                                                    NewPasswordEntry);
                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
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
