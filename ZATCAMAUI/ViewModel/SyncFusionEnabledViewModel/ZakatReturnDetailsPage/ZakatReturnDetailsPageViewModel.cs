using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage
{

    public class ZakatReturnDetailsPageViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBillsButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand OnSalesDetailsClicked { get; set; }
        public ICommand OnAmendReturnButtonClicked { get; set; }
        public ICommand OnChangeFromEstimateToAccountingBasisButtonClicked { get; set; }


        public static bool IsAmendButtonClicked = false;
        public static bool IsAmendButtonPressed = false;
        public static string Fbguid { get; set; }
        #endregion
        #region Property
        
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
        private bool _salesDetailsAndReleaseButtonVisibility = true;
        public bool SalesDetailsAndReleaseButtonVisibility
        {
            get
            {
                return _salesDetailsAndReleaseButtonVisibility;
            }
            set
            {
                _salesDetailsAndReleaseButtonVisibility = value;
                RaisePropertyChanged("SalesDetailsAndReleaseButtonVisibility");
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
        private string _abrzo;
        public string Abrzo
        {
            get
            {
                return _abrzo;
            }
            set
            {
                _abrzo = value;
                RaisePropertyChanged("Abrzo");
            }
        }
        private bool _amedmentButtonVisibility = false;
        public bool AmedmentButtonVisibility
        {
            get
            {
                return _amedmentButtonVisibility;
            }
            set
            {
                _amedmentButtonVisibility = value;
                RaisePropertyChanged("AmedmentButtonVisibility");
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


        #endregion
        #region Constructor
        public ZakatReturnDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            OnBillsButtonClicked = new Command(() =>
            {
            });
            OnAmendReturnButtonClicked = new Command(() =>
            {
                try
                {
                    IsAmendButtonClicked = true;
                    _navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
                }
                catch (Exception)
                {


                }
            });
            GoBackClick = new Command(async () =>
            {
                if (!IsLoading)
                {
                    _navigationService.GoBack();
                }
            });
            OnSalesDetailsClicked = new Command(async () =>
            {
                try
                {
                    IsAmendButtonPressed = false;
                    IsAmendButtonClicked = false;
                    await OnPageLoad(Fbguid);// Called again to get the latest status so buttton visibility can behaves properly as per web 
                    _navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
                }
                catch (Exception)
                {


                }
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
                    await PopupNavigation.Instance.PushAsync(VisitPortalPopup);
                }
                catch (Exception)
                {


                }
            });

        }
        #endregion
        #region Method
        public async Task OnReleaseOrBillsClicked()
        {
            if (ZakatReturnDetails.d.Statusz.Equals("E0001") || ZakatReturnDetails.d.Statusz.Equals("IP011"))
            {
                await ReleaseEstimateZakatReturn();
            }
            else if (ZakatReturnDetails.d.Statusz.Equals("IP014"))// E002 means Tax officer has released the return
            {
            }
            else if (ZakatReturnDetails.d.Statusz.Equals("E0002"))// E002 means Tax officer has released the return
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
            else if (ReleaseOrBillDetailsButtonText.Equals("Bills") || ReleaseOrBillDetailsButtonText.Equals("الفواتير"))
            {
                AmedmentButtonVisibility = true;
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
            else if (ZakatReturnDetails.d.Statusz.Equals("E0004") || ZakatReturnDetails.d.Statusz.Equals("E0003"))//Whent the Return is already Ameded by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
            else if (ZakatReturnDetails.d.Statusz.Equals(""))
            {
                SalesDetailsAndReleaseButtonVisibility = false;
            }
            else
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
        }
        public async Task OnPageLoad(string fbguid)
        {
            try
            {
                Fbguid = fbguid;
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
                        SetChangeFromEstimateTAccountringBasisButtonVisibility(ZakatReturnDetails.d.Statusz);

                        Abrzu = ZakatReturnListPageViewModel.ReturnPeriod;
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            //     Dear taxpayer, the return is under GAZT review and cannot be amended.
                            if (WebServiceManager.ErrorMessage.Equals("Dear taxpayer, the return is under GAZT review and cannot be amended."))// message is always coming in english from the server
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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
        private void SetReleaseOrBillDetailsButtonText(string ButtonStatus)
        {
            try
            {
                if (ButtonStatus.Equals("E0001") || ButtonStatus.Equals("IP011"))
                {
                    SalesDetailsAndReleaseButtonVisibility = true;
                    AmedmentButtonVisibility = false;// Verified
                    ReleaseOrBillDetailsButtonText = AppResources.Release;
                }
                else if (ButtonStatus.Equals("IP014"))
                {
                    SalesDetailsAndReleaseButtonVisibility = true;
                    AmedmentButtonVisibility = true;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0002"))// E0002 if return  released by GAZT officer 
                {
                    SalesDetailsAndReleaseButtonVisibility = true;
                    AmedmentButtonVisibility = true;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0003"))//E0003 The return is Paid OR Partially paid 
                {
                    SalesDetailsAndReleaseButtonVisibility = true;
                    AmedmentButtonVisibility = true;// Verified
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0004") || ButtonStatus.Equals("E0008"))//When the Return is already Amended by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
                {
                    //ButtonStatus.Equals("E0008") This has been varified by using Code
                    SalesDetailsAndReleaseButtonVisibility = true;
                    AmedmentButtonVisibility = false;// Verified
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0005"))//In Processing
                {
                    SalesDetailsAndReleaseButtonVisibility = true;
                    AmedmentButtonVisibility = false;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals("E0011"))//In Processing
                {
                    SalesDetailsAndReleaseButtonVisibility = true;
                    AmedmentButtonVisibility = true;
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if (ButtonStatus.Equals(""))//In Processing
                {
                    SalesDetailsAndReleaseButtonVisibility = false;
                }
            }
            catch (Exception)
            {


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
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(AppResources.ZZReleasedSuccessfully, AppResources.ZZNotification);
                                });
                            }
                            catch (Exception)
                            {


                            }
                        }
                        else
                        {
                            try
                            {
                              
                                MainThread.BeginInvokeOnMainThread(async () =>
                                    {
                                        
                                        await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                                        _navigationService.GoBack();
                                        WebServiceManager.ErrorMessage = string.Empty;
                                    });
                            }
                            catch (Exception)
                            {


                            }
                          
                        }
                        ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(Fbguid);
                        PopToRootPage();
                       if (zakatReturnDetails != null)
                        {
                            ZakatReturnDetails = zakatReturnDetails;
                            if (zakatReturnDetails.d != null)
                            {
                                ZakatReturnDetail = zakatReturnDetails.d;
                                GetUpdatedDataAfterAddingComma();
                                if (ZakatReturnDetails.d.Statusz != null)
                                {
                                    SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                                }
                            }
                        }
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception)
            {


            }
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

        public void ClearData()
        {
            ZakatReturnDetail = null;
        }

        #endregion
    }
}
