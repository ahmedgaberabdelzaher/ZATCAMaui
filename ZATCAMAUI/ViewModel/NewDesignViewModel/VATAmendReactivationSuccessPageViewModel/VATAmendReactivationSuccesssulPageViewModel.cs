using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Mopups.Services;
using System.Globalization;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATAmendReactivationSuccessPageViewModel
{
   
    public class VATAmendReactivationSuccesssulPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        private string _fBNumber = string.Empty;
        public string FBNumber
        {
            get
            {
                return _fBNumber;
            }
            set
            {
                if (_fBNumber == value) return;
                _fBNumber = value;
                RaisePropertyChanged("FBNumber");
            }
        }
        public VATAmendReactivationSuccesssulPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

        }
        #region Download Confirmation

        public void downloadConfirmation()
        {

            try
            {
                string Url = string.Empty;
                //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='62000006372')/$value
                Url = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + FBNumber + "')/$value?saml2=enabled";
                ShowPdf(Url);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void ShowPdf(string pdfUrl)
        {

            if (pdfUrl != null)
            {
                _navigationService.NavigateTo(App.PdfView, pdfUrl);
            }
            else
            {
                //pop that certificate is not available
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));

                    // await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                });
            }
        }


        #endregion
        public async Task LogOut()
        {
            await Task.Run(() =>
            {
                App.DisplayProgressView();
            });
            if (App.TP != null)
                App.TP = null;
            if (App.PreviousIsArabic)
            {
                string langName = "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                string langName = "en-US";
                AppResources.Culture = new CultureInfo(langName);
            }

            try
            {
                await WebServiceManager.GAZTLogOff();
            }
            catch
            {

            }

            await Task.Run(() =>
            {
                App.HideProgressView();
            });


            App.IsLogOut = true;
            App.IsLoginCalled = false;
            App.IsSamlApiCalledAndroid = false;

            try
            {
                App.httpClientHandler = new HttpClientHandler();
                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
            }
            catch (Exception)
            {


            }
            _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
        }
    }
}