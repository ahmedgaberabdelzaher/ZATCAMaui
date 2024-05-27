using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class GeneralServicesListPageView : ContentPage
    {
        private GeneralServicesViewModel _viewModel;
        public GeneralServicesListPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();

            _viewModel = App.Locator.GeneralServicesListView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = _viewModel;
            _viewModel.PopulateGeneralServicesListData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
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
                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch (Exception)
            {



            }
        }
    }
}