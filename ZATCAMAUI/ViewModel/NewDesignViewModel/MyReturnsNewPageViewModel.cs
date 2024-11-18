

using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignMyReturnsNewPageViewModel : BaseViewModel
    {
        public ICommand OnVerifyButtonClicked { get; set; }
        public MyReturnsRootObject MyReturns { get; set; }
        public ICommand OnBackButtonClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }


        public static int numberOfAttachmentComingFromServer = 0;
        #region Property
        public List<ReturnTypes> _returnTypeForFilter;
        public List<ReturnTypes> ReturnTypeForFilter
        {
            get
            {
                return _returnTypeForFilter;
            }
            set
            {
                if (_returnTypeForFilter == value) return;
                _returnTypeForFilter = value;
                OnPropertyChanged("ReturnTypeForFilter");
            }
        }

        public List<MyBillsFilterDropdown> _TaxTypeForFilter = null;
        public List<MyBillsFilterDropdown> TaxTypeForFilter
        {
            get
            {
                return _TaxTypeForFilter;
            }
            set
            {
                if (_TaxTypeForFilter == value) return;

                _TaxTypeForFilter = value;
                OnPropertyChanged("TaxTypeForFilter");
            }
        }

        public MyBillsFilterDropdown _SelectedTaxTypeForFilter = null;
        public MyBillsFilterDropdown SelectedTaxTypeForFilter
        {
            get
            {
                return _SelectedTaxTypeForFilter;
            }
            set
            {

                _SelectedTaxTypeForFilter = value;
                if (_SelectedTaxTypeForFilter != null)
                {
                    FilterLabelText = _SelectedTaxTypeForFilter.revenueTypeDescription;
                    FilterOnBasisOfTaxType();

                }
                OnPropertyChanged("SelectedTaxTypeForFilter");
            }

        }
        public bool _isListVisible = false;
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
                OnPropertyChanged("IsListVisible");
            }
        }

        Color selectedChipTextColor;
        public Color SelectedChipTextColor { get { return selectedChipTextColor; } set { selectedChipTextColor = value; OnPropertyChanged(); } }

        Color selectedChipBackground;
        public Color SelectedChipBackground { get { return selectedChipBackground; } set { selectedChipBackground = value; OnPropertyChanged(); } }

        public int _index;
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (_index == value) return;

                _index = value;
                OnPropertyChanged("Index");
            }
        }
        public bool _setNoDataLabelVisibilityALL = true;
        public bool SetNoDataLabelVisibilityALL
        {
            get
            {
                return _setNoDataLabelVisibilityALL;
            }
            set
            {
                if (_setNoDataLabelVisibilityALL == value) return;

                _setNoDataLabelVisibilityALL = value;
                OnPropertyChanged("SetNoDataLabelVisibilityALL");
            }
        }
        private MyReturnsResult _selectedListItem = null;
        public MyReturnsResult SelectedListItem
        {
            get
            {
                return _selectedListItem;
            }
            set
            {

                _selectedListItem = value;


                OnPropertyChanged("SelectedListItem");

            }
        }
        public ChipModel _selectedChipFilterItem = null;
        public ChipModel SelectedChipFilterItem
        {
            get
            {
                return _selectedChipFilterItem;
            }
            set
            {
                if (_selectedChipFilterItem == value) return;

                _selectedChipFilterItem = value;
                if (_selectedChipFilterItem != null)
                {

                    if (_selectedChipFilterItem.TemplateType.Equals("OverDue"))
                    {
                        Index = 2;
                    }
                    if (_selectedChipFilterItem.TemplateType.Equals("UnSubmitted"))
                    {
                        Index = 1;
                    }
                    if (_selectedChipFilterItem.TemplateType.Equals("Submitted"))
                    {
                        Index = 0;
                    }
                    FilterOnBasisOfTaxType();

                }
                OnPropertyChanged("SelectedChipFilterItem");
            }
        }
        public ObservableCollection<ChipModel> _chipDataFilterlist = new ObservableCollection<ChipModel>();
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
                OnPropertyChanged("ChipDataFilterlist");
            }
        }





        public string _filterLabelText;
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
                OnPropertyChanged("FilterLabelText");
            }
        }

        private bool _isArabic = false;
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                if (_isArabic == value) return;

                _isArabic = value;
                OnPropertyChanged("IsArabic");
            }
        }

        private ObservableCollection<MyReturnsResult> _listToDisplay = null;
        public ObservableCollection<MyReturnsResult> ListToDisplay
        {
            get
            {
                return _listToDisplay;
            }
            set
            {

                _listToDisplay = value;
                if (_listToDisplay != null)
                {
                    if (_listToDisplay.Count != 0)
                    {

                        IsListVisible = true;
                        SetNoDataLabelVisibilityALL = false;
                    }
                    else
                    {
                        IsListVisible = false;
                        SetNoDataLabelVisibilityALL = true;
                    }

                }
                else
                {
                    IsListVisible = false;
                    SetNoDataLabelVisibilityALL = true;

                }
                //Sum(emp => emp.Salary);
                OnPropertyChanged("ListToDisplay");
            }
        }

        private MyReturnsResult _selectedReturnsVATItem;
        public MyReturnsResult selectedReturnsVATItem
        {
            get
            {
                return _selectedReturnsVATItem;
            }
            set
            {
                if (_selectedReturnsVATItem == value) return;

                _selectedReturnsVATItem = value;
                OnPropertyChanged("selectedReturnsVATItem");

            }
        }
        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get { return _pickerModel; }
            set
            {
                if (_pickerModel == value) return;

                _pickerModel = value;
                OnPropertyChanged("PickerModel");
            }
        }


        #endregion

        #region Constructor
        public GAZTNewDesignMyReturnsNewPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {


        }
        #endregion

        #region Commands
        public ICommand OnAppearingMyReturnsPageCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    try
                    {
                        IsLoading = true;
                        await PopulateReturnTypeList();
                        SelectedChipFilterItem = null;
                        MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                        {
                            PickerModel = arg;
                            updatePicker();
                        });

                        VATDeclarationAttachmentPageViewModel.isToBeFilled = true;

                        await OnPageLoad();
                        PopulateDataInChips();

                        SelectedChipFilterItem = null;
                        // FilterAllData();
                        if (Index == 0)
                        {
                            SelectedChipFilterItem = ChipDataFilterlist.Where(x => x.TemplateType.Equals("Submitted")).FirstOrDefault();
                            SelectedChipFilterItem = ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("Submitted")).FirstOrDefault();

                        }
                        if (Index == 1)
                        {
                            SelectedChipFilterItem = ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                            SelectedChipFilterItem = ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                        }
                        if (Index == 2)
                        {
                            SelectedChipFilterItem = ChipDataFilterlist.Where(x => x.TemplateType.Equals("OverDue")).FirstOrDefault();
                            SelectedChipFilterItem = ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("OverDue")).FirstOrDefault();

                        }
                        if (Index == 5)
                        {
                            SelectedTaxTypeForFilter = TaxTypeForFilter.Where(x => x.statementFilter == "02").FirstOrDefault();
                            SelectedChipFilterItem = ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                            SelectedChipFilterItem = ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                        }
                        if (Index == 6)
                        {

                            SelectedTaxTypeForFilter = TaxTypeForFilter.Where(x => x.statementFilter == "06").FirstOrDefault();

                            SelectedChipFilterItem = ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                            SelectedChipFilterItem = ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                        }

                        IsLoading = false;
                    }
                    catch (Exception)
                    {



                    }

                });
            }
        }

        public ICommand ItemSelectedCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    if (SelectedListItem != null)
                    {
                        try
                        {
                            IsLoading = true;
                            await LoadSelctedListItem();
                            SelectedListItem = null;
                            IsLoading = false;
                        }
                        catch (Exception)
                        {

                        }

                    }

                });
            }
        }

        public ICommand ChipGroupSelectedCommand
        {
            get
            {
                return new Command(_ =>
                {
                    try
                    {

                        if (SelectedChipFilterItem.Text == AppResources.UnSubmitted)
                        {
                            SelectedChipTextColor = (Color)App.Current.Resources["Error"];
                            SelectedChipBackground = (Color)App.Current.Resources["ErrorBg"];


                        }
                        else if (SelectedChipFilterItem.Text == AppResources.OverDue)
                        {
                            SelectedChipTextColor = (Color)App.Current.Resources["Error"];
                            SelectedChipBackground = (Color)App.Current.Resources["ErrorBg"];

                        }
                        else if (SelectedChipFilterItem.Text == AppResources.Submitted)
                        {
                            SelectedChipTextColor = (Color)App.Current.Resources["Success"];
                            SelectedChipBackground = (Color)App.Current.Resources["SuccessBg"];

                        }
                    }
                    catch (Exception)
                    {


                    }
                });
            }
        }

        public ICommand FilterLabelTextCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    await ShowPickerDialog();
                });
            }
        }
        #endregion Commands

        #region Method

        private async Task LoadSelctedListItem()
        {
            IsLoading = true;

            if (SelectedListItem.error2064 == "X")
            {

                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZakatReturnsErrorMessage));
            }
            else
            {

                if (SelectedListItem.isOpen)
                {
                    if (SelectedListItem.taxType.Equals("ITAX") || SelectedListItem.taxType.Equals("ZAKT"))
                    {
                        //zakat

                        if (SelectedListItem.formBundleType.Equals("FZ12"))
                        {
                            App.IsZakatLoadingFromMyReturns = true;
                            App.selectedForm12Fbguid = SelectedListItem.formBundleGUID;
                            await _navigationService.NavigateTo(App.ZAKATReturnDetailsView, SelectedListItem.formBundleGUID);

                        }
                        else if (SelectedListItem.formBundleType.Equals("ZKTE"))
                        {

                            App.IsZakatLoadingFromMyReturns = true;
                            await _navigationService.NavigateTo(App.GAZTForm5PageView, SelectedListItem.formBundleGUID);
                        }
                        else
                        {

                            IsLoading = false;
                            var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.ZZFormFiveTappedMessage);
                            if (App.IsArabic)
                            {
                                VisitPortalPopup.OnGotoPortal = () =>
                                {

                                    Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlAR);

                                };
                            }
                            else
                            {
                                VisitPortalPopup.OnGotoPortal = () =>
                                {

                                    Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlEN);

                                };
                            }
                            await MopupService.Instance.PushAsync(VisitPortalPopup);
                        }
                    }

                    if (SelectedListItem.taxType.Equals("VATX") || SelectedListItem.taxType.Equals("VTEP"))
                    {
                        //Vat
                        await GetVATAllReturnsAsync(SelectedListItem);
                    }
                    if (SelectedListItem.taxType.Equals("ETAX"))
                    {
                        //ET
                        IsLoading = false;

                        var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.ZZFormFiveTappedMessage);
                        if (App.IsArabic)
                        {
                            VisitPortalPopup.OnGotoPortal = () =>
                            {
                                Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlAR);

                            };
                        }
                        else
                        {
                            VisitPortalPopup.OnGotoPortal = () =>
                            {

                                Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlEN);

                            };
                        }

                        await MopupService.Instance.PushAsync(VisitPortalPopup);

                    }
                    if (SelectedListItem.taxType.Equals("WHTX"))
                    {
                        //WT
                        IsLoading = false;

                        var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.ZZFormFiveTappedMessage);
                        if (App.IsArabic)
                        {
                            VisitPortalPopup.OnGotoPortal = () =>
                            {

                                Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlAR);

                            };
                        }
                        else
                        {
                            VisitPortalPopup.OnGotoPortal = () =>
                            {

                                Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlEN);

                            };
                        }

                        await MopupService.Instance.PushAsync(VisitPortalPopup);

                    }
                }

                else
                {
                    IsLoading = false;
                    string messageTodisplay = string.Empty;
                    messageTodisplay = SelectedListItem.message;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(messageTodisplay));

                }
            }

            IsLoading = false;
        }

        public async Task GetVATAllReturnsAsync(MyReturnsResult SelectedReturnsVAT)
        {
            IsLoading = true;
            await GetVATAllReturns(SelectedReturnsVAT);
            IsLoading = false;
        }
        private async Task GetVATAllReturns(MyReturnsResult SelectedReturnsVAT)
        {

            try
            {
                IsLoading = true;
                selectedReturnsVATItem = SelectedReturnsVAT;
                if (SelectedReturnsVAT != null)
                {
                    if (isStatusNotValid(SelectedReturnsVAT))
                    {
                        await GetVatAllReturnsForSelectedItem(SelectedReturnsVAT, false);
                    }
                    else
                    {
                        IsLoading = false;
                        if (SelectedReturnsVAT.userStatus == "E0020")
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZGotothePortalForVAT));
                        }
                        else
                        {
                            if (SelectedReturnsVAT.CR2215GoLive != null && SelectedReturnsVAT.CR2215GoLive == "X")
                            {
                                MessagingCenter.Subscribe<App, string>(this, "OnlyAddAttachments", async (sender, arg) =>
                                {
                                    IsLoading = true;
                                    await GetVatAllReturnsForSelectedItem(selectedReturnsVATItem, true);
                                    MessagingCenter.Unsubscribe<App, string>(this, "OnlyAddAttachments");
                                });
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZReturnUnderReviewAddAttachments));
                            }
                            else
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZReturnUnderReview));
                        }

                    }
                }
                IsLoading = false;


            }
            catch (InternetException)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                _navigationService.GoBack();
            }

        }

        public async Task GetVatAllReturnsForSelectedItem(MyReturnsResult SelectedReturnsVAT, bool navigateToAttachments)
        {
            string SelectedICRGUID = SelectedReturnsVAT.formBundleGUID;
            App.ICRStatus = SelectedReturnsVAT.userStatus;
            App.VATDeclrationFbguid = SelectedReturnsVAT.formBundleGUID;
            VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedReturnsVAT.formBundleGUID, SelectedReturnsVAT.formBundleNumber, App.TP.TIN, SelectedReturnsVAT.periodKey);
            IsLoading = false;
            await PopToRootPage();
            if (_vATDeclaration != null && _vATDeclaration.data != null)
            {
                _vATDeclaration.data.Fbguid = SelectedICRGUID;
                VATDeclaration vATDeclaration = new VATDeclaration();
                VATDeclarationD vATDeclarationD = new VATDeclarationD();
                if (_vATDeclaration.data.ATTACHSet != null && _vATDeclaration.data.ATTACHSet != null && _vATDeclaration.data.ATTACHSet.Count > 0)
                    numberOfAttachmentComingFromServer = _vATDeclaration.data.ATTACHSet.Count;
                vATDeclaration.data = vATDeclarationD;
                vATDeclaration.data.ADRSet = new List<Result5>();
                if (navigateToAttachments)
                {
                    _vATDeclaration.data.Cr2215 = SelectedReturnsVAT.CR2215GoLive;
                    await MopupService.Instance.PushAsync(new MoreOptionsNote(_vATDeclaration));

                }
                else
                    await _navigationService.NavigateTo(App.GAZTNewDesignVATReturnUpdatedUIPageView, _vATDeclaration);
            }
            else
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
            }

        }





        public bool isStatusNotValid(MyReturnsResult SelectedReturnsVAT)
        {
            bool isValid = true;
            if (SelectedReturnsVAT.userStatus == "E0020" || SelectedReturnsVAT.userStatus == "E0057" || SelectedReturnsVAT.userStatus == "E0076" || SelectedReturnsVAT.userStatus == "E0077" || SelectedReturnsVAT.userStatus == "E0078" || SelectedReturnsVAT.userStatus == "E0089" || SelectedReturnsVAT.userStatus == "E0090")
            {
                isValid = false;
            }
            return isValid;
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.SFLoginPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                await _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                _navigation.NavigationStack.ToList().Clear();
            }
        }
        public async Task OnPageLoad()
        {
            try
            {
                IsLoading = true;
                List<MyReturnsResult> AllReturns = new List<MyReturnsResult>();

                MyReturns = await WebServiceManager.GAZTGetReturnData(App.TP.TIN);

                SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();

                IsLoading = false;
            }
            catch (AggregateException ae)
            {
                IsLoading = false;
                foreach (var gex in ae.InnerExceptions)
                {
                    // Handle the GAZT custom exception.
                    if (gex is GAZTException)
                    {
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }
                        if (MessageForTheUser == AppResources.ZZInternetConnectionMessage)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            _navigationService.GoBack();
                        }
                        else if (MessageForTheUser == AppResources.NetworkConnectivityIssue)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            _navigationService.GoBack();
                        }
                        else if (MessageForTheUser == AppResources.ZYourSessionhasexpiredPleaseLoginagain)
                        {
                            await PopToRootPage();
                        }
                    }
                }
            }
            catch (GAZTSessionExpiredException)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZYourSessionhasexpiredPleaseLoginagain));
                await PopToRootPage();
            }
            catch (Exception)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                await PopToRootPage();
            }

            IsLoading = false;
        }

        public void FilterAllData()
        {
            try
            {
                if (MyReturns != null && MyReturns.ICRReturns != null && MyReturns.ICRReturns.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "ITAX" || x.taxType == "ZAKT" || x.taxType == "VATX" || x.taxType == "VTEP" || x.taxType == "ETAX" || x.taxType == "WHTX"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.returnStatusDescription == _selectedChipFilterItem.Text));
                    }


                }
            }
            catch (Exception)
            {


            }
        }

        public void FilterZakatData()
        {
            try
            {
                if (MyReturns != null && MyReturns.ICRReturns != null && MyReturns.ICRReturns.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "ZAKT"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.returnStatusDescription == _selectedChipFilterItem.Text));
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }

        public void FilterIncomeTaxData()
        {
            try
            {
                if (MyReturns != null && MyReturns.ICRReturns != null && MyReturns.ICRReturns.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "ITAX"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.returnStatusDescription == _selectedChipFilterItem.Text));
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        public void FilterVatData()
        {
            try
            {
                if (MyReturns != null && MyReturns.ICRReturns != null && MyReturns.ICRReturns.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "VATX" || x.taxType == "VTEP"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.returnStatusDescription == _selectedChipFilterItem.Text));
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        public void FilterETData()
        {
            try
            {
                if (MyReturns != null && MyReturns.ICRReturns != null && MyReturns.ICRReturns.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "ETAX"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.returnStatusDescription == _selectedChipFilterItem.Text));

                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        public void FilterWTData()
        {
            try
            {
                if (MyReturns != null && MyReturns.ICRReturns != null && MyReturns.ICRReturns.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "WHTX"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.returnStatusDescription == _selectedChipFilterItem.Text));
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        public async Task PopulateReturnTypeList()
        {


            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                var FilterValues = await WebServiceManager.GAZTGetMyBillsFilterDropdownValues(App.TP.TIN, lang);
                if (FilterValues != null)
                {
                    TaxTypeForFilter = FilterValues;
                    SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();


                    var list = new List<string>();

                    foreach (MyBillsFilterDropdown dropdown in TaxTypeForFilter)
                    {
                        list.Add(dropdown.revenueTypeDescription.ToUpper());
                    }


                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = list;
                    genericPickerModel.PickerTitle = "";
                    genericPickerModel.PickerId = "MyReturns";
                    genericPickerModel.SelectedValue = SelectedTaxTypeForFilter.revenueTypeDescription;

                    PickerModel = genericPickerModel;
                }

            }
            catch (Exception)
            {


            }


        }

        public void updatePicker()
        {

            var selectedFilter = new ObservableCollection<MyBillsFilterDropdown>(TaxTypeForFilter.Where(temp => temp.revenueTypeDescription.Equals(PickerModel.SelectedValue.ToUpper()))).ToList();

            SelectedTaxTypeForFilter = selectedFilter.FirstOrDefault();

        }

        public async Task ShowPickerDialog()
        {
            try
            {
                if (PickerModel != null)
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }
        public void PopulateDataInChips()
        {
            try
            {
                var ChipData = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "ITAX" || x.taxType == "ZAKT" || x.taxType == "VATX" || x.taxType == "VTEP" || x.taxType == "ETAX" || x.taxType == "WHTX"))
                        .Select(x => new { x.returnStatusDescription, x.statusDescription }).Distinct().ToList();
                ChipDataFilterlist.Clear();
                if (ChipData != null && ChipData.Count() > 0)
                {
                    ChipModel model = new ChipModel();
                    foreach (var item in ChipData)
                    {
                        model.Text = item.returnStatusDescription;
                        if (item.statusDescription.ToLower().Equals("submitted"))
                        {
                            ChipDataFilterlist.Add(new ChipModel() { Text = item.returnStatusDescription, TemplateType = "Submitted", ImageSource = "submited.png", TextColor = (Color)App.Current.Resources["Success"] });
                        }
                        else if (item.statusDescription.ToLower().Equals("non submitted"))
                        {
                            ChipDataFilterlist.Add(new ChipModel() { Text = item.returnStatusDescription, TemplateType = "UnSubmitted", ImageSource = "unsubmitted.png", TextColor = (Color)App.Current.Resources["Error"] });
                        }
                        else if (item.statusDescription.ToLower().Equals("overdue"))
                        {
                            ChipDataFilterlist.Add(new ChipModel() { Text = item.returnStatusDescription, TemplateType = "OverDue", ImageSource = "clockNew.png", TextColor = (Color)App.Current.Resources["Error"] });
                        }

                    }
                }
            }
            catch (Exception)
            {

            }

        }


        public void FilterOnBasisOfTaxType()
        {
            if (SelectedTaxTypeForFilter.statementFilter == "10")
            {
                FilterAllData();
            }
            if (SelectedTaxTypeForFilter.statementFilter == "02")
            {
                FilterZakatData();
            }
            if (SelectedTaxTypeForFilter.statementFilter == "01")
            {
                FilterIncomeTaxData();
            }
            if (SelectedTaxTypeForFilter.statementFilter == "06")
            {
                FilterVatData();
            }
            if (SelectedTaxTypeForFilter.statementFilter == "07")
            {
                FilterETData();

            }
            if (SelectedTaxTypeForFilter.statementFilter == "03")
            {
                FilterWTData();
            }

        }
        #endregion

    }
}
