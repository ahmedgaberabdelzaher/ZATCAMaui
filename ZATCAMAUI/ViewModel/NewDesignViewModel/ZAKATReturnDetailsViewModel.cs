using System.Globalization;
using System.Windows.Input;

using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Behaviors;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.PaymentOptions;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class ZAKATReturnDetailsViewModel : BaseViewModel
    {
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

        public bool _isRealEstateViewVisible { get; set; }
        public bool IsRealEstateViewVisible
        {
            get => _isRealEstateViewVisible;
            set
            {
                _isRealEstateViewVisible = value;
                OnPropertyChanged("IsRealEstateViewVisible");
            }
        }

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
                OnPropertyChanged("isEditVisible");
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
                OnPropertyChanged("isLabelVisible");
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
                OnPropertyChanged("IsEditTextVisible");
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
                OnPropertyChanged("SetAmendButtonVisibility");
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
                OnPropertyChanged("ZakatReturnDetail");
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
                OnPropertyChanged("ZakatReturnDetails");
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
                OnPropertyChanged("Abrzu");
            }
        }





        private bool _releaseOrBillDetailsVisible = true;
        public bool ReleaseOrBillDetailsVisible
        {
            get
            {
                return _releaseOrBillDetailsVisible;
            }
            set
            {
                if (_releaseOrBillDetailsVisible == value) return;

                _releaseOrBillDetailsVisible = value;
                OnPropertyChanged("ReleaseOrBillDetailsVisible");
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
                OnPropertyChanged("FromDate");
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
                OnPropertyChanged("ToDate");
            }
        }


        private string _releaseOrBillDetailsButtonText = string.Empty;
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
                OnPropertyChanged("ReleaseOrBillDetailsButtonText");
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
                OnPropertyChanged("TotalVATSalesEditImageSource");
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
                OnPropertyChanged("AverageNumberOfLabourEditImageSource");
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
                OnPropertyChanged("ImportValueEditImageSource");
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
                OnPropertyChanged("ImportFromPointOfSalesEditImageSource");
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
                OnPropertyChanged("ContactFromETIMADSystemEditImageSource");
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
                OnPropertyChanged("ExportValueEditImageSource");
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
                OnPropertyChanged("PurchaseValueEditImageSource");
            }
        }
        private string _realEstateValueEditImageSource;
        public string RealEstateValueEditImageSource
        {
            get
            {
                return _realEstateValueEditImageSource;
            }
            set
            {
                if (_realEstateValueEditImageSource == value) return;

                _realEstateValueEditImageSource = value;
                OnPropertyChanged("RealEstateValueEditImageSource");
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
                OnPropertyChanged("CapitalAmountEditImageSource");
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
                OnPropertyChanged("SetSubmitButtonVisibility");
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
                OnPropertyChanged("SetConfirmButtonVisibility");
            }
        }

        private string _iCRStatus = string.Empty;
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
                OnPropertyChanged("ICRStatus");
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
                OnPropertyChanged("ICRStatusImage");
            }
        }

        private Color _iCRStatusBg;
        public Color ICRStatusBG
        {
            get
            {
                return _iCRStatusBg;
            }
            set
            {
                if (_iCRStatusBg == value) return;

                _iCRStatusBg = value;
                OnPropertyChanged("ICRStatusBG");
            }
        }

        private Color _iCRStatusTextColor;
        public Color ICRStatusTextColor
        {
            get
            {
                return _iCRStatusTextColor;
            }
            set
            {
                if (_iCRStatusTextColor == value) return;

                _iCRStatusTextColor = value;
                OnPropertyChanged("ICRStatusTextColor");
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
                OnPropertyChanged("ChangeFromEstimateTAccountringBasisButtonVisibility");
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
                OnPropertyChanged("CheckBoxStatus");
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
                OnPropertyChanged("DesClaimerVisibility");
            }
        }

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
                OnPropertyChanged("SetReadOnlyToTotalVATSales");
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
                OnPropertyChanged("SetReadOnlyToOtherThanTotalVATSales");
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
                OnPropertyChanged("ZAKATReturnsPagName");
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
                OnPropertyChanged("LabnoE");
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
                OnPropertyChanged("TvtslE");
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
                OnPropertyChanged("ImpvalE");
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
                OnPropertyChanged("Sumcnt");
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
                OnPropertyChanged("PramtE");
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
                OnPropertyChanged("PaymentData");
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
                OnPropertyChanged("ReferenceNumber");
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
                OnPropertyChanged("TaxablePeriod");
            }
        }
        #endregion

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Constructor
        public ZAKATReturnDetailsViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            OnSubmitClicked = new Command(async () =>
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
                            await SubmitReturn();
                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                        }

                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit));

                         });
                    }

                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                     {
                         await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNochangesmadeFormcannotbesubmitted));

                     });
                }

            });

            OnEditClicked = new Command(() =>
            {

                MainThread.BeginInvokeOnMainThread(() =>
                 {
                     isLabelVisible = false;
                     isEditVisible = true;
                     IsEditTextVisible = false;
                     SetSubmitButtonVisibility = true;
                     SetConfirmButtonVisibility = false;
                     SetEditImage();
                 });
            });
            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });


            OnChangeFromEstimateToAccountingBasisButtonClicked = new Command(async () =>
            {
                try
                {
                    var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.PleaseVisitGAZTPortalToChangeTheRegistrationType);

                    if (App.IsArabic)
                    {
                        VisitPortalPopup.OnGotoPortal = () =>
                        {

                            Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlAR);

                        };
                    }
                    else
                    {
                        VisitPortalPopup.OnGotoPortal = () =>
                        {

                            Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlEN);

                        };
                    }
                    await MopupService.Instance.PushAsync(VisitPortalPopup);
                }
                catch (Exception)
                {


                }
            });
        }
        #endregion


        #region Method
        public async Task OnPageLoad(string fbnum)
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
                    ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(fbnum);
                    ZakatReturnDetails obj = await WebServiceManager.GAZTGetZAKATReturn(fbnum);

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
                        DateTime fromDate = Convert.ToDateTime(ZakatReturnDetail.Abrzu); // Convert.ToDateTime(myZakatReturnsListTemp[i].Abrzu);
                        DateTime toDate = Convert.ToDateTime(ZakatReturnDetail.Abrzo);// Convert.ToDateTime(myZakatReturnsListTemp[i].Abrzo);

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
                            string date2 = dts2[1] + "-" + UtilityManager.GetMonthName(dts2[2]) + "-" + dts2[3];
                            ToDate = date2;

                        }

                        var ZakatAmount = ZakatReturnDetails.d.Zkamt.Replace(",", "");
                        //if (String.IsNullOrEmpty(ZakatAmount) || Double.Parse(ZakatAmount) == 0)
                        //{
                        //    IsPayNowVisible = false;
                        //}
                        //else
                        //{
                        //    IsPayNowVisible = true;
                        //}




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
                        if (zakatReturnDetails.d.RestFlg == "X")
                        {
                            IsRealEstateViewVisible = true;
                        }
                        else
                        {
                            IsRealEstateViewVisible = false;
                        }
                        SetLabelsText();

                    }
                    else
                    {
                        //  IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                                //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            //     Dear taxpayer, the return is under GAZT review and cannot be amended.

                            //Dear taxpayer, the return is under ZATCA review and cannot be amended.
                            if (WebServiceManager.ErrorMessage.Equals("Dear taxpayer, the return is under ZATCA review and cannot be amended."))// message is always coming in english from the server
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    if (App.IsArabic)
                                    {
                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZDearTaxpayerTheReturnIsUnderGAZTReviewAndCannotBeAmended));

                                        // await _dialogService.ShowMessage(AppResources.ZDearTaxpayerTheReturnIsUnderGAZTReviewAndCannotBeAmended, AppResources.Information);
                                        _navigationService.GoBack();
                                        WebServiceManager.ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(WebServiceManager.ErrorMessage));

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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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
            ZakatReturnDetails.d.Zbamt = _zakatReturnDetails.result.Zbamt;
            ZakatReturnDetails.d.Zkamt = _zakatReturnDetails.result.Zkamt;
            //Updated data to show on UI
            ZakatReturnDetail.Zbamt = _zakatReturnDetails.result.Zbamt;
            ZakatReturnDetail.Zkamt = _zakatReturnDetails.result.Zkamt;

        }

        public async Task OnReleaseOrBillsClicked()
        {
            await Task.Run(async () =>
            {
                IsLoading = true;
                if (ZakatReturnDetails.d.Statusz.Equals("E0001") || ZakatReturnDetails.d.Statusz.Equals("IP011"))
                {// Call the Post API to release and if response is true then set the Button Name as bills and after tapping on that user needs to be navigated to Bills page 
                    await ReleaseEstimateZakatReturn();

                }
                else if (ZakatReturnDetails.d.Statusz.Equals("IP014"))// E002 means Tax officer has released the return
                {
                    IsBillsButtonTapped = true;

                    if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                    {

                        ZakatReturnDetail.Fbnum = ZAKATReturnDetailsViewModel.Fbguid;
                        App.ZakatReturnBilldetails = false;
                        _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                    }


                }
                else if (ZakatReturnDetails.d.Statusz.Equals("E0002"))// E002 means Tax officer has released the return
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    IsBillsButtonTapped = true;
                    MainThread.BeginInvokeOnMainThread(() =>
                     {
                         if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                         {

                             if (ReleaseOrBillDetailsButtonText.Equals(AppResources.PaymentMethodPayNow))
                             {

                                 if (ZakatReturnDetails.d.MadabutFg == "X")
                                 {

                                     MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                                 }
                                 else
                                 {

                                     MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                                 }

                             }
                             else
                             {
                                 App.ZakatReturnBilldetails = false;
                                 _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                             }



                         }
                         else
                         {
                             MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                         }
                     });
                }
                //  else if (ReleaseOrBillDetailsButtonText.Equals("Bills") || ReleaseOrBillDetailsButtonText.Equals("الفواتير"))
                else if (ReleaseOrBillDetailsButtonText.Equals(AppResources.PaymentMethodPayNow))
                {

                    MainThread.BeginInvokeOnMainThread(async () =>
                     {
                         if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                         {



                             if (ZakatReturnDetails.d.MadabutFg == "X")
                             {

                                 await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                             }
                             else
                             {

                                 await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                             }

                         }
                         else
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                         }
                     });
                }
                else if (ReleaseOrBillDetailsButtonText.Equals("Bills") || ReleaseOrBillDetailsButtonText.Equals("الفواتير"))
                {
                    // AmedmentButtonVisibility = true;
                    IsBillsButtonTapped = true;
                    MainThread.BeginInvokeOnMainThread(async () =>
                     {
                         if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                         {
                             App.ZakatReturnBilldetails = false;
                             _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                         }
                         else
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                         }
                     });
                }
                else if (ZakatReturnDetails.d.Statusz.Equals("E0004") || ZakatReturnDetails.d.Statusz.Equals("E0003"))//Whent the Return is already Ameded by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
                {
                    IsBillsButtonTapped = true;
                    MainThread.BeginInvokeOnMainThread(async () =>
                     {
                         if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                         {
                             if (ReleaseOrBillDetailsButtonText.Equals(AppResources.PaymentMethodPayNow))
                             {

                                 if (ZakatReturnDetails.d.MadabutFg == "X")
                                 {

                                     await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                                 }
                                 else
                                 {

                                     await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                                 }

                             }
                             else
                             {
                                 App.ZakatReturnBilldetails = false;
                                 _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                             }

                         }
                         else
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                         }
                     });
                }
                else if (ZakatReturnDetails.d.Statusz.Equals("E0005"))// In Processing
                {
                    IsBillsButtonTapped = true;
                    MainThread.BeginInvokeOnMainThread(() =>
                     {
                         MainThread.BeginInvokeOnMainThread(async () =>
                         {
                             if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                             {
                                 App.ZakatReturnBilldetails = false;
                                 _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                             }
                             else
                             {
                                 await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                             }
                         });
                     });
                }
                else
                {
                    IsBillsButtonTapped = true;
                    MainThread.BeginInvokeOnMainThread(async () =>
                     {
                         if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber == true)
                         {
                             App.ZakatReturnBilldetails = false;
                             _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                         }
                         else
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseEnterCorrectData));
                         }
                     });
                }

                IsLoading = false;
            });

        }

        public void gotoSuccessPage()

        {

            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, ZakatReturnDetail);
        }
        public async Task DoValidatePayment(string fbNum, string paymentType)
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

                ValidatePayment modelDetails = new ValidatePayment();
                modelDetails.Fbnum = fbNum;
                modelDetails.Pymntty = paymentType;
                modelDetails.Tin = App.LoginDataRetrieved.TIN;
                modelDetails.Srcid = platform;
                modelDetails.Srctile = "12";
                modelDetails.Sadad = "";

                PaymentData = await WebServiceManager.GAZTValidatePayment(modelDetails);

                if (PaymentData != null && PaymentData.d != null)
                {

                    if (PaymentData.d.Guid != null && PaymentData.d.Guid == "")
                    {
                        await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
                        return;
                    }

                    if (PaymentData.d.Guid != null)
                    {

                        if (PaymentData.d.Guid != null&&PaymentData.d.Guid == "")
                        {
                            await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
                            return;
                        }
                        
                        if (PaymentData.d.Guid != null)
                        {

                            App.PaymentGuid = PaymentData.d.Guid;

                        }


                        if (paymentType == "Mada Payment")
                        {

                            MainThread.BeginInvokeOnMainThread(async () => {
                            IsLoading = true;
                            //CR7420
                            CreateMadaResponseRoot respose = await GetWebviewContent(PaymentData.d.Srcid);
                            IsLoading = false;
                                if (!string.IsNullOrEmpty(respose?.result?.securityAuthorizationKey))
                                {
                                    App.securityAuthorizationKey = respose.result.securityAuthorizationKey;
                                    _navigationService.NavigateTo(App.PaymentProcessWebview, 0);
                                    //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());
                                }
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


                    if (paymentType == "M")
                    {

                        MainThread.BeginInvokeOnMainThread(async () =>
                        {

                            _navigationService.NavigateTo(App.PaymentProcessWebview, 0);
                            //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                        });
                    }
                    else
                    {
                        var ZakatAmount = ZakatReturnDetails.d.Zkamt.Replace(",", "");

                        await ProcessApplePay();
                    }
                }

                IsLoading = false;

            }
            catch (GAZTValidatePaymentInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    IsLoading = false;
                    if (ex.Message == "There is no open liability to be paid against this declaration" || ex.Message == "لا يوجد التزامات حالية متاحة للدفع لهذا الاقرار")
                    {

                        await MopupService.Instance.PushAsync(new PaymentOptionsPageView(false, true, false, AppResources.NoOpenLiabilityToBePaid));

                    }
                    else
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    }



                });
            }
            catch (GAZTNetworkConnectivityIssueException)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                });
            }
            catch (InternetException)
            {

                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }


        public async Task<CreateMadaResponseRoot> GetWebviewContent(string srcid)
        {
            try
            {
                var paymentPayload = new CreateMadaPaymentPayload
                {
                    GUID = App.PaymentGuid,
                    sourceId = srcid
                };
                CreateMadaResponseRoot respose = await WebServiceManager.GAZTCreateMadaPayment(paymentPayload);
                return respose;
            }
            catch (GAZTValidateMadaPaymentException ex)
            {
                IsLoading = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var message = ex.Message.Substring(0, 1).ToUpper() + ex.Message.Substring(1).ToLower();
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                });
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task UpdateApplePayPaymentGuid()
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

                        MainThread.BeginInvokeOnMainThread(() =>
                        {

                            PaymentSucess paymentInfo = new PaymentSucess();
                            paymentInfo.Paymentref = response.d.PayRef;
                            if (response.d.PerslTxt != null)
                            {
                                paymentInfo.Period = response.d.PerslTxt;
                            }

                            _navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, paymentInfo);

                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
                        });

                    }

                }
                IsLoading = false;

            }
            catch (GAZTValidatePaymentInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (InternetException)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        public async Task MadaPaymentSelectedAsync()
        {

            if (ZakatReturnDetails.d.MadabutFg == "X")
            {
                 await DoValidatePayment(fbNum: ZakatReturnDetails.d.Fbnum, "Mada Payment");
            }
            else
            {

                await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

            }



        }

        public async Task ApplePaySelected()
        {
            await DoValidatePayment(fbNum: ZakatReturnDetails.d.Fbnum, "A");

        }

        private async Task<bool> ProcessApplePay()
        {


            var Amount = Convert.ToDouble(PaymentData.d.Amount);
            var ZakatAmount = Math.Round(Amount, 2);
            DependencyService.Get<IApplePayAuthorizer>().IsPaymentFromDashboard(false);
            return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(ZakatAmount, AppResources.ApplePayText);
        }

        public void SadadPaymentSelected()
        {

            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, ZakatReturnDetail);
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
                        if (_zakatReturnDetails != null && _zakatReturnDetails.result != null)
                        {
                            try
                            {
                                //Layout visibiliy changed after releasing the ICR
                                isEditVisible = false;
                                UnSetEditImage();
                                isLabelVisible = true;
                                IsEditTextVisible = false;
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZReleasedSuccessfully));
                                    // await _dialogService.ShowMessageBox(AppResources.ZZReleasedSuccessfully, AppResources.ZZNotification);
                                });

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.Write(ex.StackTrace.ToString());
                            }
                        }
                        else
                        {
                            try
                            {
                                //if (WebServiceManager.ErrorMessage.Equals(""))// message is always coming in english from the server
                                //{
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    //if (App.IsArabic)
                                    //{
                                    //    await _dialogService.ShowMessage(AppResources.ZDearTaxpayerTheReturnIsUnderGAZTReviewAndCannotBeAmended, AppResources.Information);
                                    //    _navigationService.GoBack();
                                    //    WebServiceManager.ErrorMessage = string.Empty;
                                    //}
                                    //else
                                    //{
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(WebServiceManager.ErrorMessage));

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
                                Console.WriteLine(ex.Message);
                                Console.Write(ex.StackTrace.ToString());
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
                                    //if (String.IsNullOrEmpty(ZakatAmount) || Double.Parse(ZakatAmount) == 0)
                                    //{
                                    //    IsPayNowVisible = false;
                                    //}
                                    //else
                                    //{
                                    //    IsPayNowVisible = true;
                                    //}


                                    SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                                }
                            }
                        }

                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                IsLoading = false;
            }
        }
        public async Task SubmitReturn()
        {

           
            await Task.Run(async () =>
            {
                IsLoading = true;
                ZakatReturnDetails UpdatedPostData = GetPostDataAfterRemovingComma(ZakatReturnDetails);
                //  zakatReturnDetailsD.d.Cpamt = SalesDetailsList[7].InformationFromPartie.Replace(",", "");
                _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(UpdatedPostData, SubmitPostOperation);
                if (_zakatReturnDetails != null && _zakatReturnDetails.result != null)
                {
                    SetUpdatdDatatoTheUI(_zakatReturnDetails);
                    Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.result.Estsl);
                    //  IsCurrentZAKATTaxLess = existingZakatBase >= Convert.ToDouble(_zakatReturnDetails.d.Zkamt);
                    if (existingZakatBase > Convert.ToDouble(_zakatReturnDetails.result.Zkamt))
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
                            MainThread.BeginInvokeOnMainThread(async () =>
                             {
                                 await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseuploadtheRequiredDocumentandChangereason));

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
                    MainThread.BeginInvokeOnMainThread(async () =>
                     {
                         if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                             //   await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                             _navigationService.GoBack();
                             WebServiceManager.ErrorMessage = string.Empty;
                         }
                         else
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(WebServiceManager.ErrorMessage));

                             //await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                             _navigationService.GoBack();
                             WebServiceManager.ErrorMessage = string.Empty;
                         }
                     });
                }
                IsLoading = false;
            });

        }
        private void SetUpdatdDatatoTheUI(ZakatReturnDetails _ZakatReturnDetails)
        {
            ZakatReturnDetail = _ZakatReturnDetails.result;
        }

        public async Task ConfirmClicked(string PostOperation)
        {
            await Task.Run(async () =>
            {
                IsLoading = true;
                ZakatReturnDetails UpdatedPostData = GetPostDataAfterRemovingComma(ZakatReturnDetails);
                _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(UpdatedPostData, PostOperation);
                if (_zakatReturnDetails != null && _zakatReturnDetails.result != null)
                {
                    SetConfirmButtonVisibility = false;
                    DesClaimerVisibility = false;
                    if (PostOperation.Equals(ConfirmPostOperationWithoutObjection))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                         {
                             App.ZakatReturnBilldetails = false;
                             _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);

                         });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                         {
                             _navigationService.NavigateTo(App.ZakatObjectionSuccessfullPageView, ZakatReturnDetail);
                         });
                    }

                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                     {
                         if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                             // await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                             _navigationService.GoBack();
                             WebServiceManager.ErrorMessage = string.Empty;
                         }
                         else
                         {
                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(WebServiceManager.ErrorMessage));

                             // await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                             _navigationService.GoBack();
                             WebServiceManager.ErrorMessage = string.Empty;
                         }
                     });
                }
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
                MainThread.BeginInvokeOnMainThread(() =>
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

                    if (ZakatReturnDetail.OpenliMsg == "There is no open liability to be paid against this declaration" || ZakatReturnDetail.OpenliMsg == "لا يوجد التزامات حالية متاحة للدفع لهذا الاقرار")
                    {

                        ReleaseOrBillDetailsVisible = false;
                    }
                    else
                    {

                        ReleaseOrBillDetailsVisible = true;

                    }
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
                    if (ZakatReturnDetail.OpenliMsg == "There is no open liability to be paid against this declaration" || ZakatReturnDetail.OpenliMsg == "لا يوجد التزامات حالية متاحة للدفع لهذا الاقرار")
                    {

                        ReleaseOrBillDetailsVisible = false;
                    }
                    else
                    {

                        ReleaseOrBillDetailsVisible = true;

                    }
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
                    if (ZakatReturnDetail.OpenliMsg == "There is no open liability to be paid against this declaration" || ZakatReturnDetail.OpenliMsg == "لا يوجد التزامات حالية متاحة للدفع لهذا الاقرار")
                    {

                        ReleaseOrBillDetailsVisible = false;
                    }
                    else
                    {

                        ReleaseOrBillDetailsVisible = true;

                    }
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

                    if (ZakatReturnDetail.OpenliMsg == "There is no open liability to be paid against this declaration" || ZakatReturnDetail.OpenliMsg == "لا يوجد التزامات حالية متاحة للدفع لهذا الاقرار")
                    {

                        ReleaseOrBillDetailsVisible = false;
                    }
                    else
                    {

                        ReleaseOrBillDetailsVisible = true;

                    }
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

                    if (ZakatReturnDetail.OpenliMsg == "There is no open liability to be paid against this declaration" || ZakatReturnDetail.OpenliMsg == "لا يوجد التزامات حالية متاحة للدفع لهذا الاقرار")
                    {

                        ReleaseOrBillDetailsVisible = false;
                    }
                    else
                    {

                        ReleaseOrBillDetailsVisible = true;

                    }
                }
                else if (ButtonStatus.Equals("E0011"))// In Paid state 
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;

                    if (ZakatReturnDetail.OpenliMsg == "There is no open liability to be paid against this declaration" || ZakatReturnDetail.OpenliMsg == "لا يوجد التزامات حالية متاحة للدفع لهذا الاقرار")
                    {

                        ReleaseOrBillDetailsVisible = false;
                    }
                    else
                    {

                        ReleaseOrBillDetailsVisible = true;

                    }
                }
                else if (ButtonStatus.Equals("E0010"))// In Paid state 
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    ReleaseOrBillDetailsButtonText = AppResources.PaymentMethodPayNow;

                    if (ZakatReturnDetail.OpenliMsg == "There is no open liability to be paid against this declaration" || ZakatReturnDetail.OpenliMsg == "لا يوجد التزامات حالية متاحة للدفع لهذا الاقرار")
                    {

                        ReleaseOrBillDetailsVisible = false;
                    }
                    else
                    {

                        ReleaseOrBillDetailsVisible = true;

                    }
                }
                else if (ButtonStatus.Equals(""))//In Processing
                {
                    ReleaseOrBillDetailsVisible = true;
                }
            }
            catch (Exception)
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
                RealEstateValueEditImageSource = "";
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
                RealEstateValueEditImageSource = "ic_edit_gray.png";
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
            RealEstateValueEditImageSource = "";

        }
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
            zakatReturnDetails.d.ExamtI = ZakatReturnDetail.ExamtI.Replace(",", "");
            zakatReturnDetails.d.PramtI = ZakatReturnDetail.PramtI.Replace(",", "");
            zakatReturnDetails.d.PramtE = ZakatReturnDetail.PramtE.Replace(",", "");
            zakatReturnDetails.d.RestI = ZakatReturnDetail.RestI.Replace(",", "");
            zakatReturnDetails.d.RestE = ZakatReturnDetail.RestE.Replace(",", "");
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
            ZakatReturnDetail.ExamtI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.ExamtI);
            ZakatReturnDetail.PramtI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.PramtI);
            ZakatReturnDetail.PramtE = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.PramtE);
            ZakatReturnDetail.RestI = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.RestI);
            ZakatReturnDetail.RestE = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.RestE);

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
            zakatReturnDetailsD.RestI = ZakatReturnDetail.RestI.Replace(",", "");
            zakatReturnDetailsD.RestE = ZakatReturnDetail.RestE.Replace(",", "");

            zakatReturnDetailsD.Cpamt = ZakatReturnDetail.Cpamt.Replace(",", "");
            zakatReturnDetailsD.Estsl = ZakatReturnDetail.Estsl.Replace(",", "");
            zakatReturnDetailsD.Zbamt = ZakatReturnDetail.Zbamt.Replace(",", "");
            zakatReturnDetailsD.Zkamt = ZakatReturnDetail.Zkamt.Replace(",", "");
            return zakatReturnDetailsD;
        }


        private bool GetEstimatedZAKATValueChangeStatus()
        {
            ZakatReturnDetailsD zakatReturnDetail = GetDataAfterRemovingComma(ZakatReturnDetail);
            if (ZakatReturnDetailToCompare.TvtslI.Equals(zakatReturnDetail.TvtslI)
                && ZakatReturnDetailToCompare.LabnoI.Equals(zakatReturnDetail.LabnoI)
                && ZakatReturnDetailToCompare.ImpvalI.Equals(zakatReturnDetail.ImpvalI)
                && ZakatReturnDetailToCompare.PtoslI.Equals(zakatReturnDetail.PtoslI)
                && ZakatReturnDetailToCompare.EtimadI.Equals(zakatReturnDetail.EtimadI)
                && ZakatReturnDetailToCompare.PramtI.Equals(zakatReturnDetail.PramtI)
                && ZakatReturnDetailToCompare.RestI.Equals(zakatReturnDetail.RestI)
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
            if (string.Equals(ZakatReturnDetail.Statusz, "IP011"))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
            {
                ICRStatus = AppResources.UnSubmitted;
                ICRStatusImage = "ic_unsubmitted.png";
                ICRStatusBG = (Color)Application.Current.Resources["ErrorBg"];
                ICRStatusTextColor = (Color)Application.Current.Resources["Error"];
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.Paid;
                ICRStatusBG = (Color)Application.Current.Resources["SuccessBg"];
                ICRStatusTextColor = (Color)Application.Current.Resources["Success"];

            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "IP015"))//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = AppResources.NDInProcessing;
                ICRStatusBG = (Color)Application.Current.Resources["NeutralLightGrey"];
                ICRStatusTextColor = (Color)Application.Current.Resources["NeutralGreay"];
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "IP014"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.Paid;

                ICRStatusBG = (Color)Application.Current.Resources["SuccessBg"];
                ICRStatusTextColor = (Color)Application.Current.Resources["Success"];

            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0008"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = AppResources.NDBilled;
                ICRStatusBG = (Color)Application.Current.Resources["NeutralLightGrey"];
                ICRStatusTextColor = (Color)Application.Current.Resources["NeutralGreay"];

            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "IP021"))//To be approved || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = AppResources.ToBeApproved;
                ICRStatusBG = (Color)Application.Current.Resources["NeutralLightGrey"];
                ICRStatusTextColor = (Color)Application.Current.Resources["NeutralGreay"];
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0004"))//Amend without Objection
            {

                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.Submitted;
                ICRStatusBG = (Color)Application.Current.Resources["SuccessBg"];
                ICRStatusTextColor = (Color)Application.Current.Resources["Success"];
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0003"))// In Build state 
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.NDBilled;
                ICRStatusBG = (Color)Application.Current.Resources["SuccessBg"];
                ICRStatusTextColor = (Color)Application.Current.Resources["Success"];
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0011"))// In Paid state 
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.Paid;
                ICRStatusBG = (Color)Application.Current.Resources["SuccessBg"];
                ICRStatusTextColor = (Color)Application.Current.Resources["Success"];
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0005"))// In Processing
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = AppResources.NDInProcessing;
                ICRStatusBG = (Color)Application.Current.Resources["NeutralLightGrey"];
                ICRStatusTextColor = (Color)Application.Current.Resources["NeutralGreay"];
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0002"))// Status when the return released by GAZT officer
            {
                ICRStatusImage = "submited_check.png";
                ICRStatus = AppResources.NDBilled;
                ICRStatusBG = (Color)Application.Current.Resources["SuccessBg"];
                ICRStatusTextColor = (Color)Application.Current.Resources["Success"];
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0001"))// UnSubmitted
            {
                ICRStatusImage = "unsubmitted.png";
                ICRStatus = AppResources.UnSubmitted;
                ICRStatusBG = (Color)Application.Current.Resources["ErrorBg"];
                ICRStatusTextColor = (Color)Application.Current.Resources["Error"];
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
                ZakatReturnDetail.RestI = string.Empty;
                ZakatReturnDetail.RestE = string.Empty;

                ZakatReturnDetail.Cpamt = string.Empty;
                ZakatReturnDetail.Estsl = string.Empty;
                ZakatReturnDetail.Zbamt = string.Empty;
                ZakatReturnDetail.Zkamt = string.Empty;
                ZakatReturnDetail.Persl = string.Empty;
                ZakatReturnDetail.Fbnum = string.Empty;


                FromDate = string.Empty;
                ToDate = string.Empty;

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
            if (totalVatSalesAmount > Convert.ToDouble(ZakatReturnDetails.d.ThresholdSet[0].Value))
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
            ZakatReturnDetails.d.RestI = ZakatReturnDetail.RestI;
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

            //AttachSet attachSet = new AttachSet();
            //attachSet.results = EstimateZakatAttachmentList;
            ZakatReturnDetails.d.AttachSet = EstimateZakatAttachmentList;

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
            if (Convert.ToDouble(zakatReturnDetail.d.RestI) < Convert.ToDouble(ZakatReturnDetailToCompare.RestI))
            {
                if (AttachmentPopUpViewModel.SalesDetailList == null || AttachmentPopUpViewModel.SalesDetailList.Count == 0 || AttachmentPopUpViewModel.SalesDetailList[8].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(zakatReturnDetail.d.RestResn))
                {
                    RealEstateValueEditImageSource = "ic_Edit_red.png";
                    IsRequiredAttachmentAdded = false;

                }
                else
                {
                    RealEstateValueEditImageSource = "ic_edit_gray.png";
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
            //Setting isVisibility to true to display amount fields when clicked on Amend
            isLabelVisible = true;
            //isLabelVisible = false;
            IsEditTextVisible = false;
            SetSubmitButtonVisibility = true;
            SetAmendButtonVisibility = false;
            SetEditImage();
            SetConfirmButtonVisibility = false;
            ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
            DesClaimerVisibility = true;
            ZAKATReturnsPagName = AppResources.NDReturnAmendments;
        }

        private void SetLayoutVisibilityAfterSuccessfulAmendement()
        {
            UnSetEditImage();
            isEditVisible = false;
            isLabelVisible = true;
            IsEditTextVisible = false;
            SetSubmitButtonVisibility = false;
            SetConfirmButtonVisibility = false;
            SetAmendButtonVisibility = false;
            isThresholdValueLessThanTotalVATSales = false;
            DesClaimerVisibility = false;
        }

        public string Converthijri(DateTime FormatedFaedn)
        {
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
