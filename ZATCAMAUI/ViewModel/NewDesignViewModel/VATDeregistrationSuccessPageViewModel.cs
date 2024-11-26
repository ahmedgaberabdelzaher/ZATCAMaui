using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
 
    public class VATDeregistrationSuccessPageViewModel : BaseViewModel
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
                if (_fBNumber == value) return;
                _fBNumber = value;
                OnPropertyChanged("FBNumber");
            }
        }
        public VATDeregistrationSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

        }

        #region Download Confirmation

        public void downloadConfirmation()
        {

            try
            {
                string Url = string.Empty;
                Url = ZATCAConstants.ZOdownloadAckLetter + FBNumber;
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
                });
            }
        }


        #endregion

    }
}
