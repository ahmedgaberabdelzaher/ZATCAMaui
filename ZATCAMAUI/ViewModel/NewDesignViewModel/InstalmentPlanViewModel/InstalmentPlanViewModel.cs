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
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            ZakatBtnTapped = new Command(ZakatBtnClicked);
            IncomeTaxBtnTapped = new Command(IncomeTaxBtnClicked);
            VatBtnTapped = new Command(VatBtnClicked);
            instalmentPlanModel = new InstalmentPlanModel();
            SelectedOutletOption = new InstalmentPlanModel();


        }

        #region Commands

        public ICommand ZakatBtnTapped { get; set; }
        public ICommand IncomeTaxBtnTapped { get; set; }
        public ICommand VatBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }


        #endregion

        #region Button Actions

        public void ZakatBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);
                //_navigationService.NavigateTo(App.ZakatInstalmentPlanListPageView);
            }
            catch (GAZTUnlockAccountException)
            {



            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void IncomeTaxBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);

            }
            catch (GAZTUnlockAccountException)
            {



            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void VatBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.VatInstalmentPlanListPageView);
            }
            catch (GAZTUnlockAccountException)
            {



            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
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
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
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

            if (App.LoginDataRetrieved.VtReg == "X" || App.LoginDataRetrieved.VtReg == "R")
            {

                outletDecisionOptions.Add(new InstalmentPlanModel
                {
                    ActiveOutletDecisionOptions = AppResources.DBSMVATInstalmentPlan,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
            }


            OutletDecisionOptions = outletDecisionOptions;

            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }
        async void SimulateStartup()
        {
            await Task.Delay(3000); // Simulate a bit of startup work.

        }
    }
}
