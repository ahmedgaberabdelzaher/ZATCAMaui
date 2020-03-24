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
                    //if (SelectedICR != null)
                    //{
                    //    GetVATAllReturnsAsync();
                    //}
                    RaisePropertyChanged("SelectedICRVATSubmitted");
                }
                catch (Exception ex)
                {

                }
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
                            ICRListVATNonSubmitted = icrList.ICR_LISTSet.Where(a => a.Status == "E0001").ToList<ICRListSet>();
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


    }
}
