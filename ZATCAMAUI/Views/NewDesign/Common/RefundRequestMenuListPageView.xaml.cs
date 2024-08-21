
using Syncfusion.Maui.ListView;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefundRequestMenuListPageView : ContentPage
    {
        private GeneralServicesViewModel _viewModel;

        public RefundRequestMenuListPageView()
        {
            InitializeComponent();

            _viewModel = App.Locator.GeneralServicesListView;
            BindingContext = _viewModel;
            _viewModel.PopulateRefundRequestMenuListData();
        }





        private void RefundRequest_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            try
            {
                var selectedLv = sender as SfListView;
                GeneralServicesViewModel.GeneralServicesListModel selectedItem = (GeneralServicesViewModel.GeneralServicesListModel)selectedLv.SelectedItem;

                if (selectedItem.ZDTitle == AppResources.VATRefundsRequest)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
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