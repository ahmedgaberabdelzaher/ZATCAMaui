using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentAmendUpdateSuccessfulPage : ContentPage
    {
        private EstablishmentAmendUpdateSuccessfulPageViewModel viewModel;
        public EstablishmentAmendUpdateSuccessfulPage(TaxPayerDetails taxpayerProfile)
        {
            InitializeComponent(); SetLTR();
            viewModel = App.Locator.EstablishmentAmendUpdateSuccessfulPage;
            viewModel.taxPayerDetails = taxpayerProfile;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
            viewModel?.OnAppearing();
        }

        private void GoToDashBoardButtonClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception)
            {


            }

        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
        }
    }
}
