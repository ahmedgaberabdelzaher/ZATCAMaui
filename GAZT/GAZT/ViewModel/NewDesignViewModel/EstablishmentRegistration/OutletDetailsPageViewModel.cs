using System;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class OutletDetailsPageViewModel : BaseViewModel
    {
        #region Variable
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        public OutletNumber newNumber { get; set; } = null;
        //private ActivitySetsList activityList = null;
        private EstablishmentRegistrationOutletTabsEnum _currentTab = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
        public EstablishmentRegistrationOutletTabsEnum currentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
                switch (value)
                {
                    case EstablishmentRegistrationOutletTabsEnum.ActivityDetails:
                        SelectedOutletTabText = "Activity Details";
                        break;
                    case EstablishmentRegistrationOutletTabsEnum.AddressDetails:
                        SelectedOutletTabText = "Address Details";
                        break;
                    case EstablishmentRegistrationOutletTabsEnum.OutletDetail:
                    default:
                        SelectedOutletTabText = "Outlet Details";
                        break;
                }
                fetchTabDataAndBind(value);
            }
        }

        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 3;


        private int _currenrIndex = (int)EstablishmentRegistrationOutletTabsEnum.OutletDetail;
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
        private string _outletName = string.Empty;
        public string OutletName
        {
            get => _outletName;
            set
            {
                if (value != null)
                {
                    _outletName = value;
                    RaisePropertyChanged(nameof(OutletName));
                }
            }
        }
        private string _outletActNumber = string.Empty;
        public string OutletActNumber
        {
            get => _outletActNumber;
            set
            {
                if (value != null)
                {
                    _outletActNumber = value;
                    RaisePropertyChanged(nameof(OutletActNumber));
                }
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
            OnActivityItemButtonClick = new Command((_enum) => {
                Console.WriteLine(_enum);
                navigationService.NavigateTo(App.ActivityItemPage, new ActivityNavigationModels()
                {
                    openedTab = (EstablishmentOutletActivitiesTabsEnum)_enum,
                    taxPayerDetails = taxPayerDetails
                });
            });
        }
        #endregion

        #region Method
        public void OnAppearing()
        {
            if(currentTab == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
            {
                OutletActNumber = $"{Int16.Parse(newNumber?.Actno):000}";
            }
        }
        private void navigateToNext()
        {
            if(currentTab == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.ActivityDetails)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.AddressDetails;
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
            {
                _navigationService.GoBack();
            }
        }
        private void navigateToPre()
        {
            if (currentTab == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.ActivityDetails)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
            }
        }
        private void fetchTabDataAndBind(EstablishmentRegistrationOutletTabsEnum _enum)
        {
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
                {
                    //OutletNumber number = await WebServiceManager.ESTOutletNumber(taxPayerDetails?.Fbnumx);
                    //OutletActNumber = $"{Int16.Parse(newNumber?.Actno):000}";
                    //taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("03", "3102448184", "DKOTHI-C@GAZT.GOV.SA", OutletActNumber, taxPayerDetails?.Fbnumx);
                }
                else if (_enum == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
                {

                }
                else
                {
                    //activityList = await WebServiceManager.ESTOutletGetActivitySetsList();
                }
            }catch(Exception e)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}
