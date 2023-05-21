using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage_ViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class ZakatReturnDetailsPageViewModel: ViewModelBase
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
        // string ReturnStatus = "2";
        public static bool IsAmendButtonPressed = false;
        public static string Fbguid  { get; set; }
      //  public Label DateLabel { get; set; }
        #endregion
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
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private ZakatReturnDetailsD _zakatReturnDetail ;
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
        public ZakatReturnDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnBillsButtonClicked = new Xamarin.Forms.Command(() =>
            {
            });
            OnAmendReturnButtonClicked = new Command(() =>
            {
                try
                {
                    IsAmendButtonClicked = true;
                    _navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            });
            GoBackClick = new Command(async () =>
            {
                if(!IsLoading)
                {
                    _navigationService.GoBack();
                }
            });
            OnSalesDetailsClicked = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    IsAmendButtonPressed = false;
                    IsAmendButtonClicked = false;
                    await OnPageLoad(Fbguid);// Called again to get the latest status so buttton visibility can behaves properly as per web 
                    _navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
                }
                catch(Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            });

            OnChangeFromEstimateToAccountingBasisButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                //try
                //{
                //    await _dialogService.ShowMessage(AppResources.PleaseVisitGAZTPortalToChangeTheRegistrationType, AppResources.Information);

                //}
                //catch (Exception ex)
                //{
                //    Console.Write(ex.ToString());
                //    Console.Write(ex.StackTrace.ToString());
                //}


                try
                {


                    var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.PleaseVisitGAZTPortalToChangeTheRegistrationType);

                    if (App.IsArabic)
                    {
                        VisitPortalPopup.OnGotoPortal = () =>
                        {

                            Launcher.OpenAsync(Constants.GAZTVisitPortalUrlAR);

                        };
                    }
                    else
                    {
                        VisitPortalPopup.OnGotoPortal = () =>
                        {

                            Launcher.OpenAsync(Constants.GAZTVisitPortalUrlEN);

                        };
                    }
                    //VisitPortalPopup.OnGotoPortal = () =>
                    //{
                    //    Launcher.OpenAsync(Constants.GAZTVisitPortalUrl);

                    //};
                    await PopupNavigation.Instance.PushAsync(VisitPortalPopup);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                }
            });
            
        }
        #endregion
        #region Method
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
            else if(ZakatReturnDetails.d.Statusz.Equals("E0002"))// E002 means Tax officer has released the return
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
            else if(ZakatReturnDetails.d.Statusz.Equals(""))
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
                    if(zakatReturnDetails != null && zakatReturnDetails.d != null)
                    {
                        //  var res =   WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(zakatReturnDetails.d.Fbnum, fbguid); // Method to get the invoice
                        //  EsimatedZAKATReturnsButtonSets esimatedZAKATReturnsButtonSets = await WebServiceManager.GAZTGetZAKATReturnButtonSet();
                        ZakatReturnDetails = zakatReturnDetails;
                        ZakatReturnDetail = zakatReturnDetails.d;
                        GetUpdatedDataAfterAddingComma();
                        SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                        SetChangeFromEstimateTAccountringBasisButtonVisibility(ZakatReturnDetails.d.Statusz);

                        Abrzu = ZakatReturnListPageViewModel.ReturnPeriod;
                        //if (zakatReturnDetails.d.Abrzu != null && zakatReturnDetails.d.Abrzo != null)
                        //{
                        //    if (App.IsArabic)
                        //    {
                        //        try
                        //        {
                        //            Abrzu = JsonConvert.DeserializeObject<DateTime>(@"""" + zakatReturnDetails.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        //            Abrzu = UtilityManager.ToArabicDate(Abrzu);
                        //            Abrzo = JsonConvert.DeserializeObject<DateTime>(@"""" + zakatReturnDetails.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        //            Abrzo = UtilityManager.ToArabicDate(Abrzo);
                        //            Abrzu = Abrzu + "  " + AppResources.To + "  " + Abrzo;
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //        }
                        //        // itemCR.Udate = UtilityManager.ToArabicDate(itemCR.Udate);
                        //    }
                        //    else
                        //    {
                        //        try
                        //        {
                        //            Abrzu = JsonConvert.DeserializeObject<DateTime>(@"""" + zakatReturnDetails.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        //            Abrzo = JsonConvert.DeserializeObject<DateTime>(@"""" + zakatReturnDetails.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        //            Abrzu = Abrzu + "  " + AppResources.To + "  " + Abrzo;
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        IsLoading = false;
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
                       if(WebServiceManager.ErrorMessage.Equals("Dear taxpayer, the return is under GAZT review and cannot be amended."))// message is always coming in english from the server
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
                   await  _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
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
                else if(ButtonStatus.Equals("E0003"))//E0003 The return is Paid OR Partially paid 
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
                else if(ButtonStatus.Equals("E0005"))//In Processing
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
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                                Console.Write(ex.ToString());
                                Console.Write(ex.StackTrace.ToString());
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
                                Console.Write(ex.ToString());
                                Console.Write(ex.StackTrace.ToString());
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
                            { ZakatReturnDetail = zakatReturnDetails.d;                         
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
                        Device.BeginInvokeOnMainThread(async () =>
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
      private ZakatReturnDetailsD GetUpdatedDataAfterAddingComma()
        {
            if(ZakatReturnDetail != null)
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
