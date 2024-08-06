

using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.TPProfile;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{

    public class UpdateEmailViewModel : BaseViewModel
    {

        #region Properties
        private string _CurrentEmailText;
        public string CurrentEmailText
        {
            get { return _CurrentEmailText; }
            set
            {
                if (_CurrentEmailText == value) return;
                _CurrentEmailText = value;
                OnPropertyChanged("CurrentEmailText");
            }
        }

        private string _NewEmailText;
        public string NewEmailText
        {
            get { return _NewEmailText; }
            set
            {
                if (_NewEmailText == value) return;

                _NewEmailText = value;
                OnPropertyChanged("NewEmailText");
            }
        }

        private string _ConfirmEmailText;
        public string ConfirmEmailText
        {
            get { return _ConfirmEmailText; }
            set
            {
                if (_ConfirmEmailText == value) return;

                _ConfirmEmailText = value;
                OnPropertyChanged("ConfirmEmailText");
            }
        }

      
        #endregion

        public UpdateEmailViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
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

            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(sourceString));

            // MopupService.Instance.PushAsync(new AddPopPageView(popUp));
        }
        #endregion
    }
}