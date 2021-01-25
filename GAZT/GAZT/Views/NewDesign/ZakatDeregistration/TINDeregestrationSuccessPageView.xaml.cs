using System;
using System.Collections.Generic;
using System.Globalization;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    [Preserve(AllMembers = true)]
    public partial class TINDeregestrationSuccessPageView : ContentPage
    {
        TINDeregestrationSuccessPageViewModel viewModel;
        public TINDeregestrationSuccessPageView(TinDeregistrationResponseModel response)
        {
            InitializeComponent();

            viewModel = App.Locator.TINDeregestrationSuccessPageView;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            if (response != null)
            {
                if (response.Fbnum != null)
                {
                    if (response.ADecName != null)
                        Label_Name.Text = response.ATaxpayerName;

                    Label_ApplicationNumber.Text = response.Fbnum;
                    viewModel.FBNumber = response.Fbnum;
                    //string StartdateToshow = JsonConvert.DeserializeObject<DateTime>(@"""" + DateTime.Today.Date + @"""").ToString("dd/MM/yyyy", new CultureInfo("en-US"));

                    Label_Date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy").Replace('-', '/');
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
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
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
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
        private void btnRegistrationDetailsClicked(object sender, EventArgs e)
        {
            var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(firstPageToRemove);

            viewModel._navigationService.GoBack();
        }

        void btnDash_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(firstPageToRemove);

                var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(secondPageToRemove);

                viewModel._navigationService.GoBack();
            }
            catch (Exception ex)
            {

            }
        }
        void Download_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {

                viewModel.downloadConfirmation();
            }
            catch (Exception ex)
            {

            }
        }

        void SfButton_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {

                viewModel.downloadConfirmation();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
