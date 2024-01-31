using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Nafat;

namespace ZATCAMAUI.Views.NewDesign.Nafat
{
    public partial class NafathPopUpPage : PopupPage
    {
        NafathPopupPageViewModel viewModel;
        public NafathPopUpPage()
        {
            InitializeComponent();
            viewModel = App.Locator.NafathPopupPage;
            BindingContext = viewModel;
            On<iOS>().SetUseSafeArea(true);
        }

        private async void TappedGulf(object sender, EventArgs e)
        {
            //this.Navigation.PopAsync();
            App.successMsg = true;
            GulfImage.Source = "vat_tile_IbanCard_background.png";
            GulfText.TextColor = Colors.White;
            CitigenImage.Source = "vat_tile_IbanCard_background_white.png";
            CitigenText.TextColor = Colors.CadetBlue;

            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView, "Gulf");
        }
        private async void TappedCitizen(object sender, EventArgs e)
        {
            App.successMsg = false;
            //this.Navigation.PopAsync();
            GulfImage.Source = "vat_tile_IbanCard_background_white.png";
            GulfText.TextColor = Colors.CadetBlue;
            CitigenImage.Source = "vat_tile_IbanCard_background.png";
            CitigenText.TextColor = Colors.White;


            App.GUIDFrSSO = "";

            await PopupNavigation.Instance.PopAsync();
            try
            {
                viewModel._navigationService.NavigateTo(App.NafathLoginPageView);
            }
            catch (Exception)
            {
            }

        }


    }
}

