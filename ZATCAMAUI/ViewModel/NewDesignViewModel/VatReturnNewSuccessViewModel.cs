
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class VatReturnNewSuccessViewModel : BaseViewModel
    {
        public ICommand OnDownloadFormClicked { get; set; }
        public ICommand OnAcknowlwdgementClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }


        #region Properties

        public string _returnReferenceNumber;
        public string ReturnReferenceNumber
        {
            get
            {
                return _returnReferenceNumber;
            }
            set
            {
                if (_returnReferenceNumber == value) return;
                _returnReferenceNumber = value;
                OnPropertyChanged("ReturnReferenceNumber");
            }
        }

        public string _taxablePeriod;
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {
                if (_taxablePeriod == value) return;

                _taxablePeriod = value;
                OnPropertyChanged("TaxablePeriod");
            }
        }

        public string _sadadNumber;
        public string SadadNumber
        {
            get
            {
                return _sadadNumber;
            }
            set
            {
                if (_sadadNumber == value) return;

                _sadadNumber = value;
                OnPropertyChanged("SadadNumber");
            }
        }
        public string _amountPayable;
        public string AmountPayable
        {
            get
            {
                return _amountPayable;
            }
            set
            {
                if (_amountPayable == value) return;

                _amountPayable = value;
                OnPropertyChanged("AmountPayable");
            }
        }

        public bool _isSadadNumberVisible;
        public bool IsSadadNumberVisible
        {
            get
            {
                return _isSadadNumberVisible;
            }
            set
            {
                if (_isSadadNumberVisible == value) return;

                _isSadadNumberVisible = value;
                OnPropertyChanged("IsSadadNumberVisible");
            }
        }



        public VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                if (_vATDeclarationData == value) return;

                _vATDeclarationData = value;
                OnPropertyChanged("VATDeclarationData");
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
                if (_isButtonVisible == value) return;

                _isButtonVisible = value;
                OnPropertyChanged("IsButtonVisible");
            }
        }
        private bool _isCreditCarriedTextVisible = false;
        public bool IsCreditCarriedTextVisible
        {
            get
            {
                return _isCreditCarriedTextVisible;
            }
            set
            {
                if (_isCreditCarriedTextVisible == value) return;

                _isCreditCarriedTextVisible = value;
                OnPropertyChanged("IsCreditCarriedTextVisible");
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
                if (_isAcknowledgementButtonVisible == value) return;

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
                if (_isRefreshButtonVisible == value) return;

                _isRefreshButtonVisible = value;
                OnPropertyChanged("IsRefreshButtonVisible");
            }
        }


        #endregion

        public VatReturnNewSuccessViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnDownloadFormClicked = new Command( async () =>
            {
                string Url = string.Empty;
                Url = ZATCAConstants.ZOdownloadAckLetter + VATDeclarationData.data.Fbnum;
               await ShowPdf(Url);
            });
            OnAcknowlwdgementClicked = new Command( async () =>
            {
                string Url = string.Empty;
                Url = ZATCAConstants.ZOdownloadAckLetter + VATDeclarationData.data.Fbnum;
                await ShowPdf(Url);
            });

            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }
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
                        IsSadadNumberVisible = false;
                        IsButtonVisible = false;
                        IsAcknowledgementButtonVisible = false;
                        IsRefreshButtonVisible = true;
                    }
                }

                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }
    }
}
