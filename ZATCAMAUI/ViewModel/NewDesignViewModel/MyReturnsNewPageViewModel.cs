

using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
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

                if (_selectedListItem != null)
                {

                    Task.Run(async () =>
                    {
                        IsLoading = true;

                        if (_selectedListItem.error2064 == "X")
                        {

                           MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZakatReturnsErrorMessage));
                            });
                        }
                        else
                        {

                            if (_selectedListItem.isOpen)
                            {
                                if (_selectedListItem.taxType.Equals("ITAX") || _selectedListItem.taxType.Equals("ZAKT"))
                                {
                                    //zakat

                                    if (_selectedListItem.formBundleType.Equals("FZ12"))
                                    {
                                        App.IsZakatLoadingFromMyReturns = true;
                                       MainThread.BeginInvokeOnMainThread(() =>
                                        {
                                            App.selectedForm12Fbguid = _selectedListItem.formBundleGUID;
                                            _navigationService.NavigateTo(App.ZAKATReturnDetailsView, _selectedListItem.formBundleGUID);
                                        });

                                    }
                                    else if (_selectedListItem.formBundleType.Equals("ZKTE"))
                                    {

                                        App.IsZakatLoadingFromMyReturns = true;
                                       MainThread.BeginInvokeOnMainThread(() =>
                                        {
                                            _navigationService.NavigateTo(App.GAZTForm5PageView, _selectedListItem.formBundleGUID);
                                        });
                                    }
                                    else
                                    {

                                       MainThread.BeginInvokeOnMainThread(async () =>
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
                                        });
                                    }
                                }

                                if (_selectedListItem.taxType.Equals("VATX") || _selectedListItem.taxType.Equals("VTEP"))
                                {
                                    //Vat
                                    await GetVATAllReturnsAsync(_selectedListItem);
                                }
                                if (_selectedListItem.taxType.Equals("ETAX"))
                                {
                                    //ET
                                   MainThread.BeginInvokeOnMainThread(async () =>
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
                                    });

                                }
                                if (_selectedListItem.taxType.Equals("WHTX"))
                                {
                                    //WT
                                   MainThread.BeginInvokeOnMainThread(async () =>
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
                                    });

                                }
                            }
                            else
                            {
                               MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    IsLoading = false;
                                    string messageTodisplay = string.Empty;
                                    messageTodisplay = _selectedListItem.message;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(messageTodisplay));
                                });

                            }
                        }

                    });

                }

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
        public GAZTNewDesignMyReturnsNewPageViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService,dialogService)
        {
           

        }
        #endregion

        #region Method

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
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = false;

                            //await _dialogService.ShowMessage(AppResources.ZZZReturnUnderReview, AppResources.Information);
                            if (SelectedReturnsVAT.userStatus == "E0020")
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZGotothePortalForVAT));
                            }
                            else
                            {
                                if (SelectedReturnsVAT.CR2215GoLive != null && SelectedReturnsVAT.CR2215GoLive == "X")
                                {
                                    MessagingCenter.Subscribe<App, string>(this, "OnlyAddAttachments", async (sender, arg) => {
                                        IsLoading = true;
                                        await GetVatAllReturnsForSelectedItem(selectedReturnsVATItem, true);
                                        MessagingCenter.Unsubscribe<App, string>(this, "OnlyAddAttachments");
                                    });
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZReturnUnderReviewAddAttachments));
                                }
                                else
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
                    //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
            
        }

        public async Task GetVatAllReturnsForSelectedItem(MyReturnsResult SelectedReturnsVAT, bool navigateToAttachments)
        {
            String SelectedICRGUID = SelectedReturnsVAT.formBundleGUID;
            App.ICRStatus = SelectedReturnsVAT.userStatus;
            App.VATDeclrationFbguid = SelectedReturnsVAT.formBundleGUID;
            VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedReturnsVAT.formBundleGUID, SelectedReturnsVAT.formBundleNumber, App.TP.TIN, SelectedReturnsVAT.periodKey);
            IsLoading = false;
            PopToRootPage();
            if (_vATDeclaration != null && _vATDeclaration.data != null)
            {
                _vATDeclaration.data.Fbguid = SelectedICRGUID;
                VATDeclaration vATDeclaration = new VATDeclaration();
                VATDeclarationD vATDeclarationD = new VATDeclarationD();
                if (_vATDeclaration.data.ATTACHSet != null && _vATDeclaration.data.ATTACHSet != null && _vATDeclaration.data.ATTACHSet.Count > 0)
                    numberOfAttachmentComingFromServer = _vATDeclaration.data.ATTACHSet.Count;
                // Result5 result5 = new Result5();
                //List<Result5> lst = new List<Result5>();
                //ADRSet _aDRSet = new ADRSet();
                //lst.Add(result5);
                vATDeclaration.data = vATDeclarationD;
                vATDeclaration.data.ADRSet = new List<Result5>();
                //  vATDeclaration.data.ADRSet = lst;
               MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (navigateToAttachments)
                    {
                        _vATDeclaration.data.Cr2215 = SelectedReturnsVAT.CR2215GoLive;
                        // MopupService.Instance.PushAsync(new VATDeclarationAttachmentPageView(_vATDeclaration));
                        //MessagingCenter.Send<Object, string>(this, "IsCR2215AttachmentEnable", SelectedReturnsVAT.Cr2215);
                        MopupService.Instance.PushAsync(new MoreOptionsNote(_vATDeclaration));

                    }
                    else
                        _navigationService.NavigateTo(App.GAZTNewDesignVATReturnUpdatedUIPageView, _vATDeclaration);
                    // _navigationService.NavigateTo(App.VATReturnsPageViewEX, _vATDeclaration);
                });
            }
            else
            {
               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                });

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
                    //_navigationService.NavigateTo(App.SFLoginPageView);
                    //_navigation.NavigationStack.ToList().Clear();

                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        public async Task OnPageLoad()
        {
            List<MyReturnsResult> AllReturns = new List<MyReturnsResult>();
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            try
            {
                MyReturns = await WebServiceManager.GAZTGetReturnData(App.TP.TIN);
                SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();
                
            }
            catch (AggregateException ae)
            {
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
                                //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.NetworkConnectivityIssue)
                            {
                                //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
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
                    //await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZYourSessionhasexpiredPleaseLoginagain));
                    PopToRootPage();
                });
            }
            catch (Exception )
            {
                
                
               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    PopToRootPage();
                });
            }

            IsLoading = false;
        }

        public void FilterAllData()
        {
            try
            {
                if (MyReturns != null && MyReturns.ICRReturns != null && MyReturns.ICRReturns.Count > 0)
                {
                    //AllReturns = MyReturns.d.results;
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "ITAX" || x.taxType == "ZAKT" || x.taxType == "VATX" || x.taxType == "VTEP" || x.taxType == "ETAX" || x.taxType == "WHTX"));
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

        public void FilterZakatData()
        {
            try
            {
                if (MyReturns != null && MyReturns.ICRReturns != null && MyReturns.ICRReturns.Count > 0)
                {
                    //AllReturns = MyReturns.d.results;
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
                    //AllReturns = MyReturns.d.results;
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
                    //AllReturns = MyReturns.d.results;
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
                    //AllReturns = MyReturns.d.results;
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.ICRReturns.Where(x => x.taxType == "WHTX"));
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

        public void PopulateReturnTypeList()
        {


            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                var FilterValues = WebServiceManager.GAZTGetMyBillsFilterDropdownValues(App.TP.TIN, lang);
                if (FilterValues != null)
                {
                    TaxTypeForFilter = FilterValues;
                    SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();


                    var list = new List<string>();

                    foreach (MyBillsFilterDropdown dropdown in TaxTypeForFilter)
                    {
                        try
                        {
                            list.Add(dropdown.revenueTypeDescription.ToUpper());
                        }
                        catch (Exception ex)
                        {
                            
                            
                        }


                    }


                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = list;
                    genericPickerModel.PickerTitle = "";
                    genericPickerModel.PickerId = "MyReturns";
                    genericPickerModel.SelectedValue = SelectedTaxTypeForFilter.revenueTypeDescription;

                    PickerModel = genericPickerModel;
                }

            }
            catch (Exception ex)
            {
                
                
            }


        }

        public void updatePicker()
        {

            var selectedFilter = new ObservableCollection<MyBillsFilterDropdown>(TaxTypeForFilter.Where(temp => temp.revenueTypeDescription.Equals(PickerModel.SelectedValue.ToUpper()))).ToList();

            SelectedTaxTypeForFilter = selectedFilter.FirstOrDefault();

        }

        public async void showPickerDialog()
        {
            try
            {
                if (PickerModel != null)
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException ex)
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

                        //ChipDataFilterlist.Add(model);
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
