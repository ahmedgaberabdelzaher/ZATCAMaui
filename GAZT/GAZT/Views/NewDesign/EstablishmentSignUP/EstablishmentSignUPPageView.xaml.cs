using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentSignUPPageView : ContentPage
    {
        EstablishmentSignUPPageViewModel viewModel;
        public EstablishmentSignUPPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.EstablishmentSignUPPageView;
            BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

        }

        protected override async  void OnAppearing()
        {
            base.OnAppearing();

            if (App.IsArabic)
            {
                backArrow.Rotation = 180;
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                backArrow.Rotation = 0;
                this.FlowDirection = FlowDirection.LeftToRight;
            }

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            await Task.Run(() =>
            {
              viewModel.IsLoading = false;
              viewModel.IndividualBackImg= "vat_tile_listofsignup_W.png";
              viewModel.EstablishmentBackImg = "vat_tile_listofsignup_W.png";
            });
        }

        private async void OnEstablishmentTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
                viewModel.IndividualBackImg = "vat_tile_listofsignup_W.png";
                viewModel.EstablishmentBackImg = "vat_tile_listofsignup.png";

            });

            //viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);

            });
        }

        private async  void OnIndividualTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
                viewModel.IndividualBackImg = "vat_tile_listofsignup.png";
                viewModel.EstablishmentBackImg = "vat_tile_listofsignup_W.png";

            });

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView);

            });
        }

        private void OnBackArrowTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
    }
}