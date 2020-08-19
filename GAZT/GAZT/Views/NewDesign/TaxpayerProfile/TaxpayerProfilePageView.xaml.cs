using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void OnBackArrowBtnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.GoBack();
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // * Last Updated Taxpayer Profile Data
            /*viewModel.TINLabel = App.TP.Tin;

            var result = Regex.Match(App.TP.Mobile, @"(.{9})\s*$");
            viewModel.MobileNumber = result.ToString();

            viewModel.EmailEntry = App.TP.Email;
            viewModel.PasswordEntry = "********";*/

            /*viewModel.TINLabel = "0987654321".Remove(3);  
            viewModel.MobileNumber = "1234567890";
            viewModel.EmailEntry = "TESTS@PM.COM";
            viewModel.PasswordEntry = "********";*/


            TaxPayerProfile TPAPIResponse = await viewModel.GetTPProfileData();
            System.Diagnostics.Debug.WriteLine("TP SUCCESS RESPONSE: ", TPAPIResponse);

            if(TPAPIResponse != null)
            {
                viewModel.TINLabel = TPAPIResponse.Tin;

                var result = Regex.Match(TPAPIResponse.Mobile, @"(.{9})\s*$");
                viewModel.MobileNumber = result.ToString();

                viewModel.EmailEntry = TPAPIResponse.Email;
                viewModel.PasswordEntry = "********";
            }
        }
    }
}