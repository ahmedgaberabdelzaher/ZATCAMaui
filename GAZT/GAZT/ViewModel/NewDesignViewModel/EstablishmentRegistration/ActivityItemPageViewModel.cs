using System;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class ActivityItemPageViewModel : BaseViewModel
    {
        #region variables
        private EstablishmentOutletActivitiesTabsEnum _currentTab = EstablishmentOutletActivitiesTabsEnum.CRDetails;
        public EstablishmentOutletActivitiesTabsEnum CurrentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(CurrentTab));
                switch (value)
                {
                    case EstablishmentOutletActivitiesTabsEnum.LicenseDetails:
                        ActivityTitle = "Add License";
                        break;
                    case EstablishmentOutletActivitiesTabsEnum.ActivityList:
                        ActivityTitle = "License Details";
                        break;
                    case EstablishmentOutletActivitiesTabsEnum.CRDetails:
                    default:
                        ActivityTitle = "Commercial Registration";
                        break; 
                }
            }
        }
        private string _activityTitle = "Commercial Registration";
        public string ActivityTitle
        {
            get => _activityTitle;
            private set
            {
                _activityTitle = value;
                RaisePropertyChanged(nameof(ActivityTitle));
            }
        }
        #endregion

        #region commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }
        #endregion

        #region Constructor
        public ActivityItemPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigationService.GoBack());
        }
        #endregion

        #region Method
        private void navigateToNext()
        {
            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails || CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                CurrentTab = EstablishmentOutletActivitiesTabsEnum.ActivityList;
            }
            else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.ActivityList)
            {
                _navigationService.GoBack();
            }
        }
        #endregion
    }
}
