using EGAZT.Helper;
using EGAZT.Models.EnumModels;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;
using WebView = Xamarin.Forms.WebView;

namespace EGAZT.Views.NewDesign
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupportPageView : ContentPage
    {
        SupportPageViewModel viewModel;
        public SupportPageView()
        {

            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.SupportPageView;
            BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            SetPickerFont();
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;


            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;


                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
            {
                BranchLocation.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                BranchLocation.BackgroundColor =  (Color)Application.Current.Resources["White"];
            }



            //if (App.IsArabic)
            //{
            //    this.FlowDirection = FlowDirection.RightToLeft;
            //    Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            //}
            //else
            //{
            //    this.FlowDirection = FlowDirection.LeftToRight;
            //}




            

            if (App.isFromDashboard) {

                viewModel.currentTab = SupportTabEnum.Chat;

                viewModel.setChat();
                if (App.IsArabic)
                {

                    ChatWebView.Source = Constants.GAZTChatPartialUrlar;
                }
                else
                {
                    ChatWebView.Source = Constants.GAZTChatPartialUrlen;
                }
            }
            else {

                SetLocationToMap();
                if (viewModel.currentTab == SupportTabEnum.Parent)
                {
                    viewModel.setSupportTab();
                }
            }



            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            App.isFromDashboard = false;
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            BranchLocation.HeaderFontFamily = "Somar-SemiBold";
                            BranchLocation.ColumnHeaderFontFamily = "Somar-SemiBold";
                            BranchLocation.SelectedItemFontFamily = "Somar-SemiBold";
                            BranchLocation.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        BranchLocation.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        BranchLocation.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        BranchLocation.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        BranchLocation.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy 

                        break;
                }
            }
            catch (Exception)
            {
                
                
            }

        }
        private void SetLocationToMap()
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
                catch (FeatureNotSupportedException ex)
                {
                    
                    
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException ex)
                {
                    
                    
                    // Handle not enabled on device exception
                }
                catch (PermissionException ex)
                {
                    
                    
                    // Handle permission exception
                }
                catch (Exception)
                {
                    // Unable to get location
                    
                    
                }
            }
            catch (Exception)
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

            if (App.isFromDashboard) {

                viewModel._navigationService.GoBack();

            }
            else {

                GoToBackStep();
            }

            
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
            if(viewModel.PageTitle != AppResources.NDSuggestionsandComplaints)
            {
                viewModel.IsLoading = true;
            }
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
                //ContactWebView.Source = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                ContactWebView.Source = Constants.GAZTFAQARUrl;
            }
            else
            {

                // ContactWebView.Source = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                ContactWebView.Source = Constants.GAZTFAQEnUrl;

            }
        }

        private void OnChatTapped(object sender, EventArgs e)
        {
            viewModel.setChat();
            //var browser = new WebView();
            //var htmlSource = new HtmlWebViewSource();

            if (App.IsArabic)
            {

                ChatWebView.Source = Constants.GAZTChatPartialUrlar;
            }
            else
            {
                ChatWebView.Source = Constants.GAZTChatPartialUrlen;
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
            if (e.Url.Contains(Constants.GAZTChatPartialUrlen) || e.Url.Contains(Constants.GAZTChatPartialUrlar))
            {
                viewModel.IsLoading = false;
            }
            else
            {
                viewModel.IsLoading = true;
            }
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
                //ContactUsWebView.Source = "https://gazt.gov.sa/ar/contactus/Pages/default.aspx";
                ContactUsWebView.Source = Constants.GAZTSuggestURLar;
            }
            else
            {
                //ContactUsWebView.Source = "https://gazt.gov.sa/en/contactus/Pages/default.aspx";
                ContactUsWebView.Source = Constants.GAZTSuggestURLen;
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
        //  ContactUsWebView.IsVisible = true;        
            Uri uri = new Uri("https://twitter.com/zatca_care");
            Launcher.OpenAsync(uri);
            //   ContactUsWebView.Source = "https://twitter.com/GAZT_KSA";

        }
        private void OnTwitterVATTapped(object sender, EventArgs e)
        {

            viewModel.PageTitle = AppResources.ZakatInstalmetSelectTypeVAT;
            //ContactUsWebView.IsVisible = true;
            Uri uri = new Uri("https://twitter.com/saudivat");
            Launcher.OpenAsync(uri);
            // ContactUsWebView.Source = "https://twitter.com/saudivat";



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
            catch (FeatureNotSupportedException ex)
            {
                // Email is not supported on this device
                
                
            }
            catch (Exception)
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
            catch (ArgumentNullException ex)
            {
                
                
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                
                
                // Phone Dialer is not supported on this device.
            }
            catch (Exception)
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
            catch (ArgumentNullException ex)
            {
                // Number was null or white space
                
                
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                
                
            }
            catch (Exception)
            {
                // Other error has occurred.
                
                
            }
        }
    }
}