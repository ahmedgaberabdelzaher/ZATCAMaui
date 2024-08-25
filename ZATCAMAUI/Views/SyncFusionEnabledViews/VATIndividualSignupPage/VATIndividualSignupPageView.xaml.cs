
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATIndividualSignupPageView : ContentPage
    {
        VATIndividualSignupPageViewModel viewModel;
        public VATIndividualSignupPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATIndividualSignupPageView;
            BindingContext = viewModel;
            // image_individual_tile.Source = "vat_tile_listofsignup.png";
            viewModel.ImageIndividualTile = "vat_tile_listofsignup_W.png";
            //image_estimated_tile.Source = "vat_tile_listofsignup.png";
            viewModel.ImageEstimatedTile = "vat_tile_listofsignup_W.png";
            //image_individual_icon.Source = "vat_new_individual.png";
            viewModel.ImageIndividualIcon = "vat_new_individual_G.png";
            // image_estimated_icon.Source = "vat_new_Establishment_W.png";
            viewModel.ImageEstimatedIcon = "vat_new_Establishment_G.png";
            viewModel.EstimatedTileColor = (Color)Application.Current.Resources["Primary"];
            viewModel.IndividualTileColor = (Color)Application.Current.Resources["Primary"];
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //viewModel.IsLoading = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
            viewModel.IsLoading = false;
            viewModel.ImageIndividualTile = "vat_tile_listofsignup_W.png";

            viewModel.ImageEstimatedTile = "vat_tile_listofsignup_W.png";

            viewModel.ImageIndividualIcon = "vat_new_individual_G.png";

            viewModel.ImageEstimatedIcon = "vat_new_Establishment_G.png";
            viewModel.EstimatedTileColor = (Color)Application.Current.Resources["Primary"];
            viewModel.IndividualTileColor = (Color)Application.Current.Resources["Primary"];
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
    }
}