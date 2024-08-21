
using Syncfusion.Maui.ListView;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingFreuencyMenuListPageView : ContentPage
    {
        private GeneralServicesViewModel _viewModel;

        public FillingFreuencyMenuListPageView()
        {
            InitializeComponent();

            _viewModel = App.Locator.GeneralServicesListView;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.PopulateFillingFrequencyListData();
        }



        private void FillingFrequency_Menu_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            try
            {
                var selectedLv = sender as SfListView;
                GeneralServicesViewModel.GeneralServicesListModel selectedItem = (GeneralServicesViewModel.GeneralServicesListModel)selectedLv.SelectedItem;

                if (selectedItem.ZDTitle == AppResources.ChangeVatFillingFrequency)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeFillingPeriod_Tapped", "Change Filing Period eService");
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

                    });
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