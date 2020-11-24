using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.InstalmentPlanModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.InstalmentPlanViewModel
{
    public class InstalmentPlanViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        public InstalmentPlanViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            ZakatBtnTapped = new Command(this.ZakatBtnClicked);
            IncomeTaxBtnTapped = new Command(this.IncomeTaxBtnClicked);
            VatBtnTapped = new Command(this.VatBtnClicked);
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

        public async void ZakatBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);
            }
            catch (GAZTUnlockAccountException ex)
            {



            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void IncomeTaxBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);

            }
            catch (GAZTUnlockAccountException ex)
            {



            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void VatBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.VatInstalmentPlanListPageView);
            }
            catch (GAZTUnlockAccountException ex)
            {



            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
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
                RaisePropertyChanged("InstalmentPlanModel");
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
                RaisePropertyChanged("SelectedOutletOptionIndex");
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
                RaisePropertyChanged("IsZakatSelected");
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
                RaisePropertyChanged("IsIncomeTaxViewEnabled");
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
                RaisePropertyChanged("SelectedOutletOption");
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
                RaisePropertyChanged("OutletDecisionOptions");
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

            if (App.LoginDataRetrieved.VtReg == "X") {

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
