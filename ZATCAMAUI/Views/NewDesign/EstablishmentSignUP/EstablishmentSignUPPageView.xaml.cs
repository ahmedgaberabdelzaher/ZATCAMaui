using Mopups.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using ZATCAMAUI.Views.NewDesign.Nafat;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentSignUPPageView : ContentPage
    {
        EstablishmentSignUPPageViewModel viewModel;
        public EstablishmentSignUPPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.EstablishmentSignUPPageView;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
                viewModel.IndividualBackImg = "vat_tile_listofsignup_W.png";
                viewModel.EstablishmentBackImg = "vat_tile_listofsignup_W.png";
            });
        }

        private void OnEstablishmentTapped(object sender, TappedEventArgs e)
        {
            viewModel.IsLoading = true;
            viewModel.IndividualBackImg = "vat_tile_listofsignup_W.png";
            viewModel.EstablishmentBackImg = "vat_tile_listofsignup.png";
            viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);

        }

        private async void OnIndividualTapped(object sender, TappedEventArgs e)
        {
            viewModel.IsLoading = true;
            viewModel.IndividualBackImg = "vat_tile_listofsignup.png";
            viewModel.EstablishmentBackImg = "vat_tile_listofsignup_W.png";
            await MopupService.Instance.PushAsync(new NafathPopUpPage());//CR6094
        }
    }
}