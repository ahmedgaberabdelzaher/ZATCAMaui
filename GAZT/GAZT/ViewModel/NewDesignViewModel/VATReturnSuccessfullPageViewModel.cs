using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class VATReturnSuccessfullPageViewModel : BaseViewModel
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
                RaisePropertyChanged("ReturnReferenceNumber");
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
                RaisePropertyChanged("TaxablePeriod");
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
                RaisePropertyChanged("SadadNumber");
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
                RaisePropertyChanged("AmountPayable");
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
                RaisePropertyChanged("IsSadadNumberVisible");
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
                RaisePropertyChanged("VATDeclarationData");
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
                RaisePropertyChanged("IsButtonVisible");
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
                RaisePropertyChanged("IsCreditCarriedTextVisible");
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
                if (_isRefreshButtonVisible == value) return;

                _isRefreshButtonVisible = value;
                RaisePropertyChanged("IsRefreshButtonVisible");
            }
        }


        #endregion

        public VATReturnSuccessfullPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }




            OnDownloadFormClicked = new Xamarin.Forms.Command(() =>
            {
                String Url = string.Empty;
                // Url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=%2765000178937%27)/$value?saml2=disabled";
                // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=disabled";
                //  Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_MOB_SRV/cover_formSet(Euser='" + App.TP.Tin + "',Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=enabled";

                Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value";
                ShowPdf(Url);
            });
            OnAcknowlwdgementClicked = new Xamarin.Forms.Command(() =>
            {
                String Url = string.Empty;
                // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value?saml2=disabled";
                Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_MOB_SRV/Ack_letterSet(Euser='" + App.TP.Tin + "',Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value?saml2=enabled";
                ShowPdf(Url);
            });

            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
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
                await Task.Run(async () =>
                {
                    var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.d.Fbnum);
                    PopToRootPage();
                    if (response != null && response.d != null && response.d.results.Count != 0)
                    {
                        SadadNumber = response.d.results[0].Sopbel;
                        AmountPayable = response.d.results[0].Betrh;
                        if (!string.IsNullOrEmpty(SadadNumber))
                        {
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
                            IsSadadNumberVisible = false;
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
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
    }
}
