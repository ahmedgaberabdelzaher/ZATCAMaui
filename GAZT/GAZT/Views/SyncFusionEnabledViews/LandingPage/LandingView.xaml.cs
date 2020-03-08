using GAZT;
using GAZTeServicesApp.ViewModels.LandingPage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace GAZTeServicesApp.Views.LandingPage
{
    /// <summary>
    /// Page to show the article tile
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPageView : ContentPage
    {
        LandingPageViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPageView" /> class.
        /// </summary>
        public LandingPageView()
        {
            try
            {
                InitializeComponent();
                this.BindingContext = viewModel = App.Locator.LandingPageView;
                ParentContainer.RaiseChild(BusyIndicator);

                SetLTR();
              //  Application.Current.Resources["GAZTFontBold"] = Application.Current.Resources["GAZTBoldArabic"];
            }
            catch (Exception ex)
            {
                int i = 0;
            }
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            await  viewModel.LoadDashboardData();

            viewModel.PopulateReturnsInformation();
            viewModel.PopulateBillsInformation();
            viewModel.PopulateBillsAndReturnsSchedule();
            viewModel.PopulateeServicesApplicableToTheTaxPayer();
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo("OptionsPageView", true);
        }
    }
}