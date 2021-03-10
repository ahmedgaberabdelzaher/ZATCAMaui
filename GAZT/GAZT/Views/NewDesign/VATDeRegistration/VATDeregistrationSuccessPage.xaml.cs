using System;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATDeRegistration
{
    [Preserve(AllMembers = true)]
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
                    viewModel.FBNumber = response.d.Fbnumx;
                    //string StartdateToshow = JsonConvert.DeserializeObject<DateTime>(@"""" + DateTime.Today.Date + @"""").ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    string StartdateToshow = DateTime.Today.Date.ToString("yyyy/MM/dd").Replace('-', '/');
                    Label_Date.Text = StartdateToshow;
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
        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {
            var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(firstPageToRemove);

            viewModel._navigationService.GoBack();
        }

        protected override bool OnBackButtonPressed() => true;

        private async void Image_Copy_Tapped(object sender, EventArgs e)
        {
            if (Label_ApplicationNumber != null)
            {
                await Clipboard.SetTextAsync(Label_ApplicationNumber.Text);
                if (Clipboard.HasText)
                {
                    var text = await Clipboard.GetTextAsync();
                    var displayText = AppResources.VATRSAppNumber + " " + text;
                    await viewModel._dialogService.ShowMessage(displayText, AppResources.Copied);
                }
            }

        }

        void btnDash_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(firstPageToRemove);

                viewModel._navigationService.GoBack();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
    }
}
