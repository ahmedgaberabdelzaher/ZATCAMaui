using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.AcknowledgementDetailsPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class AcknowledgementDetailsPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnVATRefreshButtonClicked { get; set; }
        public ICommand OnDownloadFormClicked { get; set; }
        public ICommand OnAcknowlwdgementClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand GoHomeClick { get; set; }
        #endregion
        #region Property
        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
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
                RaisePropertyChanged("TPName");
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
                RaisePropertyChanged("ReturnReferenceNumber");
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
                RaisePropertyChanged("BreakdownAmountVisibility");
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
                RaisePropertyChanged("TaxablePeriod");
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
                RaisePropertyChanged("ReceiptDate");
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
                RaisePropertyChanged("SadadNumber");
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
                RaisePropertyChanged("AmountPayable");
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
                RaisePropertyChanged("IsSadadNoteVisible");
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
                RaisePropertyChanged("IsSadadNumberVisible");
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
                RaisePropertyChanged("IsButtonVisible");
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
                RaisePropertyChanged("IsAcknowledgementButtonVisible");
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
                RaisePropertyChanged("IsRefreshButtonVisible");
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
                RaisePropertyChanged("VATDeclarationData");
            }
        }
        #endregion
        #region Constructor
        public AcknowledgementDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            OnVATRefreshButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                // Call Sadad number API
            });
            OnDownloadFormClicked = new Xamarin.Forms.Command(async () =>
            {
                String Url = string.Empty;
                // Url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=%2765000178937%27)/$value?saml2=disabled";
               // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=disabled";
                Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_MOB_SRV/cover_formSet(Euser='"+ App.TP.Tin+"',Fbnum='"+VATDeclarationData.d.Fbnum+"',Utype='')/$value?saml2=enabled";
                ShowPdf(Url);
            });
            OnAcknowlwdgementClicked = new Xamarin.Forms.Command(async () =>
            {
                String Url = string.Empty;
               // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value?saml2=disabled";
                Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_MOB_SRV/Ack_letterSet(Euser='"+ App.TP.Tin+ "',Fbnum='"+VATDeclarationData.d.Fbnum+"')/$value?saml2=enabled";
                ShowPdf(Url);
            });
            GoBackClick = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
            GoHomeClick = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
        }
        #endregion
        #region Method
        public async void ShowPdf(string pdfUrl)
        {
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    if (pdfUrl != null)
            //    {
            //        //Uri uri = new Uri(pdfUrl);
            //        //Device.OpenUri(uri);
            //        _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
            //    }
            //    else
            //    {
            //        //pop that certificate is not available
            //        Device.BeginInvokeOnMainThread(async () =>
            //        {
            //            await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
            //        });
            //    }
            //}
            //else
            //{
                if (pdfUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    //pop that certificate is not available
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    });
                }
            //}
        }
        public async Task OnRefreshClick()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async() =>
                {
                    var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.d.Fbnum);
                    PopToRootPage();
                    if (response != null && response.d != null && response.d.results.Count!=0)
                    {
                        SadadNumber = response.d.results[0].Sopbel;
                        AmountPayable = response.d.results[0].Betrh;
                        if (!string.IsNullOrEmpty(SadadNumber))
                        {
                            IsSadadNoteVisible = false;
                            if (VATDeclarationData.d.RefundFg == "1")
                            {
                                IsSadadNumberVisible = false;
                            }
                            else
                            {
                                IsSadadNumberVisible = true;
                            }
                            IsButtonVisible = true;
                            if (VATDeclarationData.d.EstimatedFg == "X")
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
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        #endregion
    }
}
