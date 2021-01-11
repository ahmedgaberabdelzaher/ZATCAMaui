using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Shapes;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]

    public class TaxpayerCorrespondancePageViewModel : BaseViewModel
    {
        #region Fields
        public ICommand OnBackButtonClicked { get; set; }
      
        #endregion
        #region Properties

        private CorrespondenceRootObject _zakatCorres;
        public CorrespondenceRootObject ZakatCorres
        {
            get
            {
                return _zakatCorres;
            }
            set
            {
                if (_zakatCorres == value) return;
                _zakatCorres = value;
                RaisePropertyChanged("ZakatCorres");
            }
        }

        private CorrespondenceRootObject _vATCorres;
        public CorrespondenceRootObject VATCorres
        {
            get
            {
                return _vATCorres;
            }
            set
            {
                if (_vATCorres == value) return;

                _vATCorres = value;
                RaisePropertyChanged("VATCorres");
            }
        }

        private CorrespondenceRootObject _eTCorres;
        public CorrespondenceRootObject ETCorres
        {
            get
            {
                return _eTCorres;
            }
            set
            {
                if (_eTCorres == value) return;

                _eTCorres = value;
                RaisePropertyChanged("ETCorres");
            }
        }



        public List<ChipModel>  _selectedChipFilterItemList = null;
        public List<ChipModel> SelectedChipFilterItemList
        {
            get
            {
                return _selectedChipFilterItemList;
            }
            set
            {
                if (_selectedChipFilterItemList == value) return;

                _selectedChipFilterItemList = value;
                if (_selectedChipFilterItemList != null)
                {
                   // FilterIfTypeAndStausFilterSelected();
                }
                RaisePropertyChanged("SelectedChipFilterItemList");
            }
        }
        public ObservableCollection<ChipModel> _chipDataFilterlist = null;
        public ObservableCollection<ChipModel> ChipDataFilterlist
        {
            get
            {
                return _chipDataFilterlist;
            }
            set
            {
                if (_chipDataFilterlist == value) return;

                _chipDataFilterlist = value;
                RaisePropertyChanged("ChipDataFilterlist");
            }
        }

        private string _filterLabelText;
        public string FilterLabelText
        {
            get
            {
                return _filterLabelText;
            }
            set
            {
                if (_filterLabelText == value) return;

                _filterLabelText = value;
                RaisePropertyChanged("FilterLabelText");
            }
        }

        private List<CorrespondanceModel> _listVATCorrespondance = null;
        public List<CorrespondanceModel> ListVATCorrespondance
        {
            get
            {
                return _listVATCorrespondance;
            }
            set
            {
                if (_listVATCorrespondance == value) return;

                _listVATCorrespondance = value;
                RaisePropertyChanged("ListVATCorrespondance");
            }
        } 
        private string _CountLabel = AppResources.NDCount;
        public string CountLabel
        {
            get
            {
                return _CountLabel;
            }
            set
            {
                if (_CountLabel == value) return;

                _CountLabel = value;
                RaisePropertyChanged("CountLabel");
            }
        }
        private int _Count = 0;
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                if (_Count == value) return;

                _Count = value;
                RaisePropertyChanged("Count");
            }
        }
        private List<CorrespondanceModel> _listAllCorrespondance = null;
        public List<CorrespondanceModel> ListAllCorrespondance
        {
            get
            {
                return _listAllCorrespondance;
            }
            set
            {
                if (_listAllCorrespondance == value) return;

                _listAllCorrespondance = value;
                RaisePropertyChanged("ListAllCorrespondance");
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
                if (_listZAKATCorrespondance == value) return;

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
                if (_listETCorrespondance == value) return;

                _listETCorrespondance = value;
                RaisePropertyChanged("ListETCorrespondance");
            }
        }

        private string _filterTaxTypeLabelText = null;
        public string FilterTaxTypeLabelText
        {
            get
            {
                return _filterTaxTypeLabelText;
            }
            set
            {
                if (_filterTaxTypeLabelText == value) return;

                _filterTaxTypeLabelText = value;
                RaisePropertyChanged("FilterTaxTypeLabelText");
            }
        }


        

        public ReturnTypes _selectedDropdownItem;
        public ReturnTypes SelectedDropdownItem
        {
            get
            {
                return _selectedDropdownItem;
            }
            set
            {
                if (_selectedDropdownItem == value) return;

                _selectedDropdownItem = value;
                if (_selectedDropdownItem != null)
                {
                    FilterLabelText = _selectedDropdownItem.TaxType;
                    if (_selectedDropdownItem.Id.Equals("00"))
                        {
                        SetAllCorrespondancedata();
                    }
                    //FilterOnBasisOfTaxType();

                }

                RaisePropertyChanged("SelectedDropdownItem");
            }
        }

        public ReturnTypes _selectedTaxTypeDropdownItem;
        public ReturnTypes SelectedTaxTypeDropdownItem
        {
            get
            {
                return _selectedTaxTypeDropdownItem;
            }
            set
            {
                if (_selectedTaxTypeDropdownItem == value) return;

                _selectedTaxTypeDropdownItem = value;
                if (_selectedTaxTypeDropdownItem != null)
                {
                    FilterTaxTypeLabelText = _selectedTaxTypeDropdownItem.TaxType;
                    FilterOnbasisOfChipSelectedItem();
                }

                RaisePropertyChanged("SelectedDropdownItem");
            }
        }

        private bool _isLoading=false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;

                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private bool _isVisibleFavourite = false;
        public bool IsVisibleFavourite
        {
            get
            {
                return _isVisibleFavourite;
            }
            set
            {
                if (_isVisibleFavourite == value) return;

                _isVisibleFavourite = value;
                RaisePropertyChanged("IsVisibleFavourite");
            }
        }


        private bool _isListVisible = false;
        public bool IsListVisible
        {
            get
            {
                return _isListVisible;
            }
            set
            {
                if (_isListVisible == value) return;

                _isListVisible = value;
                RaisePropertyChanged("IsListVisible");
            }
        }
        private bool _isNoDataLableVisible = true;
        public bool IsNoDataLableVisible
        {
            get
            {
                return _isNoDataLableVisible;
            }
            set
            {
                if (_isNoDataLableVisible == value) return;

                _isNoDataLableVisible = value;
                RaisePropertyChanged("IsNoDataLableVisible");
            }
        }
        private List<ReturnTypes> _filterListForDropDown;
        public List<ReturnTypes> FilterListForDropDown
        {
            get
            {
                return _filterListForDropDown;
            }
            set
            {
                if (_filterListForDropDown == value) return;

                _filterListForDropDown = value;
                RaisePropertyChanged("FilterListForDropDown");
            }
        }


        private List<ReturnTypes> _taxTypeListForDropDown;
        public List<ReturnTypes> TaxTypeListForDropDown
        {
            get
            {
                return _taxTypeListForDropDown;
            }
            set
            {
                if (_taxTypeListForDropDown == value) return;

                _taxTypeListForDropDown = value;
                RaisePropertyChanged("TaxTypeListForDropDown");
            }
        }

        private ObservableCollection<CorrespondanceModel> _listToDisplay;
        public ObservableCollection<CorrespondanceModel> ListToDisplay
        {
            get
            {
                return _listToDisplay;
            }
            set
            {
                if (_listToDisplay == value) return;

                _listToDisplay = value;
                Count = 0;
                CountLabel = AppResources.NDCount + ": " + Count.ToString();
                if (_listToDisplay != null)
                {

                    if (_listToDisplay.Count > 0)
                    {
                        Count = _listToDisplay.Count;
                        CountLabel =AppResources.NDCount+": "+ Count.ToString();
                        IsListVisible = true;
                        IsNoDataLableVisible = false;
                    }
                    else
                    {
                        IsListVisible = false;
                        IsNoDataLableVisible = true;

                        Count = _listToDisplay.Count;
                        CountLabel = Count.ToString();
                    }
                }
                else
                {
                    Count = _listToDisplay.Count;
                    CountLabel = Count.ToString();
                    IsListVisible = false;
                    IsNoDataLableVisible = true;

                }
                RaisePropertyChanged("ListToDisplay");
            }
        }
        #endregion


        #region Constructor
        public TaxpayerCorrespondancePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion

        #region Method
        public async Task onPageLoad()
        {
            ListToDisplay = new ObservableCollection<CorrespondanceModel>();
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(() =>
                {
                    ZakatCorres = new CorrespondenceRootObject();
                    VATCorres = new CorrespondenceRootObject();
                    ETCorres = new CorrespondenceRootObject();

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
                 //   _dialogService.ShowMessage(ex.Message, AppResources.Information);
                   await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                });
            }
        }
        public void SetData()
        {
            try
            {
                ListAllCorrespondance = new List<CorrespondanceModel>();
                List<CorrespondanceModel> ZakatCo = new List<CorrespondanceModel>();
                // Assigning data in the list
                ListZAKATCorrespondance = new List<CorrespondanceModel>();
                ListVATCorrespondance = new List<CorrespondanceModel>();
                ListETCorrespondance = new List<CorrespondanceModel>();
                if (ZakatCorres != null && ZakatCorres.d!=null && ZakatCorres.d.results!=null && ZakatCorres.d.results.Count > 0)
                {
                    foreach (CorrespondenceResult itemZakat in ZakatCorres.d.results)
                    {
                        CorrespondanceModel childZakat = new CorrespondanceModel();
                        childZakat.Title = itemZakat.Descript;
                        childZakat.RefNumber = itemZakat.LetterNum;
                        if (itemZakat.LetterNum != null)
                            childZakat.Cokey = itemZakat.Cokey;
                        childZakat.Coitm = itemZakat.Coitm;
                        childZakat.Ctime = itemZakat.Ctime;
                        childZakat.Cdate = itemZakat.Cdate;
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
                        try
                        {
                            childZakat.TaxtpFg = itemZakat.TaxtpFg;
                        }
                        catch(Exception ex)
                        {
                            Console.Write(ex.ToString());
                            Console.Write(ex.StackTrace.ToString());
                        }
                        DateTime? BegDate = DateTime.Now;
                        if (itemZakat.Cdate != null)
                        {
                            BegDate = childZakat.Cdate;
                        }
                        if (itemZakat.Zzfav == "1")
                        {
                            childZakat.IsFav = true;
                            childZakat.FavImg = "ic_star.png";
                        }
                        else
                        {
                            childZakat.IsFav = false;
                            childZakat.FavImg = "arrowRight.png";
                        }
                        string StartDate = string.Empty;
                        string time = string.Empty;
                        if (App.IsArabic)
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                string[] dts = StartDate.Split('-');
                                string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                                StartDate = date;
                                

                                if (childZakat.Ctime != null)
                                {
                                    time = childZakat.Ctime;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes + " ";
                                }
                                childZakat.DateToDisplay = StartDate;
                                childZakat.TimeToDisplay = time;
                                StartDate = String.Concat(StartDate, time);
                            }
                        }
                        else
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                string[] dts = StartDate.Split('-');
                                string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                                StartDate = date;
                                time = Convert.ToDateTime(BegDate).ToString("hh:mm:ss tt", new CultureInfo("en-US"));
                                if (childZakat.Ctime != null)
                                {
                                    time = childZakat.Ctime;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes + " ";
                                }
                                childZakat.DateToDisplay = StartDate;
                                childZakat.TimeToDisplay = time;
                               StartDate = StartDate + "  " + time;
                            }
                        }
                      
                       

                        childZakat.DateAndTime = StartDate;
                        ZakatCo.Add(childZakat);
                    }
                    ListZAKATCorrespondance = ZakatCo;
                    ListAllCorrespondance = ListAllCorrespondance.Union(ListZAKATCorrespondance).ToList();
                }
                else
                {
                }
                PopToRootPage();
                // Assigning data in the list
                if (VATCorres != null && VATCorres.d!=null && VATCorres.d.results!=null && VATCorres.d.results.Count > 0)
                {
                    List<CorrespondanceModel> VATCo = new List<CorrespondanceModel>();
                    foreach (CorrespondenceResult itemVAT in VATCorres.d.results)
                    {
                        CorrespondanceModel childVAT = new CorrespondanceModel();
                        childVAT.Title = itemVAT.Descript;
                        childVAT.RefNumber = itemVAT.LetterNum;
                        childVAT.Cokey = itemVAT.Cokey;
                        childVAT.Coitm = itemVAT.Coitm;
                        childVAT.Cdate = itemVAT.Cdate;
                        childVAT.Ctime = itemVAT.Ctime;
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
                        try
                        {
                            childVAT.TaxtpFg = itemVAT.TaxtpFg;
                        }
                        catch(Exception ex)
                        {

                        }
                        DateTime? BegDate = DateTime.Now;
                        if (itemVAT.Cdate != null)
                        {
                            BegDate = childVAT.Cdate;
                        }
                        if (itemVAT.Zzfav == "1")
                        {
                            childVAT.IsFav = true;
                            childVAT.FavImg = "ic_star.png";
                        }
                        else
                        {
                            childVAT.IsFav = false;
                            childVAT.FavImg =  "arrowRight.png";
                        }
                        string StartDate = string.Empty;
                        string time = string.Empty;
                        if (App.IsArabic)
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                string[] dts = StartDate.Split('-');
                                string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                                StartDate = date;
                                if (childVAT.Ctime != null)
                                {
                                    time = childVAT.Ctime;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes + " ";
                                }
                                childVAT.DateToDisplay = StartDate;
                                childVAT.TimeToDisplay = time;
                                StartDate = String.Concat(StartDate, time);
                            }
                        }
                        else
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                string[] dts = StartDate.Split('-');
                                string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                                StartDate = date;
                                if (childVAT.Ctime != null)
                                {
                                    time = childVAT.Ctime;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes + " ";
                                }
                                childVAT.DateToDisplay = StartDate;
                                childVAT.TimeToDisplay = time;
                                StartDate = StartDate + "  " + time;
                            }
                        }
                        childVAT.DateAndTime = StartDate;
                        VATCo.Add(childVAT);
                    }
                    ListVATCorrespondance = VATCo;
                    ListAllCorrespondance = ListAllCorrespondance.Union(ListVATCorrespondance).ToList();
                }
                else
                {
                }
                // Assigning data in the list
                if (ETCorres != null && ETCorres.d!=null && ETCorres.d.results!=null && ETCorres.d.results.Count > 0)
                {
                    List<CorrespondanceModel> ETCo = new List<CorrespondanceModel>();
                    foreach (CorrespondenceResult itemET in ETCorres.d.results)
                    {
                        CorrespondanceModel childET = new CorrespondanceModel();
                        childET.Title = itemET.Descript;
                        childET.RefNumber = itemET.LetterNum;
                        if (itemET.LetterNum != null)
                            childET.Cokey = itemET.Cokey;
                        childET.Coitm = itemET.Coitm;
                        childET.Ctime = itemET.Ctime;
                        childET.Cdate = itemET.Cdate;
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
                        try
                        {
                            childET.TaxtpFg = itemET.TaxtpFg;
                        }
                        catch(Exception ex)
                        {
                            Console.Write(ex.ToString());
                            Console.Write(ex.StackTrace.ToString());
                        }
                        DateTime? BegDate = DateTime.Now;
                        if (itemET.Cdate != null)
                        {
                            BegDate = childET.Cdate;
                        }
                        if (itemET.Zzfav == "1")
                        {
                            childET.IsFav = true;
                            childET.FavImg = "ic_star.png";
                        }
                        else
                        {
                            childET.IsFav = false;
                            childET.FavImg = "arrowRight.png";
                        }
                        string StartDate = string.Empty;
                        string time = string.Empty;
                        if (App.IsArabic)
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                string[] dts = StartDate.Split('-');
                                string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                                StartDate = date;
                                if (childET.Ctime != null)
                                {
                                    time = childET.Ctime;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes + " ";
                                }
                                childET.DateToDisplay = StartDate;
                                childET.TimeToDisplay = time;
                                StartDate = String.Concat(StartDate, time);
                            }
                        }
                        else
                        {
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                string[] dts = StartDate.Split('-');
                                string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                                StartDate = date;
                                if (childET.Ctime != null)
                                {
                                    time = childET.Ctime;
                                    time = time.Replace("PT", string.Empty).Replace("H", ":").Replace("M", ":").Replace("S", " ");
                                    string[] result = time.Split(':');
                                    string hours = result[0];
                                    string minutes = result[1];
                                    string second = result[2];
                                    time = " " + hours + ":" + minutes + " ";
                                }
                                childET.DateToDisplay = StartDate;
                                childET.TimeToDisplay = time;
                                StartDate = StartDate + time;
                            }
                        }
                        childET.DateAndTime = StartDate;
                        ETCo.Add(childET);
                    }
                    ListETCorrespondance = ETCo;
                    ListAllCorrespondance = ListAllCorrespondance.Union(ListETCorrespondance).ToList();
                }
                else
                {

                }
                SelectedDropdownItem = FilterListForDropDown.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        public void PopulateFilterDropdownList()
        {
            try
            {
                List<ReturnTypes> FilterList = new List<ReturnTypes>
                {
                    new ReturnTypes {Id = "00",TaxType = AppResources.ZZCorrespondence},
                    //new ReturnTypes {Id = "01",TaxType = AppResources.ZAKATReturns},
                };
                
                FilterListForDropDown = new List<ReturnTypes>();
                FilterListForDropDown = FilterList;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        public void PopulateDataInChips()
        {
            ChipDataFilterlist = new ObservableCollection<ChipModel>()
            {
                new ChipModel(){Text =AppResources.Favorite, TemplateType = AppResources.ZZFavoriteAscending, ImageSource="ic_star_border.png"},
                               //new ChipModel(){Text =AppResources.All, TemplateType = AppResources.All,ImageSource = "ic_money.png"}
            };
        }
        public void SetAllCorrespondancedata()
        {
            ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListAllCorrespondance) ;
            if (ListToDisplay != null)
            {
                ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListToDisplay.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Ctime).ToList());
            }
        }
        public void FilterCorrespondancedata()
        {
            if(SelectedTaxTypeDropdownItem!=null)
            {

                if (SelectedTaxTypeDropdownItem.Id == "00")
                {
                    if (ListZAKATCorrespondance != null)
                    {
                        ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListAllCorrespondance);
                        if (ListToDisplay != null)
                        {
                            ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListToDisplay.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Ctime).ToList());
                        }
                    }
                    else
                    {
                        if (ListToDisplay != null)
                        {
                            ClearList();
                        }
                    }
                    IsVisibleFavourite = true;
                }
                else if (SelectedTaxTypeDropdownItem.Id=="01")
                {
                    if (ListZAKATCorrespondance != null)
                    {
                        ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListZAKATCorrespondance);
                        if (ListToDisplay != null)
                        {
                            ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListToDisplay.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Ctime).ToList());
                        }
                    }
                    else
                    {
                        if (ListToDisplay != null)
                        {
                            ClearList();
                        }
                    }
                    IsVisibleFavourite = true;
                }
                else if(SelectedTaxTypeDropdownItem.Id == "02")
                {
                    if (ListVATCorrespondance != null)
                    {
                        ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListVATCorrespondance);
                        if (ListToDisplay != null)
                        {
                            ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListToDisplay.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Ctime).ToList());
                        }
                    }
                    else
                    {
                        if (ListToDisplay != null)
                        {
                            ClearList();
                        }
                    }
                    IsVisibleFavourite = false;
                }
                else if(SelectedTaxTypeDropdownItem.Id == "03")
                {
                    if (ListETCorrespondance != null)
                    {
                        ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListETCorrespondance);
                        if (ListToDisplay != null)
                        {
                            ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListToDisplay.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Ctime).ToList());
                        }
                    }
                    else
                    {
                        if(ListToDisplay!=null)
                        {
                            ClearList();
                        }
                    }
                    IsVisibleFavourite = false;
                }
            }
           
        }

        public void ClearList()
        {
            ListToDisplay.Clear();
            Count = 0;
            CountLabel = AppResources.NDCount + ": " + Count.ToString();
            IsListVisible = false;
            IsNoDataLableVisible = true;
        }
        public async void ShowCorrespondenceDetails(CorrespondanceModel CorresModel)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _navigationService.NavigateTo(App.TaxpayerCorrespondanceDetailPageView, CorresModel);
                });
                //  _navigationService.NavigateTo(App.PdfView, pdfUrl);
            });
            await Task.Run(() =>
            {
                IsLoading = true;
            });
        }
        public void FilterOnbasisOfChipSelectedItem()
        {
            try
            {
                FilterCorrespondancedata();
                //ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListAllCorrespondance);
                if (SelectedChipFilterItemList != null && IsVisibleFavourite)
                    foreach (var Item in SelectedChipFilterItemList)
                    {
                        if (Item.TemplateType.Equals(AppResources.ZZFavoriteAscending))
                        {
                            ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListToDisplay.Where(x => x.IsFav == true).ToList());
                        }
                    }
                if (ListToDisplay != null)
                {
                    ListToDisplay = new ObservableCollection<CorrespondanceModel>(ListToDisplay.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Ctime).ToList());
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            
        }
            #endregion
    }
}
