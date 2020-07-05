using EGAZT.Enums;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLandingPage_ViewModel;
using GAZT.CustomControl;
using GAZT.Models;
using Microsoft.AppCenter.Analytics;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.SFLanding
{
    /// <summary>
    /// Page to show the article tile
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFLandingPageView : ContentPage
    {
        SFLandingPageViewModel viewModel;
        //  CalendaTapGestureRecognizer_Tapped appointments;
        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPageView" /> class.
        /// </summary>
        public SFLandingPageView()
        {
            try
            {
                InitializeComponent();

                if (Device.RuntimePlatform == Device.Android)
                {
                    LandingGreenScreen.Margin = new Thickness(0,0,0,0);
                    LandingHamberMenu.Padding = new Thickness(0,0,0,0);
                    LandingStackLayout.Padding = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    LandingGreenScreen.Margin = new Thickness(0,-50,0,0);
                    LandingHamberMenu.Padding = new Thickness(5,80,5,0);
                    LandingStackLayout.Padding = new Thickness(0, 50, 0, 0);
                }

                // On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                this.BindingContext = viewModel = App.Locator.SFLandingPageView;
                Changecornerradious();
                LoadDuesData();
                LoadData();
                //  ParentContainer.RaiseChild(BusyIndicator);
             //   calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded;
                SetLTR();

                viewModel.TaxPayerProfile = App.TP;
            }
            catch (Exception gex)
            {
                viewModel.TaxPayerProfile = App.TP;
                int i = 0;
            }
        }
        public async Task LoadDuesData()
        {
            try
            {
              await  Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(async() =>
                {
                    await viewModel.DuesData();
                    if (viewModel.listofPaymentReturn != null && viewModel.listofPaymentReturn.Count != 0)
                    {
                        List<OverduePaymentsAndUnSubmittedReturn> sortedList = new List<OverduePaymentsAndUnSubmittedReturn>();
                        viewModel.listofPaymentReturn = viewModel.listofPaymentReturn.OrderBy(icr => DateTime.Parse(icr.DueDate)).ToList();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            viewModel.CommitmentReturnsList = viewModel.listofPaymentReturn;
                          //  ReturnsList.ItemsSource = viewModel.listofPaymentReturn;
                        });
                        await Task.Delay(3000);
                        viewModel.IsListviewVisible = true;
                        viewModel.IsNoDuesLabelVisible = false;
                    }
                    else
                    {
                        viewModel.IsListviewVisible = false;
                        viewModel.IsNoDuesLabelVisible = true;
                    }
                });
              await  Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch(Exception ex)
            {
            }
        }
        private async Task LoadData()
        {
            try
            {
                await viewModel.LoadDashboardData();
                viewModel.PopulateReturnsInformation();
                viewModel.PopulateBillsInformation();
              // viewModel.PopulateBillsAndReturnsSchedule();
                viewModel.PopulateeServicesApplicableToTheTaxPayer();
            }
            catch(Exception ex)
            {
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                int transitionCount = 0;
                try
                {

                    MessagingCenter.Subscribe<SFLandingPageViewModel, TransitionType>(this, AppSettings.TransitionMessage, (sender, arg) =>
                    {
                        if (transitionCount == 0)
                        {
                            transitionCount++;
                            var transitionType = (TransitionType)arg;
                            var transitionNavigationPage = Parent as CustomNavigation;

                            if (transitionNavigationPage != null)
                            {
                                transitionNavigationPage.TransitionType = transitionType;
                                ComingToOptionScreenFrom comingToOptionScreenFrom = ComingToOptionScreenFrom.IsDashboardPage;
                                viewModel._navigationService.NavigateTo(App.SFOptionsPageView, comingToOptionScreenFrom);

                            }
                        }
                        else
                        {

                        }

                    });
                }
                catch (Exception ex)
                {

                }
                App.IsComingFromSleepMode = false;
                SetLTR();
                Changecornerradious();
                viewModel.TaxPayerProfile = App.TP;
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
                   // calendar.Locale = new System.Globalization.CultureInfo("ar-AE");
                  //  calendar.FlowDirection = FlowDirection.RightToLeft;
                    CalendarResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
                }
                else
                {
                    this.FlowDirection = FlowDirection.LeftToRight;
                   // calendar.Locale = new System.Globalization.CultureInfo("en-US");
                  //  calendar.FlowDirection = FlowDirection.LeftToRight;
                    CalendarResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
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
                 viewModel.NavigateToMyBills(billInfo);
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
                 viewModel.NavigateToMyBills(billInfo);
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
                 viewModel.NavigateToMyBills(billInfo);
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
                        if (BModel != null && BModel.eServiceName == AppResources.VatReturns)
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
                        if (BModel != null && BModel.eServiceName == AppResources.Certificates)
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
                        if (BModel != null && BModel.eServiceName == AppResources.Bills)
                        {
                            Analytics.TrackEvent(App.MyBillsView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.MyBillsView, new BillInfo());
                        }
                    if (BModel != null && BModel.eServiceName == AppResources.ZZZZVatRegistrationTile)
                    {
                        Analytics.TrackEvent(App.VATRegistrationPageView + " eService", null);
                        viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                    {
                            string mobno=string.Empty;

                            Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);
                        
                            if (App.TP != null && App.TP.Mobile != null)
                            {
                                mobno = App.TP.Mobile;
                                if (mobno.StartsWith("00966") || mobno.StartsWith("+966"))
                                {
                                    //mobno = mobno.Remove(0, 2);
                                    //mobno = "+" + mobno;

                                    await Task.Run(() =>
                                    {
                                        viewModel.IsLoading = true;
                                    });

                                    await viewModel.VerifyCommandClick();

                                    await Task.Run(() =>
                                    {
                                        viewModel.IsLoading = false;
                                    });

                                }
                                else
                                {
                                    viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                                }
                            }
                            else
                            {
                                viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                            }
                         }

                        //if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                        if (BModel != null && BModel.eServiceName == AppResources.ZZZVatLookUpTitleTextNew)
                        {
                            Analytics.TrackEvent(App.VATLookupPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                        {
                            Analytics.TrackEvent(App.VATRealEstateServicesPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.VATRealEstateServicesPageView);
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
                        if (BModel != null && BModel.eServiceName == AppResources.VatReturns)
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
                        if (BModel != null && BModel.eServiceName == AppResources.Certificates)
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
                        if (BModel != null && BModel.eServiceName == AppResources.Bills)
                        {
                            Analytics.TrackEvent(App.MyBillsView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.MyBillsView, new BillInfo());
                        }
                    if (BModel != null && BModel.eServiceName == AppResources.ZZZZVatRegistrationTile)
                    {
                        Analytics.TrackEvent(App.VATRegistrationPageView + " eService", null);
                        viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                        {
                        string mobno = string.Empty;
                        if (App.TP != null && App.TP.Mobile != null)
                        {
                            mobno = App.TP.Mobile;
                            //mobno = mobno.Remove(0, 6);
                            mobno = mobno.Replace("009665", string.Empty);
                        }
                        Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.TaxEvasionReportListPageView,mobno);
                        }
                        //if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                        if (BModel != null && BModel.eServiceName == AppResources.ZZZVatLookUpTitleTextNew)
                        {
                            Analytics.TrackEvent(App.VATLookupPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                        }
                    if (BModel != null && BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                    {
                        Analytics.TrackEvent(App.VATRealEstateServicesPageView + " eService", null);
                        viewModel._navigationService.NavigateTo(App.VATRealEstateServicesPageView);
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
                        if (BModel != null && BModel.eServiceName == AppResources.VatReturns)
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
                    if (BModel != null && BModel.eServiceName == AppResources.ZZZZVatRegistrationTile)
                    {
                        Analytics.TrackEvent(App.VATRegistrationPageView + " eService", null);
                        viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                        {
                            string mobno = string.Empty;
                            if (App.TP != null && App.TP.Mobile != null)
                            {
                                mobno = App.TP.Mobile;
                                //mobno = mobno.Remove(0, 6);
                                mobno = mobno.Replace("009665", string.Empty);
                            }
                            Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.TaxEvasionReportListPageView,mobno);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                        {
                            Analytics.TrackEvent(App.VATRealEstateServicesPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.VATRealEstateServicesPageView);
                        }
                        //if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                        if (BModel != null && BModel.eServiceName == AppResources.ZZZVatLookUpTitleTextNew)
                        {
                            Analytics.TrackEvent(App.VATLookupPageView + " eService", null);
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                        }
                    if (BModel != null && BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                    {
                        Analytics.TrackEvent(App.VATRealEstateServicesPageView + " eService", null);
                        viewModel._navigationService.NavigateTo(App.VATRealEstateServicesPageView);
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
                    viewModel._navigationService.NavigateTo(App.MyReturnsPageView, 0);
                }
                if (BModel.ReturnTypeName == AppResources.UnSubmitted)
                {
                    Analytics.TrackEvent(AppResources.UnSubmitted + " from Dashboard", null);
                    viewModel._navigationService.NavigateTo(App.MyReturnsPageView, 1);
                }
                if (BModel.ReturnTypeName == AppResources.OverDue)
                {
                    Analytics.TrackEvent(AppResources.OverDue + " from Dashboard", null);
                    viewModel._navigationService.NavigateTo(App.MyReturnsPageView, 2);
                }
            }
            if (controltype == "Xamarin.Forms.Label")
            {
                Label arrowImage = sender as Label;
                ReturnInfo BModel = (ReturnInfo)arrowImage.BindingContext;
                if (BModel.ReturnTypeName == AppResources.Submitted)
                {
                    Analytics.TrackEvent(AppResources.Submitted + " from Dashboard", null);
                    viewModel._navigationService.NavigateTo(App.MyReturnsPageView);// , 0);
                }
                if (BModel.ReturnTypeName == AppResources.UnSubmitted)
                {
                    Analytics.TrackEvent(AppResources.UnSubmitted + " from Dashboard", null);
                    viewModel._navigationService.NavigateTo(App.MyReturnsPageView, 1);
                }
                if (BModel.ReturnTypeName == AppResources.OverDue)
                {
                    Analytics.TrackEvent(AppResources.OverDue + " from Dashboard", null);
                    viewModel._navigationService.NavigateTo(App.MyReturnsPageView, 2);
                }
            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }
        protected override bool OnBackButtonPressed() => true;

        private void Changecornerradious()
        {
            if (App.IsArabic)
            {
                border.CornerRadius = new Thickness(0, 0, 0, 40);
            }
            else
            {
                border.CornerRadius = new Thickness(0, 0, 40, 0);
            }
        }

    }
}