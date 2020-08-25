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
            ChangeAeroIcon();
            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                MobileNumberCodeEntry.HorizontalTextAlignment = TextAlignment.End;
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                MobileNumberCodeEntry.HorizontalTextAlignment = TextAlignment.Start;
            }
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

            // Default
            viewModel.IsLoading = false;

            TaxPayerProfile TPAPIResponse = await viewModel.GetTPProfileData();
            System.Diagnostics.Debug.WriteLine("TP SUCCESS RESPONSE: ", TPAPIResponse);

            if (TPAPIResponse != null)
            {
                // * Update
                App.TP = TPAPIResponse;
                App.TP.Userid = TPAPIResponse.Tin;

                viewModel.TPProfileNameLbl = TPAPIResponse.Name;
                viewModel.TINLabel = TPAPIResponse.Tin;
                String lang = "E";
                if (App.IsArabic == true)
                    lang = "A";
                if (TPAPIResponse != null && TPAPIResponse.Tin != null)
                {
                    //try
                    //{
                    //    String mobilenumber = await WebServiceManager.GAZTGetTaxPayerProfile(TPAPIResponse.Tin, lang);
                    //    // PopToRootPage();
                    //    if (mobilenumber != null)
                    //    {
                    //        string MobileNo = "+" + mobilenumber.Substring(mobilenumber.Length - 12);
                    //        viewModel.MobileNumber = MobileNo;
                    //    }
                    //}
                    //catch (Exception ex)
                    //{ 

                    //}
                    if (TPAPIResponse.Mobile.Length < 12)
                    {
                        viewModel.MobileNumber = "+966" + TPAPIResponse.Mobile.Remove(0, 2);
                    }
                    else
                    {
                       
                            viewModel.MobileNumber = "+" + TPAPIResponse.Mobile.Remove(0, 2);

                    }

                    //viewModel.MobileNumber = TPAPIResponse.Mobile;
                }
                        viewModel.EmailEntry = TPAPIResponse.Email;
                viewModel.PasswordEntry = "********";
            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await viewModel.GetTinStatusDATA();
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }
    }
}