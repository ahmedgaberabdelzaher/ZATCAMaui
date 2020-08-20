using System;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class OutletDetailsPageViewModel : BaseViewModel
    {
        #region Variable
        private EstablishmentRegistrationOutletTabsEnum _currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
        public EstablishmentRegistrationOutletTabsEnum currentTab
        {
            get => _currentTab;
            private set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
            }
        }

        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 3;


        private int _currenrIndex = (int)EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                MarkComplete = _currenrIndex == MaxIndex;
                RaisePropertyChanged(nameof(MarkComplete));
            }
        }

        public string SelectedTabText { get; private set; } = "Outlets";
        private string _selectedOutletTabText = "Address Details";
        public string SelectedOutletTabText
        {
            get => _selectedOutletTabText;
            private set
            {
                _selectedOutletTabText = value;
                RaisePropertyChanged(nameof(SelectedOutletTabText));
            }
        }
        private bool _postalAsPhysical = false;
        public bool PostalAsPhysical
        {
            get => _postalAsPhysical;
            set
            {
                _postalAsPhysical = value;
                RaisePropertyChanged(nameof(PostalAsPhysical));
            }
        }
        #endregion

        #region commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }
        public ICommand OnActivityItemButtonClick { get; private set; }
        #endregion

        #region Constructor
        public OutletDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigateToPre());
            OnActivityItemButtonClick = new Command(() => {
                System.Diagnostics.Debug.WriteLine("OnActivityItemButtonClick");
                navigationService.NavigateTo(App.ActivityItemPage);
            });
        }
        #endregion

        #region Method
        private void navigateToNext()
        {
            if(currentTab == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
                SelectedOutletTabText = "Activity Details";
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.ActivityDetails)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.AddressDetails;
                SelectedOutletTabText = "Address Details";
            }
        }
        private void navigateToPre()
        {
            if (currentTab == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
                SelectedOutletTabText = "Activity Details";
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.ActivityDetails)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
                SelectedOutletTabText = "Outlet Details";
            }
        }
        #endregion
    }
}
