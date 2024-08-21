
using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Nafat;

namespace ZATCAMAUI.Views.NewDesign.Nafat
{
    public partial class NafathPopUpPage : PopupPage
    {
        NafathPopupPageViewModel viewModel;
        public NafathPopUpPage()
        {
            InitializeComponent();
            BindingContext = viewModel =App.Locator.NafathPopupPageViewModel;
        }

        private async void TappedGulf(object sender, EventArgs e)
        {
            //this.Navigation.PopAsync();
            App.successMsg = true;
            GulfImage.Source = "vat_tile_IbanCard_background.png";
            GulfText.TextColor = Colors.White;
            CitigenImage.Source = "vat_tile_IbanCard_background_white.png";
            CitigenText.TextColor = Colors.CadetBlue;

            await MopupService.Instance.PopAsync();
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

            await MopupService.Instance.PopAsync();
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

