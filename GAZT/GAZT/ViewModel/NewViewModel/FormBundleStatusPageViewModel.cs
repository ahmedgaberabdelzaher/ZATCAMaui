using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class FormBundleStatusPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private List<FormBundleResult> _formBundleList;

        public FormBundleStatusPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
        public List<FormBundleResult> FormBundleList
        {
            get
            {
                return _formBundleList;
            }
            set
            {
                _formBundleList = value;
                RaisePropertyChanged("FormBundleList");
            }
        }

        private bool _isCPickerEnable = false;
        public bool IsCPickerEnable
        {
            get
            {
                return _isCPickerEnable;
            }
            set
            {
                _isCPickerEnable = value;
                RaisePropertyChanged("IsCPickerEnable");
            }
        }




        private FormBundleApplicationNumberModelResult _selectedFormBindleFbnum;
        public FormBundleApplicationNumberModelResult SelectedFormBindleFbnum
        {
            get
            {
                return _selectedFormBindleFbnum;
            }
            set
            {
                _selectedFormBindleFbnum = value;
                if (_selectedFormBindleFbnum != null)
                {
                    Fbnumdetail = _selectedFormBindleFbnum.Fbsta;
                    List<FormBundleApplicationNumberModelResult> Formbundle = FormBundleApplicatioNumberList.Where(a => a.Fbnum == _selectedFormBindleFbnum.Fbnum).ToList();
                    List<FbnumDetailList> Child = new List<FbnumDetailList>();
                    foreach (FormBundleApplicationNumberModelResult itemF in Formbundle)
                    {
                        FbnumDetailList Item = new FbnumDetailList();
                        Item.Fbnum = itemF.Fbnum;
                        Item.Fbsta = itemF.Fbsta;
                        Item.FbDesc = itemF.Txt50;
                        Item.FbStatus = itemF.Fbstatus;
                        Child.Add(Item);
                    }
                    ListFormBudles = Child;

                }
                RaisePropertyChanged("SelectedFormBindleFbnum");
            }
        }



        private string _fbnumdetail;
        public string Fbnumdetail
        {
            get
            {
                return _fbnumdetail;
            }
            set
            {
                _fbnumdetail = value;

                RaisePropertyChanged("Fbnumdetail");
            }
        }

        private List<FormBundleApplicationNumberModelResult> _formBundleApplicationNumberList;
        public List<FormBundleApplicationNumberModelResult> FormBundleApplicatioNumberList
        {
            get
            {
                return _formBundleApplicationNumberList;
            }
            set
            {
                _formBundleApplicationNumberList = value;
                RaisePropertyChanged("FormBundleApplicatioNumberList");
            }
        }

        private FormBundleResult _selectedFormBindleFbtyp;
        public FormBundleResult SelectedFormBindleFbtyp
        {
            get
            {
                return _selectedFormBindleFbtyp;
            }
            set
            {
                _selectedFormBindleFbtyp = value;
                if (_selectedFormBindleFbtyp != null)
                {
                    IsCPickerEnable = true;
                    onSelectedFormBindleFbtyp();
                }
                ListFormBudles = null;
                RaisePropertyChanged("SelectedFormBindleFbtyp");
            }
        }



        private List<FbnumDetailList> _fbnumDetailList;
        public List<FbnumDetailList> FbnumDetailList
        {
            get
            {
                return _fbnumDetailList;
            }
            set
            {
                _fbnumDetailList = value;
                RaisePropertyChanged("FbnumDetailList");
            }
        }



        private List<FbnumDetailList> _listFormBudles = null;

        public List<FbnumDetailList> ListFormBudles
        {
            get
            {
                return _listFormBudles;
            }
            set
            {
                _listFormBudles = value;
                RaisePropertyChanged("ListFormBudles");
            }
        }




        public void onPageLoad()
        {
            try
            {
                FormBundleModel formbundleList = new FormBundleModel();
                string lang = UtilityManager.GetLanguageParameter();
                formbundleList = WebServiceManager.GAZTGetFormBundleModel();
                PopToRootPage();
                FormBundleList = formbundleList.d.results;
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }

        }
    
        public void onSelectedFormBindleFbtyp()
        {
            try{
                FormBundleApplicationNumberModel formbundleApplicationNumberList = new FormBundleApplicationNumberModel();
                formbundleApplicationNumberList = WebServiceManager.GAZTGetFormBundleApplicationNumberModel(SelectedFormBindleFbtyp.Fbtyp);
                PopToRootPage();
                  FormBundleApplicatioNumberList = formbundleApplicationNumberList.d.results;
            }
            catch(InternetException ex)
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
