using GAZT;
using GAZTeServicesApp.ViewModels.LandingPage;
using Syncfusion.SfCalendar.XForms;
using GAZTeServicesBusinessLibrary;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using GAZT.Models;

namespace GAZTeServicesApp.Views.LandingPage
{
    /// <summary>
    /// Page to show the article tile
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFLandingPageView : ContentPage
    {
        SFLandingPageViewModel viewModel;
       //  Calendar appointments;
        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPageView" /> class.
        /// </summary>
        public SFLandingPageView()
        {
            try
            {
                InitializeComponent();
                this.BindingContext = viewModel = App.Locator.SFLandingPageView;
              //  ParentContainer.RaiseChild(BusyIndicator);
                calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded;
                SetLTR();
              //  Application.Current.Resources["GAZTFontBold"] = Application.Current.Resources["GAZTBoldArabic"];
            }
            catch (Exception gex)
            {
                int i = 0;
            }
            
        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                SetLTR();

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

          //  // As default setting Month cell Background color as Green 
          ////  args.BackgroundColor = Color.Green;
          //  viewModel.BillsAndReturnsSchedule = calendar.DataSource as CalendarEventCollection;
          //  if (viewModel.BillsAndReturnsSchedule != null)
          //  {
          //      for (int i = 0; i < viewModel.BillsAndReturnsSchedule.Count; i++)
          //      {

          //          var appointment = viewModel.BillsAndReturnsSchedule[i];
          //          if (args.Date.Date == appointment.StartTime.Date)
          //          {
          //              // Setting Background color when the appointment available on specific day 
          //             // args.BackgroundColor = Color.Red;
          //          }
          //      }
          //  }
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
        private void OnTappedVAT(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.VATReturnsPageView);
        }
        private void OnTappedTest(object sender, EventArgs e)
        {
            string controltype = sender.GetType().ToString();

            if(controltype== "Xamarin.Forms.Image")
            {
                Image arrowImage = sender as Image;
                eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                if(BModel.eServiceName== AppResources.VATDeclaration)
                {
                    viewModel._navigationService.NavigateTo(App.ICRListPageView);
                }
                if (BModel.eServiceName == AppResources.EstimateZakat)
                {
                    viewModel._navigationService.NavigateTo(App.ZakatReturnListPageView);
                }
                if (BModel.eServiceName == AppResources.ZZFormBundleStatus)
                {
                    viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
                }
               
                if (BModel.eServiceName == AppResources.MyCertificate)
                {
                    viewModel._navigationService.NavigateTo(App.MyCertificate);
                }
                if (BModel.eServiceName == AppResources.ZTINStatus)
                {
                    viewModel._navigationService.NavigateTo(App.CheckTINStatusPageView);
                }
                if (BModel.eServiceName == AppResources.ZZCorrespondence)
                {
                    viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
                }
                if (BModel.eServiceName == AppResources.MyBills)
                {
                    viewModel._navigationService.NavigateTo(App.MyBillsView);
                }
                if(BModel.eServiceName== AppResources.ZTEReportReportScreenTitle)
                {
                    viewModel._navigationService.NavigateTo(App.ReturnsPageView);
                }

            }
            if (controltype == "Xamarin.Forms.Label")
            {
                Label arrowImage = sender as Label;
                eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                if (BModel.eServiceName == AppResources.VATDeclaration)
                {
                    viewModel._navigationService.NavigateTo(App.ICRListPageView);
                }
                if (BModel.eServiceName == AppResources.EstimateZakat)
                {
                    viewModel._navigationService.NavigateTo(App.ZakatReturnListPageView);
                }
                if (BModel.eServiceName == AppResources.ZZFormBundleStatus)
                {
                    viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
                }

                if (BModel.eServiceName == AppResources.MyCertificate)
                {
                    viewModel._navigationService.NavigateTo(App.MyCertificate);
                }
                if (BModel.eServiceName == AppResources.ZTINStatus)
                {
                    viewModel._navigationService.NavigateTo(App.CheckTINStatusPageView);
                }
                if (BModel.eServiceName == AppResources.ZZCorrespondence)
                {
                    viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
                }
                if (BModel.eServiceName == AppResources.MyBills)
                {
                    viewModel._navigationService.NavigateTo(App.MyBillsView);
                }
                if (BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                {
                    viewModel._navigationService.NavigateTo(App.ReturnsPageView);
                }

            }
            // if(sender.BindingContext.eServiceName)

        }



        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string controltype = sender.GetType().ToString();

            if (controltype == "Xamarin.Forms.Image")
            {
                Image arrowImage = sender as Image;
                ReturnInfo BModel = (ReturnInfo)arrowImage.BindingContext;
                if (BModel.ReturnTypeName == AppResources.Submitted)
                {
                    viewModel._navigationService.NavigateTo(App.ReturnsPageView, 0);
                }
                if (BModel.ReturnTypeName == AppResources.NonSubmitted)
                {
                    viewModel._navigationService.NavigateTo(App.ReturnsPageView, 1);
                }
                if (BModel.ReturnTypeName == AppResources.OverDue)
                {
                    viewModel._navigationService.NavigateTo(App.ReturnsPageView, 2);
                }

            }
            if (controltype == "Xamarin.Forms.Label")
            {
                Label arrowImage = sender as Label;
                ReturnInfo BModel = (ReturnInfo)arrowImage.BindingContext;
                if (BModel.ReturnTypeName == AppResources.Submitted)
                {
                    viewModel._navigationService.NavigateTo(App.ReturnsPageView, 0);
                }
                if (BModel.ReturnTypeName == AppResources.NonSubmitted)
                {
                    viewModel._navigationService.NavigateTo(App.ReturnsPageView, 1);
                }
                if (BModel.ReturnTypeName == AppResources.OverDue)
                {
                    viewModel._navigationService.NavigateTo(App.ReturnsPageView, 2);
                }

            }
        }
    }
}