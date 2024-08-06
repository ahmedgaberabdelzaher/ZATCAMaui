using System.Collections.ObjectModel;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;
using static ZATCAMAUI.ViewModel.NewDesignViewModel.Common.GeneralServicesViewModel;

namespace ZATCAMAUI.Views.NewDesign.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class GeneralServicesListPageView : ContentPage
    {
        private GeneralServicesViewModel _viewModel;
        public GeneralServicesListPageView()
        {
            InitializeComponent();
            _viewModel = App.Locator.GeneralServicesListView;
            BindingContext = _viewModel;
            _viewModel.GeneralServicesList = new ObservableCollection<GeneralServicesListModel>();
            _viewModel.generalServicesListData = new List<GeneralServicesListModel>();
            _viewModel.CaseDetailedListViewData = new ObservableCollection<Models.EscalatedGstcModel.CaseDetailsResultSet>();
            _viewModel.PopulateGeneralServicesListData();
            Task.Run(async () => await _viewModel.GetGstcCaseDetailSet()).Wait();

        }


        private void GeneralServices_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            try
            {
                var selectedLv = sender as SfListView;
                GeneralServicesViewModel.GeneralServicesListModel selectedItem = (GeneralServicesViewModel.GeneralServicesListModel)selectedLv.SelectedItem;

                if (selectedItem.ZDTitle == AppResources.NDVATRegistrationVerification)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATLookUp_Tapped", "VAT Registration Verification eService");
                    _viewModel._navigationService.NavigateTo(App.VATLookUpNewPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else if (selectedItem.ZDTitle == AppResources.NDTaxEvasionReport)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxEvasion_Tapped", "Tax Evasion eService");
                    _viewModel._navigationService.NavigateTo(App.TaxEvasionPageWebView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }

                else if (selectedItem.ZDTitle == AppResources.NDBankAccManagement)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "BankAccManagement", "BankAccManagement");
                    _viewModel._navigationService.NavigateTo(App.GAZTBankAccountManagementPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                }
                else if (selectedItem.ZDTitle == AppResources.GSTCEscalatedCasesGSTC)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "GSTCEscalatedCasesGSTC", "GSTCEscalatedCasesGSTC");

                    _viewModel._navigationService.NavigateTo(App.EscalatedCasesGSTCPageView, _viewModel.CaseDetailedListViewData);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch (Exception)
            {



            }
        }
    }
}