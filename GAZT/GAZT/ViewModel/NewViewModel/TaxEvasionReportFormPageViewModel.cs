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
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class TaxEvasionReportFormPageViewModel : ViewModelBase
    {
        #region variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand SubmitReportClicked { get; set; }

        #endregion //CreateReportPost/IsVisiblePickerAr/IsVisiblePickerEn
        //SelectedCategory SubmitReportClicked
        //TaxEvasionReportList
        //TaxEvasionReportList selectedtaxEList = new TaxEvasionReportList();
        private string _txtFType = string.Empty;
        public string TxtFType
        {
            get
            {
                return _txtFType;
            }
            set
            {
                _txtFType = value;
                RaisePropertyChanged("TxtFType");
            }
        }
        private string _txtReportDetailCity = string.Empty;
        public string TxtReportDetailCity
        {
            get
            {
                return _txtReportDetailCity;

            }
            set
            {
                _txtReportDetailCity = value;
                RaisePropertyChanged("TxtReportDetailCity");

            }
        }
        private string _txtReportDetailRegion = string.Empty;
        public string TxtReportDetailRegion
        {
            get
            {
                return _txtReportDetailRegion;
            }
            set
            {
                _txtReportDetailRegion = value;
                RaisePropertyChanged("TxtReportDetailRegion");

            }
        }

        private TaxEvasionReportList _selectedtaxEList = null;
        public TaxEvasionReportList selectedtaxEList
        {
            get
            {
                return _selectedtaxEList;

            }
            set
            {
                _selectedtaxEList = value;

                RaisePropertyChanged("selectedtaxEList");
            }
        }
        private double _latitude = 00.00;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                RaisePropertyChanged("Latitude");
            }
        }
        private double _longitude = 00.00;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                RaisePropertyChanged("Longitude");
            }
        }


        private List<FacilityCompanyType> _listfacilityCompanyType = null;
        public List<FacilityCompanyType> ListFacilityCompanyType
        {
            get
            {
                return _listfacilityCompanyType;

            }
            set
            {
                _listfacilityCompanyType = value;

                RaisePropertyChanged("ListFacilityCompanyType");
            }
        }//IsLoading
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





        private List<FacilityCompanyType> _dlistfacilityCompanyType = null;
        public List<FacilityCompanyType> DListFacilityCompanyType
        {
            get
            {
                return _dlistfacilityCompanyType;

            }
            set
            {
                _dlistfacilityCompanyType = value;

                RaisePropertyChanged("DListFacilityCompanyType");
            }
        }


        private bool _isSubmitButtonEnable = false;
        public bool IsSubmitButtonEnable
        {
            get
            {
                return _isSubmitButtonEnable;
            }
            set
            {


                _isSubmitButtonEnable = value;


                RaisePropertyChanged("IsSubmitButtonEnable");
            }
        }




        private bool _isTIN = false;
        public bool IsTIN
        {
            get
            {
                return _isTIN;
            }
            set
            {
                _isTIN = value;
                if (_isTIN == true)
                {
                    IsTINVisible = true;

                    TxtTIN = string.Empty;
                }
                else
                {
                    IsTINVisible = false;
                    TxtTIN = string.Empty;
                }
                RaisePropertyChanged("IsTIN");
            }
        }
        private string _txtTIN = string.Empty;
        public string TxtTIN
        {
            get
            {
                return _txtTIN;
            }
            set
            {
                _txtTIN = value;
                _tEReportobj.TIN = _txtTIN;
                RaisePropertyChanged("TxtTIN");
            }
        }

        private bool _isTINVisible = false;
        public bool IsTINVisible
        {
            get
            {
                return _isTINVisible;
            }
            set
            {
                _isTINVisible = value;
                _tEReportobj.HavingTIN = "true";
                RaisePropertyChanged("IsTINVisible");
            }
        }//SelectedTaxEvasionCompanyType
        private FacilityCompanyType _selectedTaxEvasionCompanyType;
        public FacilityCompanyType SelectedTaxEvasionCompanyType
        {
            get
            {
                return _selectedTaxEvasionCompanyType;
            }
            set
            {
                _selectedTaxEvasionCompanyType = value;

                if (_selectedTaxEvasionCompanyType != null)
                {
                    TxtFType = _selectedTaxEvasionCompanyType.Name;
                }

                RaisePropertyChanged("SelectedTaxEvasionCompanyType");
            }
        }





        private string _selectedCategory;
        public string SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;



                RaisePropertyChanged("SelectedCategory");
            }
        }//TReportDetail
        private string _tReportDetail = string.Empty;
        public string TReportDetail
        {
            get
            {
                return _tReportDetail;
            }
            set
            {
                _tReportDetail = value;
                _tEReportobj.ReportDetails = _tReportDetail;



                RaisePropertyChanged("TReportDetail");
            }
        }



        private bool _isVisiblePickerAr = false;
        public bool IsVisiblePickerAr
        {
            get
            {
                return _isVisiblePickerAr;
            }
            set
            {
                _isVisiblePickerAr = value;
                //_tEReportobj.ViolationType = _selectedCategory;

                RaisePropertyChanged("IsVisiblePickerAr");
            }
        }
        private bool _isVisiblePickerEn = false;
        public bool IsVisiblePickerEn
        {
            get
            {
                return _isVisiblePickerEn;
            }
            set
            {
                _isVisiblePickerEn = value;
                //_tEReportobj.ViolationType = _selectedCategory;

                RaisePropertyChanged("IsVisiblePickerEn");
            }
        }

        private string _tEmail = string.Empty;
        public string TEmail
        {
            get
            {
                return _tEmail;
            }
            set
            {
                _tEmail = value;
                _tEReportobj.ReporterEmail = _tEmail;

                RaisePropertyChanged("TEmail");
            }
        }

        private string _tName = string.Empty;
        public string TName
        {
            get
            {
                return _tName;
            }
            set
            {
                _tName = value;
                _tEReportobj.ReporterName = _tName;

                RaisePropertyChanged("TName");
            }
        }
        private string _tMobNumber = string.Empty;
        public string TMobNumber
        {
            get
            {
                return _tMobNumber;
            }
            set
            {
                _tMobNumber = value;
                _tEReportobj.ReporterMobileNumber = _tMobNumber;
                RaisePropertyChanged("TMobNumber");
            }
        }
        private string _tFaciName = string.Empty;
        public string TFaciName
        {
            get
            {
                return _tFaciName;
            }
            set
            {
                _tFaciName = value;
                _tEReportobj.CompanyName = _tFaciName;




                RaisePropertyChanged("TFaciName");
            }
        }//TFaciOwnerName
        private string _tFaciOwnerName = string.Empty;
        public string TFaciOwnerName
        {
            get
            {
                return _tFaciOwnerName;
            }
            set
            {
                _tFaciOwnerName = value;
                _tEReportobj.CompanyOwnerName = _tFaciOwnerName;




                RaisePropertyChanged("TFaciOwnerName");
            }
        }//TFaciMobNo
        private string _tFaciMobNo = string.Empty;
        public string TFaciMobNo
        {
            get
            {
                return _tFaciMobNo;
            }
            set
            {
                _tFaciMobNo = value;
                _tEReportobj.CompanyMobileNumber = _tFaciMobNo;




                RaisePropertyChanged("TFaciMobNo");
            }
        }//TFaciEmail
        private string _tFaciEmail = string.Empty;
        public string TFaciEmail
        {
            get
            {
                return _tFaciEmail;
            }
            set
            {
                _tFaciEmail = value;
                _tEReportobj.CompanyEmail = _tFaciEmail;




                RaisePropertyChanged("TFaciEmail");
            }
        }


        private string _tID = string.Empty;
        public string TID
        {
            get
            {
                return _tID;
            }
            set
            {
                _tID = value;
                _tEReportobj.ID = _tID;
                RaisePropertyChanged("TID");
            }
        }


        //TID
        private string _tVatNumber = string.Empty;
        public string TVatNumber
        {
            get
            {
                return _tVatNumber;
            }
            set
            {
                _tVatNumber = value;
                _tEReportobj.VAT = _tVatNumber;
                RaisePropertyChanged("TVatNumber");
            }
        }

        //TFDAdress
        private string _tFDAdress = string.Empty;
        public string TFDAdress
        {
            get
            {
                return _tFDAdress;
            }
            set
            {
                _tFDAdress = value;
                _tEReportobj.District = _tFDAdress;



                RaisePropertyChanged("TFDAdress");
            }
        }
        private string _tFSAddress = string.Empty;
        public string TFSAddress
        {
            get
            {
                return _tFSAddress;
            }
            set
            {
                _tFSAddress = value;
                _tEReportobj.CompanyAddress = _tFSAddress;

                RaisePropertyChanged("TFSAddress");
            }
        }
        private string _tFWType = string.Empty;
        public string TFWType
        {
            get
            {
                return _tFWType;
            }
            set
            {
                _tFWType = value;
                _tEReportobj.WorkType = _tFWType;
                RaisePropertyChanged("TFWType");
            }
        }


        private TEReport _tEReportobj = new TEReport();
        public TEReport TEReportobj
        {
            get
            {
                return _tEReportobj;

            }
            set
            {
                _tEReportobj = value;
                if (_tEReportobj != null)
                {
                    //IsCPickerEnable = true;
                    //onSelectedTaxEvasionRegion();
                }
                //ListFormBudles = null;
                RaisePropertyChanged("TEReportobj");
            }
        }




        private RegionList _selectedTaxEvasionRegion;
        public RegionList SelectedTaxEvasionRegion
        {
            get
            {
                return _selectedTaxEvasionRegion;
            }
            set
            {
                _selectedTaxEvasionRegion = value;
                if (_selectedTaxEvasionRegion != null)
                {

                    _tEReportobj.RegionCode = _selectedTaxEvasionRegion.RegionCode;
                    //IsCPickerEnable = true;
                    //string v= SelectedTaxEvasionRegion.RegionCode;
                    onSelectedTaxEvasionRegion();
                    if (App.IsArabic)
                    {
                        TxtReportDetailRegion = _selectedTaxEvasionRegion.RegionNameAR;
                    }
                    else
                    {
                        TxtReportDetailRegion = _selectedTaxEvasionRegion.RegionNameEN;

                    }
                    //TEReportobj.RegionCode = v;
                }
                //ListFormBudles = null;
                RaisePropertyChanged("SelectedTaxEvasionRegion");
            }
        }
        private DateTime _datePick = DateTime.Now;

        public DateTime DatePick
        {
            get
            {
                return _datePick;
            }
            set
            {
                _datePick = value;




                RaisePropertyChanged("DatePick");
            }
        }

        private CityList _selectLCType;
        public CityList SelectLCType
        {
            get
            {
                return _selectLCType;
            }
            set
            {
                _selectLCType = value;


                _tEReportobj.CityCode = _selectLCType.CityCode;
                if (string.IsNullOrEmpty(_selectLCType.Latitude))
                { _tEReportobj.Latitude = "0.0"; }
                else
                { _tEReportobj.Latitude = _selectLCType.Latitude; }
                if (string.IsNullOrEmpty(_selectLCType.Latitude))
                { _tEReportobj.Longitude = "0.0"; }
                else
                { _tEReportobj.Longitude = _selectLCType.Longitude; }

                if (_selectLCType != null)
                {
                    if (App.IsArabic)
                    {
                        TxtReportDetailCity = _selectLCType.CityNameAR;
                    }
                    else
                    {
                        TxtReportDetailCity = _selectLCType.CityNameEN;
                    }
                }




                //ListFormBudles = null;
                RaisePropertyChanged("SelectLCType");
            }
        }









        private List<RegionList> _rList;
        public List<RegionList> RList
        {
            get
            {
                return _rList;
            }
            set
            {
                _rList = value;
                RaisePropertyChanged("RList");
            }
        }

        private List<CityList> _cityList;
        public List<CityList> CList
        {
            get
            {
                return _cityList;
            }
            set
            {
                _cityList = value;

                RaisePropertyChanged("CList");
            }

        }






        //private FormBundleApplicationNumberModelResult _selectedFormBindleFbnum;
        //public FormBundleApplicationNumberModelResult SelectedFormBindleFbnum
        //{
        //    get
        //    {
        //        return _selectedFormBindleFbnum;
        //    }
        //    set
        //    {
        //        _selectedFormBindleFbnum = value;
        //        if (_selectedFormBindleFbnum != null)
        //        {
        //            Fbnumdetail = _selectedFormBindleFbnum.Fbsta;
        //            List<FormBundleApplicationNumberModelResult> Formbundle = FormBundleApplicatioNumberList.Where(a => a.Fbnum == _selectedFormBindleFbnum.Fbnum).ToList();
        //            List<FbnumDetailList> Child = new List<FbnumDetailList>();
        //            foreach (FormBundleApplicationNumberModelResult itemF in Formbundle)
        //            {
        //                FbnumDetailList Item = new FbnumDetailList();
        //                Item.Fbnum = itemF.Fbnum;
        //                Item.Fbsta = itemF.Fbsta;
        //                Item.FbDesc = itemF.Txt50;
        //                Item.FbStatus = itemF.Fbstatus;
        //                Child.Add(Item);
        //            }
        //            ListFormBudles = Child;

        //        }
        //        RaisePropertyChanged("SelectedFormBindleFbnum");
        //    }
        //}
        //    {
        //        return _regionList;
        //    }
        //    set
        //    {
        //        _regionList = value;
        //        RaisePropertyChanged("RegionList");
        //    }
        //}



        public TaxEvasionReportFormPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            SubmitReportClicked = new Xamarin.Forms.Command(() =>
            {


                /*if(regionlist != null && regionlist.RegionList.Count != 0)*/
                //{ RList = regionlist.RegionList; }


                //TEReportResponsePostRootObject
                //_navigationService.NavigateTo(App.TaxEvasionReportFormPageView);

            });




        }
        public void onPageLoad()
        {

            //   SelectedTaxEvasionCompanyType = ListFacilityCompanyType.Where(x => x.Id == "1").FirstOrDefault();
            //_tEReportobj.ViolationType = _selectedCategory;
            //_tEReportobj.Channel = "2";
            //_tEReportobj.WSPassword = "gazt@123";
            //_tEReportobj.WSUserName = "GAZT@CRM";
            //_facilityCompanyType.Name=AppResources


            try
            {
                TERFRegionRootObject regionlist = new TERFRegionRootObject();
                ////string lang = UtilityManager.GetLanguageParameter();
                regionlist = WebServiceManager.GAZTTESFormGetRegion();
                ////PopToRootPage();
                ///FormBundleList = formbundleList.d.results;
                if (regionlist != null && regionlist.RegionList.Count != 0)
                { RList = regionlist.RegionList; }

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }

        }

        public void onSelectedTaxEvasionRegion()
        {
            try
            {
                TERFCityRetrieveRootObject citylist = new TERFCityRetrieveRootObject();
                citylist = WebServiceManager.GAZTTESFormGetCity(SelectedTaxEvasionRegion.RegionCode);
                PopToRootPage();
                CList = citylist.CityList;
                // FormBundleApplicatioNumberList = formbundleApplicationNumberList.d.results;
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }

        }
        public async Task SubmitCreatedReport()
        {
            try
            {
                var date = DatePick.Date;
                _tEReportobj.ReceivedDate = date.ToString("dd/MM/yyyy");
                _tEReportobj.CompanyType = SelectedTaxEvasionCompanyType.Id;
                _tEReportobj.ViolationType = _selectedCategory;
                _tEReportobj.Channel = "2";
                _tEReportobj.WSPassword = "gazt@123";
                _tEReportobj.WSUserName = "GAZT@CRM";
                _tEReportobj.TaxType = "1";



                TEReportResponsePostRootObject response = new TEReportResponsePostRootObject();
                response = await WebServiceManager.GAZTTESReportSubmit(_tEReportobj);
                if (response != null && response.Success == true)
                { //ZTEReportReportSuccessResponsep1
                    var resmessage = AppResources.ZTEReportReportSuccessResponsep1;
                    var newrm = resmessage.Replace("Report Number", response.TaxEvasionNumber);
                    _tEReportobj = null;

                    _dialogService.ShowMessage(newrm, AppResources.Submitted);

                    var _navigation = Application.Current.MainPage.Navigation;
                    var _lastPage = _navigation.NavigationStack.LastOrDefault();
                    //Remove last page
                    _navigation.RemovePage(_lastPage);
                    //Go back 
                    _navigation.PopAsync();


                    //_navigationService.NavigateTo(App.TaxEvasionReportListPageView);

                }
                else
                {//ZTEReportReportSuccessResponsep2
                    _dialogService.ShowMessage(AppResources.ZTEReportReportSuccessResponsep2, " ");
                }

            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
            }

        }
        public void CreateCompanyTypeList()
        {

            try
            {
                ListFacilityCompanyType = new List<FacilityCompanyType>();
                DListFacilityCompanyType = new List<FacilityCompanyType>();
                ListFacilityCompanyType.Clear();
                DListFacilityCompanyType.Clear();

                //  FacilityCompanyType cct = new FacilityCompanyType();

                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "1", Name = AppResources.ZTERReportDetailCompanyType1 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "2", Name = AppResources.ZTERReportDetailCompanyType2 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "3", Name = AppResources.ZTERReportDetailCompanyType3 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "4", Name = AppResources.ZTERReportDetailCompanyType4 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "5", Name = AppResources.ZTERReportDetailCompanyType5 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "6", Name = AppResources.ZTERReportDetailCompanyType6 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "7", Name = AppResources.ZTERReportDetailCompanyType7 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "8", Name = AppResources.ZTERReportDetailCompanyType8 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "9", Name = AppResources.ZTERReportDetailCompanyType9 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "10", Name = AppResources.ZTERReportDetailCompanyType10 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "11", Name = AppResources.ZTERReportDetailCompanyType11 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "12", Name = AppResources.ZTERReportDetailCompanyType12 });

                DListFacilityCompanyType = ListFacilityCompanyType;
            }
            catch (Exception ex)
            {

            }
        }




        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }


    }
}
