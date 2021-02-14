using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Helper;
using EGAZT.Models;
using EGAZT.Models.PaymentModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.PaymentOptions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class ZAKATReturnDetailsViewModel : ViewModelBase
    {
        /*private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;*/
        //============================start===================================================
        public ICommand OnSubmitClicked { get; set; }
        public ICommand OnConfirmClicked { get; set; }
        public ICommand OnAmendClick { get; set; }
        public static bool IsBillsButtonTapped { get; set; }

        public ICommand OnBackButtonClicked { get; set; }
        public ICommand OnEditClicked { get; set; }
        public ICommand OnChangeFromEstimateToAccountingBasisButtonClicked { get; set; }
        List<EstimateZakatAttachment> EstimateZakatAttachmentList = new List<EstimateZakatAttachment>();
        public static string Fbguid { get; set; }
        public bool IsCurrentZAKATTaxLess = false;
        public bool isThresholdValueLessThanTotalVATSales;
        public const string SubmitPostOperation = "05";
        public const string ConfirmPostOperationWithoutObjection = "65";
        public const string ConfirmPostOperationWithObjection = "66";
        public string Estsl { get; set; }

        public double existingZakatBase = 0.00;
        public static ZakatReturnDetailsD ZakatReturnDetailToCompare = new ZakatReturnDetailsD();

        #region Property

        private bool _isEditVisible = true;
        public bool isEditVisible
        {
            get
            {
                return _isEditVisible;
            }
            set
            {
                if (_isEditTextVisible == value) return;
                _isEditVisible = value;
                RaisePropertyChanged("isEditVisible");
            }
        }
        public string ApplePayTokenData;
        private bool _isLabelVisible = false;
        public bool isLabelVisible
        {
            get
            {
                return _isLabelVisible;
            }
            set
            {
                if (_isLabelVisible == value) return;

                _isLabelVisible = value;
                RaisePropertyChanged("isLabelVisible");
            }
        }
        //============================end=================================================================
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

        private bool _isEditTextVisible = true;
        public bool IsEditTextVisible
        {
            get
            {
                return _isEditTextVisible;
            }
            set
            {
                if (_isEditTextVisible == value) return;

                _isEditTextVisible = value;
                RaisePropertyChanged("IsEditTextVisible");
            }
        }


        private bool _setAmendButtonVisibility = false;
        public bool SetAmendButtonVisibility
        {
            get
            {
                return _setAmendButtonVisibility;
            }
            set
            {
                if (_setAmendButtonVisibility == value) return;

                _setAmendButtonVisibility = value;
                RaisePropertyChanged("SetAmendButtonVisibility");
            }
        }


        private ZakatReturnDetailsD _zakatReturnDetail;
        public ZakatReturnDetailsD ZakatReturnDetail
        {
            get
            {
                return _zakatReturnDetail;
            }
            set
            {
                if (_zakatReturnDetail == value) return;

                _zakatReturnDetail = value;
                RaisePropertyChanged("ZakatReturnDetail");
            }
        }


        private ZakatReturnDetails _zakatReturnDetails;
        public ZakatReturnDetails ZakatReturnDetails
        {
            get
            {
                return _zakatReturnDetails;
            }
            set
            {
                if (_zakatReturnDetails == value) return;

                _zakatReturnDetails = value;
                RaisePropertyChanged("ZakatReturnDetails");
            }
        }


        private string _abrzu;
        public string Abrzu
        {
            get
            {
                return _abrzu;
            }
            set
            {
                if (_abrzu == value) return;

                _abrzu = value;
                RaisePropertyChanged("Abrzu");
            }
        }


        private bool _isPayNowVisible;
        public bool IsPayNowVisible
        {
            get
            {
                return _isPayNowVisible;
            }
            set
            {
                if (_isPayNowVisible == value) return;

                _isPayNowVisible = value;
                RaisePropertyChanged("IsPayNowVisible");
            }
        }
        
        private string _fromDate;
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

        private string _toDate;
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


        private string _releaseOrBillDetailsButtonText;
        public string ReleaseOrBillDetailsButtonText
        {
            get
            {
                return _releaseOrBillDetailsButtonText;
            }
            set
            {
                if (_releaseOrBillDetailsButtonText == value) return;

                _releaseOrBillDetailsButtonText = value;
                RaisePropertyChanged("ReleaseOrBillDetailsButtonText");
            }
        }

        private string _totalVATSalesEditImageSource;
        public string TotalVATSalesEditImageSource
        {
            get
            {
                return _totalVATSalesEditImageSource;
            }
            set
            {
                if (_totalVATSalesEditImageSource == value) return;

                _totalVATSalesEditImageSource = value;
                RaisePropertyChanged("TotalVATSalesEditImageSource");
            }
        }

        private string _averageNumberOfLabourEditImageSource;
        public string AverageNumberOfLabourEditImageSource
        {
            get
            {
                return _averageNumberOfLabourEditImageSource;
            }
            set
            {
                if (_averageNumberOfLabourEditImageSource == value) return;

                _averageNumberOfLabourEditImageSource = value;
                RaisePropertyChanged("AverageNumberOfLabourEditImageSource");
            }
        }

        private string _importValueEditImageSource;
        public string ImportValueEditImageSource
        {
            get
            {
                return _importValueEditImageSource;
            }
            set
            {
                if (_importValueEditImageSource == value) return;

                _importValueEditImageSource = value;
                RaisePropertyChanged("ImportValueEditImageSource");
            }
        }


        private string _importFromPointOfSalesEditImageSource;
        public string ImportFromPointOfSalesEditImageSource
        {
            get
            {
                return _importFromPointOfSalesEditImageSource;
            }
            set
            {
                if (_importFromPointOfSalesEditImageSource == value) return;

                _importFromPointOfSalesEditImageSource = value;
                RaisePropertyChanged("ImportFromPointOfSalesEditImageSource");
            }
        }

        private string _contactFromETIMADSystemEditImageSource;
        public string ContactFromETIMADSystemEditImageSource
        {
            get
            {
                return _contactFromETIMADSystemEditImageSource;
            }
            set
            {
                if (_contactFromETIMADSystemEditImageSource == value) return;

                _contactFromETIMADSystemEditImageSource = value;
                RaisePropertyChanged("ContactFromETIMADSystemEditImageSource");
            }
        }

        private string _exportValueEditImageSource;
        public string ExportValueEditImageSource
        {
            get
            {
                return _exportValueEditImageSource;
            }
            set
            {
                if (_exportValueEditImageSource == value) return;

                _exportValueEditImageSource = value;
                RaisePropertyChanged("ExportValueEditImageSource");
            }
        }

        private string _purchaseValueEditImageSource;
        public string PurchaseValueEditImageSource
        {
            get
            {
                return _purchaseValueEditImageSource;
            }
            set
            {
                if (_purchaseValueEditImageSource == value) return;

                _purchaseValueEditImageSource = value;
                RaisePropertyChanged("PurchaseValueEditImageSource");
            }
        }


        private string _capitalAmountEditImageSource;
        public string CapitalAmountEditImageSource
        {
            get
            {
                return _capitalAmountEditImageSource;
            }
            set
            {
                if (_capitalAmountEditImageSource == value) return;

                _capitalAmountEditImageSource = value;
                RaisePropertyChanged("CapitalAmountEditImageSource");
            }
        }


        private bool _setSubmitButtonVisibility = false;
        public bool SetSubmitButtonVisibility
        {
            get
            {
                return _setSubmitButtonVisibility;
            }
            set
            {
                if (_setSubmitButtonVisibility == value) return;
                _setSubmitButtonVisibility = value;
                RaisePropertyChanged("SetSubmitButtonVisibility");
            }
        }

        private bool _setConfirmButtonVisibility = false;
        public bool SetConfirmButtonVisibility
        {
            get
            {
                return _setConfirmButtonVisibility;
            }
            set
            {
                if (_setConfirmButtonVisibility == value) return;

                _setConfirmButtonVisibility = value;
                RaisePropertyChanged("SetConfirmButtonVisibility");
            }
        }

        private string _iCRStatus;
        public string ICRStatus
        {
            get
            {
                return _iCRStatus;
            }
            set
            {
                if (_iCRStatus == value) return;

                _iCRStatus = value;
                RaisePropertyChanged("ICRStatus");
            }
        }


        private string _iCRStatusImage;
        public string ICRStatusImage
        {
            get
            {
                return _iCRStatusImage;
            }
            set
            {
                if (_iCRStatusImage == value) return;

                _iCRStatusImage = value;
                RaisePropertyChanged("ICRStatusImage");
            }
        }


        private bool _changeFromEstimateTAccountringBasisButtonVisibility = false;
        public bool ChangeFromEstimateTAccountringBasisButtonVisibility
        {
            get
            {
                return _changeFromEstimateTAccountringBasisButtonVisibility;
            }
            set
            {
                if (_changeFromEstimateTAccountringBasisButtonVisibility == value) return;

                _changeFromEstimateTAccountringBasisButtonVisibility = value;
                RaisePropertyChanged("ChangeFromEstimateTAccountringBasisButtonVisibility");
            }
        }


        private bool _checkBoxStatus = false;
        public bool CheckBoxStatus
        {
            get
            {
                return _checkBoxStatus;
            }
            set
            {
                if (_checkBoxStatus == value) return;

                _checkBoxStatus = value;
                RaisePropertyChanged("CheckBoxStatus");
            }
        }

        private bool _desClaimerVisibility = false;
        public bool DesClaimerVisibility
        {
            get
            {
                return _desClaimerVisibility;
            }
            set
            {
                if (_desClaimerVisibility == value) return;

                _desClaimerVisibility = value;
                RaisePropertyChanged("DesClaimerVisibility");
            }
        }
        //private bool _confirmAndGenerateSADADBillLabelVisibility = false;
        //public bool ConfirmAndGenerateSADADBillLabelVisibility
        //{
        //    get
        //    {
        //        return _confirmAndGenerateSADADBillLabelVisibility;
        //    }
        //    set
        //    {
        //        _confirmAndGenerateSADADBillLabelVisibility = value;
        //        RaisePropertyChanged("ConfirmAndGenerateSADADBillLabelVisibility");
        //    }
        //}


        private bool _setReadOnlyToTotalVATSales = false;
        public bool SetReadOnlyToTotalVATSales
        {
            get
            {
                return _setReadOnlyToTotalVATSales;
            }
            set
            {
                if (_setReadOnlyToTotalVATSales == value) return;

                _setReadOnlyToTotalVATSales = value;
                RaisePropertyChanged("SetReadOnlyToTotalVATSales");
            }
        }

        private bool _setReadOnlyToOtherThanTotalVATSales = true;
        public bool SetReadOnlyToOtherThanTotalVATSales
        {
            get
            {
                return _setReadOnlyToOtherThanTotalVATSales;
            }
            set
            {
                if (_setReadOnlyToOtherThanTotalVATSales == value) return;

                _setReadOnlyToOtherThanTotalVATSales = value;
                RaisePropertyChanged("SetReadOnlyToOtherThanTotalVATSales");
            }
        }

        private string _zAKATReturnsPagName = AppResources.FORM5ReturnDetails;
        public string ZAKATReturnsPagName
        {
            get
            {
                return _zAKATReturnsPagName;
            }
            set
            {
                if (_zAKATReturnsPagName == value) return;

                _zAKATReturnsPagName = value;
                RaisePropertyChanged("ZAKATReturnsPagName");
            }
        }

        private string _labnoE = "";
        public string LabnoE
        {
            get
            {
                return _labnoE;
            }
            set
            {
                if (_labnoE == value) return;

                _labnoE = value;
                RaisePropertyChanged("LabnoE");
            }
        }

        private string _tvtslE = "";
        public string TvtslE
        {
            get
            {
                return _tvtslE;
            }
            set
            {
                if (_tvtslE == value) return;

                _tvtslE = value;
                RaisePropertyChanged("TvtslE");
            }
        }

        private string _impvalE = "";
        public string ImpvalE
        {
            get
            {
                return _impvalE;
            }
            set
            {
                if (_impvalE == value) return;

                _impvalE = value;
                RaisePropertyChanged("ImpvalE");
            }
        }

        private string _sumcnt = "";
        public string Sumcnt
        {
            get
            {
                return _sumcnt;
            }
            set
            {
                if (_sumcnt == value) return;

                _sumcnt = value;
                RaisePropertyChanged("Sumcnt");
            }
        }


        private string _pramtE = "";
        public string PramtE
        {
            get
            {
                return _pramtE;
            }
            set
            {
                if (_pramtE == value) return;

                _pramtE = value;
                RaisePropertyChanged("PramtE");
            }
        }

        public ValidatePaymentResponse _paymentData = null;
        public ValidatePaymentResponse PaymentData
        {
            get
            {
                return _paymentData;
            }
            set
            {
                if (_paymentData == value) return;

                _paymentData = value;
                RaisePropertyChanged("PaymentData");
            }
        }

        private string _referenceNumber = "";
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

        private string _taxablePeriod = "";
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
        #endregion

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Constructor
        public ZAKATReturnDetailsViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnSubmitClicked = new Xamarin.Forms.Command(() =>
            {
                bool IsValueChange = GetEstimatedZAKATValueChangeStatus();
                if (IsValueChange)
                {
                    if (CheckBoxStatus)
                    {
                        SetUpdatedDataToZAKATEstimated();

                        if (AttachmentPopUpViewModel.SalesDetailList != null && AttachmentPopUpViewModel.SalesDetailList.Count > 0)
                        {
                            AddAttachmetToPostData();
                        }

                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                        {
                            SubmitReturn();
                        }
                        else
                        {
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                        }

                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit));

                            // await _dialogService.ShowMessageBox(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit, AppResources.Alerts);
                        });
                    }

                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNochangesmadeFormcannotbesubmitted));

                        // await _dialogService.ShowMessageBox(AppResources.ZZNochangesmadeFormcannotbesubmitted, AppResources.Alerts);
                    });
                }

            });


            //OnConfirmClicked = new Xamarin.Forms.Command(() =>
            //    {
            //        string PostOperationID = GetConfirmOperationId();
            //         ConfirmClicked(PostOperationID);
            //    });
            OnEditClicked = new Xamarin.Forms.Command(() =>
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    isLabelVisible = false;
                    isEditVisible = true;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = true;
                    SetConfirmButtonVisibility = false;
                    SetEditImage();
                });
            });
            //=========================end=====================================================
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });


            OnChangeFromEstimateToAccountingBasisButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseVisitGAZTPortalToChangeTheRegistrationType));

                    //  await _dialogService.ShowMessage(AppResources.PleaseVisitGAZTPortalToChangeTheRegistrationType, AppResources.Information);
                }
                catch (Exception ex)
                {
                }
            });

            //OnAmendClick = new Xamarin.Forms.Command(async () =>
            //{
            //    SetLayoutVisibilityAfterTappingOnAmendButton();
            //});


            //OnSubmitButtonClicked = new Command(async () =>
            //{
            //    try
            //    {
            //        bool IsValueChange = GetEstimatedZAKATValueChangeStatus();
            //        if (IsValueChange)
            //        {
            //            if (CheckBoxStatus)
            //            {
            //                // SetUpdatedDataToZAKATEstimated();
            //                // AssignAttachmentToPostDataObject();
            //                string PostOperationID = "05";
            //                await SubmitZakatReturn(PostOperationID, "");
            //                CheckBoxStatus = false;
            //            }
            //            else
            //            {
            //                Device.BeginInvokeOnMainThread(async () => {
            //                    await _dialogService.ShowMessageBox(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit, AppResources.Alerts);
            //                });
            //            }
            //        }
            //        else
            //        {
            //            Device.BeginInvokeOnMainThread(async () => {
            //                await _dialogService.ShowMessageBox(AppResources.ZZNochangesmadeFormcannotbesubmitted, AppResources.Alerts);
            //            });
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        IsLoading = false;
            //    }
            //});

        }
        #endregion


        #region Method
        public async Task OnPageLoad(string fbguid)
        {
            try
            {
                // Fbguid = fbguid;
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(fbguid);
                    ZakatReturnDetails obj = await WebServiceManager.GAZTGetZAKATReturn(fbguid);

                    ZakatReturnDetailToCompare = obj.d;
                    PopToRootPage();
                    if (zakatReturnDetails != null && zakatReturnDetails.d != null)
                    {

                        ZakatReturnDetails = zakatReturnDetails;
                        ZakatReturnDetail = zakatReturnDetails.d;
                        GetDataAfterAddingComma();
                        existingZakatBase = Convert.ToDouble(ZakatReturnDetails.d.Zkamt);
                        GetUpdatedDataAfterAddingComma();
                        SetICRStatus();
                        DateTime fromDate = JsonConvert.DeserializeObject<DateTime>(@"""" + ZakatReturnDetail.Abrzu + @""""); // Convert.ToDateTime(myZakatReturnsListTemp[i].Abrzu);
                        DateTime toDate = JsonConvert.DeserializeObject<DateTime>(@"""" + ZakatReturnDetail.Abrzo + @"""");// Convert.ToDateTime(myZakatReturnsListTemp[i].Abrzo);

                        string CalenderType = ZakatReturnDetail.Incotyp.Substring(0, 1);
                        if (CalenderType.Equals("H"))//  Abrzu = fromDate.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")) + " " + " - " + " " + toDate.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")); ;
                        {
                            FromDate = UtilityManager.Converthijri(fromDate);// fromDate.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            ToDate = UtilityManager.Converthijri(toDate);// " - " + toDate.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        }
                        else
                        {
                            FromDate = fromDate.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            string[] dts = FromDate.Split('-');
                            string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                            FromDate = date;

                            ToDate = " - " + toDate.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                            string[] dts2 = ToDate.Split('-');
                            string date2 = dts2[0] + "-" + UtilityManager.GetMonthName(dts2[1]) + "-" + dts2[2];
                            ToDate = date2;

                        }

                        var ZakatAmount = ZakatReturnDetails.d.Zkamt.Replace(",", "");
                        if (String.IsNullOrEmpty(ZakatAmount) || Double.Parse(ZakatAmount) == 0)
                        {
                            IsPayNowVisible = false;
                        }
                        else
                        {
                            IsPayNowVisible = true;
                        }

                        SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                        SetChangeFromEstimateTAccountringBasisButtonVisibility(ZakatReturnDetails.d.Statusz);
                        isThresholdValueLessThanTotalVATSales = IsThresholdValueLessThanTotalVATSales(ZakatReturnDetails.d.TvtslI);
                        if (isThresholdValueLessThanTotalVATSales)
                        {
                            SetReadOnlyToOtherThanTotalVATSales = true;
                            SetReadOnlyToTotalVATSales = false;
                        }
                        else
                        {
                            SetReadOnlyToOtherThanTotalVATSales = false;
                            SetReadOnlyToTotalVATSales = true;
                        }
                        // Abrzu = ZakatReturnListPageViewModel.ReturnPeriod;
                        SetLabelsText();

                    }
                    else
                    {
                        //  IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                                //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            //     Dear taxpayer, the return is under GAZT review and cannot be amended.
                            if (WebServiceManager.ErrorMessage.Equals("Dear taxpayer, the return is under GAZT review and cannot be amended."))// message is always coming in english from the server
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    if (App.IsArabic)
                                    {
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZDearTaxpayerTheReturnIsUnderGAZTReviewAndCannotBeAmended));

                                        // await _dialogService.ShowMessage(AppResources.ZDearTaxpayerTheReturnIsUnderGAZTReviewAndCannotBeAmended, AppResources.Information);
                                        _navigationService.GoBack();
                                        WebServiceManager.ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(WebServiceManager.ErrorMessage));

                                        //   await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                                        _navigationService.GoBack();
                                        WebServiceManager.ErrorMessage = string.Empty;
                                    }
                                });
                            }
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
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //  await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        ///* Method to insert the comma to amount variable
        ///

        private void AssignCalculatedValueAfterSubmission()
        {
            //Updated data to post on the server
            ZakatReturnDetails.d.Zbamt = _zakatReturnDetails.d.Zbamt;
            ZakatReturnDetails.d.Zkamt = _zakatReturnDetails.d.Zkamt;
            //Updated data to show on UI
            ZakatReturnDetail.Zbamt = _zakatReturnDetails.d.Zbamt;
            ZakatReturnDetail.Zkamt = _zakatReturnDetails.d.Zkamt;

        }

        public async Task OnReleaseOrBillsClicked()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                if (ZakatReturnDetails.d.Statusz.Equals("E0001") || ZakatReturnDetails.d.Statusz.Equals("IP011"))
                {// Call the Post API to release and if response is true then set the Button Name as bills and after tapping on that user needs to be navigated to Bills page 
                    await ReleaseEstimateZakatReturn();

                }
                else if (ZakatReturnDetails.d.Statusz.Equals("IP014"))// E002 means Tax officer has released the return
                {
                    //IsAmendButtonPressed = true;
                    //_navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
                    IsBillsButtonTapped = true;

                    if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                    {
                        _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                    }
                    else
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                    }


                }
                else if (ZakatReturnDetails.d.Statusz.Equals("E0002"))// E002 means Tax officer has released the return
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    IsBillsButtonTapped = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                        {

                            //Bill details Navigation
                            //_navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);

                            Device.BeginInvokeOnMainThread(async () => {

                                _navigationService.NavigateTo(App.PaymentProcessWebview, 0);


                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                            });

                            //if(ZakatReturnDetails.d.MadabutFg == "X") {

                            //    await DoValidatePayment(fbNum: ZakatReturnDetails.d.Fbnum);
                            //}
                            //else {

                            //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                            //}

                        }
                        else
                        {
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                        }
                    });
                }
              //  else if (ReleaseOrBillDetailsButtonText.Equals("Bills") || ReleaseOrBillDetailsButtonText.Equals("الفواتير"))
                else if (ReleaseOrBillDetailsButtonText.Equals(AppResources.PaymentMethodPayNow))
                {
                    // AmedmentButtonVisibility = true;
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    IsBillsButtonTapped = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                        {

                            //Bill details Navigation
                            //_navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);


                            //await DoValidatePayment(fbNum: _zakatReturnDetails.d.Fbnum);

                            if (ZakatReturnDetails.d.MadabutFg == "X")
                            {

                                await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                            }
                            else
                            {

                                await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                            }

                        }
                        else
                        {
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                        }
                    });
                }
                else if (ReleaseOrBillDetailsButtonText.Equals("Bills") || ReleaseOrBillDetailsButtonText.Equals("الفواتير"))
                {
                    // AmedmentButtonVisibility = true;
                    IsBillsButtonTapped = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                        {
                            _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                        }
                    });
                }
                else if (ZakatReturnDetails.d.Statusz.Equals("E0004") || ZakatReturnDetails.d.Statusz.Equals("E0003"))//Whent the Return is already Ameded by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
                {
                    IsBillsButtonTapped = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                        {

                            //_navigationService.NavigateTo(App.PaymentProcessWebview);
                            //await DoValidatePayment(fbNum: _zakatReturnDetails.d.Fbnum);


                            Device.BeginInvokeOnMainThread(async () => {

                                _navigationService.NavigateTo(App.PaymentProcessWebview, 0);


                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                            });
                            //if (ZakatReturnDetails.d.MadabutFg == "X")
                            //{

                            //    await DoValidatePayment(fbNum: ZakatReturnDetails.d.Fbnum);
                            //}
                            //else
                            //{

                            //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                            //}
                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                        }
                    });
                }
                else if (ZakatReturnDetails.d.Statusz.Equals("E0005"))// In Processing
                {
                    IsBillsButtonTapped = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                            {
                                _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                            }
                            else
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                            }
                        });
                    });
                }
                else
                {
                    IsBillsButtonTapped = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                            {
                                _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                            }
                            else
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                            }
                        });
                    });
                }
            });


            await Task.Run(() =>
            {
                IsLoading = false;
            });


        }

        public void gotoSuccessPage()
        {
            _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
        }
        public async Task DoValidatePayment(string fbNum, string paymentType)
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = "";

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        platform = "C4";
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        platform = "C3";
                    }
                    PaymentData = await WebServiceManager.GAZTValidatePayment(fbNum, App.LoginDataRetrieved.TIN, platform, paymentType);


                    if (PaymentData != null && PaymentData.d != null)
                    {

                        if (PaymentData.d.Guid != null&&PaymentData.d.Guid == "")
                        {
                            await PopupNavigation.Instance.PushAsync(new PaymentExceptionPageView());
                            return;
                        }
                        
                        if (PaymentData.d.Guid != null)
                        {

                            App.PaymentGuid = PaymentData.d.Guid;

                        }


                        if (paymentType == "M")
                        {

                            Device.BeginInvokeOnMainThread(async () => {

                                _navigationService.NavigateTo(App.PaymentProcessWebview, 0);
                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                            });
                        }
                        else
                        {
                            var ZakatAmount = ZakatReturnDetails.d.Zkamt.Replace(",", "");

                              await ProcessApplePay();
                        }



                        //if (ZakatReturnDetails.d.MadabutFg == "X")
                        //{

                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                        //}
                        //else
                        //{

                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                        //}


                        //var ZakatAmount = ZakatReturnDetails.d.Zkamt.Replace(",", "");
                        //if (String.IsNullOrEmpty(ZakatAmount) || Double.Parse(ZakatAmount) == 0)
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, true, false, ZakatReturnDetails.d.OpenliMsg));

                        //}
                        //else if (!String.IsNullOrEmpty(ZakatAmount) && Double.Parse(ZakatAmount) > 20000)
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                        //}
                        //else
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ZakatReturnDetails.d.OpenliMsg));

                        //}

                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (ex.Message == "There is no open liability to be paid against this declaration")
                        {

                            await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(false, true, false, AppResources.NoOpenLiabilityToBePaid));

                        }
                        else
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            _navigationService.GoBack();
                        }


                      
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }


        public async Task UpdateApplePayPaymentGuid()
        {
            try
            {
                try
                {

                    IsLoading = true;

                    string platform = string.Empty;

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        platform = "C4";
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        platform = "C3";
                    }



                    ApplePayToken modelDetails = new ApplePayToken();
                    modelDetails.Guid = App.PaymentGuid;
                    //var token = "GrPRb/eyYkhLaxIi8ugsU5I0D2/IE6JT6SYb4o6CH/emQV7n5twiqt8IVazkcItvmCkHXeie16Nvbq+uFFx0mS4O/1+SoDHrP8HcDbJ/Q1swCCHR/Dwv69oTcTUy1riK6Zvpe0w1r+WJ21I36gorRUn7u94Yi9n4afOfnGJC3EmFd6DKSIRQWlT4BuLlNv5826XruanuFjdL3MKty/xoCyx2GKN+e8W6BFVnQc/gsBe4UW7oqHIQ5PrQJlQwymi5Ytd1IIJT8QsUMxiVjz6yVS5zdQBaN86ZtuokJRmC89jCwVkUMwDl9jQ5xYbFlIFS1VXKJjtWKDfMGwCWK3jvWdtCcdb4VrPIxtK7LvTWc+4C7m6SPzkOhdC/XPn7ufwvrh95no7p9tpQMkP7zOJIYAl+hS4oEqvOxdpw55dCytGXJ0yjN/HOQ3t4ofyW9mBGiHoq";
                    modelDetails.PaymentToken = ApplePayTokenData;
                    modelDetails.SrcId = platform;


                    ApplePayTokenResponse response = await WebServiceManager.GAZTUpdateApplePayGuid(modelDetails);


                    if (response != null && response.d != null)
                    {


                        if (response.d.Success)
                        {

                            Device.BeginInvokeOnMainThread(() =>
                            {

                                //_navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, PaymentData.d.PayRef);
                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());


                                _navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, response.d.PayRef);

                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                                //_navigationService.GoBack();

                                await PopupNavigation.Instance.PushAsync(new PaymentExceptionPageView());
                            });

                        }

                    }
                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        public async Task MadaPaymentSelectedAsync()
        {

            if (ZakatReturnDetails.d.MadabutFg == "X")
            {

                 await DoValidatePayment(fbNum: ZakatReturnDetails.d.Fbnum, "M");
            }
            else
            {

                 PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

            }

            

        }

        public async Task ApplePaySelected()
        {
             DoValidatePayment(fbNum: ZakatReturnDetails.d.Fbnum, "A");

        }

        private async Task<bool> ProcessApplePay()
        {


            var Amount = Convert.ToDouble(PaymentData.d.Amount);
            var ZakatAmount = Math.Round(Amount, 2);
            DependencyService.Get<IApplePayAuthorizer>().IsPaymentFromDashboard(false);
            return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(ZakatAmount, AppResources.ZAmount);
        }

        public async Task SadadPaymentSelected()
        {

            _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
        }

        public async Task ReleaseEstimateZakatReturn()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    try
                    {
                        //  GetUpdatedDataAfterRemovingComma();
                        WebServiceManager.ErrorMessage = string.Empty;
                        ZakatReturnDetails UpdatedPostData = GetPostDataAfterRemovingComma(ZakatReturnDetails);

                        ZakatReturnDetails _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(UpdatedPostData, "59");
                        if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                        {
                            try
                            {
                                //Layout visibiliy changed after releasing the ICR
                                isEditVisible = false;
                                UnSetEditImage();
                                isLabelVisible = true;
                                IsEditTextVisible = false;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZReleasedSuccessfully));
                                    // await _dialogService.ShowMessageBox(AppResources.ZZReleasedSuccessfully, AppResources.ZZNotification);
                                });

                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else
                        {
                            try
                            {
                                //if (WebServiceManager.ErrorMessage.Equals(""))// message is always coming in english from the server
                                //{
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    //if (App.IsArabic)
                                    //{
                                    //    await _dialogService.ShowMessage(AppResources.ZDearTaxpayerTheReturnIsUnderGAZTReviewAndCannotBeAmended, AppResources.Information);
                                    //    _navigationService.GoBack();
                                    //    WebServiceManager.ErrorMessage = string.Empty;
                                    //}
                                    //else
                                    //{
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(WebServiceManager.ErrorMessage));

                                    //   await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                                    _navigationService.GoBack();
                                    WebServiceManager.ErrorMessage = string.Empty;
                                    IsLoading = false;

                                    //}
                                });
                                //}
                            }
                            catch (Exception ex)
                            {
                            }
                            //Device.BeginInvokeOnMainThread(async () => {
                            //    await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong, AppResources.ZError);
                            //    _navigationService.GoBack();
                            //});
                        }
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(Fbguid);
                            PopToRootPage();
                            //  EsimatedZAKATReturnsButtonSets esimatedZAKATReturnsButtonSets = await WebServiceManager.GAZTGetZAKATReturnButtonSet();
                            if (zakatReturnDetails != null)
                            {
                                ZakatReturnDetails = zakatReturnDetails;
                                if (zakatReturnDetails.d != null)
                                {
                                    ZakatReturnDetail = zakatReturnDetails.d;
                                    GetUpdatedDataAfterAddingComma();
                                    var ZakatAmount = ZakatReturnDetails.d.Zkamt.Replace(",", "");
                                    if (String.IsNullOrEmpty(ZakatAmount) || Double.Parse(ZakatAmount) == 0)
                                    {
                                        IsPayNowVisible = false;
                                    }
                                    else
                                    {
                                        IsPayNowVisible = true;
                                    }
                                    SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                                }
                            }
                        }

                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                            // _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }
        }
        public async Task SubmitReturn()
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                ZakatReturnDetails UpdatedPostData = GetPostDataAfterRemovingComma(ZakatReturnDetails);
                //  zakatReturnDetailsD.d.Cpamt = SalesDetailsList[7].InformationFromPartie.Replace(",", "");
                _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(UpdatedPostData, SubmitPostOperation);
                if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                {
                    SetUpdatdDatatoTheUI(_zakatReturnDetails);
                    Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.d.Estsl);
                    //  IsCurrentZAKATTaxLess = existingZakatBase >= Convert.ToDouble(_zakatReturnDetails.d.Zkamt);
                    if (existingZakatBase > Convert.ToDouble(_zakatReturnDetails.d.Zkamt))
                    {
                        AssignCalculatedValueAfterSubmission();

                        IsCurrentZAKATTaxLess = true;
                        bool isRequiredAttachmentAdded = SetRedEditIconForMandatoryAttachment(_zakatReturnDetails);
                        if (isRequiredAttachmentAdded)
                        {
                            SetLayoutVisibilityAfterSuccessfulSubmission();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseuploadtheRequiredDocumentandChangereason));

                                //  await _dialogService.ShowMessageBox(AppResources.ZZPleaseuploadtheRequiredDocumentandChangereason, AppResources.Information);
                            });

                        }

                    }
                    else
                    {
                        IsCurrentZAKATTaxLess = false;
                        SetLayoutVisibilityAfterSuccessfulSubmission();
                    }

                    GetDataAfterAddingComma();
                    SetLabelsText();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                            //   await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                            WebServiceManager.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(WebServiceManager.ErrorMessage));

                            //await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                            _navigationService.GoBack();
                            WebServiceManager.ErrorMessage = string.Empty;
                        }
                    });
                }
            });


            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }
        private void SetUpdatdDatatoTheUI(ZakatReturnDetails _ZakatReturnDetails)
        {
            ZakatReturnDetail = _ZakatReturnDetails.d;
        }

        public async Task ConfirmClicked(string PostOperation)
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });


            await Task.Run(async () =>
            {
                ZakatReturnDetails UpdatedPostData = GetPostDataAfterRemovingComma(ZakatReturnDetails);
                _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(UpdatedPostData, PostOperation);
                if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                {
                    SetConfirmButtonVisibility = false;
                    DesClaimerVisibility = false;
                    if (PostOperation.Equals(ConfirmPostOperationWithoutObjection))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);

                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _navigationService.NavigateTo(App.ZakatObjectionSuccessfullPageView, ZakatReturnDetail);
                        });
                    }

                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                            // await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                            WebServiceManager.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(WebServiceManager.ErrorMessage));

                            // await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                            _navigationService.GoBack();
                            WebServiceManager.ErrorMessage = string.Empty;
                        }
                    });
                }

            });


            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }


        private ZakatReturnDetailsD GetUpdatedDataAfterAddingComma()
        {
            if (ZakatReturnDetail != null)
            {
                ZakatReturnDetail.Estsl = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Estsl);
                ZakatReturnDetail.Cpamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Cpamt);
                ZakatReturnDetail.Zbamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Zbamt);
                ZakatReturnDetail.Zkamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Zkamt);
            }
            return ZakatReturnDetail;
        }


        // Method to pop all the pages from the stack
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFLoginPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFLoginPageView);
                    _navigation.NavigationStack.ToList().Clear();

                });
            }
        }


        private void GetUpdatedDataAfterRemovingComma()
        {
            if (ZakatReturnDetails != null)
            {
                ZakatReturnDetails.d.Estsl = ZakatReturnDetails.d.Estsl.Replace(",", "");
                ZakatReturnDetails.d.Cpamt = ZakatReturnDetails.d.Cpamt.Replace(",", "");
                ZakatReturnDetails.d.Zbamt = ZakatReturnDetails.d.Zbamt.Replace(",", "");
                ZakatReturnDetails.d.Zkamt = ZakatReturnDetails.d.Zkamt.Replace(",", "");
            }
        }


        private void SetReleaseOrBillDetailsButtonText(string ButtonStatus)
        {
            try
            {

                //If the return is not released
                if (ButtonStatus.Equals("E0001") || ButtonStatus.Equals("IP011"))
                {
                    isEditVisible = false;
                    UnSetEditImage();
                    isLabelVisible = true;
                    IsEditTextVisible = false;
                    ReleaseOrBillDetailsButtonText = AppResources.Release;
                    SetSubmitButtonVisibility = false;
                    SetConfirmButtonVisibility = false;
                    SetAmendButtonVisibility = false;
                }
                else if (ButtonStatus.Equals("IP014"))
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;
                }
                else if (ButtonStatus.Equals("E0002"))// E0002 if return  released by GAZT officer 
                {
                    isEditVisible = false;
                    isLabelVisible = true;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = false;
                    SetAmendButtonVisibility = true;
                    UnSetEditImage();
                    SetConfirmButtonVisibility = false;
                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;
                }
                else if (ButtonStatus.Equals("E0003"))//E0003 The return is Paid OR Partially paid 
                {
                    isEditVisible = false;
                    UnSetEditImage();
                    isLabelVisible = true;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = false;

                    SetAmendButtonVisibility = true;
                    SetConfirmButtonVisibility = false;
                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;
                }
                else if (ButtonStatus.Equals("E0004") || ButtonStatus.Equals("E0008"))//When the Return is already Amended by Taxpayer(E0004),
                {
                    isEditVisible = false;
                    UnSetEditImage();
                    isLabelVisible = true;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = false;
                    SetConfirmButtonVisibility = false;
                    SetAmendButtonVisibility = false;

                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;
                }
                else if (ButtonStatus.Equals("E0005"))//In Processing
                {
                    isEditVisible = false;
                    UnSetEditImage();
                    isLabelVisible = true;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = false;
                    SetConfirmButtonVisibility = false;
                    SetAmendButtonVisibility = false;
                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;
                }
                else if (ButtonStatus.Equals("E0011"))// In Paid state 
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;
                }
                else if (ButtonStatus.Equals("E0010"))// In Paid state 
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;
                }
                else if (ButtonStatus.Equals(""))//In Processing
                {
                }
            }
            catch (Exception ex)
            {
            }
        }


        public void SetEditImage()
        {
            if (isThresholdValueLessThanTotalVATSales)
            {
                CapitalAmountEditImageSource = "";//"ic_Edit_red.png";
                PurchaseValueEditImageSource = "";
                ExportValueEditImageSource = "";
                ContactFromETIMADSystemEditImageSource = "";
                ImportFromPointOfSalesEditImageSource = "";
                ImportValueEditImageSource = "";
                AverageNumberOfLabourEditImageSource = "";
                TotalVATSalesEditImageSource = "ic_edit_gray.png";
            }
            else
            {
                CapitalAmountEditImageSource = "ic_edit_gray.png";//"ic_Edit_red.png";
                PurchaseValueEditImageSource = "ic_edit_gray.png";
                ExportValueEditImageSource = "ic_edit_gray.png";
                ContactFromETIMADSystemEditImageSource = "ic_edit_gray.png";
                ImportFromPointOfSalesEditImageSource = "ic_edit_gray.png";
                ImportValueEditImageSource = "ic_edit_gray.png";
                AverageNumberOfLabourEditImageSource = "ic_edit_gray.png";
                TotalVATSalesEditImageSource = "";
            }


        }

        public void UnSetEditImage()
        {
            CapitalAmountEditImageSource = "";//"ic_Edit_red.png";
            PurchaseValueEditImageSource = "";
            ExportValueEditImageSource = "";
            ContactFromETIMADSystemEditImageSource = "";
            ImportFromPointOfSalesEditImageSource = "";
            ImportValueEditImageSource = "";
            AverageNumberOfLabourEditImageSource = "";
            TotalVATSalesEditImageSource = "";

        }

        //if(existingZakatBase > Convert.ToDouble(_zakatReturnDetails.d.Zkamt))
        //                    {
        //                        IsCurrentZAKATTaxLess = true;
        //                    }
        //                    else
        //                    {
        //                        IsCurrentZAKATTaxLess = false;
        //                    }

        //To Return th epost operation as per objection and without objection
        public string GetConfirmOperationId()
        {
            if (IsCurrentZAKATTaxLess)
            {
                return ConfirmPostOperationWithObjection;// For Amendment with objection
            }
            else
            {
                return ConfirmPostOperationWithoutObjection;// For Amendment without objection
            }
        }





        private ZakatReturnDetails GetPostDataAfterRemovingComma(ZakatReturnDetails zakatReturnDetails)
        {
            zakatReturnDetails.d.TvtslI = ZakatReturnDetail.TvtslI.Replace(",", "");
            zakatReturnDetails.d.TvtslE = ZakatReturnDetail.TvtslE.Replace(",", "");
            zakatReturnDetails.d.LabnoI = ZakatReturnDetail.LabnoI.Replace(",", "");
            zakatReturnDetails.d.LabnoE = ZakatReturnDetail.LabnoE.Replace(",", "");
            zakatReturnDetails.d.ImpvalI = ZakatReturnDetail.ImpvalI.Replace(",", "");
            zakatReturnDetails.d.ImpvalE = ZakatReturnDetail.ImpvalE.Replace(",", "");
            zakatReturnDetails.d.PtoslI = ZakatReturnDetail.PtoslI.Replace(",", "");
            zakatReturnDetails.d.Sumcnt = ZakatReturnDetail.Sumcnt.Replace(",", "");
            zakatReturnDetails.d.EtimadI = ZakatReturnDetail.EtimadI.Replace(",", "");
            zakatReturnDetails.d.Sumcnt = ZakatReturnDetail.Sumcnt.Replace(",", "");
            zakatReturnDetails.d.ExamtI = ZakatReturnDetail.ExamtI.Replace(",", "");
            zakatReturnDetails.d.Sumcnt = ZakatReturnDetail.Sumcnt.Replace(",", "");
            zakatReturnDetails.d.PramtI = ZakatReturnDetail.PramtI.Replace(",", "");
            zakatReturnDetails.d.PramtE = ZakatReturnDetail.PramtE.Replace(",", "");
            zakatReturnDetails.d.Cpamt = ZakatReturnDetail.Cpamt.Replace(",", "");
            zakatReturnDetails.d.Estsl = ZakatReturnDetail.Estsl.Replace(",", "");
            zakatReturnDetails.d.Zbamt = ZakatReturnDetail.Zbamt.Replace(",", "");
            zakatReturnDetails.d.Zkamt = ZakatReturnDetail.Zkamt.Replace(",", "");
            return zakatReturnDetails;
        }

        private void GetDataAfterAddingComma()
        {

            ZakatReturnDetail.TvtslI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.TvtslI);
            ZakatReturnDetail.TvtslE = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.TvtslE);
            ZakatReturnDetail.LabnoI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.LabnoI);
            ZakatReturnDetail.LabnoE = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.LabnoE);
            ZakatReturnDetail.ImpvalI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.ImpvalI);
            ZakatReturnDetail.ImpvalE = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.ImpvalE);
            ZakatReturnDetail.PtoslI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.PtoslI);
            ZakatReturnDetail.Sumcnt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Sumcnt);
            ZakatReturnDetail.EtimadI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.EtimadI);
            ZakatReturnDetail.Sumcnt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Sumcnt);
            ZakatReturnDetail.ExamtI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.ExamtI);
            ZakatReturnDetail.Sumcnt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Sumcnt);
            ZakatReturnDetail.PramtI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.PramtI);
            ZakatReturnDetail.PramtE = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.PramtE);
            ZakatReturnDetail.Cpamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Cpamt);
            ZakatReturnDetail.Estsl = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Estsl);
            ZakatReturnDetail.Zbamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Zbamt);
            ZakatReturnDetail.Zkamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Zkamt);
        }

        // Method to remove the comma while comparing the changed value
        private ZakatReturnDetailsD GetDataAfterRemovingComma(ZakatReturnDetailsD zakatReturnDetails)
        {
            ZakatReturnDetailsD zakatReturnDetailsD = new ZakatReturnDetailsD();
            zakatReturnDetailsD.TvtslI = ZakatReturnDetail.TvtslI.Replace(",", "");
            zakatReturnDetailsD.TvtslE = ZakatReturnDetail.TvtslE.Replace(",", "");
            zakatReturnDetailsD.LabnoI = ZakatReturnDetail.LabnoI.Replace(",", "");
            zakatReturnDetailsD.LabnoE = ZakatReturnDetail.LabnoE.Replace(",", "");
            zakatReturnDetailsD.ImpvalI = ZakatReturnDetail.ImpvalI.Replace(",", "");
            zakatReturnDetailsD.ImpvalE = ZakatReturnDetail.ImpvalE.Replace(",", "");
            zakatReturnDetailsD.PtoslI = ZakatReturnDetail.PtoslI.Replace(",", "");
            zakatReturnDetailsD.Sumcnt = ZakatReturnDetail.Sumcnt.Replace(",", "");
            zakatReturnDetailsD.EtimadI = ZakatReturnDetail.EtimadI.Replace(",", "");
            zakatReturnDetailsD.Sumcnt = ZakatReturnDetail.Sumcnt.Replace(",", "");
            zakatReturnDetailsD.ExamtI = ZakatReturnDetail.ExamtI.Replace(",", "");
            zakatReturnDetailsD.Sumcnt = ZakatReturnDetail.Sumcnt.Replace(",", "");
            zakatReturnDetailsD.PramtI = ZakatReturnDetail.PramtI.Replace(",", "");
            zakatReturnDetailsD.PramtE = ZakatReturnDetail.PramtE.Replace(",", "");
            zakatReturnDetailsD.Cpamt = ZakatReturnDetail.Cpamt.Replace(",", "");
            zakatReturnDetailsD.Estsl = ZakatReturnDetail.Estsl.Replace(",", "");
            zakatReturnDetailsD.Zbamt = ZakatReturnDetail.Zbamt.Replace(",", "");
            zakatReturnDetailsD.Zkamt = ZakatReturnDetail.Zkamt.Replace(",", "");
            return zakatReturnDetailsD;
        }



        //private bool GetEstimatedZAKATValueChangeStatus()
        //{

        //    if (GetAmountAfterRemovingComma(ZakatReturnDetailToCompare.TvtslI).Equals(GetAmountAfterRemovingComma(ZakatReturnDetail.TvtslI))

        //        && GetAmountAfterRemovingComma(ZakatReturnDetailToCompare.LabnoI).Equals(GetAmountAfterRemovingComma(ZakatReturnDetail.LabnoI))

        //        && GetAmountAfterRemovingComma(ZakatReturnDetailToCompare.ImpvalI).Equals(GetAmountAfterRemovingComma(ZakatReturnDetail.ImpvalI))

        //        && GetAmountAfterRemovingComma(ZakatReturnDetailToCompare.PtoslI).Equals(GetAmountAfterRemovingComma(ZakatReturnDetail.PtoslI))

        //        && GetAmountAfterRemovingComma(ZakatReturnDetailToCompare.EtimadI).Equals(GetAmountAfterRemovingComma(ZakatReturnDetail.EtimadI))

        //        && GetAmountAfterRemovingComma(ZakatReturnDetailToCompare.PramtI).Equals(GetAmountAfterRemovingComma(ZakatReturnDetail.PramtI))

        //        && GetAmountAfterRemovingComma(ZakatReturnDetailToCompare.Cpamt).Equals(GetAmountAfterRemovingComma(ZakatReturnDetail.PramtI)))

        //    {
        //        return false;//ExamtI
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}
        //public string GetAmountAfterRemovingComma(string AmountText)
        //{
        //    if (AmountText.Contains(","))
        //    {
        //        return AmountText = AmountText.Replace(",", "");
        //    }
        //    else
        //    {
        //        return AmountText;
        //    }
        //}

        private bool GetEstimatedZAKATValueChangeStatus()
        {
            ZakatReturnDetailsD zakatReturnDetail = GetDataAfterRemovingComma(ZakatReturnDetail);
            if (ZakatReturnDetailToCompare.TvtslI.Equals(zakatReturnDetail.TvtslI)
                && ZakatReturnDetailToCompare.LabnoI.Equals(zakatReturnDetail.LabnoI)
                && ZakatReturnDetailToCompare.ImpvalI.Equals(zakatReturnDetail.ImpvalI)
                && ZakatReturnDetailToCompare.PtoslI.Equals(zakatReturnDetail.PtoslI)
                && ZakatReturnDetailToCompare.EtimadI.Equals(zakatReturnDetail.EtimadI)
                && ZakatReturnDetailToCompare.PramtI.Equals(zakatReturnDetail.PramtI)
                && ZakatReturnDetailToCompare.Cpamt.Equals(zakatReturnDetail.Cpamt))
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        private void SetICRStatus()
        {
            if ((string.Equals(ZakatReturnDetail.Statusz, "IP011")))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
            {
                ICRStatus = AppResources.UnSubmitted;
                ICRStatusImage = "ic_unsubmitted.png";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
            {
                ICRStatusImage = "submited_check.png";
                ICRStatusImage = AppResources.Paid;
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "IP015"))//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = AppResources.NDInProcessing;
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "IP014"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.Paid;

            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0008"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = AppResources.NDBilled;

            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "IP021"))//To be approved || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = AppResources.ToBeApproved;
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0004"))//Amend without Objection
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.Submitted;
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0003"))// In Build state 
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.NDBilled;
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0011"))// In Paid state 
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.Paid;
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0005"))// In Processing
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = AppResources.NDInProcessing;
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0002"))// Status when the return released by GAZT officer
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.NDBilled;
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0001"))// UnSubmitted
            {
                ICRStatusImage = "unsubmitted.png";
                ICRStatus = AppResources.UnSubmitted;
            }
        }

        private void SetLayoutVisibilityAfterSuccessfulSubmission()
        {
            UnSetEditImage();
            isEditVisible = false;
            isLabelVisible = true;
            IsEditTextVisible = true;
            SetSubmitButtonVisibility = false;
            SetConfirmButtonVisibility = true;
            //   ConfirmAndGenerateSADADBillLabelVisibility = true;
            ChangeFromEstimateTAccountringBasisButtonVisibility = false;
        }

        private void SetLayoutVisibilityAfterSuccessfulConfirmation()
        {
            IsEditTextVisible = true;
        }

        public void ClearData()
        {
            UnSetEditImage();
            isEditVisible = false;
            isLabelVisible = false;
            IsEditTextVisible = false;
            SetSubmitButtonVisibility = false;
            isEditVisible = false;
            isLabelVisible = false;
            IsEditTextVisible = false;
            CheckBoxStatus = false;
            //    ConfirmAndGenerateSADADBillLabelVisibility = false;
            SetConfirmButtonVisibility = false;
            SetAmendButtonVisibility = false;
            isThresholdValueLessThanTotalVATSales = false;
            DesClaimerVisibility = false;
            if (ZakatReturnDetail != null)
            {

                ZakatReturnDetail.TvtslI = string.Empty;
                ZakatReturnDetail.TvtslE = string.Empty;
                ZakatReturnDetail.LabnoI = string.Empty;
                ZakatReturnDetail.LabnoE = string.Empty;
                ZakatReturnDetail.ImpvalI = string.Empty;
                ZakatReturnDetail.ImpvalE = string.Empty;
                ZakatReturnDetail.PtoslI = string.Empty;
                ZakatReturnDetail.Sumcnt = string.Empty;
                ZakatReturnDetail.EtimadI = string.Empty;
                ZakatReturnDetail.Sumcnt = string.Empty;
                ZakatReturnDetail.ExamtI = string.Empty;
                ZakatReturnDetail.Sumcnt = string.Empty;
                ZakatReturnDetail.PramtI = string.Empty;
                ZakatReturnDetail.PramtE = string.Empty;
                ZakatReturnDetail.Cpamt = string.Empty;
                ZakatReturnDetail.Estsl = string.Empty;
                ZakatReturnDetail.Zbamt = string.Empty;
                ZakatReturnDetail.Zkamt = string.Empty;
                ZakatReturnDetail.Persl = string.Empty;
                ZakatReturnDetail.Fbnum = string.Empty;


                FromDate = string.Empty;
                ToDate = string.Empty;


                //       ZakatReturnDetailsD obj = new ZakatReturnDetailsD();
                //       Metadata metadata = new Metadata();
                //             ReasonSet reasonSet = new ReasonSet();
                //AttachSet attachSet = new AttachSet();
                //       InvoiceSet invoiceSet = new InvoiceSet();
                //       ThresholdSet thresholdSet = new ThresholdSet();
                //       metadata.uri = "";
                //       metadata.id = "";

                //       metadata.type = "";

                //       obj.AttachSet = attachSet;
                //       obj.ReasonSet = reasonSet;
                //       obj.InvoiceSet = invoiceSet;
                //       obj.ThresholdSet = thresholdSet;
                //       obj.__metadata = metadata;

                //       ZakatReturnDetail = obj;
            }


        }

        public void SetChangeFromEstimateTAccountringBasisButtonVisibility(string ButtonStatus)
        {
            if (ButtonStatus.Equals("E0001") || ButtonStatus.Equals("E0002") || ButtonStatus.Equals("E0003") || ButtonStatus.Equals("E0004"))
            {
                ChangeFromEstimateTAccountringBasisButtonVisibility = true;
            }
            else
            {
                ChangeFromEstimateTAccountringBasisButtonVisibility = false;
            }
        }


        public bool IsThresholdValueLessThanTotalVATSales(string TotalVATSales)
        {
            string TotalVatSalesAmount = string.Empty;
            if (ZakatReturnDetails.d.TvtslI.Contains(","))
            {
                TotalVatSalesAmount = TotalVATSales.Replace(",", "");
            }
            else
            {
                TotalVatSalesAmount = ZakatReturnDetails.d.TvtslI;
            }
            double totalVatSalesAmount = Convert.ToDouble(TotalVatSalesAmount);
            if (totalVatSalesAmount > Convert.ToDouble(ZakatReturnDetails.d.ThresholdSet.results[0].Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Setting the updated value to the post object
        private void SetUpdatedDataToZAKATEstimated()
        {
            ZakatReturnDetails.d.TvtslI = ZakatReturnDetail.TvtslI;
            ZakatReturnDetails.d.LabnoI = ZakatReturnDetail.LabnoI;
            ZakatReturnDetails.d.ImpvalI = ZakatReturnDetail.ImpvalI;
            ZakatReturnDetails.d.PtoslI = ZakatReturnDetail.PtoslI;
            ZakatReturnDetails.d.EtimadI = ZakatReturnDetail.EtimadI;
            ZakatReturnDetails.d.ExamtI = ZakatReturnDetail.ExamtI;

            ZakatReturnDetails.d.PramtI = ZakatReturnDetail.PramtI;
            ZakatReturnDetails.d.Cpamt = ZakatReturnDetail.Cpamt;

            if (AttachmentPopUpViewModel.SalesDetailList != null && AttachmentPopUpViewModel.SalesDetailList.Count > 0)
            {
                ZakatReturnDetails.d.TvtslResn = AttachmentPopUpViewModel.SalesDetailList[0].ChangeReason;
                ZakatReturnDetails.d.LabnoResn = AttachmentPopUpViewModel.SalesDetailList[1].ChangeReason;
                ZakatReturnDetails.d.ImpvalResn = AttachmentPopUpViewModel.SalesDetailList[2].ChangeReason;
                ZakatReturnDetails.d.PtoslResn = AttachmentPopUpViewModel.SalesDetailList[3].ChangeReason;
                ZakatReturnDetails.d.EtimadResn = AttachmentPopUpViewModel.SalesDetailList[4].ChangeReason;
                ZakatReturnDetails.d.ExamtResn = AttachmentPopUpViewModel.SalesDetailList[5].ChangeReason;
                ZakatReturnDetails.d.PramtResn = AttachmentPopUpViewModel.SalesDetailList[6].ChangeReason;
                ZakatReturnDetails.d.CpamtResn = AttachmentPopUpViewModel.SalesDetailList[7].ChangeReason;
            }

        }

        private void SetAttachmenToZAKATEstimated()
        {

        }

        private void AddAttachmetToPostData()
        {
            for (int i = 0; i < AttachmentPopUpViewModel.SalesDetailList.Count; i++)
            {
                for (int j = 0; j < AttachmentPopUpViewModel.SalesDetailList[i].estimateZakatAttachment.Count; j++)
                {
                    EstimateZakatAttachmentList.Add(AttachmentPopUpViewModel.SalesDetailList[i].estimateZakatAttachment[j]);
                }
            }

            AttachSet attachSet = new AttachSet();
            attachSet.results = EstimateZakatAttachmentList;
            ZakatReturnDetails.d.AttachSet = attachSet;

        }

        public bool SetRedEditIconForMandatoryAttachment(ZakatReturnDetails zakatReturnDetail)
        {
            bool IsRequiredAttachmentAdded = true;
            if (Convert.ToDouble(zakatReturnDetail.d.TvtslI) < Convert.ToDouble(ZakatReturnDetailToCompare.TvtslI))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[0].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.TvtslResn))
                {
                    TotalVATSalesEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;
                }
            }
            else
            {
                TotalVATSalesEditImageSource = "ic_edit_gray.png";
            }

            if (Convert.ToDouble(zakatReturnDetail.d.LabnoI) < Convert.ToDouble(ZakatReturnDetailToCompare.LabnoI))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[1].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.LabnoResn))
                {
                    AverageNumberOfLabourEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;

                }
                else
                {
                    AverageNumberOfLabourEditImageSource = "ic_edit_gray.png";
                }
            }

            if (Convert.ToDouble(zakatReturnDetail.d.ImpvalI) < Convert.ToDouble(ZakatReturnDetailToCompare.ImpvalI))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[2].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.ImpvalResn))
                {
                    ImportValueEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;

                }
                else
                {
                    ExportValueEditImageSource = "ic_edit_gray.png";
                }
            }

            if (Convert.ToDouble(zakatReturnDetail.d.PtoslI) < Convert.ToDouble(ZakatReturnDetailToCompare.PtoslI))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[3].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.PtoslResn))
                {
                    ImportFromPointOfSalesEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;

                }
                else
                {
                    ImportFromPointOfSalesEditImageSource = "ic_edit_gray.png";
                }
            }

            if (Convert.ToDouble(zakatReturnDetail.d.EtimadI) < Convert.ToDouble(ZakatReturnDetailToCompare.EtimadI))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[4].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.EtimadResn))
                {
                    ContactFromETIMADSystemEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;

                }
                else
                {
                    ContactFromETIMADSystemEditImageSource = "ic_edit_gray.png";
                }
            }

            if (Convert.ToDouble(zakatReturnDetail.d.ExamtI) < Convert.ToDouble(ZakatReturnDetailToCompare.ExamtI) || !zakatReturnDetail.d.ExamtResn.Equals(ZakatReturnDetailToCompare.ExamtResn))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[5].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.ExamtResn))
                {
                    ExportValueEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;

                }
                else
                {
                    ExportValueEditImageSource = "ic_edit_gray.png";
                }
            }

            if (Convert.ToDouble(zakatReturnDetail.d.PramtI) < Convert.ToDouble(ZakatReturnDetailToCompare.PramtI))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[6].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.PramtResn))
                {
                    PurchaseValueEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;

                }
                else
                {
                    PurchaseValueEditImageSource = "ic_edit_gray.png";
                }
            }
            if (Convert.ToDouble(zakatReturnDetail.d.Cpamt) < Convert.ToDouble(ZakatReturnDetailToCompare.Cpamt))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[7].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.CpamtResn))
                {
                    CapitalAmountEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;

                }
                else
                {
                    CapitalAmountEditImageSource = "ic_edit_gray.png";
                }

            }
            return IsRequiredAttachmentAdded;

        }

        public void SetLayoutVisibilityAfterTappingOnAmendButton()
        {
            isEditVisible = true;
            isLabelVisible = false;
            IsEditTextVisible = false;
            SetSubmitButtonVisibility = true;
            SetAmendButtonVisibility = false;
            SetEditImage();
            SetConfirmButtonVisibility = false;
            ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
            DesClaimerVisibility = true;
            ZAKATReturnsPagName = AppResources.NDReturnAmendments;
            //if (isThresholdValueLessThanTotalVATSales)
            //{
            //    SetReadOnlyToOtherThanTotalVATSales = true;
            //    SetReadOnlyToTotalVATSales = false;
            //}
            //else
            //{
            //    SetReadOnlyToOtherThanTotalVATSales = false;
            //    SetReadOnlyToTotalVATSales = true;
            //}
        }

        private void SetLayoutVisibilityAfterSuccessfulAmendement()
        {
            UnSetEditImage();
            isEditVisible = false;
            isLabelVisible = true;
            IsEditTextVisible = false;
            SetSubmitButtonVisibility = false;
            //    ConfirmAndGenerateSADADBillLabelVisibility = false;
            SetConfirmButtonVisibility = false;
            SetAmendButtonVisibility = false;
            isThresholdValueLessThanTotalVATSales = false;
            DesClaimerVisibility = false;
        }

        public string Converthijri(DateTime FormatedFaedn)
        {
            // FormatedFaedn = _faedn.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
            var calendar = new HijriCalendar();
            var day = calendar.GetDayOfMonth(FormatedFaedn);
            var year = calendar.GetYear(FormatedFaedn);
            var month = calendar.GetMonth(FormatedFaedn);
            string hijriDate = UtilityManager.FormatAccordingToDeviceHijriEnglish(day + "/" + month + "/" + year);
            return hijriDate;

        }

        private void SetLabelsText()
        {
            if (isThresholdValueLessThanTotalVATSales)
            {
                LabnoE = AppResources.ZNA;
                ImpvalE = AppResources.ZNA;
                Sumcnt = AppResources.ZNA;
                PramtE = AppResources.ZNA;
                TvtslE = ZakatReturnDetail.TvtslE;
            }
            else
            {
                LabnoE = ZakatReturnDetail.LabnoE;
                TvtslE = AppResources.ZNA;
                ImpvalE = ZakatReturnDetail.ImpvalE;
                Sumcnt = ZakatReturnDetail.Sumcnt;
                PramtE = ZakatReturnDetail.PramtE;

            }
        }
        #endregion

    }
}
