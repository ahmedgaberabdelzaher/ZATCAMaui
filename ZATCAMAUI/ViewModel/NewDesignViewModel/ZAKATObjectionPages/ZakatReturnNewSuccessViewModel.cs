using System.Windows.Input;


using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{

    public class ZakatReturnNewSuccessViewModel : BaseViewModel
    {
        public ICommand OnInvoiceClicked { get; set; }
        string Cokey = "";
        public bool IsrefreshEnabled = false;
        public ICommand OnBackButtonClicked { get; set; }

        string Cotyp = "";

        #region Property
       
        private EstimatedZAKATReturnsSADADNumberResult _estimatedZAKATSADADNumber;
        public EstimatedZAKATReturnsSADADNumberResult EstimatedZAKATSADADNumber
        {
            get
            {
                return _estimatedZAKATSADADNumber;
            }
            set
            {
                if (_estimatedZAKATSADADNumber == value) return;

                _estimatedZAKATSADADNumber = value;
                OnPropertyChanged("EstimatedZAKATSADADNumber");
            }
        }


        private string _zAKATAmount;
        public string ZAKATAmount
        {
            get
            {
                return _zAKATAmount;
            }
            set
            {
                if (_zAKATAmount == value) return;

                _zAKATAmount = value;
                OnPropertyChanged("ZAKATAmount");
            }
        }


        private string _sADADNumber;
        public string SADADNumber
        {
            get
            {
                return _sADADNumber;
            }
            set
            {
                if (_sADADNumber == value) return;

                _sADADNumber = value;
                OnPropertyChanged("SADADNumber");
            }
        }

        private string _referenceNumber;
        public string ReferenceNumber
        {
            get
            {
                return _referenceNumber;
            }
            set
            {
                if (_referenceNumber == value) return;

                _referenceNumber = value;
                OnPropertyChanged("ReferenceNumber");
            }
        }

        private string _refreshIconImageSource = "";
        public string RefreshIconImageSource
        {
            get
            {
                return _refreshIconImageSource;
            }
            set
            {
                if (_refreshIconImageSource == value) return;

                _refreshIconImageSource = value;
                OnPropertyChanged("RefreshIconImageSource");
            }
        }

        private bool _setSuccessMessageVisibility = false;
        public bool SetSuccessMessageVisibility
        {
            get
            {
                return _setSuccessMessageVisibility;
            }
            set
            {
                if (_setSuccessMessageVisibility == value) return;

                _setSuccessMessageVisibility = value;
                OnPropertyChanged("SetSuccessMessageVisibility");
            }
        }

        #endregion

        #region Constructor
        public ZakatReturnNewSuccessViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {


            OnInvoiceClicked = new Command(() =>
            {
                OnDownLoadInvoiceClicked();
            });

            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });



        }
        #endregion

        #region Method
        public async Task OnPageLoad(ZakatReturnDetailsD zakatReturnDetailsD)
        {
            IsLoading = true;
            await Task.Run(async () =>
            {
                try
                {
                    SetSuccussMessageVisibility();
                    EstimatedZAKATReturnsSADADNumber estimatedZAKATReturnsSADADNumber = await WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(zakatReturnDetailsD.Fbnum, ZAKATReturnDetailsViewModel.Fbguid); // Method to get the invoice
                                
                    if (estimatedZAKATReturnsSADADNumber != null && estimatedZAKATReturnsSADADNumber.d != null)
                    {
                        // IsMainGridVisble = true;
                        if (Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Undisamt) > 0 || Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Disamt) > 0)
                        {
                            Cokey = estimatedZAKATReturnsSADADNumber.d.Cokey;
                            Cotyp = estimatedZAKATReturnsSADADNumber.d.Cotyp;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = true;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = false;
                            EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];
                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Sopbel))
                            {
                                IsrefreshEnabled = true;
                                RefreshIconImageSource = "ic_refresh.png";
                            }
                            else
                            {
                                ReferenceNumber = EstimatedZAKATSADADNumber.Sopbel;
                                SADADNumber = EstimatedZAKATSADADNumber.Sadadid;
                                ZAKATAmount = EstimatedZAKATSADADNumber.Stotamt;
                                IsrefreshEnabled = false;
                                RefreshIconImageSource = "";

                            }
                            //  RefreshIconImageSource = "ic_refresh.png";
                            //else
                            //    IsrefreshEnabled = false;
                        }
                        else
                        {
                            Cokey = estimatedZAKATReturnsSADADNumber.d.Cokey;
                            Cotyp = estimatedZAKATReturnsSADADNumber.d.Cotyp;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = false;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = true;


                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Sopbel))
                            {
                                IsrefreshEnabled = true;
                                RefreshIconImageSource = "ic_refresh.png";
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZVatAcknowledgmentWaitingText));


                            }
                            else
                            {
                                EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];

                                ReferenceNumber = EstimatedZAKATSADADNumber.Sopbel;
                                SADADNumber = EstimatedZAKATSADADNumber.Sadadid;
                                ZAKATAmount = EstimatedZAKATSADADNumber.Stotamt;
                                IsrefreshEnabled = false;
                                RefreshIconImageSource = "";

                            }
                        }
                        GetUpdatedDataAfterAddingComma();
                    }
                    else
                    {
                       MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                            _navigationService.GoBack();
                        });
                        IsLoading = false;
                        estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = true;
                        estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = false;
                    }
                }
                catch (InternetException ex)
                {
                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        // _dialogService.ShowMessage(, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        protected void OnDownLoadInvoiceClicked()
        {
            GetPdfUrl();
        }

        public void GetPdfUrl()
        {
            string url = ZATCAConstants.GAZTGetEstimatedZAKATReturnInvoicePdf + Cokey + "',Cotyp='" + Cotyp + "')/$value?saml2=enabled";
            // string url =  await  WebServiceManager.GAZTEstimatedZAKATReturnInvoicePdf(Cokey);
            ShowPdf(url);
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

        private void GetUpdatedDataAfterAddingComma()
        {
            if (EstimatedZAKATSADADNumber != null)
            {
                try
                {
                    EstimatedZAKATSADADNumber.Undisamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Undisamt);
                    EstimatedZAKATSADADNumber.Disamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Disamt);
                    EstimatedZAKATSADADNumber.Totamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Totamt);
                    EstimatedZAKATSADADNumber.Stotamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Stotamt);
                    EstimatedZAKATSADADNumber.Sdisamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Sdisamt);
                    EstimatedZAKATSADADNumber.Stotamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Stotamt);
                }
                catch (Exception)
                {


                }
            }
        }


        public void ClearData()
        {

            ReferenceNumber = string.Empty;
            SADADNumber = string.Empty;
            ZAKATAmount = string.Empty;

            IsrefreshEnabled = false;
        }

        private void SetSuccussMessageVisibility()
        {
            if (ZAKATReturnDetailsViewModel.IsBillsButtonTapped == true)
            {
                SetSuccessMessageVisibility = false;
            }
            else
            {
                SetSuccessMessageVisibility = true;
            }
        }
        #endregion

    }
}
