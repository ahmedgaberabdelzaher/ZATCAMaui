

using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AcknowledgementDetailsPage
{

    public class AcknowledgementDetailsPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand OnVATRefreshButtonClicked { get; set; }
        public ICommand OnDownloadFormClicked { get; set; }
        public ICommand OnAcknowlwdgementClicked { get; set; }
        public ICommand GoHomeClick { get; set; }
        #endregion
        #region Property

        private string _tPName = "";
        public string TPName
        {
            get
            {
                return _tPName;
            }
            set
            {
                _tPName = value;
                OnPropertyChanged("TPName");
            }
        }
        private string _returnReferenceNumber = "";
        public string ReturnReferenceNumber
        {
            get
            {
                return _returnReferenceNumber;
            }
            set
            {
                _returnReferenceNumber = value;
                OnPropertyChanged("ReturnReferenceNumber");
            }
        }
        private bool _breakdownAmountVisibility = false;
        public bool BreakdownAmountVisibility
        {
            get
            {
                return _breakdownAmountVisibility;
            }
            set
            {
                _breakdownAmountVisibility = value;
                OnPropertyChanged("BreakdownAmountVisibility");
            }
        }
        private string _taxablePeriod = "";
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {
                _taxablePeriod = value;
                OnPropertyChanged("TaxablePeriod");
            }
        }
        private string _receiptDate = "";
        public string ReceiptDate
        {
            get
            {
                return _receiptDate;
            }
            set
            {
                _receiptDate = value;
                OnPropertyChanged("ReceiptDate");
            }
        }
        private string _sadadNumber = "";
        public string SadadNumber
        {
            get
            {
                return _sadadNumber;
            }
            set
            {
                _sadadNumber = value;
                OnPropertyChanged("SadadNumber");
            }
        }
        private string _amountPayable = "";
        public string AmountPayable
        {
            get
            {
                return _amountPayable;
            }
            set
            {
                _amountPayable = value;
                OnPropertyChanged("AmountPayable");
            }
        }
        private bool _isSadadNoteVisible = true;
        public bool IsSadadNoteVisible
        {
            get
            {
                return _isSadadNoteVisible;
            }
            set
            {
                _isSadadNoteVisible = value;
                OnPropertyChanged("IsSadadNoteVisible");
            }
        }
        private bool _isSadadNumberVisible = false;
        public bool IsSadadNumberVisible
        {
            get
            {
                return _isSadadNumberVisible;
            }
            set
            {
                _isSadadNumberVisible = value;
                if (_isSadadNumberVisible == true)
                {
                    IsSadadNoteVisible = false;
                    BreakdownAmountVisibility = true;
                }
                else
                {
                    BreakdownAmountVisibility = false;
                }
                OnPropertyChanged("IsSadadNumberVisible");
            }
        }
        private bool _isButtonVisible = false;
        public bool IsButtonVisible
        {
            get
            {
                return _isButtonVisible;
            }
            set
            {
                _isButtonVisible = value;
                OnPropertyChanged("IsButtonVisible");
            }
        }


        private bool _isAcknowledgementButtonVisible = false;
        public bool IsAcknowledgementButtonVisible
        {
            get
            {
                return _isAcknowledgementButtonVisible;
            }
            set
            {
                _isAcknowledgementButtonVisible = value;
                OnPropertyChanged("IsAcknowledgementButtonVisible");
            }
        }

        private bool _isRefreshButtonVisible = true;
        public bool IsRefreshButtonVisible
        {
            get
            {
                return _isRefreshButtonVisible;
            }
            set
            {
                _isRefreshButtonVisible = value;
                OnPropertyChanged("IsRefreshButtonVisible");
            }
        }
        private VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                _vATDeclarationData = value;
                OnPropertyChanged("VATDeclarationData");
            }
        }
        #endregion

        #region Constructor
        public AcknowledgementDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {   
            OnVATRefreshButtonClicked = new Command(() =>
            {
                // Call Sadad number API
            });
            OnDownloadFormClicked = new Command( async () =>
            {
                string Url = string.Empty;
                Url = ZATCAConstants.ZOdownloadAckLetter + VATDeclarationData.data.Fbnum;
                await  ShowPdf(Url);
            });
            OnAcknowlwdgementClicked = new Command( async () =>
            {
                string Url = string.Empty;
                Url = ZATCAConstants.ZOdownloadAckLetter + VATDeclarationData.data.Fbnum;
                await ShowPdf(Url);
            });
            GoHomeClick = new Command(async () =>
            {
                await _navigationService.NavigateTo(App.SFLandingPageView);
            });
        }
        #endregion
        #region Method
        public async Task ShowPdf(string pdfUrl)
        {

            if (pdfUrl != null)
            {
               await _navigationService.NavigateTo(App.PdfView, pdfUrl);
            }
            else
            {
                await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
            }
        }
        public async Task OnRefreshClick()
        {
            try
            {
                IsLoading = true;
                var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.data.Fbnum);
                await PopToRootPage();
                if (response != null && response.d != null && response.d.results.Count != 0)
                {
                    SadadNumber = response.d.results[0].Sopbel;
                    AmountPayable = response.d.results[0].Betrh;
                    if (!string.IsNullOrEmpty(SadadNumber))
                    {
                        IsSadadNoteVisible = false;
                        if (VATDeclarationData.data.RefundFg == "1")
                        {
                            IsSadadNumberVisible = false;
                        }
                        else
                        {
                            IsSadadNumberVisible = true;
                        }
                        IsButtonVisible = true;
                        if (VATDeclarationData.data.EstimatedFg == "X")
                        {
                            IsAcknowledgementButtonVisible = false;
                        }
                        else
                        {
                            IsAcknowledgementButtonVisible = true;
                        }
                        IsRefreshButtonVisible = false;
                    }
                    else
                    {
                        IsButtonVisible = false;
                        IsAcknowledgementButtonVisible = false;
                        IsRefreshButtonVisible = true;
                    }
                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }
        #endregion
    }
}
