using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GAZT.ViewModel.SyncFusionEnabledViewModel.ReturnsPageViewModels
{
    public class ReturnsPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static string EUser = string.Empty;
        public static String ReturnPeriod = "";
        public EstimatedZakatReturns estimatedZakatReturnsList { get; set; }
        private List<ICRListSet> _iCRListVATSubmitted;
        public List<ICRListSet> ICRListVATSubmitted
        {
            get
            {
                return _iCRListVATSubmitted;
            }
            set
            {
                _iCRListVATSubmitted = value;
                //if (_iCRList != null && _iCRList.Count != 0)
                //{
                //    IsNoDataLabelVisible = false;
                //    IsICRListVisible = true;
                //    // SelectedICRStatus = null;
                //}
                //else
                //{
                //    IsICRListVisible = false;
                //    IsNoDataLabelVisible = true;
                //    //  SelectedICRStatus = null;
                //}
                RaisePropertyChanged("ICRListVATSubmitted");
            }
        }


        private List<ICRListSet> _iCRListVATNonSubmitted;
        public List<ICRListSet> ICRListVATNonSubmitted
        {
            get
            {
                return _iCRListVATNonSubmitted;
            }
            set
            {
                _iCRListVATNonSubmitted = value;
                //if (_iCRList != null && _iCRList.Count != 0)
                //{
                //    IsNoDataLabelVisible = false;
                //    IsICRListVisible = true;
                //    // SelectedICRStatus = null;
                //}
                //else
                //{
                //    IsICRListVisible = false;
                //    IsNoDataLabelVisible = true;
                //    //  SelectedICRStatus = null;
                //}
                RaisePropertyChanged("ICRListVATNonSubmitted");
            }
        }

        private List<ICRListSet> _iCRListVATOverDue;
        public List<ICRListSet> ICRListVATOverDue
        {
            get
            {
                return _iCRListVATOverDue;
            }
            set
            {
                _iCRListVATOverDue = value;
                //if (_iCRList != null && _iCRList.Count != 0)
                //{
                //    IsNoDataLabelVisible = false;
                //    IsICRListVisible = true;
                //    // SelectedICRStatus = null;
                //}
                //else
                //{
                //    IsICRListVisible = false;
                //    IsNoDataLabelVisible = true;
                //    //  SelectedICRStatus = null;
                //}
                RaisePropertyChanged("ICRListVATOverDue");
            }
        }


        private ICRListSet _selectedICRVATSubmitted;
        public ICRListSet SelectedICRVATSubmitted
        {
            get
            {
                return _selectedICRVATSubmitted;
            }
            set
            {
                try
                {
                    _selectedICRVATSubmitted = value;
                    if (_selectedICRVATSubmitted != null)
                    {
                        GetVATAllReturnsAsync();
                    }
                    RaisePropertyChanged("SelectedICRVATSubmitted");
                }
                catch (Exception ex)
                {

                }
            }
        }

        private EstimatedZakatReturnsResult _selectedZakatReturn;
        public EstimatedZakatReturnsResult SelectedZakatReturn
        {
            get
            {
                return _selectedZakatReturn;
            }
            set
            {
                _selectedZakatReturn = value;
                RaisePropertyChanged("SelectedZakatReturn");

                if (SelectedZakatReturn != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    if (SelectedZakatReturn.Fbtyp.Equals("FZ12"))
                    {
                        ReturnPeriod = SelectedZakatReturn.Period;
                        //  ReturnPeriod =UtilityManager.GetTaxPeriodDate(ReturnPeriod);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                             _navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedZakatReturn.Fbguid);
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                        });
                    }
                }
            }
        }

        private List<EstimatedZakatReturnsResult> _myZakatReturnsSubmitted = null;
        public List<EstimatedZakatReturnsResult> MyZakatReturnsSubmitted
        {
            get
            {
                return _myZakatReturnsSubmitted;
            }
            set
            {
                _myZakatReturnsSubmitted = value;
                RaisePropertyChanged("MyZakatReturnsSubmitted");
            }
        }

        private List<EstimatedZakatReturnsResult> _myZakatReturnsNonSubmitted = null;
        public List<EstimatedZakatReturnsResult> MyZakatReturnsNonSubmitted
        {
            get
            {
                return _myZakatReturnsNonSubmitted;
            }
            set
            {
                _myZakatReturnsNonSubmitted = value;
                RaisePropertyChanged("MyZakatReturnsNonSubmitted");
            }
        }

        private List<EstimatedZakatReturnsResult> _myZakatReturnsOverDue = null;
        public List<EstimatedZakatReturnsResult> MyZakatReturnsOverDue
        {
            get
            {
                return _myZakatReturnsOverDue;
            }
            set
            {
                _myZakatReturnsOverDue = value;
                RaisePropertyChanged("MyZakatReturnsOverDue");
            }
        }

        private int _tabIndexStatus = 0;
        public int TabIndexStatus
        {
            get
            {
                return _tabIndexStatus;
            }
            set
            {
                _tabIndexStatus = value;
                RaisePropertyChanged("TabIndexStatus");
            }
        }
        public ReturnsPageViewModel(INavigationService navigationService, IDialogService dialogService) //: base(navigationService, dialogService)
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

          
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    _navigation.PopToRootAsync();
                });
            }
        }

        public async Task onPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                  //  IsLoading = true;
                });

                await Task.Run(async () =>
                {
                    ICRListVATSubmitted = null;

                    ICR icrList = null;
                    try
                    {
                        string lang = UtilityManager.GetLanguageParameter();
                        icrList = WebServiceManager.GAZTGetICRs(App.TP.Tin, lang);
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                        if (icrList != null && icrList.ICR_STATUSSet != null && icrList.ICR_STATUSSet.Count != 0)
                        {
                           // ICRStatusList = new List<ICRStatus>();
                          //  ICRStatusList = icrList.ICR_STATUSSet;
                            if (App.IsArabic)
                            {

                                //foreach (var item in ICRStatusList)
                                //{
                                //    if(item.Txt30== "All")
                                //    {
                                //        item.Txt30 = "الجميع";
                                //    }
                                //    if(item.Txt30== "To be filled & In draft")
                                //    {
                                //        item.Txt30 = "جاهز للتعبئة والحفظ كمسودة";
                                //    }
                                //}

                                //ICRStatusList[ICRStatusList.FindIndex(ind => ind.Equals("All"))].Txt30 = "الجميع";
                                //ICRStatusList[ICRStatusList.FindIndex(ind => ind.Equals("To be filled & In draft"))].Txt30 = "جاهز للتعبئة والحفظ كمسودة";

                                //  ICRStatusList.Where(p => p.Txt30 == "All").();
                            }
                            //if (string.IsNullOrEmpty(App.ICRStatus))
                            //{
                            //    SelectedICRStatus = ICRStatusList.Where(x => x.Estat == "E01TP").FirstOrDefault();
                            //}
                        }

                        VATDeclaration vATDeclaration = new VATDeclaration();
                        //  vATDeclaration.
                        // VATDeclaration _vATDeclaration  =   await WebServiceManager.GAZTGetVATReturns();

                        if (icrList != null && icrList.ICR_LISTSet != null && icrList.ICR_LISTSet.Count != 0)
                        {
                            ICRListVATSubmitted = new List<ICRListSet>();
                            ICRListVATSubmitted = icrList.ICR_LISTSet.Where(a => a.Status == "E0055").ToList<ICRListSet>();
                            ICRListVATNonSubmitted = icrList.ICR_LISTSet.Where(a => a.Status == "E0001" || a.Status== "E0013").ToList<ICRListSet>();
                            DateTime Today = DateTime.Now;
                            ICRListVATOverDue = icrList.ICR_LISTSet.Where(a => a.Status != "E0045" && a.Status != "E0055" && a.DueDateDateTime < Today).ToList<ICRListSet>();
                            // ICRDummyList = ICRList;



                        }
                        else
                        {
                            // await _dialogService.ShowMessageBox(AppResources.ZNoICRAvailable, AppResources.Information);
                        //    IsLoading = false;
                            _navigationService.GoBack();
                        }

                        try
                        {
                            estimatedZakatReturnsList = await WebServiceManager.GAZTGetEstimateZakatReturnList();
                            PopToRootPage();
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            });
                        }
                        List<EstimatedZakatReturnsResult> myZakatReturnsListTemp = new List<EstimatedZakatReturnsResult>();
                        myZakatReturnsListTemp = new List<EstimatedZakatReturnsResult>(GetSortedList(estimatedZakatReturnsList.d.listSet.results));
                        DateTime TodayNew = DateTime.Now;
                        List<EstimatedZakatReturnsResult> MyZakatReturnsNonSubmittedChild = new List<EstimatedZakatReturnsResult>();
                        List<EstimatedZakatReturnsResult> MyZakatReturnsSubmittedChild = new List<EstimatedZakatReturnsResult>();
                        List<EstimatedZakatReturnsResult> MyZakatReturnsOverDueChild = new List<EstimatedZakatReturnsResult>();
                        if (myZakatReturnsListTemp != null && myZakatReturnsListTemp.Count > 0)
                        {
                            for (int i = 0; i < myZakatReturnsListTemp.Count; i++)
                            {
                                if (myZakatReturnsListTemp[i].Fbtyp.Equals("FZ12"))
                                {
                                    if (App.IsArabic)
                                    {
                                        myZakatReturnsListTemp[i].Period = UtilityManager.GetTaxPeriodDate(myZakatReturnsListTemp[i].Period);
                                    }
                                    else
                                    {
                                        if (myZakatReturnsListTemp[i].Period.Contains("-"))
                                            myZakatReturnsListTemp[i].Period.Replace("-", "- ");
                                    }
                                    if (string.IsNullOrEmpty(myZakatReturnsListTemp[i].Statfg))
                                    {
                                        if ((string.Equals(myZakatReturnsListTemp[i].Stat, "IP011")))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
                                        {

                                            myZakatReturnsListTemp[i].StatusImage = "ic_unsubmitted.png";
                                            myZakatReturnsListTemp[i].BorderColour = "#944E22";
                                            MyZakatReturnsNonSubmittedChild.Add(myZakatReturnsListTemp[i]);
                                        }
                                        else if (string.Equals(myZakatReturnsListTemp[i].Stat, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                                        {
                                            myZakatReturnsListTemp[i].BorderColour = "#005e4b";
                                            myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                                            MyZakatReturnsSubmittedChild.Add(myZakatReturnsListTemp[i]);
                                        }
                                        if (!string.Equals(myZakatReturnsListTemp[i].Stat, "P") && !string.Equals(myZakatReturnsListTemp[i].Stat, "IP014") && Convert.ToDateTime(myZakatReturnsListTemp[i].DueDtC) < TodayNew)//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                                        {
                                            MyZakatReturnsOverDueChild.Add(myZakatReturnsListTemp[i]);
                                        }

                                    }
                                    else
                                    {
                                        if ((string.Equals(myZakatReturnsListTemp[i].Statfg, "U")))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
                                        {

                                            myZakatReturnsListTemp[i].StatusImage = "ic_unsubmitted.png";
                                            myZakatReturnsListTemp[i].BorderColour = "#944E22";
                                            MyZakatReturnsNonSubmittedChild.Add(myZakatReturnsListTemp[i]);
                                        }
                                        else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                                        {
                                            myZakatReturnsListTemp[i].BorderColour = "#005e4b";
                                            myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                                            MyZakatReturnsSubmittedChild.Add(myZakatReturnsListTemp[i]);

                                        }
                                        else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "I"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                                        {
                                            myZakatReturnsListTemp[i].BorderColour = "#005e4b";
                                            myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                                        }
                                        if (!string.Equals(myZakatReturnsListTemp[i].Statfg, "P") && !string.Equals(myZakatReturnsListTemp[i].Statfg, "IP014") && Convert.ToDateTime(myZakatReturnsListTemp[i].DueDtC) < TodayNew)//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                                        {
                                            MyZakatReturnsOverDueChild.Add(myZakatReturnsListTemp[i]);
                                        }
                                    }

                                    //  myZakatReturnsList.Add(myZakatReturnsListTemp[i]);
                                }
                            }

                           
                        }
                        MyZakatReturnsNonSubmitted = MyZakatReturnsNonSubmittedChild;
                        MyZakatReturnsSubmitted = MyZakatReturnsSubmittedChild;
                        MyZakatReturnsOverDue = MyZakatReturnsOverDueChild;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(ex.Message, AppResources.Information);
                           // IsLoading = false;
                            _navigationService.GoBack();

                        });
                        //   await Task.Run(() =>
                        //   {

                        //  });
                    }

                });

                await Task.Run(() =>
                {

                  //  IsLoading = false;
                });




                //int k = 5;
                //List<ICRStatus> icrStatus = new List<ICRStatus>();
                //ICRStatusList = new List<ICRStatus>();
                //for (k = 0; k < 6; k++)
                //{
                //    ICRStatus m = new ICRStatus();
                //    m.Estat = "Abc";
                //    m.Ltext = "Abc";
                //    m.Spras = "Abc";
                //    m.Txt04 = "Abc";
                //    m.Txt30 = "Abc";

                //    icrStatus.Add(m);
                //}
                //ICRStatusList = icrStatus;

                //int i = 5;






                //List<ICRListSet> icrList = new List<ICRListSet>();
                //ICRList = new List<ICRListSet>();
                //for (i = 0; i < 6; i++)
                //{
                //    ICRListSet m = new ICRListSet();
                //    m.Incotext = "Abc";
                //    m.Txt50 = "100";
                //    m.DueDt = "12:02:20";
                //    m.TaxPeriod = "P";

                //    icrList.Add(m);
                //}

                //int j = 5;
                ////S MyBills = new List<MyBills>();
                //for (j = 0; j < 6; j++)
                //{
                //    ICRListSet m = new ICRListSet();
                //    m.Incotext = "Abc";
                //    m.Txt50 = "100";
                //    m.DueDt = "12:02:20";
                //    m.TaxPeriod = "P";

                //    icrList.Add(m);
                //}
                //ICRList = icrList;
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                  //  IsLoading = false;
                    _navigationService.GoBack();

                });
            }
        }
        public IEnumerable<EstimatedZakatReturnsResult> GetSortedList(IList<EstimatedZakatReturnsResult> ICRList)
        {
            try
            {
                var SortedList = ICRList.OrderBy(x => Convert.ToDateTime(x.DueDtC).TimeOfDay)
                              .ThenBy(x => Convert.ToDateTime(x.DueDtC).Date)
                              .ThenBy(x => Convert.ToDateTime(x.DueDtC).Year);
                return SortedList;
            }
            catch (Exception ex)
            {
                return ICRList;
            }


        }

        public async void GetVATAllReturnsAsync()
        {

       

            await GetVATAllReturns();

           
        }
        public bool isStatusNotValid()
        {
            bool isValid = true;
            if (SelectedICRVATSubmitted.Status == "E0020" || SelectedICRVATSubmitted.Status == "E0057" || SelectedICRVATSubmitted.Status == "E0076" || SelectedICRVATSubmitted.Status == "E0077" || SelectedICRVATSubmitted.Status == "E0078" || SelectedICRVATSubmitted.Status == "E0089" || SelectedICRVATSubmitted.Status == "E0090")
            {
                isValid = false;
            }
            return isValid;
        }
        private async Task GetVATAllReturns()
        {
            ICRListSet selectedICRForStatus = null;
            try
            {
                try
                {

                    if (SelectedICRVATSubmitted != null)
                    {
                        if (isStatusNotValid())
                        {
                            selectedICRForStatus = new ICRListSet();
                            selectedICRForStatus = SelectedICRVATSubmitted;
                            App.ICRStatus = selectedICRForStatus.Status;


                            //as per discussion with Vinay - the GUID is dynamic and will remain active and attched to ICR in a session. if the list of ICR' sis refreshed; meaning if the API is called again
                            // the GUID will be different

                            String SelectedICRGUID = SelectedICRVATSubmitted.Fbguid;
                            EUser = SelectedICRVATSubmitted.Euser;
                            VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedICRVATSubmitted.Fbguid, SelectedICRVATSubmitted.Fbnum, SelectedICRVATSubmitted.Euser, SelectedICRVATSubmitted.Persl);
                            PopToRootPage();


                            if (_vATDeclaration != null && _vATDeclaration.d != null)
                            {
                                _vATDeclaration.d.Fbguid = SelectedICRGUID;
                                VATDeclaration vATDeclaration = new VATDeclaration();
                                VATDeclarationD vATDeclarationD = new VATDeclarationD();
                                Result5 result5 = new Result5();
                                List<Result5> lst = new List<Result5>();
                                ADRSet _aDRSet = new ADRSet();

                                lst.Add(result5);
                                vATDeclaration.d = vATDeclarationD;
                                vATDeclaration.d.ADRSet = _aDRSet;
                                vATDeclaration.d.ADRSet.results = lst;

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.VATReturnsPageView, _vATDeclaration);
                                });

                            }
                            else
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            }
                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZZZReturnUnderReview, AppResources.Information);
                        }
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        // IsLoading = false;
                        _navigationService.GoBack();

                    });
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //  IsLoading = false;
                    _navigationService.GoBack();

                });

            }

        }

    }
}
