using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class ICRListPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static string EUser = string.Empty;
        //  public ICommand OnBillsButtonClicked { get; set; }
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

        private ICRStatus _selectedICRStatus;
        public ICRStatus SelectedICRStatus
        {
            get
            {
                return _selectedICRStatus;
            }
            set
            {
                _selectedICRStatus = value;
                if (_selectedICRStatus != null)
                {
                    if (ICRDummyList != null && ICRDummyList.Count != 0)
                    {
                        if (string.Equals(_selectedICRStatus.Txt30, "All"))
                        {
                            ICRList = ICRDummyList;
                        }
                        else if (string.Equals(_selectedICRStatus.Estat, "E01TP"))
                        {
                            ICRList = ICRDummyList.Where(x => (x.Status == "E0001") || (x.Status == "E0013")).ToList();
                        }
                        else
                        {
                            ICRList = ICRDummyList.Where(x => x.Status == _selectedICRStatus.Estat).ToList();
                        }
                    }
                }
                RaisePropertyChanged("SelectedICRStatus");
            }
        }

        private List<ICRStatus> _iCRStatusList;
        public List<ICRStatus> ICRStatusList
        {
            get
            {
                return _iCRStatusList;
            }
            set
            {
                _iCRStatusList = value;
                RaisePropertyChanged("ICRStatusList");
            }
        }

        private ICRListSet _selectedICR;
        public ICRListSet SelectedICR
        {
            get
            {
                return _selectedICR;
            }
            set
            {
                _selectedICR = value;
                if (SelectedICR != null)
                {
                    GetVATAllReturnsAsync();
                }
                RaisePropertyChanged("SelectedICR");
            }
        }


        private List<ICRListSet> _iCRDummyList;
        public List<ICRListSet> ICRDummyList
        {
            get
            {
                return _iCRDummyList;
            }
            set
            {
                _iCRDummyList = value;
            }
        }



        private List<ICRListSet> _iCRList;
        public List<ICRListSet> ICRList
        {
            get
            {
                return _iCRList;
            }
            set
            {
                _iCRList = value;
                RaisePropertyChanged("ICRList");
            }
        }
        #endregion

        #region Constructor

        public ICRListPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;


            //OnBillsButtonClicked = new Xamarin.Forms.Command(async () =>
            //{
            //    _navigationService.NavigateTo(App.BillDetailsPageView);
            //});


        }

        #endregion

        #region Method

        public async Task onPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                await Task.Run(async () =>
                {
                    ICRList = null;

                    ICR icrList = null;
                    try
                    {
                        string lang = UtilityManager.GetLanguageParameter();
                        icrList = WebServiceManager.GAZTGetICRs(App.TP.Tin, lang);
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                        if (icrList.ICR_STATUSSet != null && icrList.ICR_STATUSSet.Count != 0)
                        {
                            ICRStatusList = new List<ICRStatus>();
                            ICRStatusList = icrList.ICR_STATUSSet;
                            SelectedICRStatus = ICRStatusList.Where(x => x.Estat == "E01TP").FirstOrDefault();
                        }

                        VATDeclaration vATDeclaration = new VATDeclaration();
                        //  vATDeclaration.
                        // VATDeclaration _vATDeclaration  =   await WebServiceManager.GAZTGetVATReturns();

                        if (icrList.ICR_LISTSet != null && icrList.ICR_LISTSet.Count != 0)
                        {
                            ICRList = new List<ICRListSet>();
                            ICRList = icrList.ICR_LISTSet;
                            ICRDummyList = ICRList;



                        }
                        else
                        {
                            // await _dialogService.ShowMessageBox(AppResources.ZNoICRAvailable, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        }
                    }
                    catch (Exception e)
                    {

                        // await _dialogService.ShowMessageBox(AppResources.ZNoICRAvailable, AppResources.Information);
                        IsLoading = false;
                        //  _navigationService.GoBack();

                    }

                });

                await Task.Run(() =>
                    {

                        IsLoading = false;
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
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        public async void GetVATAllReturnsAsync()
        {
            await GetVATAllReturns();
        }
        private async Task GetVATAllReturns()
        {
            ICRListSet selectedICRForStatus = null;
            try
            {
                try
                {

                   

                    if (SelectedICR != null)
                    {
                        selectedICRForStatus = new ICRListSet();
                        selectedICRForStatus = SelectedICR;
                        App.ICRStatus = selectedICRForStatus.Status;
                    }


                    //as per discussion with Vinay - the GUID is dynamic and will remain active and attched to ICR in a session. if the list of ICR' sis refreshed; meaning if the API is called again
                    // the GUID will be different
                    
                        String SelectedICRGUID = SelectedICR.Fbguid;
                        EUser = SelectedICR.Euser;
                        VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedICR.Fbguid, SelectedICR.Fbnum, SelectedICR.Euser, SelectedICR.Persl);
                        PopToRootPage();
                        _vATDeclaration.d.Fbguid = SelectedICRGUID;

                        if (_vATDeclaration != null && _vATDeclaration.d != null)
                        {
                            VATDeclaration vATDeclaration = new VATDeclaration();
                            VATDeclarationD vATDeclarationD = new VATDeclarationD();
                            Result5 result5 = new Result5();
                            List<Result5> lst = new List<Result5>();
                            ADRSet _aDRSet = new ADRSet();

                            lst.Add(result5);
                            vATDeclaration.d = vATDeclarationD;
                            vATDeclaration.d.ADRSet = _aDRSet;
                            vATDeclaration.d.ADRSet.results = lst;
                            IsLoading = false;


                            _navigationService.NavigateTo(App.VATReturnsPageView, _vATDeclaration);

                            //try
                            //{
                            //    // Made changes in the data to test the API
                            //    //_vATDeclaration.d.StdpurchaseAmt = "3000";
                            //    //_vATDeclaration.d.ADRSet.results[0].City = "Mumbai";
                            //    //_vATDeclaration.d.ADRSet.results[0].Street = "Church Gate";
                            //    //_vATDeclaration.d.Operationz = "05";
                            //    //_vATDeclaration.d.StepNumber = "04";
                            //    //_vATDeclaration.d.StdsalesAmt = "5400";
                            //    //_vATDeclaration.d.SalesGccAmt = "5400";

                            //    //_vATDeclaration.d.StepNumberz = "04";
                            //}
                            //catch (Exception ex)
                            //{


                            //}
                            //_vATDeclaration = await WebServiceManager.SaveVATDeclarationData(_vATDeclaration, SelectedICR.Fbguid);
                        }
                   
                    

                    //}
                    //_vATDeclaration = await WebServiceManager.SaveVATDeclarationData(_vATDeclaration, SelectedICR.Fbguid);

                }
                catch (Exception ex)
                {

                    //     _vATDeclaration.d.StdpurchaseAmt = "2000";
                    //  var response =   await WebServiceManager.SaveVATDeclarationData(_vATDeclaration.d, SelectedICR.Fbguid);
                }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
        #endregion
    }
}
