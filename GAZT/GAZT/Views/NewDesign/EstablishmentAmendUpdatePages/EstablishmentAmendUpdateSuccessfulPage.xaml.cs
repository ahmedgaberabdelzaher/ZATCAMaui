using System;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentAmendUpdatePages
{
    [Preserve(AllMembers = true)]
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
            this.Padding = safeInsets;
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
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
    }
}
