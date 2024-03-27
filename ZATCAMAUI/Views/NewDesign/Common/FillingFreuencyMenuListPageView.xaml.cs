using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
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
            ChangeAeroIcon();

            _viewModel = App.Locator.GeneralServicesListView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            _viewModel.PopulateFillingFrequencyListData();
        }

        public void ChangeAeroIcon()
        {
            try
            {
                if (App.IsArabic)
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch (Exception)
            {


            }

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