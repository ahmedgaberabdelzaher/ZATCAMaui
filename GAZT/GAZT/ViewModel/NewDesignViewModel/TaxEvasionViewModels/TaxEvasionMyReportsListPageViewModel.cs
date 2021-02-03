using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
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

namespace EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{
    [Preserve(AllMembers = true)]
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
                RaisePropertyChanged("SelectedChipFilterItemList");
            }
        }
        private List<TaxEvasionReportDetails>  _tERListReportbymobno;
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
                RaisePropertyChanged("TERListReportbymobno");
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
                RaisePropertyChanged("TERListReportbymobnoAll");
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
                if (_listToDisplay!=null)
                {
                    IsReportListVisible = false;
                       IsNoReportLabelVisible = true;
                    if (_listToDisplay.Count > 0)
                    {
                        IsReportListVisible = true;
                        IsNoReportLabelVisible = false;
                    }
                   
                }
                
                RaisePropertyChanged("ListToDisplay");
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
                RaisePropertyChanged("TERListReportbymobnoClosed");
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
                RaisePropertyChanged("IsReportListVisible");
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
                RaisePropertyChanged("IsNoReportLabelVisible");
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
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }
                    
                    RaisePropertyChanged("SelectedTaxEvasionListItem");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            }
        }
        #endregion

        public TaxEvasionMyReportsListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);
                //_navigationService.GoBack();
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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
                                    //ListToDisplay = new ObservableCollection<TaxEvasionReportDetails>(TERListReportbymobno);
                                }
                                if (Item.TemplateType.Equals(AppResources.ZReportStatusClose))
                                {
                                    foreach (var item in TERListReportbymobnoClosed)
                                    {
                                        list.Add(item);
                                    }
                                    //  ListToDisplay = new ObservableCollection<TaxEvasionReportDetails>(TERListReportbymobnoClosed);
                                }

                            }
                            try
                            {
                                ListToDisplay = new ObservableCollection<TaxEvasionReportDetails>(list.OrderByDescending(c => c.TicketId));
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.ToString());
                                Console.Write(ex.StackTrace.ToString());
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

                PopToRootPage();

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
                            else
                            {
                                //  SetNoDataLabelVisibilityforOpen = true;
                            }

                        }
                        if (rootObject.Data.Closed!=null)
                        {
                            if (rootObject.Data.Closed.Count() > 0)
                            {
                                rootObject.Data.Opened = rootObject.Data.Closed.OrderByDescending(c => c.TicketId).ToArray();
                                TERListReportbymobnoClosed = rootObject.Data.Closed.ToList();
                                foreach (var item in TERListReportbymobnoClosed)
                                {
                                    TERListReportbymobnoAll.Add(item);
                                }
                                //SetNoDataLabelVisibilityforClose = false;
                            }
                            else
                            {
                            }
                        }
                        
                    }
                    else
                    {
                    }
                }
                else
                {
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
               //     await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                });
            }
        }
       
        public void passSelectedTaxEvasionItem(TaxEvasionReportDetails SelectedTaxEvasionReport)
        {
            try
            {
                Task.Run(() =>
                {
                   IsLoading = true;

                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    _navigationService.NavigateTo(App.TaxEvasionReportDetailPageView, SelectedTaxEvasionReport);

                });
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
    }
}
