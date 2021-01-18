using EGAZT.Enums;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLandingPage_ViewModel;
using GAZT.CustomControl;
using GAZT.Models;
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
            
                //  ParentContainer.RaiseChild(BusyIndicator);
             //   calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded;
                SetLTR();

                viewModel.TaxPayerProfile = App.TP;
            }
            catch (Exception gex)
            {
                viewModel.TaxPayerProfile = App.TP;
                
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
                        List<OverduePaymentAndUnSubmittedReturn> sortedList = new List<OverduePaymentAndUnSubmittedReturn>();
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
                LoadDuesData();
                LoadData();
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

            //Analytics.TrackEvent(AppResources.MyBills + " from Dashboard", null);
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.MyBills + " from Dashboard");
            Image img = sender as Image;
            BillInfo billInfo = (BillInfo)img.BindingContext;
            viewModel.NavigateToMyBills(billInfo);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

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

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.MyBills + " from Dashboard");

            Label img = sender as Label;
            BillInfo billInfo = (BillInfo)img.BindingContext;
            viewModel.NavigateToMyBills(billInfo);

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

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

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.MyBills + " from Dashboard");

            StackLayout img = sender as StackLayout;
            BillInfo billInfo = (BillInfo)img.BindingContext;
            viewModel.NavigateToMyBills(billInfo);

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

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
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.VatReturns + " eService");
                            viewModel._navigationService.NavigateTo(App.ICRListPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.EstimateZakat)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.EstimateZakat + " eService");
                            viewModel._navigationService.NavigateTo(App.ZakatReturnListPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZFormBundleStatus)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZFormBundleStatus + " eService");
                            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.Certificates)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.Certificates + " eService");
                            viewModel._navigationService.NavigateTo(App.MyCertificate);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTINStatus)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTINStatus + " eService");
                            viewModel._navigationService.NavigateTo(App.CheckTINStatusPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZCorrespondence)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZCorrespondence + " eService");
                            viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.Bills)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.Bills + " eService");
                            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, new BillInfo());
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZZZVatRegistrationTile)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZZZVatRegistrationTile + " eService");
                            viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                        {
                            string mobno=string.Empty;

                            //Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);
                        
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


                                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                                    await viewModel.VerifyCommandClick();

                                    await Task.Run(() =>
                                    {
                                        viewModel.IsLoading = false;
                                    });

                                }
                                else
                                {
                                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                                    viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                                }
                            }
                            else
                            {
                                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                                viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                            }
                         }
                        //if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                        if (BModel != null && BModel.eServiceName == AppResources.ZZZVatLookUpTitleTextNew)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZZVatLookUpTitleTextNew + " eService");
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZRealEstateServiceTitle + " eService");
                            viewModel._navigationService.NavigateTo(App.VATRealEstateServicesPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
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
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.VatReturns + " eService");
                            viewModel._navigationService.NavigateTo(App.ICRListPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.EstimateZakat)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.EstimateZakat + " eService");
                            viewModel._navigationService.NavigateTo(App.ZakatReturnListPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZFormBundleStatus)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZFormBundleStatus + " eService");
                            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.Certificates)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.Certificates + " eService");
                            viewModel._navigationService.NavigateTo(App.MyCertificate);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTINStatus)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTINStatus + " eService");
                            viewModel._navigationService.NavigateTo(App.CheckTINStatusPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZCorrespondence)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZCorrespondence + " eService");
                            viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.Bills)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.Bills + " eService");
                            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, new BillInfo());
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZZZVatRegistrationTile)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZZZVatRegistrationTile + " eService");
                            viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                    if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                    {
                        string mobno = string.Empty;

                        //Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);

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

                                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                                AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                                await viewModel.VerifyCommandClick();

                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });

                            }
                            else
                            {
                                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                                viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                            }
                        }
                        else
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                            viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                    }
                    //if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                    if (BModel != null && BModel.eServiceName == AppResources.ZZZVatLookUpTitleTextNew)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZZVatLookUpTitleTextNew + " eService");
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZRealEstateServiceTitle + " eService");
                            viewModel._navigationService.NavigateTo(App.VATRealEstateServicesPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
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
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.VatReturns + " eService");
                            viewModel._navigationService.NavigateTo(App.ICRListPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.EstimateZakat)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZRealEstateServiceTitle + " eService");
                            viewModel._navigationService.NavigateTo(App.ZakatReturnListPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZFormBundleStatus)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZFormBundleStatus + " eService");
                            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.MyCertificate)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.MyCertificate + " eService");
                            viewModel._navigationService.NavigateTo(App.MyCertificate);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZTINStatus)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTINStatus + " eService");
                            viewModel._navigationService.NavigateTo(App.CheckTINStatusPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZCorrespondence)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZCorrespondence + " eService");
                            viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.MyBills)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.MyBills + " eService");
                            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, new BillInfo());
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        if (BModel != null && BModel.eServiceName == AppResources.ZZZZVatRegistrationTile)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZZZVatRegistrationTile + " eService");
                            viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                    if (BModel != null && BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                    {
                        string mobno = string.Empty;

                        //Analytics.TrackEvent(App.TaxEvasionReportListPageView + " eService", null);

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

                                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                                AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                                await viewModel.VerifyCommandClick();

                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });

                            }
                            else
                            {
                                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                                viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                            }
                        }
                        else
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZTEReportReportScreenTitle + " eService");
                            viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                    }

                    if (BModel != null && BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZRealEstateServiceTitle + " eService");
                            viewModel._navigationService.NavigateTo(App.VATRealEstateServicesPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                        //if (BModel != null && BModel.eServiceName == AppResources.VATLookup)
                        if (BModel != null && BModel.eServiceName == AppResources.ZZZVatLookUpTitleTextNew)
                        {
                            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZZZVatLookUpTitleTextNew + " eService");
                            viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                        }
                    if (BModel != null && BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                    {
                        var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.ZRealEstateServiceTitle + " eService");
                        viewModel._navigationService.NavigateTo(App.VATRealEstateServicesPageView);
                        AppDynamics.Agent.Instrumentation.EndCall(callTracker);
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

        private void openTaxEvasion()
        {

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
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.Submitted + " from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 0);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                if (BModel.ReturnTypeName == AppResources.UnSubmitted)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.UnSubmitted + " from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 1);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                if (BModel.ReturnTypeName == AppResources.OverDue)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.OverDue + " from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 2);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
            }
            if (controltype == "Xamarin.Forms.Label")
            {
                Label arrowImage = sender as Label;
                ReturnInfo BModel = (ReturnInfo)arrowImage.BindingContext;
                if (BModel.ReturnTypeName == AppResources.Submitted)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.Submitted + " from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView,0);// , 0);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                if (BModel.ReturnTypeName == AppResources.UnSubmitted)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.UnSubmitted + " from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 1);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                if (BModel.ReturnTypeName == AppResources.OverDue)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.OverDue + " from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 2);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
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