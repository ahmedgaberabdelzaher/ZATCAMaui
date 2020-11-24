using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.PopulateGeneralServicesListData();
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
                    Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                    Resources["ImageReverse"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                    Resources["ImageReverse"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch (Exception ex)
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
                else if (selectedItem.ZDTitle == AppResources.DBSMTaxEvasionReport)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxEvasion_Tapped", "Tax Evasion eService");
                    _viewModel._navigationService.NavigateTo(App.TaxEvasionPageWebView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
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