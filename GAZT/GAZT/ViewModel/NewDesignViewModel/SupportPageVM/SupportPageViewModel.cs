using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight.Views;
using System;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class SupportPageViewModel: BaseViewModel
    {
        #region Variable
        private SupportTabEnum _currentTab = SupportTabEnum.Parent;
        public SupportTabEnum currentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
            }
        }

        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
                else if (MarkComplete == true && _currenrIndex < MaxIndex)
                {
                    MarkComplete = false;
                    RaisePropertyChanged(nameof(MarkComplete));
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
                RaisePropertyChanged("IsLoading");
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
                RaisePropertyChanged("PageTitle");
            }
        }
        #endregion

        #region Constructor
        public SupportPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
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
