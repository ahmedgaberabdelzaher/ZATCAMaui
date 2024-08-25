
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

            _viewModel = App.Locator.ObjectionsSelectionPageView;

            BindingContext = _viewModel;

            _viewModel.AddSelectionOptions();

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

                _viewModel.AddSelectionOptions();

            }
            catch (Exception)
            {


            }
        }
    }
}