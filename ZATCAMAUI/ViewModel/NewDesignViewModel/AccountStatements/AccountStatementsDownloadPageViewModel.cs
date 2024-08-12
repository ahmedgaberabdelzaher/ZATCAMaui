using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.AccountStatements;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements
{

    public class AccountStatementsDownloadPageViewModel : BaseViewModel
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
                OnPropertyChanged("FromDate");
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
                OnPropertyChanged("ToDate");
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
                OnPropertyChanged("ASTaxpayerSelectedValues");
            }
        }

        //ASTaxpayerSelectedValues aSTaxpayerSelectedValues
        public AccountStatementsDownloadPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            GoBackBtnTapped = new Command(() =>
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

            if (dateTimeFrom > dateTimeTo)
            {
                _dialogService.ShowMessage(AppResources.ASFromDateShouldNotbeGreaterThanToDate, AppResources.Information);
            }
            else
            {
                var temp = (dateTimeTo - dateTimeFrom).TotalDays;

                if (temp > 365)
                {
                    _dialogService.ShowMessage(AppResources.ASViewStatementForOnlyOneYear, AppResources.Information);
                }
                else
                {
                    string fromStr = dateTimeFrom.ToString("yyyy-MM-dd");
                    string toStr = dateTimeTo.ToString("yyyy-MM-dd");

                    string pdfUrl = Core.Helper.ZATCAConstants.AccountStatementDownloadPdf + "Fguid='" + App.LoginDataRetrieved.FbGuid + "'" + ",Taxtype='" + ASTaxpayerSelectedValues.TaxType + "',FiscalYear='" + dateTimeFrom.Year + "',StatementFilter='" + ASTaxpayerSelectedValues.StatementFilter + "',FromDt=datetime'" + fromStr + "T00:00:00',ToDt=datetime'" + toStr + "T00:00:00',Langz='" + GetLangZParameter() + "')/$value";
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
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
                    });
                }
            }
<<<<<<< HEAD:ZATCAMAUI/ViewModel/NewDesignViewModel/AccountStatements/AccountStatementsDownloadPageViewModel.cs
            catch (Exception)
            {
=======
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
>>>>>>> c4bcf28b6 (CR6238 code merge to prod by chandu):GAZT/GAZT/ViewModel/NewDesignViewModel/AccountStatements/AccountStatementsDownloadPageViewModel.cs
            }
        }

    }
}
