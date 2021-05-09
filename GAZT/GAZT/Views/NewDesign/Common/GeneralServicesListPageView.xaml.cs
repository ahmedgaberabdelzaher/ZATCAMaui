using System;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class GeneralServicesListPageView : ContentPage
    {
        private GeneralServicesViewModel _viewModel;
        public GeneralServicesListPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();

            SetLTR();

            _viewModel = App.Locator.GeneralServicesListView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = _viewModel;
            _viewModel.PopulateGeneralServicesListData();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            //Check for Large Tax payer or not
           // await _viewModel.CheckUserIsLargeTaxPayerOrNot();
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
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
                else if(selectedItem.ZDTitle== AppResources.NDRelationContact)
                {
                    //var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Relationship manager contact details Tapped", "Relationship manager contact details");
                    _viewModel._navigationService.NavigateTo(App.RelationShipManagerInfoPageView);
                   // AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
            
                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}