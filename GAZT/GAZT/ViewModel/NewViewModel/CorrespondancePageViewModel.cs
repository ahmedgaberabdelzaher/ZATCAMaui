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
using System.Threading.Tasks;
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
        public ICommand OnHomeButtonClicked { get; set; }
        private List<CorrespondanceModel> _listVATCorrespondance = null;
        CorrespondenceRootObject ZakatCorres = new CorrespondenceRootObject();
        CorrespondenceRootObject VATCorres = new CorrespondenceRootObject();
        CorrespondenceRootObject ETCorres = new CorrespondenceRootObject();
        private string _txtSelectedStatusZakat = string.Empty;
        public string TxtSelectedStatusZakat
        {
            get
            {
                return _txtSelectedStatusZakat;
            }
            set
            {
                _txtSelectedStatusZakat = value;
                RaisePropertyChanged("TxtSelectedStatusZakat");
            }
        }

        private string _txtSelectedStatusVAT = string.Empty;
        public string TxtSelectedStatusVAT
        {
            get
            {
                return _txtSelectedStatusVAT;
            }
            set
            {
                _txtSelectedStatusVAT = value;
                RaisePropertyChanged("TxtSelectedStatusVAT");
            }
        }

        private string _txtSelectedStatusET = string.Empty;
        public string TxtSelectedStatusET
        {
            get
            {
                return _txtSelectedStatusET;
            }
            set
            {
                _txtSelectedStatusET = value;
                RaisePropertyChanged("TxtSelectedStatusET");
            }
        }
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


        private bool _setNoDataLabelVisibility = false;

        public bool SetNoDataLabelVisibility
        {
            get
            {
                return _setNoDataLabelVisibility;
            }
            set
            {
                _setNoDataLabelVisibility = value;
                RaisePropertyChanged("SetNoDataLabelVisibility");
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

     

        private List<CorrespondenceFiltersModel> _corresFilterZakat;

        public List<CorrespondenceFiltersModel> CorresFilterZakat
        {
            get
            {
                return _corresFilterZakat;
            }
            set
            {
                _corresFilterZakat = value;
                RaisePropertyChanged("CorresFilterZakat");
            }
        }
        private List<CorrespondenceFiltersModel> _corresFilterVAT;

        public List<CorrespondenceFiltersModel> CorresFilterVAT
        {
            get
            {
                return _corresFilterVAT;
            }
            set
            {
                _corresFilterVAT = value;
                RaisePropertyChanged("CorresFilterVAT");
            }
        }
        private List<CorrespondenceFiltersModel> _corresFilterET;

        public List<CorrespondenceFiltersModel> CorresFilterET
        {
            get
            {
                return _corresFilterET;
            }
            set
            {
                _corresFilterET = value;
                RaisePropertyChanged("CorresFilterET");
            }
        }

        private CorrespondenceFiltersModel _selectedFilterZakat = null;
        public CorrespondenceFiltersModel SelectedFilterZakat
        {
            get
            {
                return _selectedFilterZakat;
            }
            set
            {
                _selectedFilterZakat = value;
                if (_selectedFilterZakat != null)
                {
                    if (_selectedFilterZakat.ID == 1)
                    {
                      
                            if (ListZAKATCorrespondance != null)
                            {
                                List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                                CorreTosort = ListZAKATCorrespondance;
                                ListZAKATCorrespondance = null;
                                var SortedList = CorreTosort.OrderBy(x => x.StartDate);


                                ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            
                        }

                    
                    }
                    if (_selectedFilterZakat.ID == 2)
                    {
                       
                            if (ListZAKATCorrespondance != null)
                            {
                                List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                                CorreTosort = ListZAKATCorrespondance;
                                ListZAKATCorrespondance = null;
                                if (CorreTosort != null)
                                {
                                    var SortedList = CorreTosort.OrderByDescending(x => x.StartDate);
                                    ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                                }
                            }

                        

                       
                    }
                    if (_selectedFilterZakat.ID == 3)
                    {
                     
                            if (ListZAKATCorrespondance != null)
                            {
                                var SortedList = from item in ListZAKATCorrespondance
                                                 orderby item.IsFav ascending
                                                 select item;


                                ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                        

                      
                    }
                    if (_selectedFilterZakat.ID == 4)
                    {
                       
                            if (ListZAKATCorrespondance != null)
                            {
                                var SortedList = from item in ListZAKATCorrespondance
                                                 orderby item.IsFav descending
                                                 select item;


                                ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                        
                    }
                    TxtSelectedStatusZakat = _selectedFilterZakat.Filter;
                }
               
                RaisePropertyChanged("SelectedFilterZakat");
            }
        }

        private CorrespondenceFiltersModel _selectedFilterVAT = null;
        public CorrespondenceFiltersModel SelectedFilterVAT
        {
            get
            {
                return _selectedFilterVAT;
            }
            set
            {
                _selectedFilterVAT = value;
                if (_selectedFilterVAT != null)
                {
                    if (_selectedFilterVAT.ID == 1)
                    {
                       

                        if (ListVATCorrespondance != null)
                            {
                                List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                                CorreTosort = ListVATCorrespondance;
                                ListVATCorrespondance = null;
                                var SortedList = CorreTosort.OrderBy(x => x.StartDate);


                                ListVATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                     
                    }
                    if (_selectedFilterVAT.ID == 2)
                    {
                        
                           

                      
                            if (ListVATCorrespondance != null)
                            {
                                List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                                CorreTosort = ListVATCorrespondance;
                                ListVATCorrespondance = null;
                                if (CorreTosort != null)
                                {
                                    var SortedList = CorreTosort.OrderByDescending(x => x.StartDate);
                                    ListVATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                                }
                            }
                       
                    }
                    if (_selectedFilterVAT.ID == 3)
                    {
                      
                            if (ListVATCorrespondance != null)
                            {
                                var SortedList = from item in ListVATCorrespondance
                                                 orderby item.IsFav ascending
                                                 select item;


                                ListVATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                    
                    }
                    if (_selectedFilterVAT.ID == 4)
                    {
                            if (ListVATCorrespondance != null)
                            {
                                var SortedList = from item in ListVATCorrespondance
                                                 orderby item.IsFav descending
                                                 select item;


                                ListVATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                       
                    }
                    TxtSelectedStatusVAT = _selectedFilterVAT.Filter;
                }

                RaisePropertyChanged("SelectedFilterVAT");
            }
        }

        private CorrespondenceFiltersModel _selectedFilterET = null;
        public CorrespondenceFiltersModel SelectedFilterET
        {
            get
            {
                return _selectedFilterET;
            }
            set
            {
                _selectedFilterET = value;
                if (_selectedFilterET != null)
                {
                    if (_selectedFilterET.ID == 1)
                    {
                      
                            if (ListETCorrespondance != null)
                            {
                                List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                                CorreTosort = ListETCorrespondance;
                                ListETCorrespondance = null;
                                var SortedList = CorreTosort.OrderBy(x => x.StartDate);
                               
                                ListETCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                        
                    }
                    if (_selectedFilterET.ID == 2)
                    {
                        
                            if (ListETCorrespondance != null)
                            {
                                List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                                CorreTosort = ListETCorrespondance;
                                ListETCorrespondance = null;

                                if (CorreTosort != null)
                                {
                                    var SortedList = CorreTosort.OrderByDescending(x => x.StartDate);
                                    ListETCorrespondance = SortedList.ToList<CorrespondanceModel>();
                                }
                            }
                        
                    }
                    if (_selectedFilterET.ID == 3)
                    {
                      
                            if (ListETCorrespondance != null)
                            {
                                var SortedList = from item in ListETCorrespondance
                                                 orderby item.IsFav ascending
                                                 select item;


                                ListETCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                        
                    }
                    if (_selectedFilterET.ID == 4)
                    {
                        
                            if (ListETCorrespondance != null)
                            {
                                var SortedList = from item in ListETCorrespondance
                                                 orderby item.IsFav descending
                                                 select item;


                                ListETCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                        
                    }
                    TxtSelectedStatusET = _selectedFilterET.Filter;
                }

                RaisePropertyChanged("SelectedFilterET");
            }
        }


        private int? _setSelectedIndexZakat = 0;
        public int? SetSelectedIndexZakat
        {
            get
            {
                return _setSelectedIndexZakat;
            }
            set
            {
                _setSelectedIndexZakat = value;
                RaisePropertyChanged("SetSelectedIndexZakat");
            }
        }

        private int? _setSelectedIndexVAT = 0;
        public int? SetSelectedIndexVAT
        {
            get
            {
                return _setSelectedIndexVAT;
            }
            set
            {
                _setSelectedIndexVAT = value;
                RaisePropertyChanged("SetSelectedIndexVAT");
            }
        }

        private int? _setSelectedIndexET = 0;
        public int? SetSelectedIndexET
        {
            get
            {
                return _setSelectedIndexET;
            }
            set
            {
                _setSelectedIndexET = value;
                RaisePropertyChanged("SetSelectedIndexET");
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
            onZakatLabelClicked = new Xamarin.Forms.Command( () =>
            {
                
                
            });
            onVATLabelClicked = new Xamarin.Forms.Command( () =>
            {
                try
                {
                  

                    List<CorrespondenceFiltersModel> Filters = new List<CorrespondenceFiltersModel>();
                    Filters.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
                    Filters.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });

                  
                }
                catch (Exception ex)
                {

                }
               
            });

            onETLabelClicked = new Xamarin.Forms.Command( () =>
            {
            });
            OnHomeButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
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
            string Url = Constants.GAZTGetCorrespondenceAttach + "'" + CorrespondenceD.Cokey + "',Cotyp='" + CorrespondenceD.Cotype + "')/$value?saml2=disabled";
            ShowPdf(Url);
        }

        public void ShowETPDF(CorrespondanceModel CorrespondenceD)
        {
            string Url = Constants.GAZTGetCorrespondenceAttach + "'" + CorrespondenceD.Cokey + "',Cotyp='" + CorrespondenceD.Cotype + "')/$value?saml2=disabled";
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
        public async Task onPageLoad()
        {
            try
            { await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(() =>
                {
                  
                    SetNoDataLabelVisibility = false;
                    SetStatusPickerItem();
                        ZakatCorres = WebServiceManager.GAZTGetZakatCorrespondece();
                        VATCorres = WebServiceManager.GAZTGetVATCorrespondece();
                        ETCorres = WebServiceManager.GAZTGetETCorrespondece();
                        PopToRootPage();
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

        private void SetStatusPickerItem()
        {
            try
            {
                List<CorrespondenceFiltersModel> FiltersZAKAT = new List<CorrespondenceFiltersModel>();
                List<CorrespondenceFiltersModel> FiltersVATET = new List<CorrespondenceFiltersModel>();

                FiltersVATET.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
                FiltersVATET.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });

                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });
                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 3, Filter = AppResources.ZZFavoriteAscending });
                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 4, Filter = AppResources.ZZFavoriteDescending });

                CorresFilterZakat = FiltersZAKAT;
                CorresFilterVAT = FiltersVATET;
                CorresFilterET = FiltersVATET;
            }
            catch (Exception ex)
            {

            }


        }

        public void SetData()
        {
            try
            {
                List<CorrespondanceModel> ZakatCo = new List<CorrespondanceModel>();

                // Assigning data in the list
                if (ZakatCorres != null && ZakatCorres.d != null)
                {
                    ZakatCountDisplay = AppResources.ZZZAKAT + "(" + ZakatCorres.d.results.Count + ")";
                    foreach (CorrespondenceResult itemZakat in ZakatCorres.d.results)
                    {
                        CorrespondanceModel childZakat = new CorrespondanceModel();
                        childZakat.Title = itemZakat.Descript;
                        childZakat.RefNumber = itemZakat.LetterNum;
                        childZakat.Cokey = itemZakat.Cokey;
                        childZakat.Coitm = itemZakat.Coitm;

                        if (itemZakat.Copri != null)
                        {
                            childZakat.Txtco = JsonConvert.DeserializeObject<DateTime>(@"""" + itemZakat.Copri + @"""");
                        }
                        if (itemZakat.Coidt != null)
                        {
                            childZakat.StartDate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemZakat.Coidt + @"""");
                        }
                        childZakat.Cotype = itemZakat.Cotyp;
                        childZakat.Vkont = itemZakat.Vkont;
                        childZakat.Gpart = itemZakat.Gpart;
                        childZakat.Begdaz = itemZakat.Begdaz;
                        childZakat.Enddaz = itemZakat.Enddaz;
                        
                        DateTime? BegDate = DateTime.Now;
                        if (itemZakat.Coidt != null)
                        {
                            BegDate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemZakat.Coidt + @"""");
                        }

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
                        string time = string.Empty;

                        if (App.IsArabic)
                        {
                            if (BegDate != null)
                            {


                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                //time= Convert.ToDateTime(BegDate).ToString("hh:mm:ss tt", new CultureInfo("en-US"));
                                if (childZakat.Coitm != null)
                                {
                                    time = childZakat.Coitm;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = UtilityManager.ConvertNumerals(result[0]);
                                    string minutes = UtilityManager.ConvertNumerals(result[1]);
                                    string second= UtilityManager.ConvertNumerals(result[2]);
                                    time=  " "+ hours + ":"+ minutes+ " ";
                                    

                                }
                                //16H36M15S

                                var Tstartdate = UtilityManager.ToArabicDate(StartDate);
                                StartDate = String.Concat(Tstartdate, time);
                                //StartDate = StartDate + "  " + time;
                            }

                        }
                        else
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                time = Convert.ToDateTime(BegDate).ToString("hh:mm:ss tt", new CultureInfo("en-US"));
                                if (childZakat.Coitm != null)
                                {
                                    time = childZakat.Coitm;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes+" ";
                                }

                                StartDate = StartDate + "  " + time;















                            }

                        }
                        childZakat.DateAndTime = StartDate;
                        ZakatCo.Add(childZakat);
                    }

                    ListZAKATCorrespondance = ZakatCo;
                }
               
                PopToRootPage();

                // Assigning data in the list
                if (VATCorres != null && VATCorres.d != null)
                {
                    List<CorrespondanceModel> VATCo = new List<CorrespondanceModel>();
                    VATCountDisplay = AppResources.ZZVAT + "(" + VATCorres.d.results.Count + ")";
                    foreach (CorrespondenceResult itemVAT in VATCorres.d.results)
                    {
                        CorrespondanceModel childVAT = new CorrespondanceModel();
                        childVAT.Title = itemVAT.Descript;
                        childVAT.RefNumber = itemVAT.LetterNum;
                        childVAT.Cokey = itemVAT.Cokey;
                        childVAT.Coitm = itemVAT.Coitm;
                        if (itemVAT.Copri != null)
                        {
                            childVAT.Txtco = JsonConvert.DeserializeObject<DateTime>(@"""" + itemVAT.Copri + @"""");
                        }
                        if (itemVAT.Coidt != null)
                        {
                            childVAT.StartDate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemVAT.Coidt + @"""");
                        }
                        childVAT.Cotype = itemVAT.Cotyp;
                        childVAT.Vkont = itemVAT.Vkont;
                        childVAT.Gpart = itemVAT.Gpart;
                        childVAT.Begdaz = itemVAT.Begdaz;
                        childVAT.Enddaz = itemVAT.Enddaz;
                        DateTime? BegDate = DateTime.Now;
                        if (itemVAT.Coidt != null)
                        {
                            BegDate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemVAT.Coidt + @"""");
                        }
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
                        string time = string.Empty;
                        if (App.IsArabic)
                        {
                            if (BegDate != null)
                            {

                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                //time = Convert.ToDateTime(BegDate).ToString("hh:mm:ss tt", new CultureInfo("en-US"));
                                if (childVAT.Coitm != null)
                                {
                                    time = childVAT.Coitm;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = UtilityManager.ConvertNumerals(result[0]);
                                    string minutes = UtilityManager.ConvertNumerals(result[1]);
                                    string second = UtilityManager.ConvertNumerals(result[2]);
                                    time = " " + hours + ":" + minutes + " ";

                                }
                                var Tstartdate = UtilityManager.ToArabicDate(StartDate);
                                StartDate = String.Concat(Tstartdate, time);

                            }
                        }
                        else
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                //time = Convert.ToDateTime(BegDate).ToString("hh:mm:ss tt", new CultureInfo("en-US"));
                                if (childVAT.Coitm != null)
                                {
                                    time = childVAT.Coitm;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                 
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes + " ";

                                }
                                StartDate = StartDate + "  " + time;
                            }
                        }
                        childVAT.DateAndTime = StartDate;
                        VATCo.Add(childVAT);
                    }
                    ListVATCorrespondance = VATCo;
                }
               
                PopToRootPage();

                // Assigning data in the list
                if(ETCorres != null && ETCorres.d != null)
                {
                    List<CorrespondanceModel> ETCo = new List<CorrespondanceModel>();
                    ETCountDisplay = AppResources.ZZET + "(" + ETCorres.d.results.Count + ")";
                    foreach (CorrespondenceResult itemET in ETCorres.d.results)
                    {
                        CorrespondanceModel childET = new CorrespondanceModel();
                        childET.Title = itemET.Descript;
                        childET.RefNumber = itemET.LetterNum;
                        childET.Cokey = itemET.Cokey;
                        childET.Coitm = itemET.Coitm;
                        if (itemET.Copri != null)
                        {
                            childET.Txtco = JsonConvert.DeserializeObject<DateTime>(@"""" + itemET.Copri + @"""");
                        }
                        if (itemET.Coidt != null)
                        {
                            childET.StartDate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemET.Coidt + @"""");
                        }
                        childET.Cotype = itemET.Cotyp;
                        childET.Vkont = itemET.Vkont;
                        childET.Gpart = itemET.Gpart;
                        childET.Begdaz = itemET.Begdaz;
                        childET.Enddaz = itemET.Enddaz;
                        DateTime? BegDate = DateTime.Now;
                        if (itemET.Coidt != null)
                        {
                            BegDate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemET.Coidt + @"""");
                        }
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
                        string time = string.Empty;
                        if (App.IsArabic)
                        {
                            if (BegDate != null)
                            {

                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                if (childET.Coitm != null)
                                {
                                    time = childET.Coitm;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = UtilityManager.ConvertNumerals(result[0]);
                                    string minutes = UtilityManager.ConvertNumerals(result[1]);
                                    string second = UtilityManager.ConvertNumerals(result[2]);
                                    time = " " + hours + ":" + minutes + " ";

                                }

                                var Tstartdate = UtilityManager.ToArabicDate(StartDate);
                                StartDate = String.Concat(Tstartdate, time);
                            }

                        }
                        else
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                if (childET.Coitm != null)
                                {
                                    time = childET.Coitm;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes + " ";

                                }
                                StartDate = StartDate + time;
                            }

                        }
                        childET.DateAndTime = StartDate;
                        ETCo.Add(childET);
                    }
                    ListETCorrespondance = ETCo;
                    SetSelectedIndexZakat = 4;
                    SetSelectedIndexET = 2;
                    SetSelectedIndexVAT = 2;
                }
               
            }
            catch(Exception ex)
            {

            }
            
        }

        //public void OnZAKATClicked()
        //{
        //    try
        //    {
        //        IsZakatVisible = true;
        //        IsVATVisible = false;
        //        IsETVisible = false;

        //        List<CorrespondenceFiltersModel> Filters = new List<CorrespondenceFiltersModel>();

        //        Filters.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
        //        Filters.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });
        //        Filters.Add(new CorrespondenceFiltersModel { ID = 3, Filter = AppResources.ZZFavoriteAscending });
        //        Filters.Add(new CorrespondenceFiltersModel { ID = 4, Filter = AppResources.ZZFavoriteDescending });

        //        CorresFilter = Filters;
        //        SetSelectedIndex = 4;
        //        if(ListZAKATCorrespondance != null && ListZAKATCorrespondance.Count > 0)
        //        {
        //            SetNoDataLabelVisibility = false;
        //        }
        //        else
        //        {
        //            SetNoDataLabelVisibility = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public void OnVATLabelClicked()
        //{
        //    try
        //    {
        //        IsZakatVisible = false;
        //        IsVATVisible = true;
        //        IsETVisible = false;

        //        List<CorrespondenceFiltersModel> Filters = new List<CorrespondenceFiltersModel>();
        //        Filters.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
        //        Filters.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });

        //        CorresFilter = Filters;
        //        SetSelectedIndex = 2;
        //        if (ListVATCorrespondance != null && ListVATCorrespondance.Count > 0)
        //        {
        //            SetNoDataLabelVisibility = false;
        //        }
        //        else
        //        {
        //            SetNoDataLabelVisibility = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public void OnEtLabelClicked()
        //{
        //    try
        //    {
        //        IsZakatVisible = false;
        //        IsVATVisible = false;
        //        IsETVisible = true;

        //        List<CorrespondenceFiltersModel> Filters = new List<CorrespondenceFiltersModel>();
        //        Filters.Add(new CorrespondenceFiltersModel { ID = 1, Filter = AppResources.ZZDateAscending });
        //        Filters.Add(new CorrespondenceFiltersModel { ID = 2, Filter = AppResources.ZZDateDescending });


        //        CorresFilter = Filters;
        //        SetSelectedIndex = 2;
        //        if (ListETCorrespondance != null && ListETCorrespondance.Count > 0)
        //        {
        //            SetNoDataLabelVisibility = false;
        //        }
        //        else
        //        {
        //            SetNoDataLabelVisibility = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #endregion
    }
}
 