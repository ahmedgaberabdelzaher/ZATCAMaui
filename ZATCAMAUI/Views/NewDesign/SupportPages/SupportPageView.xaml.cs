

using Maui.GoogleMaps;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.SupportPageVM;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.SupportPages
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
            App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            SetPickerFont();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                BranchLocation.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                BranchLocation.BackgroundColor = (Color)Application.Current.Resources["White"];
            }

            if (App.isFromDashboard)
            {

                viewModel.currentTab = SupportTabEnum.Chat;

                viewModel.setChat();
                if (App.IsArabic)
                {

                    ChatWebView.Source = ZATCAConstants.GAZTChatPartialUrlar;
                }
                else
                {
                    ChatWebView.Source = ZATCAConstants.GAZTChatPartialUrlen;
                }
            }
            else
            {

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
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {

                            BranchLocation.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            BranchLocation.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            BranchLocation.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            BranchLocation.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:
                        {
                            BranchLocation.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            BranchLocation.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            BranchLocation.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            BranchLocation.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";//ddlLIssuedBy

                        }

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
                catch (FeatureNotSupportedException)
                {
                }
                catch (FeatureNotEnabledException )
                {
                }
                catch (PermissionException )
                {
                }
                catch (Exception)
                {
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

            if (App.isFromDashboard)
            {

                viewModel._navigationService.GoBack();

            }
            else
            {

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


        private void ContactWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (viewModel.PageTitle != AppResources.NDSuggestionsandComplaints)
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
                ContactWebView.Source = ZATCAConstants.GAZTFAQARUrl;
            }
            else
            {

                // ContactWebView.Source = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                ContactWebView.Source = ZATCAConstants.GAZTFAQEnUrl;

            }
        }

        private void OnChatTapped(object sender, EventArgs e)
        {
            viewModel.setChat();
            //var browser = new WebView();
            //var htmlSource = new HtmlWebViewSource();

            if (App.IsArabic)
            {

                ChatWebView.Source = ZATCAConstants.GAZTChatPartialUrlar;
            }
            else
            {
                ChatWebView.Source = ZATCAConstants.GAZTChatPartialUrlen;
            }
        }

        private void OnContactUsTapped(object sender, EventArgs e)
        {
            viewModel.setContactUs();

        }

        private void ChatWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains(ZATCAConstants.GAZTChatPartialUrlen) || e.Url.Contains(ZATCAConstants.GAZTChatPartialUrlar))
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
                ContactUsWebView.Source = ZATCAConstants.GAZTSuggestURLar;
            }
            else
            {
                //ContactUsWebView.Source = "https://gazt.gov.sa/en/contactus/Pages/default.aspx";
                ContactUsWebView.Source = ZATCAConstants.GAZTSuggestURLen;
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
                await Email.ComposeAsync("", "", email.Text);
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
           
            catch (Exception)
            {
                // Other error has occurred.


            }
        }
    }
}