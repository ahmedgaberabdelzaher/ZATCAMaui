
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{
   
    public partial class TINDeregestrationSuccessPageView : ContentPage
    {
        TINDeregestrationSuccessPageViewModel viewModel;
        public TINDeregestrationSuccessPageView(TinDeregistrationResponseModel response)
        {
            InitializeComponent();

            viewModel = App.Locator.TINDeregestrationSuccessPageView;
            BindingContext = viewModel;
            if (response != null)
            {
                if (response.Fbnumz != null)
                {
                    if (response.ADecName != null)
                        Label_Name.Text = response.ATaxpayerName;

                    Label_ApplicationNumber.Text = response.Fbnumz;
                    viewModel.FBNumber = response.Fbnumz;
                    Label_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
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
