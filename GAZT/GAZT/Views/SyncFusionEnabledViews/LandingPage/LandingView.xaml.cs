using GAZT;
using GAZTeServicesApp.ViewModels.LandingPage;
using Syncfusion.SfCalendar.XForms;
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
       //  Calendar appointments;
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
                calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded;
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
            try
            {
                base.OnAppearing();

                await viewModel.LoadDashboardData();

                viewModel.PopulateReturnsInformation();
                viewModel.PopulateBillsInformation();
                viewModel.PopulateBillsAndReturnsSchedule();
                viewModel.PopulateeServicesApplicableToTheTaxPayer();
            }
            catch(Exception ex)
            {

            }
        }

        private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs args)
        {

            // As default setting Month cell Background color as Green 
            args.BackgroundColor = Color.Green;
            viewModel.BillsAndReturnsSchedule = calendar.DataSource as CalendarEventCollection;
            if (viewModel.BillsAndReturnsSchedule != null)
            {
                for (int i = 0; i < viewModel.BillsAndReturnsSchedule.Count; i++)
                {
                    var appointment = viewModel.BillsAndReturnsSchedule[i];
                    if (args.Date.Date == appointment.StartTime.Date)
                    {
                        // Setting Background color when the appointment available on specific day 
                        args.BackgroundColor = Color.Red;
                    }
                }
            }
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
            viewModel._navigationService.NavigateTo("OptionsPageView");
        }
    }
}