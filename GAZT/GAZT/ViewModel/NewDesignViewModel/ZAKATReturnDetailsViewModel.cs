using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage_ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class ZAKATReturnDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
//============================start===================================================
        public ICommand OnSubmitClicked { get; set; }
        public ICommand OnConfirmClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }
        public ICommand OnEditClicked { get; set; }
        public ICommand OnChangeFromEstimateToAccountingBasisButtonClicked { get; set; }

        public string Fbguid { get; set; }
        public bool IsCurrentZAKATTaxLess = false;
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
                _isEditVisible = value;
                RaisePropertyChanged("isEditVisible");
            }
        }

        private bool _isLabelVisible = false;
        public bool isLabelVisible
        {
            get
            {
                return _isLabelVisible;
            }
            set
            {
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
                _isEditTextVisible = value;
                RaisePropertyChanged("IsEditTextVisible");
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
                _abrzu = value;
                RaisePropertyChanged("Abrzu");
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
                _iCRStatusImage = value;
                RaisePropertyChanged("ICRStatus");
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
                _changeFromEstimateTAccountringBasisButtonVisibility = value;
                RaisePropertyChanged("ChangeFromEstimateTAccountringBasisButtonVisibility");
            }
        }

        private bool _confirmAndGenerateSADADBillLabelVisibility = false;
        public bool ConfirmAndGenerateSADADBillLabelVisibility
        {
            get
            {
                return _confirmAndGenerateSADADBillLabelVisibility;
            }
            set
            {
                _confirmAndGenerateSADADBillLabelVisibility = value;
                RaisePropertyChanged("ConfirmAndGenerateSADADBillLabelVisibility");
            }
        }
        #endregion

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
//=======================start==================================================
           

            OnSubmitClicked = new Xamarin.Forms.Command(() =>
            {
            bool IsValueChange = GetEstimatedZAKATValueChangeStatus();
            if (IsValueChange)
            {
                SubmitReturn();
                   
                }
                else
                {
                Device.BeginInvokeOnMainThread(async () => {
                    await _dialogService.ShowMessageBox(AppResources.ZZNochangesmadeFormcannotbesubmitted, AppResources.Alerts);
                });
                }

            });


            OnConfirmClicked = new Xamarin.Forms.Command(() =>
                {
                    string PostOperationID = GetConfirmOperationId();
                     ConfirmClicked(PostOperationID);
                });
            OnEditClicked = new Xamarin.Forms.Command(() =>
            {
                isLabelVisible = false;
                isEditVisible = true;
                SetEditImage();
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
                    await _dialogService.ShowMessage(AppResources.PleaseVisitGAZTPortalToChangeTheRegistrationType, AppResources.Information);
                }
                catch (Exception ex)
                {
                }
            });
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

                        existingZakatBase = Convert.ToDouble(ZakatReturnDetails.d.Zkamt);
                        GetUpdatedDataAfterAddingComma();
                        SetICRStatus();
                        DateTime _abrzu = JsonConvert.DeserializeObject<DateTime>(@"""" + ZakatReturnDetail.Abrzu + @""""); // Convert.ToDateTime(myZakatReturnsListTemp[i].Abrzu);
                        DateTime _abrzo = JsonConvert.DeserializeObject<DateTime>(@"""" + ZakatReturnDetail.Abrzo + @"""");// Convert.ToDateTime(myZakatReturnsListTemp[i].Abrzo);
                        Abrzu = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")) + " " + AppResources.To + " " + _abrzo.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")); ;


                        SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                        SetChangeFromEstimateTAccountringBasisButtonVisibility(ZakatReturnDetails.d.Statusz);

                       // Abrzu = ZakatReturnListPageViewModel.ReturnPeriod;
                       
                    }
                    else
                    {
                      //  IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            //     Dear taxpayer, the return is under GAZT review and cannot be amended.
                            if (WebServiceManager.ErrorMessage.Equals("Dear taxpayer, the return is under GAZT review and cannot be amended."))// message is always coming in english from the server
                            {
                                Device.BeginInvokeOnMainThread(async () => {
                                    if (App.IsArabic)
                                    {
                                        await _dialogService.ShowMessage(AppResources.ZDearTaxpayerTheReturnIsUnderGAZTReviewAndCannotBeAmended, AppResources.Information);
                                        _navigationService.GoBack();
                                        WebServiceManager.ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
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
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            //zakatReturnDetailsD.d.Zbamt = _zakatReturnDetails.d.Zbamt;
            //zakatReturnDetailsD.d.Zkamt = _zakatReturnDetails.d.Zkamt;
        }


        public async Task OnReleaseOrBillsClicked()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async() =>
            {
                if (ZakatReturnDetails.d.Statusz.Equals("E0001") || ZakatReturnDetails.d.Statusz.Equals("IP011"))
                {// Call the Post API to release and if response is true then set the Button Name as bills and after tapping on that user needs to be navigated to Bills page 
                    await ReleaseEstimateZakatReturn();

                }
                else if (ZakatReturnDetails.d.Statusz.Equals("IP014"))// E002 means Tax officer has released the return
                {
                    //IsAmendButtonPressed = true;
                    //_navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
                }
                else if (ZakatReturnDetails.d.Statusz.Equals("E0002"))// E002 means Tax officer has released the return
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                }
                else if (ReleaseOrBillDetailsButtonText.Equals("Bills") || ReleaseOrBillDetailsButtonText.Equals("الفواتير"))
                {
                    // AmedmentButtonVisibility = true;
                    _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                }
                else if (ZakatReturnDetails.d.Statusz.Equals("E0004") || ZakatReturnDetails.d.Statusz.Equals("E0003"))//Whent the Return is already Ameded by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                    });
                }
                else if (ZakatReturnDetails.d.Statusz.Equals(""))
                {
                    // SalesDetailsAndReleaseButtonVisibility = false;
                }
                else
                {
                    _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);
                }
            });


            await Task.Run(() =>
            {
                IsLoading = false;
            });

            
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
                        GetUpdatedDataAfterRemovingComma();
                        ZakatReturnDetails _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(ZakatReturnDetails, "59");
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
                                    await _dialogService.ShowMessageBox(AppResources.ZZReleasedSuccessfully, AppResources.ZZNotification);
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
                                    await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                                    _navigationService.GoBack();
                                    WebServiceManager.ErrorMessage = string.Empty;
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
                                SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                            }
                        }
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            }
        }
        public async Task SubmitReturn()
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async() =>
            {
                ZakatReturnDetails UpdatedPostData = GetPostDataAfterRemovingComma(ZakatReturnDetails);
                //  zakatReturnDetailsD.d.Cpamt = SalesDetailsList[7].InformationFromPartie.Replace(",", "");
                _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(UpdatedPostData, SubmitPostOperation);
                if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                {

                    Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.d.Estsl);
                    //  IsCurrentZAKATTaxLess = existingZakatBase >= Convert.ToDouble(_zakatReturnDetails.d.Zkamt);
                    if (existingZakatBase > Convert.ToDouble(_zakatReturnDetails.d.Zkamt))
                    {
                        IsCurrentZAKATTaxLess = true;
                    }
                    else
                    {
                        IsCurrentZAKATTaxLess = false;
                    }
                    SetLayoutVisibilityAfterSuccessfulSubmission();
                    AssignCalculatedValueAfterSubmission();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                            WebServiceManager.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
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

        public async Task ConfirmClicked(string PostOperation)
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });


            await Task.Run(async() =>
            {
                _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(ZakatReturnDetails, PostOperation);
                if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        _navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, ZakatReturnDetail);

                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                            WebServiceManager.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
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
                }
                else if (ButtonStatus.Equals("IP014"))
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0002"))// E0002 if return  released by GAZT officer 
                {
                    isEditVisible = true;
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = true;
                    SetConfirmButtonVisibility = false;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0003"))//E0003 The return is Paid OR Partially paid 
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = true;
                    SetConfirmButtonVisibility = false;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0004") || ButtonStatus.Equals("E0008"))//When the Return is already Amended by Taxpayer(E0004),
                {
                    isEditVisible = false;
                    UnSetEditImage();
                    isLabelVisible = true;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = false;
                    SetConfirmButtonVisibility = false;

                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0005"))//In Processing
                {
                    isEditVisible = false;
                    UnSetEditImage();
                    isLabelVisible = true;
                    IsEditTextVisible = false;
                    SetSubmitButtonVisibility = false;
                    SetConfirmButtonVisibility = false;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0011"))// In Paid state 
                {
                    isEditVisible = true;
                    SetEditImage();
                    isLabelVisible = false;
                    IsEditTextVisible = false;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
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
            CapitalAmountEditImageSource = "ic_edit_gray.png";//"ic_Edit_red.png";
            PurchaseValueEditImageSource = "ic_edit_gray.png";
            ExportValueEditImageSource = "ic_edit_gray.png";
            ContactFromETIMADSystemEditImageSource = "ic_edit_gray.png";
            ImportFromPointOfSalesEditImageSource = "ic_edit_gray.png";
            ImportValueEditImageSource = "ic_edit_gray.png";
            AverageNumberOfLabourEditImageSource = "ic_edit_gray.png";
            TotalVATSalesEditImageSource = "ic_edit_gray.png";

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
private string GetConfirmOperationId()
        {
            if (IsCurrentZAKATTaxLess)
            {
                return "66";// For Amendment
            }
            else
            {
                return "65";// For Objection
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

        private bool GetEstimatedZAKATValueChangeStatus()
        {
            if( ZakatReturnDetailToCompare.TvtslI.Equals(ZakatReturnDetail.TvtslI)
                && ZakatReturnDetailToCompare.LabnoI.Equals(ZakatReturnDetail.LabnoI)
                && ZakatReturnDetailToCompare.ImpvalI.Equals(ZakatReturnDetail.ImpvalI)
                && ZakatReturnDetailToCompare.PtoslI.Equals(ZakatReturnDetail.PtoslI)
                && ZakatReturnDetailToCompare.EtimadI.Equals(ZakatReturnDetail.EtimadI)
                && ZakatReturnDetailToCompare.PramtI.Equals(ZakatReturnDetail.PramtI)
                && ZakatReturnDetailToCompare.Cpamt.Equals(ZakatReturnDetail.Cpamt))
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
                ICRStatusImage = "UnSubmitted";
                ICRStatusImage = "ic_unsubmitted.png";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
            {
                ICRStatusImage = "Paid";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "IP015"))//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = "In Processing";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "IP014"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = "Paid";

            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0008"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            { 
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = "Build";

            }
            else if(string.Equals(ZakatReturnDetail.Statusz, "IP021"))//To be approved || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = "To be approved";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0004"))//Amend without Objection
            {
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = "Submitted";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0003"))// In Build state 
            {
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = "Build";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0011"))// In Paid state 
            {
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = "Paid";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0005"))// In Processing
            {
                ICRStatusImage = "ic_loading.png";
                ICRStatus = "In Processing";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0002"))// Status when the return released by GAZT officer
            {
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = "Paid";
            }
            else if (string.Equals(ZakatReturnDetail.Statusz, "E0001"))// UnSubmitted
            {
                ICRStatusImage = "ic_Paid.png";
                ICRStatus = "Paid";
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
            ConfirmAndGenerateSADADBillLabelVisibility = true;
            ChangeFromEstimateTAccountringBasisButtonVisibility = false;
        }

        private void SetLayoutVisibilityAfterSuccessfulConfirmation()
        {
            IsEditTextVisible = true;
        }


        private void ClearData()
        {
            UnSetEditImage();
            isEditVisible = false;
            isLabelVisible = false;
            IsEditTextVisible = false;
            SetSubmitButtonVisibility = false;
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
        #endregion

    }
}
