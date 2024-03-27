using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{
   
    public partial class TINDeregestrationSuccessPageView : ContentPage
    {
        TINDeregestrationSuccessPageViewModel viewModel;
        public TINDeregestrationSuccessPageView(TinDeregistrationResponseModel response)
        {
            InitializeComponent();

            viewModel = App.Locator.TINDeregestrationSuccessPageView;
            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
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
            Padding = safeInsets;
        }
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
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
        private void btnRegistrationDetailsClicked(object sender, EventArgs e)
        {
            var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(firstPageToRemove);

            viewModel._navigationService.GoBack();
        }

        void btnDash_Clicked(object sender, EventArgs e)
        {
            try
            {
                var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(firstPageToRemove);

                var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(secondPageToRemove);

                viewModel._navigationService.GoBack();
            }
            catch (Exception)
            {


            }
        }
        void Download_Clicked(object sender, EventArgs e)
        {
            try
            {

                viewModel.downloadConfirmation();
            }
            catch (Exception)
            {


            }
        }

        void SfButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                viewModel.downloadConfirmation();
            }
            catch (Exception)
            {


            }
        }
    }
}
