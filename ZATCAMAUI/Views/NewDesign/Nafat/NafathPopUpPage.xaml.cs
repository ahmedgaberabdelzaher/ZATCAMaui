
using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Nafat;

namespace ZATCAMAUI.Views.NewDesign.Nafat
{
    public partial class NafathPopUpPage : PopupPage
    {
        NafathPopupPageViewModel viewModel;
        public NafathPopUpPage()
        {
            InitializeComponent();
            BindingContext = viewModel = App.Locator.NafathPopupPageViewModel;
        }

        private async void TappedGulf(object sender, EventArgs e)
        {
            App.successMsg = true;
            GulfImage.Source = "vat_tile_listofsignup.png";
            GulfText.TextColor = Colors.White;
            CitigenImage.Source = "vat_tile_listofsignup_W.png";
            CitigenText.TextColor = Colors.CadetBlue;

            await MopupService.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView, "Gulf");
        }
        private async void TappedCitizen(object sender, EventArgs e)
        {
            try
            {
                App.successMsg = false;
                GulfImage.Source = "vat_tile_listofsignup_W.png";
                GulfText.TextColor = Colors.CadetBlue;
                CitigenImage.Source = "vat_tile_listofsignup.png";
                CitigenText.TextColor = Colors.White;


                App.GUIDFrSSO = "";

                await MopupService.Instance.PopAsync();
                viewModel._navigationService.NavigateTo(App.NafathLoginView, ZATCAConstants.NAFATH_SIGNUP);
            }
            catch (Exception)
            {
            }

        }


    }
}

