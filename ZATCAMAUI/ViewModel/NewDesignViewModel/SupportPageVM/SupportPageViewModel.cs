
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.SupportPageVM
{
    public class SupportPageViewModel : BaseViewModel
    {
        #region Variable
        private SupportTabEnum _currentTab = SupportTabEnum.Parent;
        public SupportTabEnum currentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                OnPropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        private int _currenrIndex = 0;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
                else if (MarkComplete == true && _currenrIndex < MaxIndex)
                {
                    MarkComplete = false;
                    OnPropertyChanged(nameof(MarkComplete));
                }
            }
        }


        private bool _IsLoading = false;
        public new bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 5;
        #endregion

        #region Property
        private string _PageTitle = AppResources.ZZZSupport;
        public string PageTitle
        {
            get
            {
                return _PageTitle;
            }
            set
            {
                _PageTitle = value;
                OnPropertyChanged("PageTitle");
            }
        }
        #endregion

        #region Constructor
        public SupportPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        #endregion

        #region Methods
        public void setSupportTab()
        {
            PageTitle = AppResources.ZZZSupport;
            currentTab = SupportTabEnum.Parent;
        }
        public void setFAQ()
        {
            PageTitle = AppResources.ZZZFaq;
            currentTab = SupportTabEnum.FAQ;
        }
        public void setChat()
        {
            PageTitle = AppResources.NDChat;
            currentTab = SupportTabEnum.Chat;
        }
        public void setContactUs()
        {
            PageTitle = AppResources.ZZZContactus;
            currentTab = SupportTabEnum.ContactUs;
        }
        public void SetBranchLocator()
        {
            PageTitle = AppResources.NDBranchLocator;
            currentTab = SupportTabEnum.BranchLocator;
        }
        public void ChcekCurrentTab()
        {
            if (currentTab == SupportTabEnum.Parent)
            {
                _navigationService.GoBack();
            }
            else
            {
                setSupportTab();
            }
        }
        #endregion
    }
}
