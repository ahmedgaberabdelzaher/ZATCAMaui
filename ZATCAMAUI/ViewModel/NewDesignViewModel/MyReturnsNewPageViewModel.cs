using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignMyReturnsNewPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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
                RaisePropertyChanged("ReturnTypeForFilter");
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
                RaisePropertyChanged("TaxTypeForFilter");
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
                    FilterLabelText = _SelectedTaxTypeForFilter.Txt30;
                    FilterOnBasisOfTaxType();

                }
                RaisePropertyChanged("SelectedTaxTypeForFilter");
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
                RaisePropertyChanged("IsListVisible");
            }
        }

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
                RaisePropertyChanged("Index");
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
                RaisePropertyChanged("SetNoDataLabelVisibilityALL");
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

                
                RaisePropertyChanged("SelectedListItem");

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
                RaisePropertyChanged("SelectedChipFilterItem");
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
                RaisePropertyChanged("ChipDataFilterlist");
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
                RaisePropertyChanged("FilterLabelText");
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
                RaisePropertyChanged("IsArabic");
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
                RaisePropertyChanged("ListToDisplay");
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
                RaisePropertyChanged("PickerModel");
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
                if (_isLoading == value) return;

                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        #endregion

        #region Constructor
        public GAZTNewDesignMyReturnsNewPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });

        }
        #endregion

        #region Method

        public void PopulateData()
        {
            MainThread.BeginInvokeOnMainThread(async() =>
            {
                if (_selectedListItem != null)
                {

                    IsLoading = true;

                    if (_selectedListItem.Open)
                    {
                        if (_selectedListItem.TaxType.Equals("ITAX") || _selectedListItem.TaxType.Equals("ZAKT"))
                        {
                            //zakat

                            if (_selectedListItem.Fbtyp.Equals("FZ12"))
                            {
                                App.IsZakatLoadingFromMyReturns = true;

                                App.selectedForm12Fbguid = _selectedListItem.Fbguid;
                                _navigationService.NavigateTo(App.ZAKATReturnDetailsView, _selectedListItem.Fbguid);


                            }
                            else if (_selectedListItem.Fbtyp.Equals("ZKTE"))
                            {

                                App.IsZakatLoadingFromMyReturns = true;

                                _navigationService.NavigateTo(App.GAZTForm5PageView, _selectedListItem.Fbguid);

                            }
                            else
                            {

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

                        if (_selectedListItem.TaxType.Equals("VATX") || _selectedListItem.TaxType.Equals("VTEP"))
                        {
                            //Vat
                            await GetVATAllReturnsAsync(_selectedListItem);
                        }
                        if (_selectedListItem.TaxType.Equals("ETAX"))
                        {
                            //ET


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
                        if (_selectedListItem.TaxType.Equals("WHTX"))
                        {
                            //WT


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
                        string messageTodisplay = string.Empty;
                        messageTodisplay = _selectedListItem.Msg;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(messageTodisplay));


                    }

                    IsLoading = false;
                }
            });
           

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

                if (SelectedReturnsVAT != null)
                {
                    if (isStatusNotValid(SelectedReturnsVAT))
                    {
                        String SelectedICRGUID = SelectedReturnsVAT.Fbguid;
                        App.ICRStatus = SelectedReturnsVAT.Stat;
                        App.VATDeclrationFbguid = SelectedReturnsVAT.Fbguid;
                        VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedReturnsVAT.Fbguid, SelectedReturnsVAT.Fbnum, App.TP.Tin, SelectedReturnsVAT.Persl);
                        PopToRootPage();
                        if (_vATDeclaration != null && _vATDeclaration.d != null)
                        {
                            _vATDeclaration.d.Fbguid = SelectedICRGUID;
                            VATDeclaration vATDeclaration = new VATDeclaration();
                            VATDeclarationD vATDeclarationD = new VATDeclarationD();
                            if (_vATDeclaration.d.ATTACHSet != null && _vATDeclaration.d.ATTACHSet.results != null && _vATDeclaration.d.ATTACHSet.results.Count > 0)
                                numberOfAttachmentComingFromServer = _vATDeclaration.d.ATTACHSet.results.Count;
                            Result5 result5 = new Result5();
                            List<Result5> lst = new List<Result5>();
                            ADRSet _aDRSet = new ADRSet();
                            lst.Add(result5);
                            vATDeclaration.d = vATDeclarationD;
                            vATDeclaration.d.ADRSet = _aDRSet;
                            vATDeclaration.d.ADRSet.results = lst;
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                _navigationService.NavigateTo(App.GAZTNewDesignVATReturnUpdatedUIPageView, _vATDeclaration);
                                // _navigationService.NavigateTo(App.VATReturnsPageViewEX, _vATDeclaration);
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                            });

                        }
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = false;
                            if (SelectedReturnsVAT.Stat == "E0020")
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZGotothePortalForVAT));
                            }
                            else
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZReturnUnderReview));
                            }
                        });

                    }
                }
                IsLoading = false;


            }
            catch (InternetException)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
            
        }

        public bool isStatusNotValid(MyReturnsResult SelectedReturnsVAT)
        {
            bool isValid = true;
            if (SelectedReturnsVAT.Stat == "E0020" || SelectedReturnsVAT.Stat == "E0057" || SelectedReturnsVAT.Stat == "E0076" || SelectedReturnsVAT.Stat == "E0077" || SelectedReturnsVAT.Stat == "E0078" || SelectedReturnsVAT.Stat == "E0089" || SelectedReturnsVAT.Stat == "E0090")
            {
                isValid = false;
            }
            return isValid;
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(() =>
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

                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                    _navigation.NavigationStack.ToList().Clear();

                });
            }
        }
        public async Task OnPageLoad()
        {
            List<MyReturnsResult> AllReturns = new List<MyReturnsResult>();
           

            try
            {
                IsLoading = true;
                MyReturns = await WebServiceManager.GAZTGetReturnData(UtilityManager.GetLanguageParameter(), App.TP.Userid);

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
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
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
                                PopToRootPage();
                            }
                        });
                    }
                }
            }
            catch (GAZTSessionExpiredException)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZYourSessionhasexpiredPleaseLoginagain));
                    PopToRootPage();
                });
            }
            catch (Exception)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    PopToRootPage();
                });
            }

            
        }

        public void FilterAllData()
        {
            try
            {
                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "ITAX" || x.TaxType == "ZAKT" || x.TaxType == "VATX" || x.TaxType == "VTEP" || x.TaxType == "ETAX" || x.TaxType == "WHTX"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.RetStatTxt == _selectedChipFilterItem.Text));
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
                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "ZAKT"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.RetStatTxt == _selectedChipFilterItem.Text));

                    }
                }
            }
            catch (Exception)
            {
                
            }

        }
        public void FilterIncomeTaxData()
        {
            try
            {
                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    //AllReturns = MyReturns.d.results;
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "ITAX"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.RetStatTxt == _selectedChipFilterItem.Text));

                    }
                }
            }
            catch (Exception)
            {
               
            }

        }
        public void FilterVatData()
        {
            try
            {

                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "VATX" || x.TaxType == "VTEP"));

                    if (_selectedChipFilterItem != null)

                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.RetStatTxt == _selectedChipFilterItem.Text));

                    }

                }

            }

            catch (Exception)

            {

            }

        }
        public void FilterETData()
        {

            try
            {

                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    //AllReturns = MyReturns.d.results;
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "ETAX"));
                    if (_selectedChipFilterItem != null)
                    {
                        if (_selectedChipFilterItem.TemplateType.Equals("Submitted"))
                        {

                            ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.StatusTxt == "Submitted"));
                            if (ListToDisplay != null)
                            {
                                foreach (var item in ListToDisplay)
                                {
                                    item.StatusMessage = "submitted";
                                }

                            }
                        }
                        if (_selectedChipFilterItem.TemplateType.Equals("UnSubmitted"))
                        {

                            ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.StatusTxt == "Non Submitted"));
                            if (ListToDisplay != null)
                            {
                                foreach (var item in ListToDisplay)
                                {
                                    item.StatusMessage = "unsubmitted";
                                }
                            }
                        }

                        if (_selectedChipFilterItem.TemplateType.Equals("OverDue"))
                        {
                            ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.StatusTxt == "Non Submitted" && x.Due == "X"));


                            if (ListToDisplay != null)
                            {
                                foreach (var item in ListToDisplay)
                                {
                                    item.StatusMessage = "overdue";
                                }
                            }

                        }

                        if (_selectedChipFilterItem.TemplateType.Equals("All"))
                        {
                            ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay);


                            if (ListToDisplay != null)
                            {
                                foreach (var item in ListToDisplay)
                                {
                                    if (item.StatusTxt == "Non Submitted")
                                    {
                                        item.StatusMessage = "unsubmitted";

                                    }
                                    if (item.StatusTxt == "Non Submitted" && item.Due == "X")
                                    {
                                        item.StatusMessage = "overdue";
                                    }
                                    if (item.StatusTxt == "Submitted")
                                    {
                                        item.StatusMessage = "submitted";
                                    }



                                }
                            }

                        }

                    }
                }
            }
            catch (Exception )
            {
            }
        }
        public void FilterWTData()
        {

            try
            {

                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    //AllReturns = MyReturns.d.results;
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "WHTX"));
                    if (_selectedChipFilterItem != null)
                    {
                        ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.RetStatTxt == _selectedChipFilterItem.Text));

                    }
                }
            }
            catch (Exception )
            {
            }
        }
        
        public void PopulateReturnTypeList()
        {


            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                var FilterValues = WebServiceManager.GAZTGetMyBillsFilterDropdownValues(App.TP.Tin, lang);
                if (FilterValues != null)
                {
                    TaxTypeForFilter = FilterValues;
                    SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();


                    var list = new List<string>();

                    foreach (MyBillsFilterDropdown dropdown in TaxTypeForFilter)
                    {
                        try
                        {
                            list.Add(dropdown.Txt30.ToUpper());
                        }
                        catch (Exception )
                        {
                           
                        }


                    }


                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = list;
                    genericPickerModel.PickerTitle = "";
                    genericPickerModel.PickerId = "MyReturns";
                    genericPickerModel.SelectedValue = SelectedTaxTypeForFilter.Txt30;

                    PickerModel = genericPickerModel;
                }

            }
            catch (Exception )
            {
              
            }


          

        }

        public void updatePicker()
        {

            var selectedFilter = new ObservableCollection<MyBillsFilterDropdown>(TaxTypeForFilter.Where(temp => temp.Txt30.Equals(PickerModel.SelectedValue.ToUpper()))).ToList();

            SelectedTaxTypeForFilter = selectedFilter.FirstOrDefault();

        }

        public async void showPickerDialog()
        {
            try
            {
                if (PickerModel != null)
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException)
            {
              
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public void PopulateDataInChips()
        {
            try
            {
                var ChipData = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "ITAX" || x.TaxType == "ZAKT" || x.TaxType == "VATX" || x.TaxType == "VTEP" || x.TaxType == "ETAX" || x.TaxType == "WHTX"))
                        .Select(x => new { x.RetStatTxt, x.StatusTxt }).Distinct().ToList();
                ChipDataFilterlist.Clear();
                if (ChipData != null && ChipData.Count() > 0)
                {
                    ChipModel model = new ChipModel();
                    foreach (var item in ChipData)
                    {
                        model.Text = item.RetStatTxt;
                        if (item.StatusTxt.ToLower().Equals("submitted"))
                        {
                            ChipDataFilterlist.Add(new ChipModel() { Text = item.RetStatTxt, TemplateType = "Submitted", ImageSource = "submited.png", TextColor = (Color)App.Current.Resources["Success"] });

                        }
                        else if (item.StatusTxt.ToLower().Equals("non submitted"))
                        {
                            ChipDataFilterlist.Add(new ChipModel() { Text = item.RetStatTxt, TemplateType = "UnSubmitted", ImageSource = "unsubmitted.png", TextColor = (Color)App.Current.Resources["Error"] });

                        }
                        else if (item.StatusTxt.ToLower().Equals("overdue"))
                        {
                            ChipDataFilterlist.Add(new ChipModel() { Text = item.RetStatTxt, TemplateType = "OverDue", ImageSource = "clockNew.png", TextColor = (Color)App.Current.Resources["Error"] });

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
            if (SelectedTaxTypeForFilter.StatementFilter == "10")
            {
                FilterAllData();
            }
            if (SelectedTaxTypeForFilter.StatementFilter == "02")
            {
                FilterZakatData();
            }
            if (SelectedTaxTypeForFilter.StatementFilter == "01")
            {
                FilterIncomeTaxData();
            }
            if (SelectedTaxTypeForFilter.StatementFilter == "06")
            {
                FilterVatData();
            }
            if (SelectedTaxTypeForFilter.StatementFilter == "07")
            {
                FilterETData();

            }
            if (SelectedTaxTypeForFilter.StatementFilter == "03")
            {
                FilterWTData();
            }

        }
        #endregion

    }
}
