using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel
{
    public class NewTaxEvasionFormPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Variable
        private NewTaxEvasionTabEnum _currentTab = NewTaxEvasionTabEnum.ReporterInfo;
        public NewTaxEvasionTabEnum currentTab
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
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 4;
        #endregion

        #region Property
        private string _PageTitle = "Reporter Information";
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

        private string _BodyTitle = "Complete the below details";
        public string BodyTitle
        {
            get
            {
                return _BodyTitle;
            }
            set
            {
                _BodyTitle = value;
                RaisePropertyChanged("BodyTitle");
            }
        }
        #endregion

        #region Commands
        public ICommand OnContinueClicked { get; set; }
        public ICommand OnBackStepClicked { get; set; }
        #endregion

        #region Constructor
        public NewTaxEvasionFormPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnContinueClicked = new Command(() => navigateToNext());
            OnBackStepClicked = new Command(() => navigateToBack());
        }
        #endregion

        #region Method
        private void navigateToNext()
        {
            switch (currentTab)
            {
                case NewTaxEvasionTabEnum.ReporterInfo: currentTab = NewTaxEvasionTabEnum.FacilityInfo;
                    PageTitle = "Facility Information";
                    break;

                case NewTaxEvasionTabEnum.FacilityInfo: currentTab = NewTaxEvasionTabEnum.ReportDetails;
                    PageTitle = "Report Details";
                    break;

                case NewTaxEvasionTabEnum.ReportDetails: 
                    currentTab = NewTaxEvasionTabEnum.Summary;
                    PageTitle = "Summary";
                    BodyTitle = "Review the below information";
                    break;
            }
        }

        private void navigateToBack()
        {
            switch (currentTab)
            {
                case NewTaxEvasionTabEnum.Summary: 
                    currentTab = NewTaxEvasionTabEnum.ReportDetails;
                    PageTitle = "Report Details";
                    BodyTitle = "Complete the below details";
                    break;

                case NewTaxEvasionTabEnum.ReportDetails: 
                    currentTab = NewTaxEvasionTabEnum.FacilityInfo;
                    PageTitle = "Facility Information";
                    break;

                case NewTaxEvasionTabEnum.FacilityInfo:
                    currentTab= NewTaxEvasionTabEnum.ReporterInfo;
                    PageTitle = "Reporter Information";
                    break;
            }

        }
        #endregion
    }
}
