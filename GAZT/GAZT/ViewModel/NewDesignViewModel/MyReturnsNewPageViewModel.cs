using EGAZT.Models;
using EGAZT.Models.PaymentModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.PaymentOptions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class GAZTNewDesignMyReturnsNewPageViewModel : ViewModelBase
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

                if (_selectedListItem != null)
                {
              
                    Task.Run(async () =>
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = true;
                        });

                         if (_selectedListItem.Err2064 == "X")
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                //    await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZakatReturnsErrorMessage));
                            });
                        }
                         else {

                            if (_selectedListItem.Open)
                            {
                                if (_selectedListItem.TaxType.Equals("ITAX") || _selectedListItem.TaxType.Equals("ZAKT"))
                                {
                                    //zakat

                                    if (_selectedListItem.Fbtyp.Equals("FZ12"))
                                    {
                                        App.IsZakatLoadingFromMyReturns = true;
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            App.selectedForm12Fbguid = _selectedListItem.Fbguid;
                                            _navigationService.NavigateTo(App.ZAKATReturnDetailsView, _selectedListItem.Fbguid);
                                        });

                                    }
                                    else if (_selectedListItem.Fbtyp.Equals("ZKTE"))
                                    {

                                        App.IsZakatLoadingFromMyReturns = true;
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            _navigationService.NavigateTo(App.GAZTForm5PageView, _selectedListItem.Fbguid);
                                        });
                                    }
                                    else
                                    {

                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            await Task.Run(() =>
                                            {
                                                IsLoading = false;
                                            });
                                            // await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFormFiveTappedMessage));
                                        });
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
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await Task.Run(() =>
                                        {
                                            IsLoading = false;
                                        });
                                        //await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFormFiveTappedMessage));
                                    });

                                }
                                if (_selectedListItem.TaxType.Equals("WHTX"))
                                {
                                    //WT
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await Task.Run(() =>
                                        {
                                            IsLoading = false;
                                        });
                                        //    await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFormFiveTappedMessage));
                                    });

                                }
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Task.Run(() =>
                                    {
                                        IsLoading = false;
                                    });
                                    string messageTodisplay = string.Empty;
                                    messageTodisplay = _selectedListItem.Msg;
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(messageTodisplay));
                                });

                            }
                        }



                       

     
                    });

                }

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
        public ReturnTypes _selectedReturnTypeForFilter;
        public ReturnTypes SelectedReturnTypeForFilter
        {
            get
            {
                return _selectedReturnTypeForFilter;
            }
            set
            {
                if (_selectedReturnTypeForFilter == value) return;

                _selectedReturnTypeForFilter = value;
                if (_selectedReturnTypeForFilter != null)
                {
                    FilterLabelText = _selectedReturnTypeForFilter.TaxType;
                    FilterOnBasisOfTaxType();

                }

                RaisePropertyChanged("_selectedReturnTypeForFilter");
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
                if (_listToDisplay == value) return;

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
        public GAZTNewDesignMyReturnsNewPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });

        }
        #endregion

        #region Method

        public async Task GetVATAllReturnsAsync(MyReturnsResult SelectedReturnsVAT)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                await GetVATAllReturns(SelectedReturnsVAT);
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        private async Task GetVATAllReturns(MyReturnsResult SelectedReturnsVAT)
        {
            try
            {
                try
                {
                    await Task.Run(() =>
                    {
                        IsLoading = true;
                    });


                    await Task.Run(async () =>
                    {
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
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        _navigationService.NavigateTo(App.GAZTNewDesignVATReturnUpdatedUIPageView, _vATDeclaration);
                                        // _navigationService.NavigateTo(App.VATReturnsPageViewEX, _vATDeclaration);
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        IsLoading = false;

                                        // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                                    });

                                }
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    IsLoading = false;

                                    //await _dialogService.ShowMessage(AppResources.ZZZReturnUnderReview, AppResources.Information);
                                    if (SelectedReturnsVAT.Stat == "E0020")
                                    {
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZGotothePortalForVAT));
                                    }
                                    else
                                    {
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZReturnUnderReview));
                                    }
                                });

                            }
                        }
                    });

                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });



                }
                catch (InternetException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
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
                Device.BeginInvokeOnMainThread(() =>
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

            await Task.Run(async () =>
            {


                try
                {
                    //Task GetReturnDataTask = null;
                    //GetReturnDataTask = Task.Run(() =>
                    //{

                    MyReturns = await WebServiceManager.GAZTGetReturnData(UtilityManager.GetLanguageParameter(), App.TP.Userid);
                    //});
                    SelectedReturnTypeForFilter = ReturnTypeForFilter.FirstOrDefault();
                    //try
                    //{
                    //    if (GetReturnDataTask != null)
                    //        GetReturnDataTask.Wait();
                    //}
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                if (MessageForTheUser == AppResources.ZZInternetConnectionMessage)
                                {
                                    //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                    _navigationService.GoBack();
                                }
                                else if (MessageForTheUser == AppResources.NetworkConnectivityIssue)
                                {
                                    //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                    _navigationService.GoBack();
                                }
                                else if (MessageForTheUser == AppResources.ZYourSessionhasexpiredPleaseLoginagain)
                                {
                                    PopToRootPage();
                                }
                            });
                        }
                        // Rethrow any other exception.
                        else
                        {
                            throw;
                        }
                    }
                }
                catch (GAZTSessionExpiredException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZYourSessionhasexpiredPleaseLoginagain));
                        PopToRootPage();
                    });
                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        PopToRootPage();
                    });
                }
            });

            if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
            {
                AllReturns = MyReturns.d.results;
                ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "ITAX" || x.TaxType == "ZAKT" || x.TaxType == "VATX" || x.TaxType == "VTEP" || x.TaxType == "ETAX" || x.TaxType == "WHTX"));
            }
            else
            {

            }
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        public void FilterAllData()
        {
            try
            {
                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    //AllReturns = MyReturns.d.results;
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "ITAX" || x.TaxType == "ZAKT" || x.TaxType == "VATX" || x.TaxType == "VTEP" || x.TaxType == "ETAX" || x.TaxType == "WHTX"));
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }
        public void FilterZakatData()
        {
            try
            {
                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    //AllReturns = MyReturns.d.results;
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "ITAX" || x.TaxType == "ZAKT"));
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }
        public void FilterVatData()
        {
            try
            {

                if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
                {
                    //AllReturns = MyReturns.d.results;
                    ListToDisplay = new ObservableCollection<MyReturnsResult>(MyReturns.d.results.Where(x => x.TaxType == "VATX" || x.TaxType == "VTEP"));
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        //public void FilterIfTypeAndStausFilterSelected()
        //{
        //    try
        //    {
        //        if (_selectedChipFilterItem.TemplateType.Equals("Submitted"))
        //        {
        //           Index = 0;
        //            FilterOnBasisOfTaxType();
        //            ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.StatusTxt == "Submitted"));
        //            //ListToDisplay = new ObservableCollection<MyBills>(MyBills.Where(x => x. == Enum.GetName(typeof(BillStatus), 0)).ToList());
        //        }
        //        if (_selectedChipFilterItem.TemplateType.Equals("UnSubmitted"))
        //        {
        //            Index = 1;
        //            FilterOnBasisOfTaxType();
        //            ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.StatusTxt == "Non Submitted"));
        //            if (ListToDisplay != null)
        //            {
        //                foreach (var item in ListToDisplay)
        //                {
        //                    item.StatusMessage = "unsubmitted";
        //                }
        //            }
        //        }

        //        if (_selectedChipFilterItem.TemplateType.Equals("OverDue"))
        //        {
        //            Index = 2;
        //            FilterOnBasisOfTaxType();
        //            ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay.Where(x => x.StatusTxt == "Non Submitted" && x.Due == "X"));


        //                if (ListToDisplay != null)
        //                {
        //                    foreach (var item in ListToDisplay)
        //                    {
        //                        item.StatusMessage = "overdue";
        //                    }
        //                }

        //            }

        //        if (_selectedChipFilterItem.TemplateType.Equals("All"))
        //        {
        //            FilterOnBasisOfTaxType();
        //            ListToDisplay = new ObservableCollection<MyReturnsResult>(ListToDisplay);


        //            if (ListToDisplay != null)
        //            {
        //                foreach (var item in ListToDisplay)
        //                {
        //                    if (item.StatusTxt == "Non Submitted")
        //                    {
        //                        item.StatusMessage = "unsubmitted";

        //                    }
        //                    if (item.StatusTxt == "Non Submitted" && item.Due == "X")
        //                    {
        //                        item.StatusMessage = "overdue";
        //                    }
        //                    if (item.StatusTxt == "Submitted")
        //                    {
        //                        item.StatusMessage = "submitted";
        //                    }



        //                }
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    { }
        //    }
        public void PopulateReturnTypeList()
        {
            try
            {
                List<ReturnTypes> ReturnTypesList = new List<ReturnTypes>
                {
                    new ReturnTypes {Id = "00",TaxType = AppResources.AllReturns},
                   // new ReturnTypes {Id = "01",TaxType = AppResources.ZakatnewUi},
                    new ReturnTypes {Id = "01",TaxType = AppResources.ZakatandIncomeTax},
                    new ReturnTypes {Id = "02",TaxType = AppResources.VatReturns},
                    new ReturnTypes {Id = "03",TaxType = AppResources.ETReturns},
                    new ReturnTypes {Id = "04",TaxType = AppResources.ZZWithholding},
            };
                ReturnTypeForFilter = new List<ReturnTypes>();
                ReturnTypeForFilter = ReturnTypesList;
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
                new ChipModel(){Text =AppResources.Submitted, TemplateType = "Submitted", ImageSource="submited.png"},
                new ChipModel(){Text =AppResources.OverDue, TemplateType = "OverDue",ImageSource = "clockNew.png"},
                new ChipModel(){Text =AppResources.UnSubmitted, TemplateType = "UnSubmitted",ImageSource = "unsubmitted.png"},
                //new ChipModel(){Text =AppResources.All, TemplateType = "All",ImageSource = "clockNew.png"},
               };

        }
        public void FilterOnBasisOfTaxType()
        {
            if (_selectedReturnTypeForFilter.Id == "00")
            {
                FilterAllData();
            }
            if (_selectedReturnTypeForFilter.Id == "01")
            {
                FilterZakatData();
            }
            if (_selectedReturnTypeForFilter.Id == "02")
            {
                FilterVatData();
            }
            if (_selectedReturnTypeForFilter.Id == "03")
            {
                FilterETData();

            }
            if (_selectedReturnTypeForFilter.Id == "04")
            {
                FilterWTData();
            }

        }
        #endregion

    }
}
