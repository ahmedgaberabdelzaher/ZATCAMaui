using System.Windows.Input;


using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{


    public class TINDeregestrationSuccessPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand GoBackClick { get; set; }

        #endregion

        private string _fBNumber = string.Empty;
        public string FBNumber
        {
            get
            {
                return _fBNumber;
            }
            set
            {
                _fBNumber = value;
                OnPropertyChanged("FBNumber");
            }
        }
        public TINDeregestrationSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
           
            GoBackClick = new Command( () =>
            {
                _navigationService.GoBack();
            });

        }

        #region Download Confirmation

        public void downloadConfirmation()
        {
            var localPath = string.Empty;
            try
            {

                string Url = string.Empty;
                // Url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=%2765000178937%27)/$value?saml2=disabled";
                // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=disabled";

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
                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                });
            }
        }

        #endregion
    }
}
