using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage_ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class ZAKATReturnDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBackButtonClicked { get; set; }
        public ICommand OnSubmitButtonClicked { get; set; }
//============================start===================================================
        public ICommand OnSubmitClicked { get; set; }
        public ICommand OnEditClicked { get; set; }
        public string Fbguid { get; set; }
        
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
            isLabelVisible = false;
            isEditVisible = true;

            OnSubmitClicked = new Xamarin.Forms.Command(() =>
            {
                //isEditVisible = false;
                //isLabelVisible = true;
            });

            OnEditClicked = new Xamarin.Forms.Command(() =>
            {
                //isLabelVisible = false;
                //isEditVisible = true;
            });
//=========================end=====================================================
            // OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            // {
            //    _navigationService.GoBack();
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
                    PopToRootPage();
                    if (zakatReturnDetails != null && zakatReturnDetails.d != null)
                    {
                        ZakatReturnDetails = zakatReturnDetails;
                        ZakatReturnDetail = zakatReturnDetails.d;
                        GetUpdatedDataAfterAddingComma();

                        SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                        //SetChangeFromEstimateTAccountringBasisButtonVisibility(ZakatReturnDetails.d.Statusz);

                        Abrzu = ZakatReturnListPageViewModel.ReturnPeriod;
                       
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
                   // IsLoading = false;
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

        ///* Method to insert the comma to amount variable
        ///

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


        private async Task SubmitZakatReturn(String PostOperation, string InvFlag)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    WebServiceManager.ErrorMessage = string.Empty;
                    if (PostOperation.Equals("66") || PostOperation.Equals("65"))
                    {
                      //  _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(zakatReturnDetailsD, PostOperation);
                        if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                        {
                           // HideDisclaimer();
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
                    }
                    else
                    {
                        //ZakatReturnDetails UpdatedPostData = GetPostDataAfterRemovingComma(zakatReturnDetailsD);
                        //  zakatReturnDetailsD.d.Cpamt = SalesDetailsList[7].InformationFromPartie.Replace(",", "");
                      //  _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(UpdatedPostData, PostOperation);
                        if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                        {
                            //HideDisclaimer();
                            //Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.d.Estsl);
                            ////  IsCurrentZAKATTaxLess = existingZakatBase >= Convert.ToDouble(_zakatReturnDetails.d.Zkamt);
                            //if (existingZakatBase > Convert.ToDouble(_zakatReturnDetails.d.Zkamt))
                            //{
                            //    IsCurrentZAKATTaxLess = true;
                            //}
                            //else
                            //{
                            //    IsCurrentZAKATTaxLess = false;
                            //}
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
                    }
                    if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                    {
                        if (PostOperation.Equals("66") || PostOperation.Equals("65"))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                               // HideAllButton();
                                await _dialogService.ShowMessageBox(AppResources.ZZReturnSubmittedSuccessfully, AppResources.Information);
                                // IsComingFromSalesDetailsPage = true;
                                //_navigationService.NavigateTo(App.BillDetailsPageView, zakatReturnDetailsD.d);
                            });
                        }
                        if (PostOperation.Equals("05"))
                        {
                            //if (Convert.ToDouble(_zakatReturnDetails.d.Zkamt) >= existingZakatBase)//existingZakatBase
                            //{
                            //    Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.d.Estsl);
                            //    ShowOnlyInfoIcon();
                            //    ShowConfirmButton();
                            //    SetSalesDetailsData(_zakatReturnDetails);
                            //    HideDisclaimer();
                            //}
                            //else
                            //{
                            //    ShowDisclaimer();
                            //    SetChangedValueToUploadAttachment();
                            //    bool ISAllRequiredDocumentUploadedwithReason = IsAllRequiredAttachmentUploaded();
                            //    if (ISAllRequiredDocumentUploadedwithReason)
                            //    {
                            //        Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.d.Estsl);
                            //        ShowConfirmButton();
                            //        SetSalesDetailsData(_zakatReturnDetails);
                            //        // ShowEditIcon();// Commented 
                            //        ShowOnlyInfoIcon();
                            //        HideDisclaimer();
                            //    }
                            //    else
                            //    {
                            //        Device.BeginInvokeOnMainThread(async () => {
                            //            await _dialogService.ShowMessageBox(AppResources.ZZPleaseuploadtheRequiredDocumentandChangereason, AppResources.Information);
                            //        });
                            //    }
                            //}
                        }
                    }
                    else
                    {
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        private void AssignCalculatedValueAfterSubmission()
        {
            //zakatReturnDetailsD.d.Zbamt = _zakatReturnDetails.d.Zbamt;
            //zakatReturnDetailsD.d.Zkamt = _zakatReturnDetails.d.Zkamt;
        }


        public async Task OnReleaseOrBillsClicked()
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
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
            else if (ReleaseOrBillDetailsButtonText.Equals("Bills") || ReleaseOrBillDetailsButtonText.Equals("الفواتير"))
            {
               // AmedmentButtonVisibility = true;
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
            else if (ZakatReturnDetails.d.Statusz.Equals("E0004") || ZakatReturnDetails.d.Statusz.Equals("E0003"))//Whent the Return is already Ameded by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
            else if (ZakatReturnDetails.d.Statusz.Equals(""))
            {
               // SalesDetailsAndReleaseButtonVisibility = false;
            }
            else
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
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
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(AppResources.ZZReleasedSuccessfully, AppResources.ZZNotification);
                                });
                                //Device.BeginInvokeOnMainThread(async () =>
                                //{
                                //    _dialogService.ShowMessageBox(AppResources.ZZReleasedSuccessfully, AppResources.ZZSUCCESS);
                                //});
                                // 
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
                if (ButtonStatus.Equals("E0001") || ButtonStatus.Equals("IP011"))
                {
                    ReleaseOrBillDetailsButtonText = AppResources.Release;
                }
                else if (ButtonStatus.Equals("IP014"))
                {
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0002"))// E0002 if return  released by GAZT officer 
                {
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0003"))//E0003 The return is Paid OR Partially paid 
                {
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0004") || ButtonStatus.Equals("E0008"))//When the Return is already Amended by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
                {
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0005"))//In Processing
                {
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0011"))//In Processing
                {
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




        #endregion

    }
}
