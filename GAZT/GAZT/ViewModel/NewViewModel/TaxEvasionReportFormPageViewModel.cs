using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class TaxEvasionReportFormPageViewModel : ViewModelBase
    {
        #region variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #endregion //CreateReportPost

        private CreateReportPost _createReportPostobj=null;
        public CreateReportPost CreateReportPostobj
        {
            get
            {
                return _createReportPostobj;
            }
            set
            {
                _createReportPostobj = value;
                if (_createReportPostobj != null)
                {
                    //IsCPickerEnable = true;
                    //onSelectedTaxEvasionRegion();
                }
                //ListFormBudles = null;
                RaisePropertyChanged("CreateReportPostobj");
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
                    //IsCPickerEnable = true;
                    string v= SelectedTaxEvasionRegion.RegionCode;
                    onSelectedTaxEvasionRegion();
                    _createReportPostobj.RegionCode = v;
                    //CreateReportPostobj.RegionCode = v;
                }
                //ListFormBudles = null;
                RaisePropertyChanged("SelectedTaxEvasionRegion");
            }
        }
        private CityList _selectedTaxEvasionCity;
        public CityList SelectedTaxEvasionCity
        {
            get
            {
                return _selectedTaxEvasionCity;
            }
            set
            {
                _selectedTaxEvasionCity = value;
                if (_selectedTaxEvasionCity != null)
                {
                    //IsCPickerEnable = true;
                    //onSelectedTaxEvasionRegion();
                    CreateReportPostobj.CityCode = SelectedTaxEvasionCity.CityCode;
                    CreateReportPostobj.Latitude = SelectedTaxEvasionCity.Latitude;
                    CreateReportPostobj.Longitude = SelectedTaxEvasionCity.Longitude;
                }
                //ListFormBudles = null;
                RaisePropertyChanged("SelectedTaxEvasionRegion");
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
        }
        public void onPageLoad()
        {
            try
            {
                TERFRegionRootObject regionlist = new TERFRegionRootObject();
                ////string lang = UtilityManager.GetLanguageParameter();
                regionlist = WebServiceManager.GAZTTESFormGetRegion();
                ////PopToRootPage();
                ///FormBundleList = formbundleList.d.results;

              RList = regionlist.RegionList;
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
