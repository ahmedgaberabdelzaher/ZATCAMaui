using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class CorrespondancePageViewModel : ViewModelBase
    {
        #region Properties
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand onZakatLabelClicked { get; set; }
        public ICommand onVATLabelClicked { get; set; }
        public ICommand onETLabelClicked { get; set; }
        public ICommand onCITLabelClicked { get; set; }
        private List<CorrespondanceModel> _listVATCorrespondance = null;
        public List<CorrespondanceModel> ListVATCorrespondance
        {
            get
            {
                return _listVATCorrespondance;
            }
            set
            {
                _listVATCorrespondance = value;
                RaisePropertyChanged("ListVATCorrespondance");
            }
        }

        private List<CorrespondanceModel> _listZAKATCorrespondance = null;
        public List<CorrespondanceModel> ListZAKATCorrespondance
        {
            get
            {
                return _listZAKATCorrespondance;
            }
            set
            {
                _listZAKATCorrespondance = value;
                RaisePropertyChanged("ListZAKATCorrespondance");
            }
        }

        private List<CorrespondanceModel> _listETCorrespondance = null;
        public List<CorrespondanceModel> ListETCorrespondance
        {
            get
            {
                return _listETCorrespondance;
            }
            set
            {
                _listETCorrespondance = value;
                RaisePropertyChanged("ListETCorrespondance");
            }
        }

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

        private bool _isZakatVisible = true;
        public bool IsZakatVisible
        {
            get
            {
                return _isZakatVisible;
            }
            set
            {
                _isZakatVisible = value;
                RaisePropertyChanged("IsZakatVisible");
            }
        }

        private bool _isVATVisible = false;
        public bool IsVATVisible
        {
            get
            {
                return _isVATVisible;
            }
            set
            {
                _isVATVisible = value;
                RaisePropertyChanged("IsVATVisible");
            }
        }

        private bool _isETVisible = false;
        public bool IsETVisible
        {
            get
            {
                return _isETVisible;
            }
            set
            {
                _isETVisible = value;
                RaisePropertyChanged("IsETVisible");
            }
        }

        private string _zakatCountDisplay = string.Empty;
        public string ZakatCountDisplay
        {
            get
            {
                return _zakatCountDisplay;
            }
            set
            {
                _zakatCountDisplay = value;
                RaisePropertyChanged("ZakatCountDisplay");
            }
        }

        private string _vATCountDisplay = string.Empty;
        public string VATCountDisplay
        {
            get
            {
                return _vATCountDisplay;
            }
            set
            {
                _vATCountDisplay = value;
                RaisePropertyChanged("VATCountDisplay");
            }
        }

        private string _eTCountDisplay = string.Empty;
        public string ETCountDisplay
        {
            get
            {
                return _eTCountDisplay;
            }
            set
            {
                _eTCountDisplay = value;
                RaisePropertyChanged("ETCountDisplay");
            }
        }

        private List<CorrespondenceFiltersModel> _corresFilter = null;

        public List<CorrespondenceFiltersModel> CorresFilter
        {
            get
            {
                return _corresFilter;
            }
            set
            {
                _corresFilter = value;
                RaisePropertyChanged("CorresFilter");
            }
        }

        private CorrespondenceFiltersModel _selectedFilter = null;
        public CorrespondenceFiltersModel SelectedFilter
        {
            get
            {
                return _selectedFilter;
            }
            set
            {
                _selectedFilter = value;
                if (_selectedFilter != null)
                {
                    if (_selectedFilter.ID == 1)
                    {
                        if (IsZakatVisible == true)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = ListZAKATCorrespondance;
                            ListZAKATCorrespondance = null;
                            var SortedList = CorreTosort.OrderBy(x => x.StartDate);


                            ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }

                        if (IsVATVisible == true)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = ListVATCorrespondance;
                            ListVATCorrespondance = null;
                            var SortedList = CorreTosort.OrderBy(x => x.StartDate);


                            ListVATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                        if (IsETVisible == true)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = ListETCorrespondance;
                            ListETCorrespondance = null;
                            var SortedList = CorreTosort.OrderBy(x => x.StartDate);


                            ListETCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                    }
                    if (_selectedFilter.ID == 2)
                    {
                        if (IsZakatVisible == true)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = ListZAKATCorrespondance;
                            ListZAKATCorrespondance = null;
                            var SortedList = CorreTosort.OrderByDescending(x => x.StartDate);


                            ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }

                        if (IsVATVisible == true)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = ListVATCorrespondance;
                            ListVATCorrespondance = null;
                            var SortedList = CorreTosort.OrderByDescending(x => x.StartDate);


                            ListVATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                        if (IsETVisible == true)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = ListETCorrespondance;
                            ListETCorrespondance = null;
                            var SortedList = CorreTosort.OrderByDescending(x => x.StartDate);


                            ListETCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                    }
                    if (_selectedFilter.ID == 3)
                    {
                        if (IsZakatVisible == true)
                        {
                            var SortedList = from item in ListZAKATCorrespondance
                                             orderby item.IsFav ascending
                                             select item;


                            ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }

                        if (IsVATVisible == true)
                        {
                            var SortedList = from item in ListVATCorrespondance
                                             orderby item.IsFav ascending
                                             select item;


                            ListVATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                        if (IsETVisible == true)
                        {
                            var SortedList = from item in ListETCorrespondance
                                             orderby item.IsFav ascending
                                             select item;


                            ListETCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                    }
                    if (_selectedFilter.ID == 4)
                    {
                        if (IsZakatVisible == true)
                        {
                            var SortedList = from item in ListZAKATCorrespondance
                                             orderby item.IsFav descending
                                             select item;


                            ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }

                        if (IsVATVisible == true)
                        {
                            var SortedList = from item in ListVATCorrespondance
                                             orderby item.IsFav descending
                                             select item;


                            ListVATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                        if (IsETVisible == true)
                        {
                            var SortedList = from item in ListETCorrespondance
                                             orderby item.IsFav descending
                                             select item;


                            ListETCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                    }
                }
                RaisePropertyChanged("SelectedFilter");
            }
        }

        private int? _setSelectedIndex = 0;
        public int? SetSelectedIndex
        {
            get
            {
                return _setSelectedIndex;
            }
            set
            {
                _setSelectedIndex = value;
                RaisePropertyChanged("SetSelectedIndex");
            }
        }
        #endregion

        #region Constructor
        public CorrespondancePageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            onZakatLabelClicked = new Xamarin.Forms.Command(async () =>
            {
                IsZakatVisible = true;
                IsVATVisible = false;
                IsETVisible = false;
               
                List<CorrespondenceFiltersModel> Filters = new List<CorrespondenceFiltersModel>();

                Filters.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
                Filters.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });
                Filters.Add(new CorrespondenceFiltersModel { ID = 3, Filter = AppResources.ZZFavoriteAscending });
                Filters.Add(new CorrespondenceFiltersModel { ID = 4, Filter = AppResources.ZZFavoriteDescending });

                CorresFilter = Filters;
                SetSelectedIndex = 4;
            });
            onVATLabelClicked = new Xamarin.Forms.Command(async () =>
            {
                IsZakatVisible = false;
                IsVATVisible = true;
                IsETVisible = false;

                List<CorrespondenceFiltersModel> Filters = new List<CorrespondenceFiltersModel>();
                Filters.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
                Filters.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });
               
                CorresFilter = Filters;
                SetSelectedIndex = 2;
            });
            onETLabelClicked = new Xamarin.Forms.Command(async () =>
            {
                IsZakatVisible = false;
                IsVATVisible = false;
                IsETVisible = true;
               
                List<CorrespondenceFiltersModel> Filters = new List<CorrespondenceFiltersModel>();
                Filters.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
                Filters.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });
               

                CorresFilter = Filters;
                SetSelectedIndex = 2;
            });

           
        }
        #endregion

        #region Methods

        public void ShowCorrespondenceDetails(CorrespondanceModel CorresModel)
        {
            _navigationService.NavigateTo(App.CorrespondenceDetailsPageView, CorresModel);
        }

        public void ShowVATPDF(CorrespondanceModel CorrespondenceD)
        {
            string Url = Constants.GAZTGetCorrespondenceAttach + "'" + CorrespondenceD.Cokey + "',Cotyp='" + CorrespondenceD.Cotype + "')/$value";
            ShowPdf(Url);
        }

        public void ShowETPDF(CorrespondanceModel CorrespondenceD)
        {
            string Url = Constants.GAZTGetCorrespondenceAttach + "'" + CorrespondenceD.Cokey + "',Cotyp='" + CorrespondenceD.Cotype + "')/$value";
            ShowPdf(Url);
        }

        public async void ShowPdf(string pdfUrl)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (pdfUrl != null)
                {
                    //Uri uri = new Uri(pdfUrl);
                    //Device.OpenUri(uri);
                    _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
                }
                else
                {
                    //pop that certificate is not available
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    });
                }
            }
            else
            {
                if (pdfUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    //pop that certificate is not available
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    });
                }
            }
        }
        public void onPageLoad()
        {
            try
            {
                CorresFilter = null;
                SelectedFilter = null;
                ListZAKATCorrespondance = null;
                ListVATCorrespondance = null;
                ListETCorrespondance = null;
                List<CorrespondenceFiltersModel> Filters = new List<CorrespondenceFiltersModel>();

                Filters.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
                Filters.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });
                Filters.Add(new CorrespondenceFiltersModel { ID = 3, Filter = AppResources.ZZFavoriteAscending });
                Filters.Add(new CorrespondenceFiltersModel { ID = 4, Filter = AppResources.ZZFavoriteDescending });
                CorresFilter = Filters;

                CorrespondenceRootObject ZakatCorres = new CorrespondenceRootObject();

                ZakatCorres = WebServiceManager.GAZTGetZakatCorrespondece();
                PopToRootPage();
                List<CorrespondanceModel> ZakatCo = new List<CorrespondanceModel>();
                ZakatCountDisplay = AppResources.ZZZAKAT + "(" + ZakatCorres.d.results.Count + ")";
                foreach (CorrespondenceResult itemZakat in ZakatCorres.d.results)
                {
                    CorrespondanceModel childZakat = new CorrespondanceModel();
                    childZakat.Title = itemZakat.Descript;
                    childZakat.RefNumber = itemZakat.LetterNum;
                    childZakat.Cokey = itemZakat.Cokey;
                    childZakat.Txtco = itemZakat.Cdate;
                    childZakat.StartDate = itemZakat.Coidt;
                    childZakat.Cotype = itemZakat.Cotyp;
                    childZakat.Vkont = itemZakat.Vkont;
                    childZakat.Gpart = itemZakat.Gpart;
                    childZakat.Begdaz = itemZakat.Begdaz;
                    childZakat.Enddaz = itemZakat.Enddaz;
                    DateTime? BegDate = itemZakat.Coidt;

                    if (itemZakat.Zzfav == "1")
                    {
                        childZakat.IsFav = true;
                        childZakat.FavImg = "ic_star.png";
                    }
                    else
                    {
                        childZakat.IsFav = false;
                        childZakat.FavImg = "ic_star_border.png";
                    }
                    string StartDate = string.Empty;

                    if (App.IsArabic)
                    {
                        if (BegDate != null)
                        {

                            StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                            StartDate = UtilityManager.ToArabicDate(StartDate);
                        }

                    }
                    else
                    {
                        if (BegDate != null)
                        {
                            StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                        }

                    }
                    childZakat.DateAndTime = StartDate;
                    ZakatCo.Add(childZakat);
                }



                ListZAKATCorrespondance = ZakatCo;


                CorrespondenceRootObject VATCorres = new CorrespondenceRootObject();

                VATCorres = WebServiceManager.GAZTGetVATCorrespondece();
                PopToRootPage();
                List<CorrespondanceModel> VATCo = new List<CorrespondanceModel>();
                VATCountDisplay = AppResources.ZZVAT + "(" + VATCorres.d.results.Count + ")";
                foreach (CorrespondenceResult itemVAT in VATCorres.d.results)
                {
                    CorrespondanceModel childVAT = new CorrespondanceModel();
                    childVAT.Title = itemVAT.Descript;
                    childVAT.RefNumber = itemVAT.LetterNum;
                    childVAT.Cokey = itemVAT.Cokey;
                    childVAT.Txtco = itemVAT.Copri;
                    childVAT.StartDate = itemVAT.Coidt;
                    childVAT.Cotype = itemVAT.Cotyp;
                    childVAT.Vkont = itemVAT.Vkont;
                    childVAT.Gpart = itemVAT.Gpart;
                    childVAT.Begdaz = itemVAT.Begdaz;
                    childVAT.Enddaz = itemVAT.Enddaz;
                    DateTime? BegDate = itemVAT.Coidt;

                    if (itemVAT.Zzfav == "1")
                    {
                        childVAT.IsFav = true;
                        childVAT.FavImg = "ic_star.png";
                    }
                    else
                    {
                        childVAT.IsFav = false;
                        childVAT.FavImg = "ic_star_border.png";
                    }
                    string StartDate = string.Empty;

                    if (App.IsArabic)
                    {
                        if (BegDate != null)
                        {

                            StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                            StartDate = UtilityManager.ToArabicDate(StartDate);
                        }

                    }
                    else
                    {
                        if (BegDate != null)
                        {
                            StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));



                        }

                    }
                    childVAT.DateAndTime = StartDate;
                    VATCo.Add(childVAT);
                }



                ListVATCorrespondance = VATCo;

                CorrespondenceRootObject ETCorres = new CorrespondenceRootObject();

                ETCorres = WebServiceManager.GAZTGetETCorrespondece();
                PopToRootPage();
                List<CorrespondanceModel> ETCo = new List<CorrespondanceModel>();
                ETCountDisplay = AppResources.ZZET + "(" + ETCorres.d.results.Count + ")";
                foreach (CorrespondenceResult itemET in ETCorres.d.results)
                {
                    CorrespondanceModel childET = new CorrespondanceModel();
                    childET.Title = itemET.Descript;
                    childET.RefNumber = itemET.LetterNum;
                    childET.Cokey = itemET.Cokey;
                    childET.Txtco = itemET.Copri;
                    childET.StartDate = itemET.Coidt;
                    childET.Cotype = itemET.Cotyp;
                    childET.Vkont = itemET.Vkont;
                    childET.Gpart = itemET.Gpart;
                    childET.Begdaz = itemET.Begdaz;
                    childET.Enddaz = itemET.Enddaz;
                    DateTime? BegDate = itemET.Coidt;

                    if (itemET.Zzfav == "1")
                    {
                        childET.IsFav = true;
                        childET.FavImg = "ic_star.png";
                    }
                    else
                    {
                        childET.IsFav = false;
                        childET.FavImg = "ic_star_border.png";
                    }
                    string StartDate = string.Empty;

                    if (App.IsArabic)
                    {
                        if (BegDate != null)
                        {

                            StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                            StartDate = UtilityManager.ToArabicDate(StartDate);
                        }

                    }
                    else
                    {
                        if (BegDate != null)
                        {
                            StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));



                        }

                    }
                    childET.DateAndTime = StartDate;
                    ETCo.Add(childET);
                }



                ListETCorrespondance = ETCo;
                //IsZakatVisible = true;
                //IsVATVisible = false;
                //IsETVisible = false;
                SetSelectedIndex = 4;
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
        #endregion
    }
}
