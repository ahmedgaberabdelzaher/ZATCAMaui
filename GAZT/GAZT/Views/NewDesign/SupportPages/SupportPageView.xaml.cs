using EGAZT.Helper;
using EGAZT.Models.EnumModels;
using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupportPageView : ContentPage
    {
        SupportPageViewModel viewModel;
        public SupportPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.SupportPageView;
            BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetPickerFont();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.Android)
            {
                BranchLocation.BackgroundColor = Color.FromHex("#f7f7f7");
            }
            else
            {
                BranchLocation.BackgroundColor = Color.FromHex("#FFFFFF");
            }

            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }

            SetLocationToMap();

            if(viewModel.currentTab== SupportTabEnum.Parent)
            {
                viewModel.setSupportTab();
            }
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            BranchLocation.HeaderFontFamily = "SSTArabic-Medium";
                            BranchLocation.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            BranchLocation.SelectedItemFontFamily = "SSTArabic-Medium";
                            BranchLocation.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        BranchLocation.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        BranchLocation.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        BranchLocation.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        BranchLocation.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy 

                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        private async void SetLocationToMap()
        {
            try
            {
                double lat = 24.655933, lon = 46.713687;
                try
                {
                    Position position = new Position(lat, lon);
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.444));
                    mapView.MoveToRegion(mapSpan);

                    Pin pin = new Pin
                    {
                        Label = "General Authority of Zakat and Tax - Headquarter",
                        ///Address = AppResources.NDHeadQAddress,
                        Type = PinType.Place,
                        Position = new Position(lat, lon)
                    };
                    mapView.Pins.Add(pin);
                }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            }
            catch(Exception e)
            {

            }
        }

        protected override bool OnBackButtonPressed()
        {
            GoToBackStep();
            return true;
        }
        private void OnBackArrowTapped(object sender, EventArgs e)
        {
            GoToBackStep();
        }

        public void GoToBackStep()
        {

            ContactUsWebView.Source = "about:blank";
            viewModel.IsLoading = false;
            if (ContactUsWebView.IsVisible)
            {
                viewModel.IsLoading = false;
                ContactUsWebView.IsVisible = false;
                viewModel.setContactUs();
            }
            else
            {
                viewModel.ChcekCurrentTab();
            }
        }

        private void OnBranchLocatorTapped(object sender, EventArgs e)
        {
            viewModel.SetBranchLocator();
        }

        private void Branch_Clicked(object sender, EventArgs e)
        {
            BranchLocation.IsOpen = true;
        }

        private void BranchLocation_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ContactWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        private void ContactWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }

        private void OnFAQTapped(object sender, EventArgs e)
        {
            viewModel.setFAQ();
            if (App.IsArabic)
            {
                ContactWebView.Source = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/default.aspx";
            }
            else
            {
                ContactWebView.Source = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/default.aspx";
            }
        }

        private void OnChatTapped(object sender, EventArgs e)
        {
            viewModel.setChat();
            if (App.IsArabic)
            {
                ChatWebView.Source = "https://chat.gazt.gov.sa/I3root/index.html?lang=ar";
            }
            else
            {
                ChatWebView.Source = "https://chat.gazt.gov.sa/I3root/index.html?lang=en";
            }
        }

        private void OnContactUsTapped(object sender, EventArgs e)
        {
            viewModel.setContactUs();
            //if (App.IsArabic)
            //{
            //    //ContactUsWebView.Source = "https://gazt.gov.sa/ar/contactus/Pages/default.aspx";
            //}
            //else
            //{
            //    //ContactUsWebView.Source = "https://gazt.gov.sa/en/contactus/Pages/default.aspx";
            //}
        }

        private void ChatWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        private void ChatWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }

        private void SuggestionsandComplaintsClicked(object sender, EventArgs e)
        {
       
            viewModel.PageTitle = AppResources.NDSuggestionsandComplaints;
            ContactUsWebView.IsVisible = true;
            if (App.IsArabic)
            {
                ContactUsWebView.Source = "https://gazt.gov.sa/ar/contactus/Pages/default.aspx";
            }
            else
            {
                ContactUsWebView.Source = "https://gazt.gov.sa/en/contactus/Pages/default.aspx";
            }
        }

        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);
        }

        private void Aboutus_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.AboutUsPageView);
        }
        private void OnTwitterGAZTTapped(object sender, EventArgs e)
        {
           
        
            viewModel.PageTitle = AppResources.NDGAZT;
            ContactUsWebView.IsVisible = true;
           
                ContactUsWebView.Source = "https://twitter.com/GAZT_KSA";
           
        }
        private void OnTwitterVATTapped(object sender, EventArgs e)
        {
            
            viewModel.PageTitle = AppResources.ZakatInstalmetSelectTypeVAT;
            ContactUsWebView.IsVisible = true;
            
            ContactUsWebView.Source = "https://twitter.com/saudivat";

        }

        private async void OnEmailTapped(object sender, EventArgs e)
        {
            try
            {
                //var message = new EmailMessage
                //{
                //    Subject = "",
                //    Body = "",
                //    To = "info@gazt.gov.sa",
                //    //Cc = ccRecipients,
                //    //Bcc = bccRecipients
                //};
                //await Email.ComposeAsync(message);
                await Xamarin.Essentials.Email.ComposeAsync("", "", Email.Text);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }

        private void OnInternationMobileTapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open(InternationalPhone.Text);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        private void OnMobileTapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open(LocalPhone.Text);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}