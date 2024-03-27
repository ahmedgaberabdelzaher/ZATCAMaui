using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
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
            On<iOS>().SetUseSafeArea(true);


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (App.IsArabic)
            {
                backArrow.Rotation = 180;
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                backArrow.Rotation = 0;
                FlowDirection = FlowDirection.LeftToRight;
            }

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

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
           await PopupNavigation.Instance.PushAsync(new NafathPopUpPage());//CR6094
        }

        private void OnBackArrowTapped(object sender, TappedEventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
    }
}