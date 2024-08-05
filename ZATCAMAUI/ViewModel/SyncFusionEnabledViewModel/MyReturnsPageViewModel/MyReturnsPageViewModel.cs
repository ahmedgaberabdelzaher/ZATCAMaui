

using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.MyReturnsPageViewModel
{

    public class MyReturnsPageViewModel : BaseViewModel
    {
        #region Veriables
        public ICommand GoBackClick { get; set; }
        public MyReturnsRootObject MyReturns { get; set; }
        #endregion
        #region Properties
       
        private bool _isArabic = false;
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                _isArabic = value;
                OnPropertyChanged("IsArabic");
            }
        }
        private MyReturnsResult _selectedReturnsVATSubmited = null;
        public MyReturnsResult SelectedReturnsVATSubmited
        {
            get
            {
                return _selectedReturnsVATSubmited;
            }
            set
            {
                _selectedReturnsVATSubmited = value;
                if (_selectedReturnsVATSubmited != null)
                {
                    GetVATAllReturnsAsync(_selectedReturnsVATSubmited);
                }
                OnPropertyChanged("SelectedReturnsVATSubmited");
            }
        }
        private MyReturnsResult _selectedReturnsVATNonSubmited = null;
        public MyReturnsResult SelectedReturnsVATNonSubmited
        {
            get
            {
                return _selectedReturnsVATNonSubmited;
            }
            set
            {
                _selectedReturnsVATNonSubmited = value;
                if (_selectedReturnsVATNonSubmited != null)
                {
                    GetVATAllReturnsAsync(_selectedReturnsVATNonSubmited);
                }
                OnPropertyChanged("SelectedReturnsVATNonSubmited");
            }
        }
        private MyReturnsResult _selectedReturnsVATOverDue = null;
        public MyReturnsResult SelectedReturnsVATOverDue
        {
            get
            {
                return _selectedReturnsVATOverDue;
            }
            set
            {
                _selectedReturnsVATOverDue = value;
                if (_selectedReturnsVATOverDue != null)
                {
                    GetVATAllReturnsAsync(_selectedReturnsVATOverDue);
                }
                OnPropertyChanged("SelectedReturnsVATOverDue");
            }
        }
        private MyReturnsResult _selectedZakatReturnSubmitted;
        public MyReturnsResult SelectedZakatReturnSubmitted
        {
            get
            {
                return _selectedZakatReturnSubmitted;
            }
            set
            {
                _selectedZakatReturnSubmitted = value;
                OnPropertyChanged("SelectedZakatReturnSubmitted");
                if (SelectedZakatReturnSubmitted != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    if (SelectedZakatReturnSubmitted.Fbtyp.Equals("FZ12"))
                    {
                        App.IsZakatLoadingFromMyReturns = true;
                        _navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedZakatReturnSubmitted.Fbguid);
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                        });
                    }
                }
            }
        }
        private MyReturnsResult _selectedZakatReturnNonSubmitted;
        public MyReturnsResult SelectedZakatReturnNonSubmitted
        {
            get
            {
                return _selectedZakatReturnNonSubmitted;
            }
            set
            {
                _selectedZakatReturnNonSubmitted = value;
                OnPropertyChanged("SelectedZakatReturnNonSubmitted");
                if (SelectedZakatReturnNonSubmitted != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    if (SelectedZakatReturnNonSubmitted.Fbtyp.Equals("FZ12"))
                    {
                        App.IsZakatLoadingFromMyReturns = true;
                        _navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedZakatReturnNonSubmitted.Fbguid);
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                        });
                    }
                }
            }
        }
        private MyReturnsResult _selectedZakatReturnOverDue;
        public MyReturnsResult SelectedZakatReturnOverDue
        {
            get
            {
                return _selectedZakatReturnOverDue;
            }
            set
            {
                _selectedZakatReturnOverDue = value;
                OnPropertyChanged("SelectedZakatReturnOverDue");
                if (SelectedZakatReturnOverDue != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    if (SelectedZakatReturnOverDue.Fbtyp.Equals("FZ12"))
                    {
                        App.IsZakatLoadingFromMyReturns = true;
                        _navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedZakatReturnOverDue.Fbguid);
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                        });
                    }
                }
            }
        }
        private MyReturnsResult _selectedReturnsETSubmitted;
        public MyReturnsResult SelectedReturnsETSubmitted
        {
            get
            {
                return _selectedReturnsETSubmitted;
            }
            set
            {
                _selectedReturnsETSubmitted = value;
                OnPropertyChanged("SelectedReturnsETSubmitted");
                if (_selectedReturnsETSubmitted != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
            }
        }
        private MyReturnsResult _selectedReturnsETNonSubmitted;
        public MyReturnsResult SelectedReturnsETNonSubmitted
        {
            get
            {
                return _selectedReturnsETNonSubmitted;
            }
            set
            {
                _selectedReturnsETNonSubmitted = value;
                OnPropertyChanged("SelectedReturnsETNonSubmitted");
                if (_selectedReturnsETNonSubmitted != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
            }
        }
        private MyReturnsResult _selectedReturnsETOverDue;
        public MyReturnsResult SelectedReturnsETOverDue
        {
            get
            {
                return _selectedReturnsETOverDue;
            }
            set
            {
                _selectedReturnsETOverDue = value;
                OnPropertyChanged("SelectedReturnsETOverDue");
                if (_selectedReturnsETOverDue != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
            }
        }
        private MyReturnsResult _selectedReturnsWHSubmitted;
        public MyReturnsResult SelectedReturnsWHSubmitted
        {
            get
            {
                return _selectedReturnsWHSubmitted;
            }
            set
            {
                _selectedReturnsWHSubmitted = value;
                OnPropertyChanged("SelectedReturnsWHSubmitted");
                if (_selectedReturnsWHSubmitted != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
            }
        }
        private MyReturnsResult _selectedReturnsWHNonSubmitted;
        public MyReturnsResult SelectedReturnsWHNonSubmitted
        {
            get
            {
                return _selectedReturnsWHNonSubmitted;
            }
            set
            {
                _selectedReturnsWHNonSubmitted = value;
                OnPropertyChanged("SelectedReturnsWHNonSubmitted");
                if (_selectedReturnsWHNonSubmitted != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
            }
        }
        private MyReturnsResult _selectedReturnsWHOverDue;
        public MyReturnsResult SelectedReturnsWHOverDue
        {
            get
            {
                return _selectedReturnsWHOverDue;
            }
            set
            {
                _selectedReturnsWHOverDue = value;
                OnPropertyChanged("SelectedReturnsWHOverDue");
                if (_selectedReturnsWHOverDue != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
            }
        }
        private List<MyReturnsResult> _returnsZakatSubmited = null;
        public List<MyReturnsResult> ReturnsZakatSubmited
        {
            get
            {
                return _returnsZakatSubmited;
            }
            set
            {
                _returnsZakatSubmited = value;
                OnPropertyChanged("ReturnsZakatSubmited");
            }
        }
        private List<MyReturnsResult> _returnsZakatNonSubmited = null;
        public List<MyReturnsResult> ReturnsZakatNonSubmited
        {
            get
            {
                return _returnsZakatNonSubmited;
            }
            set
            {
                _returnsZakatNonSubmited = value;
                OnPropertyChanged("ReturnsZakatNonSubmited");
            }
        }
        private List<MyReturnsResult> _returnsZakatOverDue = null;
        public List<MyReturnsResult> ReturnsZakatOverDue
        {
            get
            {
                return _returnsZakatOverDue;
            }
            set
            {
                _returnsZakatOverDue = value;
                OnPropertyChanged("ReturnsZakatOverDue");
            }
        }
        private List<MyReturnsResult> _returnsVATSubmited = null;
        public List<MyReturnsResult> ReturnsVATSubmited
        {
            get
            {
                return _returnsVATSubmited;
            }
            set
            {
                _returnsVATSubmited = value;
                OnPropertyChanged("ReturnsVATSubmited");
            }
        }
        private List<MyReturnsResult> _returnsVATNonSubmited = null;
        public List<MyReturnsResult> ReturnsVATNonSubmited
        {
            get
            {
                return _returnsVATNonSubmited;
            }
            set
            {
                _returnsVATNonSubmited = value;
                OnPropertyChanged("ReturnsVATNonSubmited");
            }
        }
        private List<MyReturnsResult> _returnsVATOverDue = null;
        public List<MyReturnsResult> ReturnsVATOverDue
        {
            get
            {
                return _returnsVATOverDue;
            }
            set
            {
                _returnsVATOverDue = value;
                OnPropertyChanged("ReturnsVATOverDue");
            }
        }
        private List<MyReturnsResult> _returnsETSubmited = null;
        public List<MyReturnsResult> ReturnsETSubmited
        {
            get
            {
                return _returnsETSubmited;
            }
            set
            {
                _returnsETSubmited = value;
                OnPropertyChanged("ReturnsETSubmited");
            }
        }
        private List<MyReturnsResult> _returnsETNonSubmited = null;
        public List<MyReturnsResult> ReturnsETNonSubmited
        {
            get
            {
                return _returnsETNonSubmited;
            }
            set
            {
                _returnsETNonSubmited = value;
                OnPropertyChanged("ReturnsETNonSubmited");
            }
        }
        private List<MyReturnsResult> _returnsETOverDue = null;
        public List<MyReturnsResult> ReturnsETOverDue
        {
            get
            {
                return _returnsETOverDue;
            }
            set
            {
                _returnsETOverDue = value;
                OnPropertyChanged("ReturnsETOverDue");
            }
        }
        private List<MyReturnsResult> _returnsWHSubmited = null;
        public List<MyReturnsResult> ReturnsWHSubmited
        {
            get
            {
                return _returnsWHSubmited;
            }
            set
            {
                _returnsWHSubmited = value;
                OnPropertyChanged("ReturnsWHSubmited");
            }
        }
        private List<MyReturnsResult> _returnsWHNonSubmited = null;
        public List<MyReturnsResult> ReturnsWHNonSubmited
        {
            get
            {
                return _returnsWHNonSubmited;
            }
            set
            {
                _returnsWHNonSubmited = value;
                OnPropertyChanged("ReturnsWHNonSubmited");
            }
        }
        private List<MyReturnsResult> _returnsWHOverDue = null;
        public List<MyReturnsResult> ReturnsWHOverDue
        {
            get
            {
                return _returnsWHOverDue;
            }
            set
            {
                _returnsWHOverDue = value;
                OnPropertyChanged("ReturnsWHOverDue");
            }
        }
        private bool _isVisibleVATSumbitted = false;
        public bool IsVisibleVATSumbitted
        {
            get
            {
                return _isVisibleVATSumbitted;
            }
            set
            {
                _isVisibleVATSumbitted = value;
                OnPropertyChanged("IsVisibleVATSumbitted");
            }
        }
        private bool _isVisibleVATSumbittedLabel = false;
        public bool IsVisibleVATSumbittedLabel
        {
            get
            {
                return _isVisibleVATSumbittedLabel;
            }
            set
            {
                _isVisibleVATSumbittedLabel = value;
                OnPropertyChanged("IsVisibleVATSumbittedLabel");
            }
        }
        private bool _isVisibleVATNonSumbitted = false;
        public bool IsVisibleVATNonSumbitted
        {
            get
            {
                return _isVisibleVATNonSumbitted;
            }
            set
            {
                _isVisibleVATNonSumbitted = value;
                OnPropertyChanged("IsVisibleVATNonSumbitted");
            }
        }
        private bool _isVisibleVATNonSumbittedLabel = false;
        public bool IsVisibleVATNonSumbittedLabel
        {
            get
            {
                return _isVisibleVATNonSumbittedLabel;
            }
            set
            {
                _isVisibleVATNonSumbittedLabel = value;
                OnPropertyChanged("IsVisibleVATNonSumbittedLabel");
            }
        }
        private bool _isVisibleVATOverDue = false;
        public bool IsVisibleVATOverDue
        {
            get
            {
                return _isVisibleVATOverDue;
            }
            set
            {
                _isVisibleVATOverDue = value;
                OnPropertyChanged("IsVisibleVATOverDue");
            }
        }
        private bool _isVisibleVATOverDueLabel = false;
        public bool IsVisibleVATOverDueLabel
        {
            get
            {
                return _isVisibleVATOverDueLabel;
            }
            set
            {
                _isVisibleVATOverDueLabel = value;
                OnPropertyChanged("IsVisibleVATOverDueLabel");
            }
        }
        private bool _isVisibleZakatSumbitted = false;
        public bool IsVisibleZakatSumbitted
        {
            get
            {
                return _isVisibleZakatSumbitted;
            }
            set
            {
                _isVisibleZakatSumbitted = value;
                OnPropertyChanged("IsVisibleZakatSumbitted");
            }
        }
        private bool _isVisibleZakatSumbittedLabel = false;
        public bool IsVisibleZakatSumbittedLabel
        {
            get
            {
                return _isVisibleZakatSumbittedLabel;
            }
            set
            {
                _isVisibleZakatSumbittedLabel = value;
                OnPropertyChanged("IsVisibleZakatSumbittedLabel");
            }
        }
        private bool _isVisibleZakatNonSumbitted = false;
        public bool IsVisibleZakatNonSumbitted
        {
            get
            {
                return _isVisibleZakatNonSumbitted;
            }
            set
            {
                _isVisibleZakatNonSumbitted = value;
                OnPropertyChanged("IsVisibleZakatNonSumbitted");
            }
        }
        private bool _isVisibleZakatNonSumbittedLabel = false;
        public bool IsVisibleZakatNonSumbittedLabel
        {
            get
            {
                return _isVisibleZakatNonSumbittedLabel;
            }
            set
            {
                _isVisibleZakatNonSumbittedLabel = value;
                OnPropertyChanged("IsVisibleZakatNonSumbittedLabel");
            }
        }
        private bool _isVisibleZakatOverDue = false;
        public bool IsVisibleZakatOverDue
        {
            get
            {
                return _isVisibleZakatOverDue;
            }
            set
            {
                _isVisibleZakatOverDue = value;
                OnPropertyChanged("IsVisibleZakatOverDue");
            }
        }
        private bool _isVisibleZakatOverDueLabel = false;
        public bool IsVisibleZakatOverDueLabel
        {
            get
            {
                return _isVisibleZakatOverDueLabel;
            }
            set
            {
                _isVisibleZakatOverDueLabel = value;
                OnPropertyChanged("IsVisibleZakatOverDueLabel");
            }
        }
        private bool _isVisibleETSumbitted = false;
        public bool IsVisibleETSumbitted
        {
            get
            {
                return _isVisibleETSumbitted;
            }
            set
            {
                _isVisibleETSumbitted = value;
                OnPropertyChanged("IsVisibleETSumbitted");
            }
        }
        private bool _isVisibleETSumbittedLabel = false;
        public bool IsVisibleETSumbittedLabel
        {
            get
            {
                return _isVisibleETSumbittedLabel;
            }
            set
            {
                _isVisibleETSumbittedLabel = value;
                OnPropertyChanged("IsVisibleETSumbittedLabel");
            }
        }
        private bool _isVisibleETNonSumbitted = false;
        public bool IsVisibleETNonSumbitted
        {
            get
            {
                return _isVisibleETNonSumbitted;
            }
            set
            {
                _isVisibleETNonSumbitted = value;
                OnPropertyChanged("IsVisibleETNonSumbitted");
            }
        }
        private bool _isVisibleETNonSumbittedLabel = false;
        public bool IsVisibleETNonSumbittedLabel
        {
            get
            {
                return _isVisibleETNonSumbittedLabel;
            }
            set
            {
                _isVisibleETNonSumbittedLabel = value;
                OnPropertyChanged("IsVisibleETNonSumbittedLabel");
            }
        }
        private bool _isVisibleETOverDue = false;
        public bool IsVisibleETOverDue
        {
            get
            {
                return _isVisibleETOverDue;
            }
            set
            {
                _isVisibleETOverDue = value;
                OnPropertyChanged("IsVisibleETOverDue");
            }
        }
        private bool _isVisibleETOverDueLabel = false;
        public bool IsVisibleETOverDueLabel
        {
            get
            {
                return _isVisibleETOverDueLabel;
            }
            set
            {
                _isVisibleETOverDueLabel = value;
                OnPropertyChanged("IsVisibleETOverDueLabel");
            }
        }
        private bool _isVisibleWHSumbitted = false;
        public bool IsVisibleWHSumbitted
        {
            get
            {
                return _isVisibleWHSumbitted;
            }
            set
            {
                _isVisibleWHSumbitted = value;
                OnPropertyChanged("IsVisibleWHSumbitted");
            }
        }
        private bool _isVisibleWHSumbittedLabel = false;
        public bool IsVisibleWHSumbittedLabel
        {
            get
            {
                return _isVisibleWHSumbittedLabel;
            }
            set
            {
                _isVisibleWHSumbittedLabel = value;
                OnPropertyChanged("IsVisibleWHSumbittedLabel");
            }
        }
        private bool _isVisibleWHNonSumbitted = false;
        public bool IsVisibleWHNonSumbitted
        {
            get
            {
                return _isVisibleWHNonSumbitted;
            }
            set
            {
                _isVisibleWHNonSumbitted = value;
                OnPropertyChanged("IsVisibleWHNonSumbitted");
            }
        }
        private bool _isVisibleWHNonSumbittedLabel = false;
        public bool IsVisibleWHNonSumbittedLabel
        {
            get
            {
                return _isVisibleWHNonSumbittedLabel;
            }
            set
            {
                _isVisibleWHNonSumbittedLabel = value;
                OnPropertyChanged("IsVisibleWHNonSumbittedLabel");
            }
        }
        private bool _isVisibleWHOverDue = false;
        public bool IsVisibleWHOverDue
        {
            get
            {
                return _isVisibleWHOverDue;
            }
            set
            {
                _isVisibleWHOverDue = value;
                OnPropertyChanged("IsVisibleWHOverDue");
            }
        }
        private bool _isVisibleWHOverDueLabel = false;
        public bool IsVisibleWHOverDueLabel
        {
            get
            {
                return _isVisibleWHOverDueLabel;
            }
            set
            {
                _isVisibleWHOverDueLabel = value;
                OnPropertyChanged("IsVisibleWHOverDueLabel");
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
                OnPropertyChanged("IsVATVisible");
            }
        }
        private bool _isZakatVisible = false;
        public bool IsZakatVisible
        {
            get
            {
                return _isZakatVisible;
            }
            set
            {
                _isZakatVisible = value;
                OnPropertyChanged("IsZakatVisible");
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
                OnPropertyChanged("IsETVisible");
            }
        }
        private bool _isWHVisible = false;
        public bool IsWHVisible
        {
            get
            {
                return _isWHVisible;
            }
            set
            {
                _isWHVisible = value;
                OnPropertyChanged("IsWHVisible");
            }
        }
        #endregion
        #region Custructor
        public MyReturnsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command( () =>
            {
                _navigationService.GoBack();
            });
        }
        private FlowDirection _fDirection = FlowDirection.RightToLeft;
        public FlowDirection FDirection
        {
            get
            {
                return _fDirection;
            }
            set
            {
                _fDirection = value;
                OnPropertyChanged("FDirection");
            }
        }
        private int _tabIndexStatus = 0;
        public int TabIndexStatus
        {
            get
            {
                return _tabIndexStatus;
            }
            set
            {
                _tabIndexStatus = value;
                OnPropertyChanged("TabIndexStatus");
            }
        }
        private string _submittedVATReturnsCount = string.Empty;
        public string SubmittedVATReturnsCount
        {
            get
            {
                return _submittedVATReturnsCount;
            }
            set
            {
                _submittedVATReturnsCount = value;
                OnPropertyChanged("SubmittedVATReturnsCount");
            }
        }
        private string _nonSubmittedVATReturnsCount = string.Empty;
        public string NonSubmittedVATReturnsCount
        {
            get
            {
                return _nonSubmittedVATReturnsCount;
            }
            set
            {
                _nonSubmittedVATReturnsCount = value;
                OnPropertyChanged("NonSubmittedVATReturnsCount");
            }
        }
        private string _overDueVATReturnsCount = string.Empty;
        public string OverDueVATReturnsCount
        {
            get
            {
                return _overDueVATReturnsCount;
            }
            set
            {
                _overDueVATReturnsCount = value;
                OnPropertyChanged("OverDueVATReturnsCount");
            }
        }
        private string _submittedZakatReturnsCount = string.Empty;
        public string SubmittedZakatReturnsCount
        {
            get
            {
                return _submittedZakatReturnsCount;
            }
            set
            {
                _submittedZakatReturnsCount = value;
                OnPropertyChanged("SubmittedZakatReturnsCount");
            }
        }
        private string _nonSubmittedZakatReturnsCount = string.Empty;
        public string NonSubmittedZakatReturnsCount
        {
            get
            {
                return _nonSubmittedZakatReturnsCount;
            }
            set
            {
                _nonSubmittedZakatReturnsCount = value;
                OnPropertyChanged("NonSubmittedZakatReturnsCount");
            }
        }
        private string _overDueZakatReturnsCount = string.Empty;
        public string OverDueZakatReturnsCount
        {
            get
            {
                return _overDueZakatReturnsCount;
            }
            set
            {
                _overDueZakatReturnsCount = value;
                OnPropertyChanged("OverDueZakatReturnsCount");
            }
        }
        private string _submittedETReturnsCount = string.Empty;
        public string SubmittedETReturnsCount
        {
            get
            {
                return _submittedETReturnsCount;
            }
            set
            {
                _submittedETReturnsCount = value;
                OnPropertyChanged("SubmittedETReturnsCount");
            }
        }
        private string _nonSubmittedETReturnsCount = string.Empty;
        public string NonSubmittedETReturnsCount
        {
            get
            {
                return _nonSubmittedETReturnsCount;
            }
            set
            {
                _nonSubmittedETReturnsCount = value;
                OnPropertyChanged("NonSubmittedETReturnsCount");
            }
        }
        private string _overDueETReturnsCount = string.Empty;
        public string OverDueETReturnsCount
        {
            get
            {
                return _overDueETReturnsCount;
            }
            set
            {
                _overDueETReturnsCount = value;
                OnPropertyChanged("OverDueETReturnsCount");
            }
        }
        private string _submittedWHReturnsCount = string.Empty;
        public string SubmittedWHReturnsCount
        {
            get
            {
                return _submittedWHReturnsCount;
            }
            set
            {
                _submittedWHReturnsCount = value;
                OnPropertyChanged("SubmittedWHReturnsCount");
            }
        }
        private string _nonSubmittedWHReturnsCount = string.Empty;
        public string NonSubmittedWHReturnsCount
        {
            get
            {
                return _nonSubmittedWHReturnsCount;
            }
            set
            {
                _nonSubmittedWHReturnsCount = value;
                OnPropertyChanged("NonSubmittedWHReturnsCount");
            }
        }
        private string _overDueWHReturnsCount = string.Empty;
        public string OverDueWHReturnsCount
        {
            get
            {
                return _overDueWHReturnsCount;
            }
            set
            {
                _overDueWHReturnsCount = value;
                OnPropertyChanged("OverDueWHReturnsCount");
            }
        }
        #endregion
        #region Methods
        public async void GetVATAllReturnsAsync(MyReturnsResult SelectedReturnsVAT)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsLoading = true;
            });
            await GetVATAllReturns(SelectedReturnsVAT);
            MainThread.BeginInvokeOnMainThread(() =>
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
                    if (SelectedReturnsVAT != null)
                    {
                        if (isStatusNotValid(SelectedReturnsVAT))
                        {
                            string SelectedICRGUID = SelectedReturnsVAT.Fbguid;
                            App.ICRStatus = SelectedReturnsVAT.Stat;
                            VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedReturnsVAT.Fbguid, SelectedReturnsVAT.Fbnum, App.TP.Tin, SelectedReturnsVAT.Persl);
                            PopToRootPage();
                            if (_vATDeclaration != null && _vATDeclaration.d != null)
                            {
                                _vATDeclaration.d.Fbguid = SelectedICRGUID;
                                VATDeclaration vATDeclaration = new VATDeclaration();
                                VATDeclarationD vATDeclarationD = new VATDeclarationD();
                                Result5 result5 = new Result5();
                                List<Result5> lst = new List<Result5>();
                                ADRSet _aDRSet = new ADRSet();
                                lst.Add(result5);
                                vATDeclaration.d = vATDeclarationD;
                                vATDeclaration.d.ADRSet = _aDRSet;
                                vATDeclaration.d.ADRSet.results = lst;
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.VATReturnsPageView, _vATDeclaration);
                                });
                            }
                            else
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            }
                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZZZReturnUnderReview, AppResources.Information);
                        }
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFAnonymousLandingPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        public async Task OnPageLoad()
        {
            IsZakatVisible = false;
            IsVATVisible = false;
            IsETVisible = false;
            IsWHVisible = false;
            ReturnsZakatNonSubmited = null;
            ReturnsZakatOverDue = null;
            ReturnsZakatSubmited = null;
            List<MyReturnsResult> ReturnsZakatNonSubmitedChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsZakatOverDueChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsZakatSubmitedChild = new List<MyReturnsResult>();
            ReturnsVATNonSubmited = null;
            ReturnsVATOverDue = null;
            ReturnsVATSubmited = null;
            List<MyReturnsResult> ReturnsVATNonSubmitedChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsVATOverDueChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsVATSubmitedChild = new List<MyReturnsResult>();
            ReturnsETNonSubmited = null;
            ReturnsETOverDue = null;
            ReturnsETSubmited = null;
            List<MyReturnsResult> ReturnsETNonSubmitedChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsETOverDueChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsETSubmitedChild = new List<MyReturnsResult>();
            ReturnsWHNonSubmited = null;
            ReturnsWHOverDue = null;
            ReturnsWHSubmited = null;
            List<MyReturnsResult> ReturnsWHNonSubmitedChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsWHOverDueChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsWHSubmitedChild = new List<MyReturnsResult>();
            Task GetReturnDataTask = null;
            GetReturnDataTask = Task.Run(async () =>
            {
                MyReturns = await WebServiceManager.GAZTGetReturnData(UtilityManager.GetLanguageParameter(), App.TP.Userid);
            });
            try
            {
                if (GetReturnDataTask != null)
                    GetReturnDataTask.Wait();
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
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.NetworkConnectivityIssue)
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    PopToRootPage();
                });
            }
            catch (Exception)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    PopToRootPage();
                });
            }
            if (!string.IsNullOrEmpty(UtilityManager.TPTaxAvalable))
            {
                string[] TpTypes = UtilityManager.TPTaxAvalable.Split(',');
                foreach (string ItemType in TpTypes)
                {
                    if (ItemType == "05")
                    {
                        IsZakatVisible = true;
                    }
                    if (ItemType == "03" || ItemType == "13")
                    {
                        IsVATVisible = true;
                    }
                    if (ItemType == "07")
                    {
                        IsETVisible = true;
                    }
                    if (ItemType == "01")
                    {
                        IsWHVisible = true;
                    }
                }
            }
            if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
            {
                foreach (MyReturnsResult ItemR in MyReturns.d.results)
                {
                    if (0 == string.Compare(ItemR.TaxType, "ITAX", true) || 0 == string.Compare(ItemR.TaxType, "ZAKT", true))
                    {
                        if (0 == string.Compare(ItemR.StatusTxt, "Submitted", true))
                        {
                            ReturnsZakatSubmitedChild.Add(ItemR);
                        }
                        else if (0 == string.Compare(ItemR.StatusTxt, "Non Submitted", true))
                        {
                            ReturnsZakatNonSubmitedChild.Add(ItemR);
                            if (0 == string.Compare(ItemR.Due, "X", true))
                            {
                                ReturnsZakatOverDueChild.Add(ItemR);
                            }
                        }
                    }
                    if (0 == string.Compare(ItemR.TaxType, "VATX", true) || 0 == string.Compare(ItemR.TaxType, "VTEP", true))
                    {
                        if (0 == string.Compare(ItemR.StatusTxt, "Submitted", true))
                        {
                            ReturnsVATSubmitedChild.Add(ItemR);
                        }
                        else if (0 == string.Compare(ItemR.StatusTxt, "Non Submitted", true))
                        {
                            ReturnsVATNonSubmitedChild.Add(ItemR);
                            if (0 == string.Compare(ItemR.Due, "X", true))
                            {
                                ReturnsVATOverDueChild.Add(ItemR);
                            }
                        }
                    }
                    if (0 == string.Compare(ItemR.TaxType, "ETAX", true))
                    {
                        if (0 == string.Compare(ItemR.StatusTxt, "Submitted", true))
                        {
                            ReturnsETSubmitedChild.Add(ItemR);
                        }
                        else if (0 == string.Compare(ItemR.StatusTxt, "Non Submitted", true))
                        {
                            ReturnsETNonSubmitedChild.Add(ItemR);
                            if (0 == string.Compare(ItemR.Due, "X", true))
                            {
                                ReturnsETOverDueChild.Add(ItemR);
                            }
                        }
                    }
                    if (0 == string.Compare(ItemR.TaxType, "WHTX", true))
                    {
                        if (0 == string.Compare(ItemR.StatusTxt, "Submitted", true))
                        {
                            ReturnsWHSubmitedChild.Add(ItemR);
                        }
                        else if (0 == string.Compare(ItemR.StatusTxt, "Non Submitted", true))
                        {
                            ReturnsWHNonSubmitedChild.Add(ItemR);
                            if (0 == string.Compare(ItemR.Due, "X", true))
                            {
                                ReturnsWHOverDueChild.Add(ItemR);
                            }
                        }
                    }
                }
                if (ReturnsZakatNonSubmitedChild.Count > 0)
                {
                    ReturnsZakatNonSubmited = ReturnsZakatNonSubmitedChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleZakatNonSumbitted = true;
                    IsVisibleZakatNonSumbittedLabel = false;
                    NonSubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(" + ReturnsZakatNonSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleZakatNonSumbitted = false;
                    IsVisibleZakatNonSumbittedLabel = true;
                    NonSubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(0)";
                }
                if (ReturnsZakatOverDueChild.Count > 0)
                {
                    ReturnsZakatOverDue = ReturnsZakatOverDueChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleZakatOverDue = true;
                    IsVisibleZakatOverDueLabel = false;
                    OverDueZakatReturnsCount = AppResources.ZAKATReturns + "(" + ReturnsZakatOverDueChild.Count + ")";
                }
                else
                {
                    IsVisibleZakatOverDue = false;
                    IsVisibleZakatOverDueLabel = true;
                    OverDueZakatReturnsCount = AppResources.ZAKATReturns + "(0)";
                }
                if (ReturnsZakatSubmitedChild.Count > 0)
                {
                    ReturnsZakatSubmited = ReturnsZakatSubmitedChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleZakatSumbitted = true;
                    IsVisibleZakatSumbittedLabel = false;
                    SubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(" + ReturnsZakatSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleZakatSumbitted = false;
                    IsVisibleZakatSumbittedLabel = true;
                    SubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(0)";
                }
                if (ReturnsVATNonSubmitedChild.Count > 0)
                {
                    ReturnsVATNonSubmited = ReturnsVATNonSubmitedChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleVATNonSumbitted = true;
                    IsVisibleVATNonSumbittedLabel = false;
                    NonSubmittedVATReturnsCount = AppResources.VatReturns + "(" + ReturnsVATNonSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleVATNonSumbitted = false;
                    IsVisibleVATNonSumbittedLabel = true;
                    NonSubmittedVATReturnsCount = AppResources.VatReturns + "(0)";
                }
                if (ReturnsVATOverDueChild.Count > 0)
                {
                    ReturnsVATOverDue = ReturnsVATOverDueChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleVATOverDue = true;
                    IsVisibleVATOverDueLabel = false;
                    OverDueVATReturnsCount = AppResources.VatReturns + "(" + ReturnsVATOverDueChild.Count + ")";
                }
                else
                {
                    IsVisibleVATOverDue = false;
                    IsVisibleVATOverDueLabel = true;
                    OverDueVATReturnsCount = AppResources.VatReturns + "(0)";
                }
                if (ReturnsVATSubmitedChild.Count > 0)
                {
                    ReturnsVATSubmited = ReturnsVATSubmitedChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleVATSumbitted = true;
                    IsVisibleVATSumbittedLabel = false;
                    SubmittedVATReturnsCount = AppResources.VatReturns + "(" + ReturnsVATSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleVATSumbitted = false;
                    IsVisibleVATSumbittedLabel = true;
                    SubmittedVATReturnsCount = AppResources.VatReturns + "(0)";
                }
                if (ReturnsETNonSubmitedChild.Count > 0)
                {
                    ReturnsETNonSubmited = ReturnsETNonSubmitedChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleETNonSumbitted = true;
                    IsVisibleETNonSumbittedLabel = false;
                    NonSubmittedETReturnsCount = AppResources.ETReturns + "(" + ReturnsETNonSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleETNonSumbitted = false;
                    IsVisibleETNonSumbittedLabel = true;
                    NonSubmittedETReturnsCount = AppResources.ETReturns + "(0)";
                }
                if (ReturnsETOverDueChild.Count > 0)
                {
                    ReturnsETOverDue = ReturnsETOverDueChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleETOverDue = true;
                    IsVisibleETOverDueLabel = false;
                    OverDueETReturnsCount = AppResources.ETReturns + "(" + ReturnsETOverDueChild.Count + ")";
                }
                else
                {
                    IsVisibleETOverDue = false;
                    IsVisibleETOverDueLabel = true;
                    OverDueETReturnsCount = AppResources.ETReturns + "(0)";
                }
                if (ReturnsETSubmitedChild.Count > 0)
                {
                    ReturnsETSubmited = ReturnsETSubmitedChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleETSumbitted = true;
                    IsVisibleETSumbittedLabel = false;
                    SubmittedETReturnsCount = AppResources.ETReturns + "(" + ReturnsETSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleETSumbitted = false;
                    IsVisibleETSumbittedLabel = true;
                    SubmittedETReturnsCount = AppResources.ETReturns + "(0)";
                }
                if (ReturnsWHNonSubmitedChild.Count > 0)
                {
                    ReturnsWHNonSubmited = ReturnsWHNonSubmitedChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleWHNonSumbitted = true;
                    IsVisibleWHNonSumbittedLabel = false;
                    NonSubmittedWHReturnsCount = AppResources.ZZWithholding + "(" + ReturnsWHNonSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleWHNonSumbitted = false;
                    IsVisibleWHNonSumbittedLabel = true;
                    NonSubmittedWHReturnsCount = AppResources.ZZWithholding + "(0)";
                }
                if (ReturnsWHOverDueChild.Count > 0)
                {
                    ReturnsWHOverDue = ReturnsWHOverDueChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleWHOverDue = true;
                    IsVisibleWHOverDueLabel = false;
                    OverDueWHReturnsCount = AppResources.ZZWithholding + "(" + ReturnsWHOverDueChild.Count + ")";
                }
                else
                {
                    IsVisibleWHOverDue = false;
                    IsVisibleWHOverDueLabel = true;
                    OverDueWHReturnsCount = AppResources.ZZWithholding + "(0)";
                }
                if (ReturnsWHSubmitedChild.Count > 0)
                {
                    ReturnsWHSubmited = ReturnsWHSubmitedChild.OrderByDescending(a => a.DueDt).ToList();
                    IsVisibleWHSumbitted = true;
                    IsVisibleWHSumbittedLabel = false;
                    SubmittedWHReturnsCount = AppResources.ZZWithholding + "(" + ReturnsWHSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleWHSumbitted = false;
                    IsVisibleWHSumbittedLabel = true;
                    SubmittedWHReturnsCount = AppResources.ZZWithholding + "(0)";
                }
            }
            else
            {
                IsVisibleETNonSumbitted = false;
                IsVisibleETNonSumbittedLabel = true;
                IsVisibleETOverDue = false;
                IsVisibleETOverDueLabel = true;
                IsVisibleETSumbitted = false;
                IsVisibleETSumbittedLabel = true;
                IsVisibleVATNonSumbitted = false;
                IsVisibleVATNonSumbittedLabel = true;
                IsVisibleVATOverDue = false;
                IsVisibleVATOverDueLabel = true;
                IsVisibleVATSumbitted = false;
                IsVisibleVATSumbittedLabel = true;
                IsVisibleZakatNonSumbitted = false;
                IsVisibleZakatNonSumbittedLabel = true;
                IsVisibleZakatOverDue = false;
                IsVisibleZakatOverDueLabel = true;
                IsVisibleZakatSumbitted = false;
                IsVisibleZakatSumbittedLabel = true;
                IsVisibleWHNonSumbitted = false;
                IsVisibleWHNonSumbittedLabel = true;
                IsVisibleWHOverDue = false;
                IsVisibleWHOverDueLabel = true;
                IsVisibleWHSumbitted = false;
                IsVisibleWHSumbittedLabel = true;
                NonSubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(0)";
                OverDueZakatReturnsCount = AppResources.ZAKATReturns + "(0)";
                SubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(0)";
                NonSubmittedVATReturnsCount = AppResources.VatReturns + "(0)";
                OverDueVATReturnsCount = AppResources.VatReturns + "(0)";
                SubmittedVATReturnsCount = AppResources.VatReturns + "(0)";
                NonSubmittedETReturnsCount = AppResources.ETReturns + "(0)";
                OverDueETReturnsCount = AppResources.ETReturns + "(0)";
                SubmittedETReturnsCount = AppResources.ETReturns + "(0)";
                NonSubmittedWHReturnsCount = AppResources.ZZWithholding + "(0)";
                OverDueWHReturnsCount = AppResources.ZZWithholding + "(0)";
                SubmittedWHReturnsCount = AppResources.ZZWithholding + "(0)";
            }
        }
        #endregion
    }
}
