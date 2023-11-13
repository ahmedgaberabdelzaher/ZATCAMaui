using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using EGAZT.ViewModel.NewDesignViewModel;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    public partial class NafathPopUpPage : PopupPage
    {
        NafathPopupPageViewModel viewModel;
        public NafathPopUpPage()
        {
            InitializeComponent();
            viewModel = App.Locator.NafathPopupPage;
            BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private async void TappedGulf(object sender, EventArgs e)
        {
            //this.Navigation.PopAsync();
            App.successMsg = true;
            GulfImage.Source = "vat_tile_IbanCard_background.png";
            GulfText.TextColor = Color.White;
            CitigenImage.Source = "vat_tile_IbanCard_background_white.png";
            CitigenText.TextColor = Color.CadetBlue;
       
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView, "Gulf");
        }
        private async void TappedCitizen(object sender, EventArgs e)
        {
            App.successMsg = false;
            //this.Navigation.PopAsync();
            GulfImage.Source = "vat_tile_IbanCard_background_white.png";
            GulfText.TextColor = Color.CadetBlue;
            CitigenImage.Source = "vat_tile_IbanCard_background.png";
            CitigenText.TextColor = Color.White;


            App.GUIDFrSSO = "";

            await PopupNavigation.Instance.PopAsync();
            try
            {
                viewModel._navigationService.NavigateTo(App.NafathLoginPageView);
            }
            catch(Exception)
            {
            }
            
        }

     
    }
}

