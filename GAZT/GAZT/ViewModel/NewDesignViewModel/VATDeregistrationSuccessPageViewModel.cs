using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using pdfjs.Interfaces;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class VATDeregistrationSuccessPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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
                RaisePropertyChanged("FBNumber");
            }
        }
        public VATDeregistrationSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

        }

        #region Download Confirmation

        public void downloadConfirmation()
        {
           
            try
            {
                String Url = string.Empty;
            //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='62000006372')/$value
                Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + FBNumber + "')/$value?saml2=enabled";
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));

                    // await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                });
            }
        }


        #endregion

    }
}
