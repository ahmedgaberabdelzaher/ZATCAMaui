
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATDeRegistration
{

    public partial class VATDeregistrationSuccessPage : ContentPage
    {
        VATDeregistrationSuccessPageViewModel viewModel;
        public VATDeregistrationSuccessPage(VATDeRegistrationDetails response)
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeregistrationSuccessPage;
            this.BindingContext = viewModel;

            if (response != null)
            {
                if (response.d != null)
                {
                    Label_Name.Text = response.d.headerSet.Contactnm;
                    Label_ApplicationNumber.Text = response.d.headerSet.Fbnumx;
                    viewModel.FBNumber = response.d.headerSet.Fbnumx;
                    string StartdateToshow = DateTime.Today.Date.ToString("yyyy/MM/dd").Replace('-', '/');
                    Label_Date.Text = StartdateToshow;
                }
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
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        viewModel._dialogService.ShowMessage(displayText, AppResources.Copied);
                    });
                }
            }

        }

        void btnDash_Clicked(object sender, EventArgs e)
        {
            try
            {
                var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(firstPageToRemove);

                viewModel._navigationService.GoBack();
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
