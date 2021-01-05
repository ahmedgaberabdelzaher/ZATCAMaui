using System;
using System.Windows.Input;
using EGAZT.Models.AccountStatements;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    [Preserve(AllMembers = true)]
    public class AccountStatementsDownloadPageViewModel: BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand DownloadBtnTappedDownloadPage { get; set; }

        private string _fromDate = AppResources.ASAccountStatementFrom;
        public string FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                if (_fromDate == value) return;

                _fromDate = value;
                RaisePropertyChanged("FromDate");
            }
        }

        private string _toDate = AppResources.ASAccountStatementTo;
        public string ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                if (_toDate == value) return;

                _toDate = value;
                RaisePropertyChanged("ToDate");
            }
        }

        private ASTaxpayerSelectedValues _asTaxpayerSelectedValues;
        public ASTaxpayerSelectedValues ASTaxpayerSelectedValues
        {
            get
            {
                return _asTaxpayerSelectedValues;
            }
            set
            {
                if (_asTaxpayerSelectedValues == value) return;

                _asTaxpayerSelectedValues = value;
                RaisePropertyChanged("ASTaxpayerSelectedValues");
            }
        }

        //ASTaxpayerSelectedValues aSTaxpayerSelectedValues
        public AccountStatementsDownloadPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            DownloadBtnTappedDownloadPage = new Command(DownloadBtnClickedDownloadPage);
            ASTaxpayerSelectedValues = new ASTaxpayerSelectedValues();
        }

        public void DownloadBtnClickedDownloadPage()
        {
            DateTime dateTimeFrom = DateTime.Parse(FromDate);
            DateTime dateTimeTo = DateTime.Parse(ToDate);

            if(dateTimeFrom > dateTimeTo)
            {
                _dialogService.ShowMessage(AppResources.ASFromDateShouldNotbeGreaterThanToDate, AppResources.Information);
            }
            else
            {
                var temp = (dateTimeTo - dateTimeFrom).TotalDays;

                if(temp > 365)
                {
                    _dialogService.ShowMessage(AppResources.ASViewStatementForOnlyOneYear, AppResources.Information);
                }
                else
                {
                    string fromStr = dateTimeFrom.ToString("yyyy-MM-dd");
                    string toStr = dateTimeTo.ToString("yyyy-MM-dd");

                    String pdfUrl = Constants.AccountStatementDownloadPdf + "Fguid='" + App.LoginDataRetrieved.FbGuid + "'" + ",Taxtype='" + ASTaxpayerSelectedValues.TaxType + "',FiscalYear='" + dateTimeFrom.Year + "',StatementFilter='" + ASTaxpayerSelectedValues.StatementFilter + "',FromDt=datetime'" + fromStr + "T00:00:00',ToDt=datetime'" + toStr + "T00:00:00',Langz='" + GetLangZParameter() + "')/$value";
                    ShowPdf(pdfUrl);
                }
            }

        }

        private static char GetLangZParameter()
        {
            if (App.IsArabic)
                return 'A';
            else
                return 'E';
        }

        public void ShowPdf(string pdfUrl)
        {
            try
            {
                if (pdfUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
