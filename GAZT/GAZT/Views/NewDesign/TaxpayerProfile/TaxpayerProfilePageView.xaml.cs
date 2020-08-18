using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerProfilePageView : ContentPage
    {
        NewTaxpayerProfileViewModel viewModel;

        public TaxpayerProfilePageView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            viewModel = App.Locator.TaxpayerProfilePageView;
            this.BindingContext = viewModel;

            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
        }

        private void OnMobileEditTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new UpdateMobilePopUp());
        }

        private void OnEmailEditTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new UpdateEmailPopUp());
        }

        private void OnPasswordEditTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new UpdatePasswordPopUp());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // * Last Updated Taxpayer Profile Data
            /*viewModel.TINLabel = App.TP.Tin;
            viewModel.MobileNumberEntry = App.TP.Mobile;
            viewModel.EmailEntry = App.TP.Email;
            viewModel.PasswordEntry = "********";*/

            viewModel.TINLabel = "0987654321";
            viewModel.MobileNumberEntry = "1234567890";
            viewModel.EmailEntry = "TESTS@PM.COM";
            viewModel.PasswordEntry = "********";
        }
    }
}