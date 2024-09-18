using System.Collections.ObjectModel;
using System.Windows.Input;


using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.InstalmentPlanModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.InstalmentPlanViewModel
{
    public class InstalmentPlanViewModel : BaseViewModel
    {

        public InstalmentPlanViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            ZakatBtnTapped = new Command(async () => await ZakatBtnClicked());
            IncomeTaxBtnTapped = new Command(async ()=> await IncomeTaxBtnClicked());
            VatBtnTapped = new Command(async () => await VatBtnClicked());
            instalmentPlanModel = new InstalmentPlanModel();
            SelectedOutletOption = new InstalmentPlanModel();


        }

        #region Commands

        public ICommand ZakatBtnTapped { get; set; }
        public ICommand IncomeTaxBtnTapped { get; set; }
        public ICommand VatBtnTapped { get; set; }


        #endregion

        #region Button Actions

        public async Task ZakatBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task IncomeTaxBtnClicked()
        {
            try
            {
                 _navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);

            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task VatBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.VatInstalmentPlanListPageView);
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }


        #endregion

        public InstalmentPlanModel instalmentPlanModel { get; set; }
        public InstalmentPlanModel InstalmentPlanModel
        {
            get
            {
                return instalmentPlanModel;
            }

            set
            {
                if (instalmentPlanModel == value)
                {
                    return;
                }

                instalmentPlanModel = value;
                OnPropertyChanged("InstalmentPlanModel");
            }
        }
        private int _selectedOutletOptionIndex;
        public int SelectedOutletOptionIndex
        {
            get
            {
                return _selectedOutletOptionIndex;
            }
            set
            {
                _selectedOutletOptionIndex = value;
                OnPropertyChanged("SelectedOutletOptionIndex");
            }
        }

        //Zakat Slection starts here

        private bool _isZakatSelected = true;
        public bool IsZakatSelected
        {
            get
            {
                return _isZakatSelected;
            }
            set
            {
                _isZakatSelected = value;
                OnPropertyChanged("IsZakatSelected");
            }
        }
        //Custom Spinner Items starts here

        private bool _isIncomeTaxViewEnabled = false;
        public bool IsIncomeTaxViewEnabled
        {
            get
            {
                return _isIncomeTaxViewEnabled;
            }
            set
            {
                _isIncomeTaxViewEnabled = value;
                OnPropertyChanged("IsIncomeTaxViewEnabled");
            }
        }
        private InstalmentPlanModel _selectedOutletOption;
        public InstalmentPlanModel SelectedOutletOption
        {
            get
            {
                return _selectedOutletOption;
            }
            set
            {
                _selectedOutletOption = value;
                OnPropertyChanged("SelectedOutletOption");
            }
        }

        public ObservableCollection<InstalmentPlanModel> c { get; set; }
        public ObservableCollection<InstalmentPlanModel> outletDecisionOptions { get; set; }
        public ObservableCollection<InstalmentPlanModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                OnPropertyChanged("OutletDecisionOptions");
            }
        }

        public void AddOutletDecisionOptions()
        {

            var outletDecisionOptions = new ObservableCollection<InstalmentPlanModel>();

            if (App.LoginDataRetrieved.ZkReg == "X")
            {

                outletDecisionOptions.Add(new InstalmentPlanModel
                {
                    ActiveOutletDecisionOptions = AppResources.DBSMZakatInstalmentPlan,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
                outletDecisionOptions.Add(new InstalmentPlanModel
                {
                    ActiveOutletDecisionOptions = AppResources.DBSMIncomeTax,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
            }

            if (App.LoginDataRetrieved.VtReg == "X" || App.LoginDataRetrieved.VtReg == "R" || App.LoginDataRetrieved.VtReg == "G")
            {

                outletDecisionOptions.Add(new InstalmentPlanModel
                {
                    ActiveOutletDecisionOptions = AppResources.DBSMVATInstalmentPlan,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
            }


            OutletDecisionOptions = outletDecisionOptions;

        }
    }
}
