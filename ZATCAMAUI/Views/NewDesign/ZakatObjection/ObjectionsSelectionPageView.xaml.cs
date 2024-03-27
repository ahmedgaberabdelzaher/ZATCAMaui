using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatObjection
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectionsSelectionPageView : ContentPage
    {

        private ObjectionViewModel _viewModel;

        public ObjectionsSelectionPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            _viewModel = App.Locator.ObjectionsSelectionPageView;

            BindingContext = _viewModel;

            _viewModel.AddSelectionOptions();

        }
        public void ChangeAeroIcon()
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

        async void outletDecisionOptionsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            try
            {
                ObjectionViewModel.SelectionModel selectedItem = e.AddedItems[0] as ObjectionViewModel.SelectionModel;

                await Task.Delay(1000);
                if (selectedItem.SelectionTitle == AppResources.DBSMVATObjection)
                {
                    _viewModel._navigationService.NavigateTo(App.VatReviewListPageView);

                }
                else if (selectedItem.SelectionTitle == AppResources.DBSMZAKATObjection)
                {
                    _viewModel._navigationService.NavigateTo(App.ZakatObjectionsListPageView);
                }

            }
            catch (Exception)
            {


            }

        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;

                _viewModel.AddSelectionOptions();

            }
            catch (Exception)
            {


            }
        }
    }
}