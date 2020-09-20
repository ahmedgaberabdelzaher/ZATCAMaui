using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
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
        }

        protected override void OnAppearing()
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
            if (ContactUsWebView.IsVisible)
            {
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

        }

        private void ContactWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {

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
            if (App.IsArabic)
            {
                //ContactUsWebView.Source = "https://gazt.gov.sa/ar/contactus/Pages/default.aspx";
            }
            else
            {
                //ContactUsWebView.Source = "https://gazt.gov.sa/en/contactus/Pages/default.aspx";
            }
        }

        private void ChatWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {

        }

        private void ChatWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {

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
    }
}