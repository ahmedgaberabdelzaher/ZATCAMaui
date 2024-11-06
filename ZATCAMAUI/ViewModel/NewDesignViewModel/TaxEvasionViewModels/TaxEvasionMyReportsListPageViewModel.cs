
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{

    public class TaxEvasionMyReportsListPageViewModel : BaseViewModel
    {
        #region Properties
        public ICommand OnBackButtonClicked { get; set; }
        public List<ChipModel> _selectedChipFilterItemList = null;
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
                OnPropertyChanged("SelectedChipFilterItemList");
            }
        }
        private List<TaxEvasionReportDetails> _tERListReportbymobno;
        public List<TaxEvasionReportDetails> TERListReportbymobno
        {
            get
            {
                return _tERListReportbymobno;
            }
            set
            {
                if (_tERListReportbymobno == value) return;

                _tERListReportbymobno = value;
                OnPropertyChanged("TERListReportbymobno");
            }
        }
        private List<TaxEvasionReportDetails> _tERListReportbymobnoAll;
        public List<TaxEvasionReportDetails> TERListReportbymobnoAll
        {
            get
            {
                return _tERListReportbymobnoAll;
            }
            set
            {
                if (_tERListReportbymobnoAll == value) return;

                _tERListReportbymobnoAll = value;
                OnPropertyChanged("TERListReportbymobnoAll");
            }
        }
        private ObservableCollection<TaxEvasionReportDetails> _listToDisplay;
        public ObservableCollection<TaxEvasionReportDetails> ListToDisplay
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
                    IsReportListVisible = false;
                    IsNoReportLabelVisible = true;
                    if (_listToDisplay.Count > 0)
                    {
                        IsReportListVisible = true;
                        IsNoReportLabelVisible = false;
                    }

                }

                OnPropertyChanged("ListToDisplay");
            }
        }
        private List<TaxEvasionReportDetails> _taxEvasionReportListClosed;
        public List<TaxEvasionReportDetails> TERListReportbymobnoClosed
        {
            get
            {
                return _taxEvasionReportListClosed;
            }
            set
            {
                if (_taxEvasionReportListClosed == value) return;

                _taxEvasionReportListClosed = value;
                OnPropertyChanged("TERListReportbymobnoClosed");
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
                OnPropertyChanged("ChipDataFilterlist");
            }
        }
        public bool _isReportListVisible = false;
        public bool IsReportListVisible
        {
            get
            {
                return _isReportListVisible;
            }
            set
            {
                if (_isReportListVisible == value) return;

                _isReportListVisible = value;
                OnPropertyChanged("IsReportListVisible");
            }
        }
        public bool _IsNoReportLabelVisible = true;
        public bool IsNoReportLabelVisible
        {
            get
            {
                return _IsNoReportLabelVisible;
            }
            set
            {
                if (_IsNoReportLabelVisible == value) return;

                _IsNoReportLabelVisible = value;
                OnPropertyChanged("IsNoReportLabelVisible");
            }
        }
        private TaxEvasionReportDetails _selectedTaxEvasionListItem;
        public TaxEvasionReportDetails SelectedTaxEvasionListItem
        {
            get
            {
                return _selectedTaxEvasionListItem;
            }
            set
            {
                try
                {
                    if (_selectedTaxEvasionListItem == value) return;

                    _selectedTaxEvasionListItem = value;
                    try
                    {
                        if (_selectedTaxEvasionListItem != null)
                        {
                            passSelectedTaxEvasionItem(_selectedTaxEvasionListItem);
                        }
                    }
                    catch (Exception)
                    {
                    }

                    OnPropertyChanged("SelectedTaxEvasionListItem");
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        public TaxEvasionMyReportsListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Command(async () =>
            {
               await _navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);
            });
        }
        public void PopulateDataInChips()
        {
            try
            {
                ChipDataFilterlist = new ObservableCollection<ChipModel>()
            {
                new ChipModel(){Text =AppResources.ZTEReportReportOpen, TemplateType = AppResources.ZTEReportReportOpen, ImageSource="ic_star_border.png"},
                               new ChipModel(){Text =AppResources.ZTEReportStatusCompleted, TemplateType = AppResources.ZReportStatusClose,ImageSource = "ic_money.png"}
            };
            }
            catch (Exception)
            {
            }

        }
        public void FilterOnbasisOfChipSelectedItem()
        {
            try
            {
                if (TERListReportbymobnoAll != null)
                {
                    List<TaxEvasionReportDetails> list = new List<TaxEvasionReportDetails>();
                    try
                    {
                        ListToDisplay = new ObservableCollection<TaxEvasionReportDetails>(TERListReportbymobnoAll);
                    }
                    catch (Exception)
                    {
                    }

                    if (SelectedChipFilterItemList != null)
                    {
                        if (SelectedChipFilterItemList.Count > 0)
                        {
                            foreach (var Item in SelectedChipFilterItemList)
                            {
                                if (Item.TemplateType.Equals(AppResources.ZTEReportReportOpen))
                                {
                                    foreach (var item in TERListReportbymobno)
                                    {
                                        list.Add(item);
                                    }
                                }
                                if (Item.TemplateType.Equals(AppResources.ZReportStatusClose))
                                {
                                    foreach (var item in TERListReportbymobnoClosed)
                                    {
                                        list.Add(item);
                                    }
                                }

                            }
                            try
                            {
                                ListToDisplay = new ObservableCollection<TaxEvasionReportDetails>(list.OrderByDescending(c => c.TicketId));
                            }
                            catch (Exception)
                            {
                            }


                        }
                    }
                }
            }
            catch
            {

            }



        }
        public async Task OnPageLoad()
        {
            try
            {
                TERListReportbymobnoAll = new List<TaxEvasionReportDetails>();
                TERListReportbymobno = new List<TaxEvasionReportDetails>();
                TERListReportbymobnoClosed = new List<TaxEvasionReportDetails>();
                TaxEvasionReportsModel rootObject = new TaxEvasionReportsModel();
                TaxEvasionSendSmsModel taxEvasionSendSmsModel = new TaxEvasionSendSmsModel();
                taxEvasionSendSmsModel.mobile = App.TaxEvasionUserData.Mobile;

                rootObject = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetAllReportsByMobileNumber(taxEvasionSendSmsModel);

                await PopToRootPage();

                if (rootObject != null)
                {
                    if (rootObject.Data != null)
                    {
                        if (rootObject.Data.Opened != null)
                        {
                            if (rootObject.Data.Opened.Count() > 0)
                            {
                                rootObject.Data.Opened = rootObject.Data.Opened.OrderByDescending(c => c.TicketId).ToArray();
                                TERListReportbymobno = rootObject.Data.Opened.ToList();
                                // SetNoDataLabelVisibilityforOpen = false;
                                foreach (var item in TERListReportbymobno)
                                {
                                    TERListReportbymobnoAll.Add(item);
                                }
                            }

                        }
                        if (rootObject.Data.Closed != null)
                        {
                            if (rootObject.Data.Closed.Count() > 0)
                            {
                                rootObject.Data.Opened = rootObject.Data.Closed.OrderByDescending(c => c.TicketId).ToArray();
                                TERListReportbymobnoClosed = rootObject.Data.Closed.ToList();
                                foreach (var item in TERListReportbymobnoClosed)
                                {
                                    TERListReportbymobnoAll.Add(item);
                                }
                            }
                        }

                    }
                }
            }
            catch (GAZTException gex)
            {
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
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
                IsLoading = false;

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
            }
            catch (Exception)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
            }
        }

        public void passSelectedTaxEvasionItem(TaxEvasionReportDetails SelectedTaxEvasionReport)
        {
            try
            {
               _navigationService.NavigateTo(App.TaxEvasionReportDetailPageView, SelectedTaxEvasionReport);


            }
            catch (Exception)
            {
            }
        }
    }
}
