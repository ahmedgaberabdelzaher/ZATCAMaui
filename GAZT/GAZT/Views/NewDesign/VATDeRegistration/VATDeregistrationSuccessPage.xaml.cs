using System;
using System.Collections.Generic;
using System.Globalization;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATDeRegistration
{
    public partial class VATDeregistrationSuccessPage : ContentPage
    {
        VATDeregistrationSuccessPageViewModel viewModel;
        public VATDeregistrationSuccessPage(VATDeRegistrationDetails response)
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeregistrationSuccessPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetLTR();

            if (response != null)
            {
                if (response.d != null)
                {
                    Label_Name.Text = response.d.Contactnm;
                    Label_ApplicationNumber.Text = response.d.Fbnumx;
                    string StartdateToshow = JsonConvert.DeserializeObject<DateTime>(@"""" + DateTime.Today.Date + @"""").ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    Label_Date.Text = StartdateToshow;
                }
            }
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
            }
        }
        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);

        }

   
        protected override bool OnBackButtonPressed() => true;

        private void btnDashboard_Clicked(object sender, EventArgs e)
        {

            var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(firstPageToRemove);
            var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(secondPageToRemove);
            viewModel._navigationService.GoBack();
        
        }
        private async void Image_Copy_Tapped(object sender, EventArgs e)
        {
            if (Label_ApplicationNumber != null)
            {
                Clipboard.SetTextAsync(Label_ApplicationNumber.Text);
                if (Clipboard.HasText)
                {
                    var text = await Clipboard.GetTextAsync();
                    var displayText = AppResources.VATRSAppNumber + " " + text;
                    viewModel._dialogService.ShowMessage(displayText, AppResources.Copied);
                }
            }

        }
    }
}
