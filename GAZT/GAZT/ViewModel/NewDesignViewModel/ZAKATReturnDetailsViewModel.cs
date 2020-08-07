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
                isEditVisible = false;
                isLabelVisible = true;
            });

            OnEditClicked = new Xamarin.Forms.Command(() =>
            {
                isLabelVisible = false;
                isEditVisible = true;
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

                        //SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
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
        #endregion

    }
}
