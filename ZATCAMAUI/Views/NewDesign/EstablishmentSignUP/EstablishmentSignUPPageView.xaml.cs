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
            try
            {
                InitializeComponent();
                viewModel = App.Locator.EstablishmentSignUPPageView;
                BindingContext = viewModel;
                On<iOS>().SetUseSafeArea(true); 
            }
            catch (Exception)
            {

            }


        }

        protected override async void OnAppearing()
        {
            try
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
            catch (Exception)
            {

            }
        }

        private async void OnEstablishmentTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
                viewModel.IndividualBackImg = "vat_tile_listofsignup_W.png";
                viewModel.EstablishmentBackImg = "vat_tile_listofsignup.png";

            });

            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);

            });
        }

        private async void OnIndividualTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
                viewModel.IndividualBackImg = "vat_tile_listofsignup.png";
                viewModel.EstablishmentBackImg = "vat_tile_listofsignup_W.png";

            });

            MainThread.BeginInvokeOnMainThread(() =>
            {

                PopupNavigation.Instance.PushAsync(new NafathPopUpPage());//CR6094

            });
        }

        private void OnBackArrowTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
    }
}