using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class UpdateEmailViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Properties
        private string _CurrentEmailText;
        public string CurrentEmailText
        {
            get { return _CurrentEmailText; }
            set
            {
                _CurrentEmailText = value;
                RaisePropertyChanged("CurrentEmailText");
            }
        }

        private string _NewEmailText;
        public string NewEmailText
        {
            get { return _NewEmailText; }
            set
            {
                _NewEmailText = value;
                RaisePropertyChanged("NewEmailText");
            }
        }

        private string _ConfirmEmailText;
        public string ConfirmEmailText
        {
            get { return _ConfirmEmailText; }
            set
            {
                _ConfirmEmailText = value;
                RaisePropertyChanged("ConfirmEmailText");
            }
        }

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
        #endregion

        public UpdateEmailViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null) { throw new ArgumentNullException("navigationService"); }
            _navigationService = navigationService;

            if (dialogService == null) { throw new ArgumentNullException("dialogService"); }
            _dialogService = dialogService;
        }

        #region Method
        public async Task<bool> VarifyEmail()
        {
            IsLoading = true;
            bool APIResponse = false;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                // API Calls
                await Task.Run(async () =>
                {
                    System.Diagnostics.Debug.WriteLine("NEW EMAIL : ", NewEmailText);
                    System.Diagnostics.Debug.WriteLine("CONFIRM EMAIL : ", ConfirmEmailText);

                    if((String.Compare(CurrentEmailText, NewEmailText,true) !=0) && ((String.Compare(NewEmailText, ConfirmEmailText, true)==0)))
                        APIResponse = await WebServiceManager.GAZTGetOTPForEmail(lang, App.TP.Tin, CurrentEmailText, NewEmailText);
                    else
                    {
                        if (String.Compare(CurrentEmailText, NewEmailText, true) == 0)
                            throw new Exception(AppResources.NDNewEmailCannotBeSameAsOldEmail);
                        else if (String.Compare(NewEmailText, ConfirmEmailText, true) != 0)
                            throw new Exception(AppResources.ZZZZEmailandconfirmemailshouldmatchup);
                    }

                    // setup updated email's
                    UpdateEmailDataModel updateEmailData = new UpdateEmailDataModel();
                    updateEmailData.CurrentEmail = CurrentEmailText;
                    updateEmailData.NewEmail = NewEmailText;
                    IsLoading = false;

                });
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
        #endregion
    }
}