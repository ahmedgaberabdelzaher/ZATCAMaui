using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.Views.NewDesign.Common;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class ActivityItemPageViewModel : BaseViewModel
    {
        #region variables
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        private ActivitySetsList activityList = null;
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
        private TaxpayerNationality _cRIssueCountry = null;
        public TaxpayerNationality CRIssueCountry
        {
            get => _cRIssueCountry;
            set
            {
                if (value != null)
                {
                    _cRIssueCountry = value;
                    RaisePropertyChanged(nameof(CRIssueCountry));
                }
            }
        }

        private List<TaxpayerNationality> _taxpayerFullNationlityList;
        public List<TaxpayerNationality> TaxpayerFullNationlityList
        {
            get => _taxpayerFullNationlityList;
            set
            {
                _taxpayerFullNationlityList = value;
                RaisePropertyChanged(nameof(TaxpayerFullNationlityList));
            }
        }
        #endregion

        #region commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }
        public ICommand OnNatinalitySelectButtonClick { get; set; }
        #endregion

        #region Constructor
        public ActivityItemPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigationService.GoBack());
            OnNatinalitySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerFullNationlityList);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        CRIssueCountry = item as TaxpayerNationality;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
        }
        #endregion

        #region Method
        private void navigateToNext()
        {
            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                CurrentTab = EstablishmentOutletActivitiesTabsEnum.ActivityList;
            }
            else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails || CurrentTab == EstablishmentOutletActivitiesTabsEnum.ActivityList)
            {
                _navigationService.GoBack();
            }
        }
        public void OnAppearing()
        {
            fetchTabDataAndBind();
        }

        private async void fetchTabDataAndBind()
        {
            try
            {
                IsLoading = true;
                TaxpayerFullNationlityList = await WebServiceManager.ESTTaxPayerNationality("FOREIGN");
                activityList = await WebServiceManager.ESTOutletGetActivitySetsList();
            }
            catch (Exception e)
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
