using GAZT;
using GAZTeServicesApp.ViewModels.LandingPage;
using Syncfusion.SfCalendar.XForms;
using GAZTeServicesBusinessLibrary;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using GAZT.Models;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                this.BindingContext = viewModel = App.Locator.SFLandingPageView;
                
                LoadData();

                //  ParentContainer.RaiseChild(BusyIndicator);
                calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded;

                SetLTR();
                viewModel.TaxPayerProfile = App.TP;


            }
            catch (Exception gex)
            {
                viewModel.TaxPayerProfile = App.TP;
                int i = 0;
            }
            
        }

        private async void LoadData()
        {
            await viewModel.LoadDashboardData();

            viewModel.PopulateReturnsInformation();
            viewModel.PopulateBillsInformation();
            viewModel.PopulateBillsAndReturnsSchedule();
            viewModel.PopulateeServicesApplicableToTheTaxPayer();
        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                SetLTR();
                viewModel.TaxPayerProfile = App.TP;
                //await viewModel.LoadDashboardData();

                //viewModel.PopulateReturnsInformation();
                //viewModel.PopulateBillsInformation();
                //viewModel.PopulateBillsAndReturnsSchedule();
                //viewModel.PopulateeServicesApplicableToTheTaxPayer();
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
            try
            {
                if (App.IsArabic)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                    calendar.Locale = new System.Globalization.CultureInfo("ar-AE");
                    calendar.FlowDirection = FlowDirection.RightToLeft;
                }
                else
                {
                    this.FlowDirection = FlowDirection.LeftToRight;
                    calendar.Locale = new System.Globalization.CultureInfo("en-US");
                    calendar.FlowDirection = FlowDirection.LeftToRight;
                }

            }
            catch (Exception gec)
            {

            }
          
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo("OptionsPageView");
        }

        private async  void OnImageClicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });

            Analytics.TrackEvent(AppResources.MyBills + " from Dashboard", null);
                Image img = sender as Image;
                BillInfo billInfo = (BillInfo)img.BindingContext;
                await viewModel.NavigateToMyBills(billInfo);

            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });


        }
        private async void OnLabelClicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });

            viewModel.IsButtonEnabled = false;
                Analytics.TrackEvent(AppResources.MyBills + " from Dashboard", null);
                Label img = sender as Label;
                BillInfo billInfo = (BillInfo)img.BindingContext;
                await viewModel.NavigateToMyBills(billInfo);

            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });



        }

        private async void OnStackLayoutClicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            Analytics.TrackEvent(AppResources.MyBills + " from Dashboard", null);
                StackLayout img = sender as StackLayout;
                BillInfo billInfo = (BillInfo)img.BindingContext;
                await viewModel.NavigateToMyBills(billInfo);

            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           // viewModel.IsButtonEnabled = true;
        }

  

        private async void OnTappedTest(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });




            string controltype = sender.GetType().ToString();

                if (controltype == "Xamarin.Forms.Image")
                {
                    try
                    {
                        Image arrowImage = sender as Image;
                        eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                        if (BModel != null && BModel.eServiceName == AppResources.VATDeclaration)
                        {
                            Analytics.TrackEvent(AppResources.VATDeclaration + " eService", null);
                            viewModel._navigationService.NavigateTo(App.ICRListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.EstimateZakat)
                        {
                            Analytics.TrackEvent(App.ZakatReturnListPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.ZakatReturnListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZFormBundleStatus)
                        {
                            Analytics.TrackEvent(App.FormBundleStatusPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
                        }

                        if (BModel != null && BModel.eServiceName == AppResources.MyCertificate)
                        {
                            Analytics.TrackEvent(App.MyCertificate + " eService", null);
                            viewModel._navigationService.NavigateTo(App.MyCertificate);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTINStatus)
                        {
                            Analytics.TrackEvent(App.CheckTINStatusPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.CheckTINStatusPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZCorrespondence)
                        {
                            Analytics.TrackEvent(App.CorrespondancePageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.MyBills)
                        {
                            Analytics.TrackEvent(App.MyBillsView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.MyBillsView, new BillInfo());
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                        {
                            Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.TaxEvasionReportListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                        {
                            Analytics.TrackEvent(App.VATLookupPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                        }
                    }
                    catch (Exception ex)
                    {

                    }


                }
                if (controltype == "Xamarin.Forms.Label")
                {
                    try
                    {
                        Label arrowImage = sender as Label;
                        eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                        if (BModel != null && BModel.eServiceName == AppResources.VATDeclaration)
                        {
                            Analytics.TrackEvent(AppResources.VATDeclaration + " eService", null);
                            viewModel._navigationService.NavigateTo(App.ICRListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.EstimateZakat)
                        {
                            Analytics.TrackEvent(App.ZakatReturnListPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.ZakatReturnListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZFormBundleStatus)
                        {
                            Analytics.TrackEvent(App.FormBundleStatusPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
                        }

                        if (BModel != null && BModel.eServiceName == AppResources.MyCertificate)
                        {
                            Analytics.TrackEvent(App.MyCertificate + " eService", null);
                            viewModel._navigationService.NavigateTo(App.MyCertificate);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTINStatus)
                        {
                            Analytics.TrackEvent(App.CheckTINStatusPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.CheckTINStatusPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZCorrespondence)
                        {
                            Analytics.TrackEvent(App.CorrespondancePageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.MyBills)
                        {
                            Analytics.TrackEvent(App.MyBillsView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.MyBillsView, new BillInfo());
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                        {
                            Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.TaxEvasionReportListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                        {
                            Analytics.TrackEvent(App.VATLookupPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                        }

                    }
                    catch (Exception ex)
                    {

                    }

                }

                if (controltype == "Xamarin.Forms.StackLayout")
                {
                    try
                    {
                        Label arrowImage = sender as Label;
                        eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                        if (BModel != null && BModel.eServiceName == AppResources.VATDeclaration)
                        {
                            Analytics.TrackEvent(AppResources.VATDeclaration + " eService", null);
                            viewModel._navigationService.NavigateTo(App.ICRListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.EstimateZakat)
                        {
                            Analytics.TrackEvent(App.ZakatReturnListPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.ZakatReturnListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZFormBundleStatus)
                        {
                            Analytics.TrackEvent(App.FormBundleStatusPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
                        }

                        if (BModel != null && BModel.eServiceName == AppResources.MyCertificate)
                        {
                            Analytics.TrackEvent(App.MyCertificate + " eService", null);
                            viewModel._navigationService.NavigateTo(App.MyCertificate);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTINStatus)
                        {
                            Analytics.TrackEvent(App.CheckTINStatusPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.CheckTINStatusPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZCorrespondence)
                        {
                            Analytics.TrackEvent(App.CorrespondancePageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.MyBills)
                        {
                            Analytics.TrackEvent(App.MyBillsView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.MyBillsView, new BillInfo());
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                        {
                            Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.TaxEvasionReportListPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                        {
                            Analytics.TrackEvent(App.VATLookupPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                
               
            }


            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });


        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });

            string controltype = sender.GetType().ToString();

                if (controltype == "Xamarin.Forms.StackLayout")
                {
                    StackLayout arrowImage = sender as StackLayout;
                    ReturnInfo BModel = (ReturnInfo)arrowImage.BindingContext;
                    if (BModel.ReturnTypeName == AppResources.Submitted)
                    {
                        Analytics.TrackEvent(AppResources.Submitted + " from Dashboard", null);
                        viewModel._navigationService.NavigateTo(App.ReturnsPageView, 0);
                    }
                    if (BModel.ReturnTypeName == AppResources.NonSubmitted)
                    {
                        Analytics.TrackEvent(AppResources.NonSubmitted + " from Dashboard", null);
                        viewModel._navigationService.NavigateTo(App.ReturnsPageView, 1);
                    }
                    if (BModel.ReturnTypeName == AppResources.OverDue)
                    {
                        Analytics.TrackEvent(AppResources.OverDue + " from Dashboard", null);
                        viewModel._navigationService.NavigateTo(App.ReturnsPageView, 2);
                    }

                }
                if (controltype == "Xamarin.Forms.Label")
                {
                    Label arrowImage = sender as Label;
                    ReturnInfo BModel = (ReturnInfo)arrowImage.BindingContext;
                    if (BModel.ReturnTypeName == AppResources.Submitted)
                    {
                        Analytics.TrackEvent(AppResources.Submitted + " from Dashboard", null);
                        viewModel._navigationService.NavigateTo(App.ReturnsPageView, 0);
                    }
                    if (BModel.ReturnTypeName == AppResources.NonSubmitted)
                    {
                        Analytics.TrackEvent(AppResources.NonSubmitted + " from Dashboard", null);
                        viewModel._navigationService.NavigateTo(App.ReturnsPageView, 1);
                    }
                    if (BModel.ReturnTypeName == AppResources.OverDue)
                    {
                        Analytics.TrackEvent(AppResources.OverDue + " from Dashboard", null);
                        viewModel._navigationService.NavigateTo(App.ReturnsPageView, 2);
                    }

                }


            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });




        }
    }
}