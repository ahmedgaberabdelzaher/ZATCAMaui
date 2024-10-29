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
            ItemSelectedCommand = new Command<object>(async (obj) => await ItemSelectedMethod(obj));
            ZakatBtnTapped = new Command(async () => await ZakatBtnClicked());
            IncomeTaxBtnTapped = new Command(async ()=> await IncomeTaxBtnClicked());
            VatBtnTapped = new Command(async () => await VatBtnClicked());
            instalmentPlanModel = new InstalmentPlanModel();
            SelectedOutletOption = new InstalmentPlanModel();


        }

        private async Task ItemSelectedMethod(object obj)
        {
            var selectedItem = (obj as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as InstalmentPlanModel;
            SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(selectedItem);
            if (selectedItem.ActiveOutletDecisionOptions == AppResources.DBSMZakatInstalmentPlan)
            {
                IsZakatSelected = true;
                IsIncomeTaxViewEnabled = false;
                Preferences.Set("isZakat", true);
                await ZakatBtnClicked();
            }
            else if (selectedItem.ActiveOutletDecisionOptions == AppResources.DBSMIncomeTax)
            {
                IsZakatSelected = false;
                IsIncomeTaxViewEnabled = true;
                Preferences.Set("isZakat", false);
                await IncomeTaxBtnClicked();
            }
            else
            {
                IsZakatSelected = false;
                IsIncomeTaxViewEnabled = false;
                await VatBtnClicked();
            }
        }

        #region Commands

        public ICommand ItemSelectedCommand { get; set; }
        public ICommand ZakatBtnTapped { get; set; }
        public ICommand IncomeTaxBtnTapped { get; set; }
        public ICommand VatBtnTapped { get; set; }


        #endregion

        #region Button Actions

        public async Task ZakatBtnClicked()
        {
            try
            {
               await _navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);
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
                await _navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);

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
              await  _navigationService.NavigateTo(App.VatInstalmentPlanListPageView);
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
