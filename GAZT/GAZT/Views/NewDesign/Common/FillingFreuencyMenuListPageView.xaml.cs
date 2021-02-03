using System;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Common
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingFreuencyMenuListPageView : ContentPage
    {
        private GeneralServicesViewModel _viewModel;

        public FillingFreuencyMenuListPageView()
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
            _viewModel.PopulateFillingFrequencyListData();
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

        private void FillingFrequency_Menu_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            try
            {
                var selectedLv = sender as SfListView;
                GeneralServicesViewModel.GeneralServicesListModel selectedItem = (GeneralServicesViewModel.GeneralServicesListModel)selectedLv.SelectedItem;

                if (selectedItem.ZDTitle == AppResources.ChangeVatFillingFrequency)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeFillingPeriod_Tapped", "Change Filing Period eService");
                    Device.BeginInvokeOnMainThread(() =>
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