using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Acr.UserDialogs;
using ZATCAMAUI.Models.CustomServices.TraiffSection;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class TraifSectionsViewModel : BaseViewModel
    {
        int searchRsltCount { get; set; }

        public int SearchRsltCount
        {
            get { return searchRsltCount; }

            set
            {
                searchRsltCount = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<Section> traiffSections { get; set; }

        public ObservableCollection<Section> TraiffSections
        {
            get { return traiffSections; }

            set
            {

                traiffSections = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<Section> traiffSectionsLst { get; set; }

        public ObservableCollection<Section> TraiffSectionsLst
        {
            get { return traiffSectionsLst; }

            set
            {

                traiffSectionsLst = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<Note> sectionNotesLst { get; set; }

        public ObservableCollection<Note> SectionNotesLst
        {
            get { return sectionNotesLst; }

            set
            {

                sectionNotesLst = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<Note> chapterNotesLst { get; set; }

        public ObservableCollection<Note> ChapterNotesLst
        {
            get { return chapterNotesLst; }

            set
            {

                chapterNotesLst = value;
                RaisePropertyChanged();
            }
        }


        Section selectedTraiffSections { get; set; }

        public Section SelectedTraiffSections
        {
            get { return selectedTraiffSections; }

            set
            {

                selectedTraiffSections = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<Chapter> traiffChapters { get; set; }

        public ObservableCollection<Chapter> TraiffChapters
        {
            get { return traiffChapters; }

            set
            {

                traiffChapters = value;
                RaisePropertyChanged();
            }
        }

        Chapter selectedTraiffChapters { get; set; }

        public Chapter SelectedTraiffChapters
        {
            get { return selectedTraiffChapters; }

            set
            {

                selectedTraiffChapters = value;
                RaisePropertyChanged();
            }
        }

        string searchKey { get; set; }

        public string SearchKey
        {
            get { return searchKey; }

            set
            {

                searchKey = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<MainHarmonizedTariff> mainHarmonizedTariffs { get; set; }

        public ObservableCollection<MainHarmonizedTariff> MainHarmonizedTariffs
        {
            get { return mainHarmonizedTariffs; }

            set
            {

                mainHarmonizedTariffs = value;
                RaisePropertyChanged();
            }
        }

        MainHarmonizedTariff selectedMainHarmonizedTariffs { get; set; }

        public MainHarmonizedTariff SelectedMainHarmonizedTariffs
        {
            get { return selectedMainHarmonizedTariffs; }

            set
            {

                selectedMainHarmonizedTariffs = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<SubHarmonizedTraiffs> subHarmonizedTariffs { get; set; }

        public ObservableCollection<SubHarmonizedTraiffs> SubHarmonizedTariffs
        {
            get { return subHarmonizedTariffs; }

            set
            {

                subHarmonizedTariffs = value;
                RaisePropertyChanged();
            }
        }

        HarmonizedTarrif selectedSubHarmonizedTariffs { get; set; }

        public HarmonizedTarrif SelectedSubHarmonizedTariffs
        {
            get { return selectedSubHarmonizedTariffs; }

            set
            {

                selectedSubHarmonizedTariffs = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<TariffSearch> searchResultLst { get; set; }

        public ObservableCollection<TariffSearch> SearchResultLst
        {
            get { return searchResultLst; }

            set
            {

                searchResultLst = value;
                if (value != null)
                {
                    SearchRsltCount = value.Count();
                }
                else
                {
                    SearchRsltCount = 0;
                }
                RaisePropertyChanged();
            }
        }

        TariffSearch selectedSearchResultLst { get; set; }

        public TariffSearch SelectedSearchResultLst
        {
            get { return selectedSearchResultLst; }

            set
            {

                selectedSearchResultLst = value;
                RaisePropertyChanged();
            }
        }



        HarmonizedTarrif selectedSearchTariffs { get; set; }

        public HarmonizedTarrif SelectedSearchTariffs
        {
            get { return selectedSearchTariffs; }

            set
            {

                selectedSearchTariffs = value;
                RaisePropertyChanged();
            }
        }




        ObservableCollection<HarmonizedTarrif> harmonizedTariffs { get; set; }

        public ObservableCollection<HarmonizedTarrif> HarmonizedTariffs
        {
            get { return harmonizedTariffs; }

            set
            {

                harmonizedTariffs = value;
                RaisePropertyChanged();
            }
        }

        HarmonizedTarrif selectedHarmonizedTariffs { get; set; }

        public HarmonizedTarrif SelectedHarmonizedTariffs
        {
            get { return selectedHarmonizedTariffs; }

            set
            {

                selectedHarmonizedTariffs = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<HarmonizedTarrif> harmonizedTariffslvl2 { get; set; }

        public ObservableCollection<HarmonizedTarrif> HarmonizedTariffslvl2
        {
            get { return harmonizedTariffslvl2; }

            set
            {

                harmonizedTariffslvl2 = value;
                RaisePropertyChanged();
            }
        }

        HarmonizedTarrif selectedHarmonizedTariffslvl2 { get; set; }

        public HarmonizedTarrif SelectedHarmonizedTariffslvl2
        {
            get { return selectedHarmonizedTariffslvl2; }

            set
            {

                selectedHarmonizedTariffslvl2 = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<HarmonizedTarrif> harmonizedTariffslvl3 { get; set; }

        public ObservableCollection<HarmonizedTarrif> HarmonizedTariffslvl3
        {
            get { return harmonizedTariffslvl3; }

            set
            {

                harmonizedTariffslvl3 = value;
                RaisePropertyChanged();
            }
        }

        HarmonizedTarrif selectedHarmonizedTariffslvl3 { get; set; }

        public HarmonizedTarrif SelectedHarmonizedTariffslvl3
        {
            get { return selectedHarmonizedTariffslvl3; }

            set
            {

                selectedHarmonizedTariffslvl3 = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<HarmonizedTarrif> harmonizedTariffslvl4 { get; set; }

        public ObservableCollection<HarmonizedTarrif> HarmonizedTariffslvl4
        {
            get { return harmonizedTariffslvl4; }

            set
            {

                harmonizedTariffslvl4 = value;
                RaisePropertyChanged();
            }
        }

        HarmonizedTarrif selectedHarmonizedTariffslvl4 { get; set; }

        public HarmonizedTarrif SelectedHarmonizedTariffslvl4
        {
            get { return selectedHarmonizedTariffslvl4; }

            set
            {

                selectedHarmonizedTariffslvl4 = value;
                RaisePropertyChanged();
            }
        }

        int? levelNo = null;
        public int? LevelNo { get { return levelNo; } set { levelNo = value; RaisePropertyChanged(); } }

        bool isChapterSection;
        public bool IsChapterSection
        {
            get { return isChapterSection; }
            set { isChapterSection = value; RaisePropertyChanged(); }
        }

        bool isHarmonizedTarrifs;
        public bool IsHarmonizedTarrifs
        {
            get { return isHarmonizedTarrifs; }
            set { isHarmonizedTarrifs = value; RaisePropertyChanged(); }
        }

        bool isSectNote;
        public bool IsSectNote
        {
            get { return isSectNote; }
            set { isSectNote = value; RaisePropertyChanged(); }
        }

        bool isChapterNote;
        public bool IsChapterNote
        {
            get { return isChapterNote; }
            set { isChapterNote = value; RaisePropertyChanged(); }
        }

        bool isSearchVIewVisbible;
        public bool IsSearchVIewVisible
        {
            get { return isSearchVIewVisbible; }
            set { isSearchVIewVisbible = value; RaisePropertyChanged(); }
        }


        bool isSearchFocus;
        public bool IsSearchFocus
        {
            get { return isSearchFocus; }
            set { isSearchFocus = value; RaisePropertyChanged(); }
        }

        bool isSearchFilterVisbible;
        public bool IsSearchFilterVisbible
        {
            get { return isSearchFilterVisbible; }
            set { isSearchFilterVisbible = value; RaisePropertyChanged(); }
        }

        bool isSectionView;
        public bool IsSectionView
        {
            get { return isSectionView; }
            set { isSectionView = value; RaisePropertyChanged(); }
        }


        int searchBy = 1;
        public int SearchBy
        {
            get { return searchBy; }
            set { searchBy = value; RaisePropertyChanged(); }
        }

        bool isMainHarmonizedTariffs;
        public bool IsMainHarmonizedTariffs
        {
            get { return isMainHarmonizedTariffs; }
            set { isMainHarmonizedTariffs = value; RaisePropertyChanged(); }
        }

        bool isSubHarmonizedTariffs;
        public bool IsSubHarmonizedTariffs
        {
            get { return isSubHarmonizedTariffs; }
            set { isSubHarmonizedTariffs = value; RaisePropertyChanged(); }
        }


        bool isHarmonizedTariffs2lvl;
        public bool IsHarmonizedTariffs2lvl
        {
            get { return isHarmonizedTariffs2lvl; }
            set { isHarmonizedTariffs2lvl = value; RaisePropertyChanged(); }
        }

        bool isTarrifSearch;
        public bool IsTarrifSearch
        {
            get { return isTarrifSearch; }
            set { isTarrifSearch = value; RaisePropertyChanged(); }
        }

        bool isHarmonizedTariffs3lvl;
        public bool IsHarmonizedTariffs3lvl
        {
            get { return isHarmonizedTariffs3lvl; }
            set { isHarmonizedTariffs3lvl = value; RaisePropertyChanged(); }
        }

        bool isHarmonizedTariffs4lvl;
        public bool IsHarmonizedTariffs4lvl
        {
            get { return isHarmonizedTariffs4lvl; }
            set { isHarmonizedTariffs4lvl = value; RaisePropertyChanged(); }
        }

        bool isitemDetilsVisible;
        public bool IsitemDetilsVisible
        {
            get { return isitemDetilsVisible; }
            set { isitemDetilsVisible = value; RaisePropertyChanged(); }
        }

        string title;
        public string Title { get { return title; } set { title = value; RaisePropertyChanged(); } }

        public string ChapterTitle { get; set; }
        public string MainHarmonizedTitle { get; set; }
        ITraiffSectionsServices _traiffSectionServices;

        public TraifSectionsViewModel(INavigationService navigationServices, IDialogService dialogService, ITraiffSectionsServices traiffSectionsServices) : base(navigationServices, dialogService)
        {
            // SetFlowDirection();

            IsSectionView = true;
            _traiffSectionServices = traiffSectionsServices;
            Title = AppResources.CustomsZATCAIntegrat;
        }

        public ICommand LoadIntialTraiffSectionsCommand
        {
            get
            {
                return new Command(async () =>
                {

                    SetFlowDirection();
                    if (IsSectionView)
                    {
                        Title = AppResources.CustomsZATCAIntegrat;
                    }
                    await GetTraiffsections();
                });
            }
        }

        public ICommand CopyHSCommand
        {
            get
            {
                return new Command(async () =>
                {

                    await Clipboard.SetTextAsync(SelectedSubHarmonizedTariffs.hrmnzd_code);
                    var toastConfig = new ToastConfig($"{SelectedSubHarmonizedTariffs.hrmnzd_code} {AppResources.Copied}");
                    //toastConfig.
                    toastConfig.SetDuration(1500);
                    toastConfig.SetBackgroundColor(System.Drawing.Color.Black);
                    toastConfig.SetPosition(ToastPosition.Bottom);

                    UserDialogs.Instance.Toast(toastConfig);

                });
            }
        }

        public ICommand OpenNoteCommand
        {
            get
            {
                return new Command<string>(async (e) =>
                {
                    if (IsChapterSection)
                    {
                        IsSectNote = true;
                    }
                    else
                    {
                        IsChapterNote = true;

                    }
                });
            }
        }

        public ICommand CloseNoteCommand
        {
            get
            {
                return new Command<string>(async (e) =>
                {
                    if (e == "1")
                    {
                        IsSectNote = false;
                    }
                    else
                    {
                        IsChapterNote = false;

                    }
                });
            }
        }

        public ICommand ViewSearchViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (IsSearchVIewVisible && !IsSearchFilterVisbible)
                    {
                        IsSearchFilterVisbible = true;
                    }
                    else if (!IsSearchVIewVisible)
                    {
                        SearchKey = "";
                        Title = AppResources.SearchText;
                        IsSearchFilterVisbible = true;
                        IsSearchVIewVisible = true;
                    }


                });
            }
        }

        public ICommand CloseSearchCommand
        {
            get
            {
                return new Command(() =>
                {
                    SearchKey = "";
                    IsSearchVIewVisible = false;
                });
            }
        }

        public ICommand ChangeSearchByCommand
        {
            get
            {
                return new Command<string>((key) =>
                {
                    SearchBy = int.Parse(key);
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    if (!string.IsNullOrEmpty(e.ToString()) && e.ToString().Length > 3)
                    {
                        try
                        {
                            /*IsSearchFilterVisbible = false;
                            var res = TraiffSectionsLst.Where(c => c.Name.Contains(e.ToString()));
                            TraiffSections = new ObservableCollection<Section>(res);
                            */
                            await TariffSearch();
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        // TraiffSections = TraiffSectionsLst;
                    }

                });
            }
        }

        int searchNavigationLvl;
        public int SearchNavigationLvl { get { return searchNavigationLvl; } set { searchNavigationLvl = value; RaisePropertyChanged(); } }
        public ICommand TraiffSearchSelectionChangedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await TarrifSearchSelectionChanged();
                });
            }
        }

        public ICommand SelectedTarrifSearchSelectionChangedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await SelectedTarrifSearchSelectionChanged();
                });
            }
        }
        int PreviousCount;
        private async Task TarrifSearchSelectionChanged()
        {
            try
            {
                IsLoading = true;
                TariffSearch SelectedItem = null;

                if (SelectedSearchResultLst != null && IsSearchVIewVisible)
                {
                    SelectedItem = SelectedSearchResultLst;
                    SelectedSearchResultLst = null;
                }

                if (SelectedItem != null)
                {
                    /*  if (SelectedItem.item_type == 3 || SelectedItem.item_type == 1)
                      {
                          SelectedItem = null;
                          return;
                      }*/
                    var last4digit = SelectedItem.hrmnzd_code.Substring(8, 4);
                    var third2digit = SelectedItem.hrmnzd_code.Substring(4, 2);
                    var fr2digit = SelectedItem.hrmnzd_code.Substring(6, 2);
                    var Sec2digit = SelectedItem.hrmnzd_code.Substring(2, 2);

                    string ParentCode;
                    bool isAllzeros = last4digit.All(c => c == '0');
                    if (!isAllzeros)
                    {
                        //SelectedHarmonizedTariffslvl2.hrmnzd_code.Remove(8, 4);
                        ParentCode = SelectedItem.hrmnzd_code.Substring(0, 8);
                    }
                    else
                    {
                        if (!fr2digit.All(c => c == '0'))
                        {
                            ParentCode = SelectedItem.hrmnzd_code.Substring(0, 6);
                        }
                        else if (!third2digit.All(c => c == '0'))
                        {
                            ParentCode = SelectedItem.hrmnzd_code.Substring(0, 4);

                        }
                        else if (!Sec2digit.All(c => c == '0'))
                        {
                            ParentCode = SelectedItem.hrmnzd_code.Substring(0, 2);

                        }
                        else
                        {
                            return;
                        }

                    }
                    /* bool isthird2digitAllzeros = third2digit.All(c => c == '0');
                     bool isfr2digitAllzeros = fr2digit.All(c => c == '0');
                     string parentItemCode;
                     if (isthird2digitAllzeros && isfr2digitAllzeros)
                     {
                         return;
                     }
                     else if (!isthird2digitAllzeros)
                     {
                         parentItemCode = SelectedHarmonizedTariffslvl2.hrmnzd_code.Substring(0, 6);
                     }
                     else
                     {
                         parentItemCode = SelectedHarmonizedTariffslvl2.hrmnzd_code.Substring(0, 8);

                     }*/

                    HarmonizedTariffslvl3 = await GetHarmonizedTariffs(ParentCode);
                    PreviousCount = ParentCode.Length;
                    if (SearchNavigationLvl == 0)
                    {
                        IsTarrifSearch = true;
                        IsSearchVIewVisible = false;
                        IsSectionView = false;
                    }
                    SearchNavigationLvl++;

                    /*  Title = SelectedSearchResultLst.Name;
               // Level3Title = Title;
                SelectedHarmonizedTariffslvl2 = null;
                IsChapterSection = false;
                IsSectionView = false;
                IsMainHarmonizedTariffs = true;
                LevelNo = 2;
                IsHarmonizedTariffs2lvl = false;

                IsHarmonizedTariffs3lvl = true;*/
                }

            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        private async Task SelectedTarrifSearchSelectionChanged()
        {
            try
            {
                IsLoading = true;
                HarmonizedTarrif SelectedItem = null;


                if (SelectedSearchTariffs != null && IsTarrifSearch)
                {
                    SelectedItem = SelectedSearchTariffs;
                    SelectedSearchTariffs = null;
                }
                if (SelectedItem != null)
                {
                    //GetMainHarmonizedTariffs(SelectedTraiffChapters.chpt_code);
                    if (SelectedItem.item_type == 3 || SelectedItem.item_type == 1)
                    {
                        SelectedItem = null;
                        return;
                    }
                    string parentItemCode;
                    if (PreviousCount == 2)

                    {
                        parentItemCode = SelectedItem.hrmnzd_code.Substring(0, 4);
                        PreviousCount = parentItemCode.Length;
                    }
                    else
                    {
                        var last4digit = SelectedItem.hrmnzd_code.Substring(8, 4);
                        var third2digit = SelectedItem.hrmnzd_code.Substring(4, 2);
                        var fr2digit = SelectedItem.hrmnzd_code.Substring(6, 2);
                        var Sec2digit = SelectedItem.hrmnzd_code.Substring(2, 2);

                        bool isAllzeros = last4digit.All(c => c == '0');
                        if (isAllzeros)
                        {
                            SelectedItem.hrmnzd_code.Remove(8, 4);
                        }
                        bool isthird2digitAllzeros = third2digit.All(c => c == '0');
                        bool isfr2digitAllzeros = fr2digit.All(c => c == '0');
                        bool isSec2digitAllzeros = Sec2digit.All(c => c == '0');


                        if (isthird2digitAllzeros && isfr2digitAllzeros && isSec2digitAllzeros)
                        {
                            return;
                        }
                        else if (!isthird2digitAllzeros)
                        {
                            parentItemCode = SelectedItem.hrmnzd_code.Substring(0, 6);
                        }

                        else
                        {
                            parentItemCode = SelectedItem.hrmnzd_code.Substring(0, 8);

                        }
                    }
                    var lst = await GetHarmonizedTariffs(parentItemCode);
                    if (lst != null && lst.Count > 0)
                    {
                        HarmonizedTariffslvl3 = lst;
                    }
                    else
                    {
                        return;
                    }
                    if (IsTarrifSearch)
                    {
                        return;
                    }
                    Title = SelectedHarmonizedTariffslvl2.Name;
                    Level3Title = Title;
                    SelectedHarmonizedTariffslvl2 = null;
                    IsChapterSection = false;
                    IsSectionView = false;
                    IsMainHarmonizedTariffs = true;
                    LevelNo = 2;
                    IsHarmonizedTariffs2lvl = false;

                    IsHarmonizedTariffs3lvl = true;
                }


            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        public ICommand TraiffSectionsSelectionChangedCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (SelectedTraiffSections != null)
                    {
                        GetTraiffChapters(SelectedTraiffSections.sect_code);
                        Title = SelectedTraiffSections.Name;
                        ChapterTitle = title;
                        SectionNotesLst = SelectedTraiffSections.notes;
                        SelectedTraiffSections = null;
                        IsChapterSection = true;
                        IsSectionView = false;
                    }
                });
            }
        }

        public ICommand TraiffChapterSelectionChangedCommand
        {
            get
            {
                return new Command(

                      execute: async () =>
                      {
                          try
                          {
                              if (SelectedTraiffChapters != null)
                              {
                                  IsChapterSection = false;
                                  IsHarmonizedTarrifs = true;
                                  //GetMainHarmonizedTariffs(SelectedTraiffChapters.chpt_code);
                                  HarmonizedTariffs = await GetHarmonizedTariffs(SelectedTraiffChapters.chpt_code);
                                  IsHarmonizedTarrifs = true;
                                  Title = SelectedTraiffChapters.Name;
                                  MainHarmonizedTitle = Title;

                                  IsSectionView = false;
                                  //IsMainHarmonizedTariffs = true;
                                  Level1Title = SelectedTraiffChapters.Name;
                                  LevelNo = 0;
                                  ChapterNotesLst = SelectedTraiffChapters.notes;

                                  SelectedTraiffChapters = null;
                                  RefreshCanExecutes();
                              }

                          }
                          catch (Exception)
                          {

                          }

                      },
            canExecute: () =>
            {
                return !IsHarmonizedTarrifs;
            });


            }
        }
        void RefreshCanExecutes()
        {
            (TraiffChapterSelectionChangedCommand as Command).ChangeCanExecute();

        }
        string level1Title;
        public string Level1Title { get { return level1Title; } set { level1Title = value; RaisePropertyChanged(); } }


        string level2Title;
        public string Level2Title { get { return level2Title; } set { level2Title = value; RaisePropertyChanged(); } }

        string level3Title;
        public string Level3Title { get { return level3Title; } set { level3Title = value; RaisePropertyChanged(); } }

        public ICommand TraiffLevel1SelectionChangedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {


                        if (SelectedHarmonizedTariffs != null)
                        {
                            //GetMainHarmonizedTariffs(SelectedTraiffChapters.chpt_code);
                            if (SelectedHarmonizedTariffs.item_type == 3 || SelectedHarmonizedTariffs.item_type == 1)
                            {
                                return;
                            }



                            var parentItemCode = SelectedHarmonizedTariffs.hrmnzd_code.Substring(0, 4);
                            HarmonizedTariffslvl2 = await GetHarmonizedTariffs(parentItemCode);
                            Title = SelectedHarmonizedTariffs.Name;
                            Level2Title = Title;
                            SelectedHarmonizedTariffs = null;
                            IsChapterSection = false;
                            IsSectionView = false;
                            IsMainHarmonizedTariffs = true;
                            LevelNo = 1;
                            IsHarmonizedTarrifs = false;
                            IsHarmonizedTariffs2lvl = true;
                        }
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        public ICommand TraiffLevel2SelectionChangedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {

                        if (SelectedHarmonizedTariffslvl2 != null)
                        {
                            //GetMainHarmonizedTariffs(SelectedTraiffChapters.chpt_code);
                            if (SelectedHarmonizedTariffslvl2.item_type == 3 || SelectedHarmonizedTariffslvl2.item_type == 1)
                            {
                                SelectedHarmonizedTariffslvl2 = null;
                                return;
                            }
                            var last4digit = SelectedHarmonizedTariffslvl2.hrmnzd_code.Substring(8, 4);
                            var third2digit = SelectedHarmonizedTariffslvl2.hrmnzd_code.Substring(4, 2);
                            var fr2digit = SelectedHarmonizedTariffslvl2.hrmnzd_code.Substring(6, 2);

                            bool isAllzeros = last4digit.All(c => c == '0');
                            if (isAllzeros)
                            {
                                SelectedHarmonizedTariffslvl2.hrmnzd_code.Remove(8, 4);
                            }
                            bool isthird2digitAllzeros = third2digit.All(c => c == '0');
                            bool isfr2digitAllzeros = fr2digit.All(c => c == '0');
                            string parentItemCode;
                            if (isthird2digitAllzeros && isfr2digitAllzeros)
                            {
                                return;
                            }
                            else if (!isthird2digitAllzeros)
                            {
                                parentItemCode = SelectedHarmonizedTariffslvl2.hrmnzd_code.Substring(0, 6);
                            }
                            else
                            {
                                parentItemCode = SelectedHarmonizedTariffslvl2.hrmnzd_code.Substring(0, 8);

                            }
                            var data = await GetHarmonizedTariffs(parentItemCode);
                            if (data == null || data.Count <= 0)
                            {
                                SelectedHarmonizedTariffslvl2 = null;
                                return;
                            }
                            HarmonizedTariffslvl3 = data;

                            Title = SelectedHarmonizedTariffslvl2.Name;
                            Level3Title = Title;
                            SelectedHarmonizedTariffslvl2 = null;
                            IsChapterSection = false;
                            IsSectionView = false;
                            IsMainHarmonizedTariffs = true;
                            LevelNo = 2;
                            IsHarmonizedTariffs2lvl = false;

                            IsHarmonizedTariffs3lvl = true;
                        }

                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        public ICommand TraiffLevel3SelectionChangedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {

                        if (SelectedHarmonizedTariffslvl3 != null)
                        {
                            //GetMainHarmonizedTariffs(SelectedTraiffChapters.chpt_code);
                            if (SelectedHarmonizedTariffslvl3.item_type == 3 || SelectedHarmonizedTariffslvl3.item_type == 1)
                            {
                                return;
                            }
                            var parentItemCode = SelectedHarmonizedTariffslvl3.hrmnzd_code.Substring(0, 8);

                            var data = await GetHarmonizedTariffs(parentItemCode);
                            if (data == null || data.Count <= 0)
                            {
                                SelectedHarmonizedTariffslvl3 = null;
                                return;
                            }
                            HarmonizedTariffslvl4 = data;
                            Title = SelectedHarmonizedTariffslvl3.Name;
                            Level3Title = Title;
                            SelectedHarmonizedTariffslvl3 = null;
                            IsChapterSection = false;
                            IsSectionView = false;
                            IsMainHarmonizedTariffs = true;
                            LevelNo = 3;
                            IsHarmonizedTariffs3lvl = false;

                            IsHarmonizedTariffs4lvl = true;
                        }

                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }


        public ICommand TraiffHarmonizedChangedCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {

                        if (SelectedMainHarmonizedTariffs != null)
                        {
                            GetSubHarmonizedTariffs(SelectedMainHarmonizedTariffs.chpt_code, SelectedMainHarmonizedTariffs.main_item_code);
                            Title = SelectedMainHarmonizedTariffs.Name;
                            SelectedMainHarmonizedTariffs = null;
                            IsChapterSection = IsSectionView = IsMainHarmonizedTariffs = false;
                            //MainHarmonizedTitle = Title;
                            IsSubHarmonizedTariffs = true;
                        }

                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }


        public ICommand DownloadTraifDataCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsSearchVIewVisible)
                    {
                        DownLoadSearchRslt();
                    }
                    else
                    {
                        await DownloadallTarriff();
                    }
                });
            }
        }

        private async Task DownloadallTarriff()
        {
            IsLoading = true;
            DownloadFile downloadFile = new DownloadFile();
            string Lang = "ar";
            if (!App.IsArabic)
            {
                Lang = "en";

            }
            else
            {
                Lang = "ar";
            }
            await downloadFile.DownloadxlFile($"{App.VatBaseUrl}/Taarefa/GetAll?lang={Lang}", _dialogService);
            IsLoading = false;
        }

        public ICommand DownloadSearcTraifDataCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await DownLoadSearchRslt();
                });
            }
        }

        private async Task DownLoadSearchRslt()
        {
            IsLoading = true;
            DownloadFile downloadFile = new DownloadFile();
            string Lang = "ar";
            if (!App.IsArabic)
            {
                Lang = "en";

            }
            else
            {
                Lang = "ar";
            }
            await downloadFile.DownloadxlFile($"{App.VatBaseUrl}/Taarefa/GetAllWithSearch?lang={Lang}&searchType={SearchBy}&searchKey={SearchKey}", _dialogService);
            IsLoading = false;
        }

        public ICommand TraiffSubHarmonizedChangedCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (e != null)
                    {
                        SelectedSubHarmonizedTariffs = e as HarmonizedTarrif;
                        IsitemDetilsVisible = true;
                    }
                });
            }
        }

        public ICommand CloseItemDetailsCommand
        {
            get
            {
                return new Command(() =>
                {
                    // if (SelectedSubHarmonizedTariffs != null)
                    {
                        SelectedSubHarmonizedTariffs = null;
                        IsitemDetilsVisible = false;
                    }
                });
            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    GoBack();

                });
            }
        }

        public void GoBack()
        {
            if (IsSearchVIewVisible)
            {
                IsSearchVIewVisible = false;
                IsSectionView = true;
                Title = AppResources.TariffSections;

            }
            else if (IsTarrifSearch)
            {
                IsSearchVIewVisible = true;
                IsTarrifSearch = false;
                SearchNavigationLvl = 0;
            }
            else if (IsSubHarmonizedTariffs)
            {
                SubHarmonizedTariffs = null;
                IsMainHarmonizedTariffs = true;
                IsChapterSection = IsSectionView = IsSubHarmonizedTariffs = false;
                Title = MainHarmonizedTitle;


            }
            /* else if (IsMainHarmonizedTariffs)
             {

                 IsChapterSection = true;
                 Title = ChapterTitle;
                 IsSectionView = IsMainHarmonizedTariffs = false;
                 MainHarmonizedTariffs = null;
             }*/
            else if (IsChapterSection)
            {
                IsChapterSection = false;
                IsSectionView = true;
                Title = AppResources.CustomsZATCAIntegrat;
                TraiffChapters = null;
                IsSectNote = false;
            }
            else if (IsHarmonizedTariffs4lvl)
            {
                // HarmonizedTariffslvl4 = new ObservableCollection<HarmonizedTarrif>();
                LevelNo = 2;
                Title = level3Title;
                IsHarmonizedTariffs4lvl = false;
                IsHarmonizedTariffs3lvl = true;
            }
            else if (LevelNo == 2)
            {
                LevelNo = 1;
                Title = level2Title;
                IsHarmonizedTariffs3lvl = false;
                IsHarmonizedTariffs2lvl = true;
            }
            else if (LevelNo == 1)
            {
                // HarmonizedTariffslvl2 = new ObservableCollection<HarmonizedTarrif>();
                LevelNo = 0;
                //IsChapterSection = true;

                Title = level1Title;
                IsHarmonizedTariffs2lvl = false;
                IsHarmonizedTarrifs = true;
            }
            else if (LevelNo == 0)
            {
                // HarmonizedTariffs = new ObservableCollection<HarmonizedTarrif>();
                LevelNo = null;
                IsHarmonizedTarrifs = false;
                IsChapterSection = true;
                Title = ChapterTitle;
                IsSectionView = IsMainHarmonizedTariffs = false;
                MainHarmonizedTariffs = null;
                IsChapterNote = false;
            }
            else
            {
                _navigationService.GoBack();
            }
        }

        public ICommand BackFromChaptersCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsChapterSection = false;
                    IsSectionView = true;
                    //  List<Task> tasks = new List<Task>();

                });
            }
        }

        public ICommand ClearSearchTextCommand
        {
            get
            {
                return new Command(() =>
                {
                    SearchKey = "";
                    IsSearchFilterVisbible = true;
                    //  List<Task> tasks = new List<Task>();

                });
            }
        }


        public async Task GetTraiffsections()
        {
            try
            {
                IsLoading = true;
                var data = await _traiffSectionServices.GetTraiffSections();
                if (data.Item2)
                {

                    var res = data.Item1.data;
                    TraiffSections = res;
                    traiffSectionsLst = res;
                }
            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        public async Task GetTraiffChapters(string scCode)
        {
            try
            {
                IsLoading = true;
                var data = await _traiffSectionServices.GetTariffChapters(scCode);
                if (data.Item2)
                {

                    var res = data.Item1.data;
                    TraiffChapters = res;
                }
            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        public async Task GetMainHarmonizedTariffs(string chptCode)
        {
            try
            {
                IsLoading = true;
                var data = await _traiffSectionServices.GetMainHarmonizedTariffs(chptCode);
                if (data.Item2)
                {

                    var res = data.Item1.data;
                    MainHarmonizedTariffs = res;
                }
            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        public async Task GetSubHarmonizedTariffs(string chptCode, string mainItemCode)
        {
            try
            {
                IsLoading = true;
                var data = await _traiffSectionServices.GetSubHarmonizedTariffs(chptCode, mainItemCode);
                if (data.Item2)
                {

                    var res = data.Item1.data;
                    SubHarmonizedTariffs = res;
                }
            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        public async Task TariffSearch()
        {
            try
            {
                IsLoading = true;
                var data = await _traiffSectionServices.Search(SearchBy, SearchKey);
                if (data.Item2)
                {

                    var res = data.Item1.data;
                    SearchResultLst = res;
                    IsSearchFilterVisbible = false;
                }
            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }


        public async Task<ObservableCollection<HarmonizedTarrif>> GetHarmonizedTariffs(string parentItemCode)
        {
            try
            {
                IsLoading = true;
                var data = await _traiffSectionServices.GetHarmonizedTarrifs(parentItemCode);
                if (data.Item2)
                {
                    if (data.Item1.code != 404)
                    {
                        var res = data.Item1.data;
                        return res;
                    }
                    else
                    {
                        return new ObservableCollection<HarmonizedTarrif>();
                    }

                }
                return new ObservableCollection<HarmonizedTarrif>();
            }
            catch (Exception)
            {
                return new ObservableCollection<HarmonizedTarrif>();
            }
            finally { IsLoading = false; }
        }

    }
}
