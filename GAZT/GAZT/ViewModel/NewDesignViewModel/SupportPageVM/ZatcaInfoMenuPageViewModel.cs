using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight.Views;
using System;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class ZatcaInfoMenuPageViewModel : BaseViewModel
    {
        #region Variable
        private CustomsEnum _currentTab = CustomsEnum.parentCstoms;
        public CustomsEnum currentTab
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
        private string _PageTitle = AppResources.ZatcaInfoMenu;
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
        public ZatcaInfoMenuPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
        public void setMenuTab()
        {
            PageTitle = AppResources.ZatcaInfoMenu;
            currentTab = CustomsEnum.parentCstoms;
        }
        public void setMenu1Tab()
        {
            PageTitle = AppResources.CustomsZATCAIntegrat;
            currentTab = CustomsEnum.customsTarrifs;
        }
        public void setMenu2Tab()
        {
            PageTitle = AppResources.CustomsZatcaDelca;
            currentTab = CustomsEnum.customsDelecrations;
        }
        //public void setMenu3Tab()
        //{
        //    PageTitle = AppResources.NDChat;
        //    currentTab = SupportTabEnum.Chat;
        //}
        //public void setMenu4Tab()
        //{
        //    PageTitle = AppResources.ZZZContactus;
        //    currentTab = SupportTabEnum.Chat;
        //}
        //public void SetBranchLocator()
        //{
        //    PageTitle = AppResources.NDBranchLocator;
        //    currentTab = SupportTabEnum.BranchLocator;
        //}
        public void ChcekCurrentTabCustoms()
        {
            if (currentTab == CustomsEnum.parentCstoms)
            {
                _navigationService.GoBack();
            }
            else
            {
                setMenuTab();
            }
        }
        #endregion
    }
}

