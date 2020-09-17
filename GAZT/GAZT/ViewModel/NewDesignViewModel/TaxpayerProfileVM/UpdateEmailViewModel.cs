using System;
using System.Threading.Tasks;
using EGAZT.Models.TPProfile;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;

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
        public async Task<TaxPayerProfile> VarifyEmail()
        {
            IsLoading = true;
            TaxPayerProfile TP = null;

            try
            {
                // API Calls
                await Task.Run(async () =>
                {
                    TPProfileAPIRequestDataModel APIRequestDataModel = new TPProfileAPIRequestDataModel();
                    APIRequestDataModel.RequestType = "GETOTPEMAIL";
                    APIRequestDataModel.NewEmail = NewEmailText;
                    APIRequestDataModel.OldEmail = CurrentEmailText;

                    TPProfileAPIRequest TPProfileAPIRequestData = TPProfileAPIRequest.PrepareRequestData(APIRequestDataModel);
                    TP = await WebServiceManager.POSTTPProfileAPICalls(TPProfileAPIRequestData, "GETOTPEMAIL");

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
                ShowValidationPopup(ex.Message);
            }

            return TP;
        }

        public void ShowValidationPopup(string sourceString)
        {
            PopUp popUp = new PopUp();
            popUp.Message = sourceString;
            popUp.IsLinkAvailable = false;

            if (App.IsArabic)
                popUp.FlowDirections = "RightToLeft";
            else
                popUp.FlowDirections = "LeftToRight";

            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(sourceString));

           // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }
        #endregion
    }
}