using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{

    public class ZakatReturnNewSuccessViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnInvoiceClicked { get; set; }
        string Cokey = "";
        public bool IsrefreshEnabled = false;
        public ICommand OnBackButtonClicked { get; set; }

        string Cotyp = "";

        #region Property
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

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
                RaisePropertyChanged("EstimatedZAKATSADADNumber");
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
                RaisePropertyChanged("ZAKATAmount");
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
                RaisePropertyChanged("SADADNumber");
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
                RaisePropertyChanged("ReferenceNumber");
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
                RaisePropertyChanged("RefreshIconImageSource");
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
                RaisePropertyChanged("SetSuccessMessageVisibility");
            }
        }

        #endregion

        #region Constructor
        public ZakatReturnNewSuccessViewModel(INavigationService navigationService, IDialogService dialogService)
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
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    SetSuccussMessageVisibility();
                    EstimatedZAKATReturnsSADADNumber estimatedZAKATReturnsSADADNumber = await WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(zakatReturnDetailsD.Fbnum, ZAKATReturnDetailsViewModel.Fbguid); // Method to get the invoice
                                                                                                                                                                                                                       //  PopToRootPage();
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZVatAcknowledgmentWaitingText));


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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

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
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));

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
