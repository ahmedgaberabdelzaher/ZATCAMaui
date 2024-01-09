using AppDynamics.Agent;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Newtonsoft.Json;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Picker;
using Syncfusion.Maui.ProgressBar;
using System.Globalization;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.DashBoardPages.PopUpPages;
using ZATCAMAUI.Views.NewDesign.VATDeRegistration;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
using ScrollView = Microsoft.Maui.Controls.ScrollView;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignDashBoardPageView : ContentPage
    {
        #region Variable

        GAZTNewDesignDashBoardPageViewModel viewModel;
        private bool isTimerOff = false;

        #endregion

        private bool isFirstTime = true;
        public GAZTNewDesignDashBoardPageView(bool isMenu = false)
        {
            try
            {
                InitializeComponent();
                NavigationPage.SetBackButtonTitle(this, "");


                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Constructor", AppResources.Dashboard);
                Instrumentation.EndCall(callTracker);
                viewModel = App.Locator.GAZTNewDesignDashBoardPageView;
                BindingContext = viewModel;
                viewModel.MyObligationAmount = 0.0;

                viewModel.MyObligationAmountCommas = "";
                viewModel.IsPendingBillsVisible = false;
                viewModel.IsInstalmentPlanVisible = false;
                viewModel.IsMyObligationsClear = false;
                viewModel.MenuViewVisible = false;
                viewModel.IfnotRegInVATAndZakat = false;
                viewModel.IsBodyMyTaxVisible = false;

                if (viewModel.AccountStatementsList != null)
                {

                    viewModel.AccountStatementsList.Clear();
                }
                viewModel.GetDashBoardMenuLst(1);

                //CR6264 data
                if (App.TP.VtpmFg == "X")
                {
                    viewModel.istileUpdated = true;
                }
                else
                {
                    viewModel.istileUpdated = false;
                }

                //ends 


                MessagingCenter.Subscribe<object>(this, "HideProfitGoods", (sender) =>
                {

                    viewModel.istileUpdated = false;
                    tileUpdatedView.IsVisible = false;
                    tileUpdatedBoxView.IsVisible = false;

                    OnDataLoad();

                    //         MainThread.BeginInvokeOnMainThread(() => TaxBalanceProgress.RangeColors = rangeColors);

                });
                SetLTR();
            }
            catch (Exception)
            {


            }
        }

        public GAZTNewDesignDashBoardPageView(string tab)
        {
            try
            {
                InitializeComponent();
                NavigationPage.SetBackButtonTitle(this, "");


                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Constructor", AppResources.Dashboard);
                Instrumentation.EndCall(callTracker);
                viewModel = App.Locator.GAZTNewDesignDashBoardPageView;
                BindingContext = viewModel;
                viewModel.MyObligationAmount = 0.0;

                viewModel.MyObligationAmountCommas = "";
                viewModel.IsPendingBillsVisible = false;
                viewModel.IsInstalmentPlanVisible = false;
                viewModel.IsMyObligationsClear = false;
                viewModel.MenuViewVisible = false;
                viewModel.IfnotRegInVATAndZakat = false;
                viewModel.IsBodyMyTaxVisible = false;

                if (viewModel.AccountStatementsList != null)
                {

                    viewModel.AccountStatementsList.Clear();
                }
                viewModel.GetDashBoardMenuLst(1);

                SetLTR();

            }
            catch (Exception)
            {


            }
        }


        private void Vat_Registration_Tapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRegistration_Details_Tapped", "VAT Registration Details eService");
            viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);

            Instrumentation.EndCall(callTracker);
        }

        private void General_Services_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "GeneralServices_Tapped", "General Services");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.GeneralServicesListPageView);

            });

            Instrumentation.EndCall(callTracker);
        }

        private void TaxPayerSubsidyRequestTapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxPayerSubsidyRequestTapped", "TaxPayer Subsidy Request");
            var a = App.LoginDataRetrieved;
            viewModel.IsLoading = true;
            Task.Run(async () =>
            {
                viewModel.IsLoading = true;

                string response = await TaxpayerSubsidyWebServiceManager.TaxpayerSubsidyPostRequestAsync();

                if (response != null && response.Length > 0)
                {
                    SubsidyResponseModel subsidyResponseModel = JsonConvert.DeserializeObject<SubsidyResponseModel>(response);
                    if (subsidyResponseModel != null && subsidyResponseModel.D != null)
                    {

                        if (!string.IsNullOrEmpty(subsidyResponseModel.D.Fbguid))
                        {

                            string url = subsidyResponseModel.D.ExternalPortal;
                            url += "?";
                            url += "culture=" + WebServiceManager.GetLangZParameterAREN();
                            url += "&tin=" + App.LoginDataRetrieved.TIN;
                            url += "&token=" + subsidyResponseModel.D.Fbguid;
                            url += "&device=MA";
                            ZATCAConstants.TaxpayerSubsidyRequest = url;


                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                viewModel._navigationService.NavigateTo(App.TaxpayerSubsidyRequest);
                            });

                            Instrumentation.EndCall(callTracker);
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread( () =>
                            {
                                viewModel.IsLoading = false;

                            });
                        }


                    }
                }


            });
        }

        private void btnCommitmentsPickerClicked(object sender, EventArgs e)
        {
            CommitmentsPicker.IsOpen = true;
        }

        public void ChangeArrowDirection()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {

                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        #region Method
        public class ColorModel
        {

            public ChartColorCollection Colors { get; set; }

            public ColorModel()
            {

                Colors = new ChartColorCollection();

                Colors.Add(Color.FromRgb(0, 128, 0));

                Colors.Add(Color.FromRgb(128, 0, 128));

                Colors.Add(Color.FromRgb(255, 0, 0));

            }

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            OnDataLoad();
            RefreshDashboardCommand();
            getYesCommandToLogout();
            getNoCommandToLogout();
            SetPickerFont();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            viewModel.isPayNowTapped = false;

            viewModel.NextCommitmentsString = AppResources.ZZMyCommitments;
            viewModel.PaidString = AppResources.Paid + " " + viewModel.PaidBillCount;
            viewModel.UnPaidString = AppResources.UnPaid + " " + viewModel.UnPaidBillCount;
            viewModel.PartiallyPaidString = AppResources.Partiallynewui + " " + viewModel.PartiallyPaidBillCount;
            viewModel.TotalString = AppResources.NDTotalNumberOfBills;




            ChangeArrowDirection();
            MessagingCenter.Subscribe<object>(this, "UpdateProgressBar", (sender) =>
            {
                SfLinearProgressBar rangeColors = new SfLinearProgressBar();
                rangeColors.GradientStops.Add( new ProgressGradientStop
                {
                    Color = (Color)Application.Current.Resources["Green"],
                    Value = 0
                });
                rangeColors.GradientStops.Add(new ProgressGradientStop
                {
                    Color = (Color)Application.Current.Resources["Error"],
                    Value = 100
                });
               
            });
            isTimerOff = false;
            StartTimer();
            viewModel.IsLoading = false;



            if (isFirstTime)
            {

                await frameToolbar.FadeTo(0, 0);
                await btn_frameToolbar.FadeTo(1, 0);
                isFirstTime = false;
            }


            if (App.isMybillsRefresh)
            {

                await viewModel.LoadDashboardData();

            }


            //code to refresh Dashboard Returns count 

            if (viewModel.SubmittedCount != null)
            {

                viewModel.IsLoading = true;

                try
                {

                    viewModel.DashboardData = await WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.Userid);

                    viewModel.PopulateReturnsInformation();
                    viewModel.IsLoading = false;
                }
                catch (GAZTErrorException )
                {



                }

            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "MultipleBillsContinue", (sender, arg) =>
                {
                    viewModel.showPaymentOptions();
                    viewModel.isPayNowTapped = false;

                });
            }
            catch (Exception)
            {
            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Card_Payment", (sender, arg) =>
                {
                    viewModel.MadaPaymentSelected();
                    viewModel.isPayNowTapped = false;

                });
            }
            catch (Exception)
            {
            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Apple_Pay", async (sender, arg) =>
                {
                    await viewModel.ApplePaySelected();
                    viewModel.isPayNowTapped = false;

                });
            }
            catch (Exception)
            {
            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "SADAD", async (sender, arg) =>
                {
                    await viewModel.SadadPaymentSelected();
                    viewModel.isPayNowTapped = false;
                });
            }
            catch (Exception)
            {
            }

            try
            {
                MessagingCenter.Subscribe<App, string>(this, "DashboardApplePayData", async (sender, arg) =>
                {

                    viewModel.ApplePayTokenData = arg.ToString();

                    await viewModel.UpdateApplePayPaymentGuid();


                });

            }
            catch (Exception)
            {

            }
        }

        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {
                            CommitmentsPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            CommitmentsPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            CommitmentsPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            CommitmentsPicker.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Device.Android:
                        {
                            CommitmentsPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            CommitmentsPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            CommitmentsPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            CommitmentsPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        }
                        break;
                }
            }
            catch (Exception)
            {


            }

        }

        private void ProfitOnGoods_Tapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.NewYesorNoPageView);
            });
        }

        public void getYesCommandToLogout()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToLogout", async (sender, arg) =>
                {
                    App.TP = null;
                    await viewModel.LogOut();
                });
            }
            catch (Exception)
            {


            }
        }

        public void getNoCommandToLogout()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoPressedToLogout", (sender, arg) =>
                {
                });
            }
            catch (Exception)
            {


            }
        }
        private void OnDataLoad()
        {

            App.IsComingFromSleepMode = false;


            try
            {
                LoadData();

                if (App.LoginDataRetrieved != null)
                {
                    if (App.TP.VtpmFg == "X")
                    {
                        viewModel.istileUpdated = true;
                    }
                    else
                    {
                        viewModel.istileUpdated = false;
                    }
                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        viewModel.IsEstablishmentRegistrationTileVisible = false;
                        viewModel.IsVatRegistrationTileVisible = true;
                        viewModel.IsRegistrationDetailsTileVisible = true;
                        viewModel.IfRegInZakat = true;
                        refundreqMenu.IsVisible = refundreqMenuBox.IsVisible = false;
                        fillingMenu.IsVisible = fillingMenuBox.IsVisible = false;
                    }
                    else if (App.LoginDataRetrieved.ZkReg == "U")
                    {
                        viewModel.IsEstablishmentRegistrationTileVisible = true;
                        viewModel.IsRegistrationDetailsTileVisible = false;

                    }
                    else if (App.LoginDataRetrieved.ZkReg == "N")
                    {
                        viewModel.IsEstablishmentRegistrationTileVisible = false;
                        viewModel.IsRegistrationDetailsTileVisible = false;
                    }

                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        viewModel.IsVatRegistrationTileVisible = false;
                        viewModel.IfRegInZakat = false;
                        viewModel.IsSubsidyTileVisible = true;

                    }
                    else if (App.LoginDataRetrieved.VtReg == "R")
                    {
                        //viewModel.IsVatRegistrationTileVisible = false;
                        //viewModel.IfSignUpnNotRegInVATShowVATServie = true;
                        //viewModel.IfSignUpnNotRegInVAT = false;
                        //viewModel.IsRegistrationDetailsTileVisible = true;
                        viewModel.IsVatRegistrationTileVisible = false;
                        viewModel.IfRegInZakat = false;
                        viewModel.IfnotRegInVATAndZakat = true;
                        viewModel.IfSignUpnNotRegInVATShowVATServie = true;
                        viewModel.IsSubsidyTileVisible = false;
                        //viewModel.IsRegistrationDetailsTileVisible = true;
                        //refundreqMenu.IsVisible = refundreqMenuBox.IsVisible = true;
                        //fillingMenu.IsVisible = fillingMenuBox.IsVisible = true;

                    }
                    else if (App.LoginDataRetrieved.VtReg == "")
                    {
                        viewModel.IfSignUpnNotRegInVATShowVATServie = false;
                    }

                    if (App.LoginDataRetrieved.ZkSignup == "X")
                    {
                        if (App.LoginDataRetrieved.ZkReg == string.Empty)
                        {
                            viewModel.IsVatRegistrationTileVisible = false;
                            viewModel.IsEstablishmentRegistrationTileVisible = true;
                        }

                    }
                    else if (App.LoginDataRetrieved.VtSignup == "X")
                    {
                        if (App.LoginDataRetrieved.VtReg == string.Empty)
                        {
                            viewModel.IsEstablishmentRegistrationTileVisible = false;
                            viewModel.IsVatRegistrationTileVisible = true;
                            viewModel.IfSignUpnNotRegInVAT = true;
                            viewModel.IsSubsidyTileVisible = false;
                        }
                    }
                    if (App.LoginDataRetrieved.ZkReg == "X" && App.LoginDataRetrieved.VtReg == "X")
                    {
                        viewModel.IfRegInZakat = true;
                        refundreqMenu.IsVisible = refundreqMenuBox.IsVisible = true;
                        fillingMenu.IsVisible = fillingMenuBox.IsVisible = true;


                    }

                    if ((App.LoginDataRetrieved.VtSignup == "X" || App.LoginDataRetrieved.ZkSignup == "X") && App.LoginDataRetrieved.ZkReg == string.Empty && App.LoginDataRetrieved.VtReg == string.Empty)
                    {
                        viewModel.IfnotRegInVATAndZakat = false;
                    }
                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        viewModel.IfnotRegInVATAndZakat = true;
                        viewModel.IfSignUpnNotRegInVATShowVATServie = true;
                        viewModel.IfSignUpnNotRegInVAT = false;
                    }
                    else if (App.LoginDataRetrieved.VtReg == string.Empty)
                    {
                        viewModel.IfSignUpnNotRegInVATShowVATServie = false;
                        viewModel.IfSignUpnNotRegInVAT = true;
                    }
                    else if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        viewModel.IfSignUpnNotRegInVAT = true;
                    }
                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        viewModel.IfnotRegInVATAndZakat = true;
                    }
                }

            }
            catch
            {
                viewModel.IsVatRegistrationTileVisible = true;
            }

            ChangeArrowDirection();
            App.HasToRefreshLoaderOnDashboard = false;


        }

        private void StartTimer()
        {
            int counter = 120;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                counter = counter - 1;
                if (counter == 0)
                {
                    if (App.TP != null)
                    {

                        counter = 120;
                        App.HasToRefreshLoaderOnDashboard = true;
                        OnDataLoad();
                    }
                    else
                    {
                        isTimerOff = true;

                    }


                }
                return !isTimerOff;
            });
        }

        public void RefreshDashboardCommand()
        {
            if (viewModel.MenuViewVisible)
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnMenuTapped", AppResources.ZZZMenu + " Page");
                viewModel.MenuViewVisible = true;
                viewModel.HomeViewVisible = false;
                viewModel.AccountStatementVisible = false;
                viewModel.LiveChatVisible = false;
                viewModel.HomeIndicatorColor = Colors.White;
                viewModel.MenuIndicatorColor = (Color)Application.Current.Resources["Primary"];
                viewModel.StackMenuColor = Colors.Transparent;
                viewModel.TabbarColor = Colors.Transparent;
                viewModel.IsToolbarTaxVisible = false;

                frameToolbar.IsVisible = false;


                Instrumentation.EndCall(callTracker);
            }
            else
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnHomeTapped", "Home Page");
                viewModel.MenuViewVisible = false;
                viewModel.HomeViewVisible = true;
                viewModel.AccountStatementVisible = false;
                viewModel.LiveChatVisible = false;
                viewModel.HomeIndicatorColor = (Color)Application.Current.Resources["Primary"];
                viewModel.MenuIndicatorColor = Colors.White;
                viewModel.TabbarColor = Colors.DarkGray;
                viewModel.StackMenuColor = Colors.White;

                frameToolbar.IsVisible = true;

                viewModel.IsToolbarTaxVisible = true;

                Instrumentation.EndCall(callTracker);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<GAZTNewDesignDashBoardPageView, string>(this, "StartTimerForDashboard");
            MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToLogout");
            MessagingCenter.Unsubscribe<object, string>(this, "NoPressedToLogout");
            MessagingCenter.Unsubscribe<object>(this, "UpdateProgressBar");
            MessagingCenter.Unsubscribe<object, string>(this, "Card_Payment");
            MessagingCenter.Unsubscribe<object, string>(this, "Apple_Pay");
            MessagingCenter.Unsubscribe<object, string>(this, "SADAD");
            MessagingCenter.Unsubscribe<App, string>(this, "DashboardApplePayData");
            MessagingCenter.Unsubscribe<object, string>(this, "MultipleBillsContinue");
            MessagingCenter.Unsubscribe<object, string>(this, "HideProfitGoods");


            isTimerOff = true;
        }
        private async void LoadData()
        {
            try
            {
                viewModel.IsLoading = true;


                if (App.TP != null)
                {
                    await viewModel.LoadDashboardData();

                }
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.BillCount = string.Empty;
                    viewModel.BillsAndReturnsCommitments = new List<OverduePaymentAndUnSubmittedReturn>();
                    viewModel.PopulateBillsInformation();
                    viewModel.PopulateReturnsInformation();
                    viewModel.PopualateCommittmentsInformation();
                    viewModel.IsLoading = false;
                });
            }
            finally { viewModel.IsLoading = false; }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }

        #endregion

        private void OnHomeTapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnHomeTapped", "Home Page");
            viewModel.MenuViewVisible = false;
            viewModel.HomeViewVisible = true;
            viewModel.AccountStatementVisible = false;
            viewModel.LiveChatVisible = false;
            viewModel.HomeIndicatorColor = (Color)Application.Current.Resources["Primary"];
            viewModel.MenuIndicatorColor = Colors.White;
            viewModel.TabbarColor = Colors.DarkGray;
            viewModel.StackMenuColor = Colors.White;

            frameToolbar.IsVisible = true;

            viewModel.IsToolbarTaxVisible = true;


            Instrumentation.EndCall(callTracker);
        }
        void HOmeView()
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnHomeTapped", "Home Page");
            viewModel.MenuViewVisible = false;
            viewModel.HomeViewVisible = true;
            viewModel.AccountStatementVisible = false;
            viewModel.LiveChatVisible = false;
            viewModel.HomeIndicatorColor = (Color)Application.Current.Resources["Primary"];
            viewModel.MenuIndicatorColor = Colors.White;
            viewModel.TabbarColor = Colors.DarkGray;
            viewModel.StackMenuColor = Colors.White;

            frameToolbar.IsVisible = true;

            viewModel.IsToolbarTaxVisible = true;

            Instrumentation.EndCall(callTracker);
            isFirstTime = true;
            // OnAppearing();
        }
        void menuView()
        {
            if (true)
            {
                if (App.LoginDataRetrieved != null)
                {

                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        viewModel.IsEstablishmentRegistrationTileVisible = false;
                        viewModel.IsVatRegistrationTileVisible = true;
                        viewModel.IsRegistrationDetailsTileVisible = true;
                        viewModel.IfRegInZakat = true;
                        refundreqMenu.IsVisible = refundreqMenuBox.IsVisible = false;
                        fillingMenu.IsVisible = fillingMenuBox.IsVisible = false;
                    }
                    else if (App.LoginDataRetrieved.ZkReg == "U")
                    {
                        viewModel.IsEstablishmentRegistrationTileVisible = true;
                        viewModel.IsRegistrationDetailsTileVisible = false;

                    }
                    else if (App.LoginDataRetrieved.ZkReg == "N")
                    {
                        viewModel.IsEstablishmentRegistrationTileVisible = false;
                        viewModel.IsRegistrationDetailsTileVisible = false;
                    }

                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        viewModel.IsVatRegistrationTileVisible = false;
                        viewModel.IfRegInZakat = false;
                        viewModel.IsSubsidyTileVisible = true;

                    }
                    else if (App.LoginDataRetrieved.VtReg == "R")
                    {
                        viewModel.IsVatRegistrationTileVisible = false;
                        viewModel.IfRegInZakat = false;
                        viewModel.IfnotRegInVATAndZakat = true;
                        viewModel.IfSignUpnNotRegInVATShowVATServie = true;
                        viewModel.IsSubsidyTileVisible = false;

                    }
                    else if (App.LoginDataRetrieved.VtReg == "")
                    {
                        viewModel.IfSignUpnNotRegInVATShowVATServie = false;
                    }

                    if (App.LoginDataRetrieved.ZkSignup == "X")
                    {
                        if (App.LoginDataRetrieved.ZkReg == string.Empty)
                        {
                            viewModel.IsVatRegistrationTileVisible = false;
                            viewModel.IsEstablishmentRegistrationTileVisible = true;
                        }

                    }
                    else if (App.LoginDataRetrieved.VtSignup == "X")
                    {
                        if (App.LoginDataRetrieved.VtReg == string.Empty)
                        {
                            viewModel.IsEstablishmentRegistrationTileVisible = false;
                            viewModel.IsVatRegistrationTileVisible = true;
                            viewModel.IfSignUpnNotRegInVAT = true;
                            viewModel.IsSubsidyTileVisible = false;
                        }
                    }
                    if (App.LoginDataRetrieved.ZkReg == "X" && App.LoginDataRetrieved.VtReg == "X")
                    {
                        viewModel.IfRegInZakat = true;
                        refundreqMenu.IsVisible = refundreqMenuBox.IsVisible = true;
                        fillingMenu.IsVisible = fillingMenuBox.IsVisible = true;


                    }

                    if ((App.LoginDataRetrieved.VtSignup == "X" || App.LoginDataRetrieved.ZkSignup == "X") && App.LoginDataRetrieved.ZkReg == string.Empty && App.LoginDataRetrieved.VtReg == string.Empty)
                    {
                        viewModel.IfnotRegInVATAndZakat = false;
                    }
                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        viewModel.IfnotRegInVATAndZakat = true;
                        viewModel.IfSignUpnNotRegInVATShowVATServie = true;
                        viewModel.IfSignUpnNotRegInVAT = false;
                    }
                    else if (App.LoginDataRetrieved.VtReg == string.Empty)
                    {
                        viewModel.IfSignUpnNotRegInVATShowVATServie = false;
                        viewModel.IfSignUpnNotRegInVAT = true;
                    }
                    else if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        viewModel.IfSignUpnNotRegInVAT = true;
                    }
                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        viewModel.IfnotRegInVATAndZakat = true;
                    }
                }

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnMenuTapped", AppResources.ZZZMenu + " Page");
                viewModel.MenuViewVisible = true;
                viewModel.HomeViewVisible = false;
                viewModel.AccountStatementVisible = false;
                viewModel.LiveChatVisible = false;
                viewModel.HomeIndicatorColor = Colors.White;
                viewModel.MenuIndicatorColor = (Color)Application.Current.Resources["Primary"];
                viewModel.StackMenuColor = Colors.Transparent;
                viewModel.TabbarColor = Colors.Transparent;
                viewModel.IsToolbarTaxVisible = false;

                frameToolbar.IsVisible = false;
                isFirstTime = true;


                Instrumentation.EndCall(callTracker);
                // OnAppearing();
            }
            else
            {
                PopupNavigation.Instance.PushAsync(new InfoPopUpPage());
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (true)
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnMenuTapped", AppResources.ZZZMenu + " Page");
                viewModel.MenuViewVisible = true;
                viewModel.HomeViewVisible = false;
                viewModel.AccountStatementVisible = false;
                viewModel.LiveChatVisible = false;
                viewModel.HomeIndicatorColor = Colors.White;
                viewModel.MenuIndicatorColor = (Color)Application.Current.Resources["Primary"];
                viewModel.StackMenuColor = Colors.Transparent;
                viewModel.TabbarColor = Colors.Transparent;
                viewModel.IsToolbarTaxVisible = false;

                frameToolbar.IsVisible = false;

                Instrumentation.EndCall(callTracker);
            }
            else
            {
                PopupNavigation.Instance.PushAsync(new InfoPopUpPage());
            }
        }
        private void TappedOnMyBills(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyBills", AppResources.MyBills + " Page");
                BillInfo billInfo = new BillInfo();
                //billInfo.BillTypeName = AppResources.UnPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

                Instrumentation.EndCall(callTracker);
            });
        }
        private void TappedOnMyReturns(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyReturns", AppResources.Returns + " Page");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 3);

                Instrumentation.EndCall(callTracker);
            });
        }

        private void TappedOnUnSubmitted(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {


                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Unsubmitted Return from Dashboard");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 1);

                Instrumentation.EndCall(callTracker);

            });
        }
        private void TappedOnSubmitted(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {


                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Submitted Return from Dashboard");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 0);


                Instrumentation.EndCall(callTracker);
            });
        }
        private void TappedOnOverDue(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {


                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Overdue Return from Dashboard");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 2);

                Instrumentation.EndCall(callTracker);

            });
        }
        private void paidClicked(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {


                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "PaidBills_Tapped", "Paid Bills from Dashboard");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.Paid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

                Instrumentation.EndCall(callTracker);
            });
        }

        private async void OnQuickActionClicked(object sender, EventArgs e)
        {
            try
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnQuickAction_Tapped", "Quick Actions");
                await PopupNavigation.Instance.PushAsync(new QuickActionPopUpPageView());


                Instrumentation.EndCall(callTracker);
            }
            catch (Exception)
            {


            }
        }

        private void OnAccountStatementsClicked(object sender, EventArgs e)
        {
            try
            {
                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "AccountStatements_Tapped", "Account Statements eService");
                // viewModel._navigationService.NavigateTo(App.AccountStatementsPageView);
                viewModel._navigationService.NavigateTo(App.AccountStatementBillsPageView);


                Instrumentation.EndCall(callTracker);
            }
            catch (Exception)
            {


            }
        }

        private void OnLiveChatClicked(object sender, EventArgs e)
        {
            try
            {
               
                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnSupportTapped", "Support");
                App.isFromDashboard = true;
                viewModel._navigationService.NavigateTo(App.SupportPageView);

                Instrumentation.EndCall(callTracker);
            }
            catch (Exception)
            {


            }
        }

        private void partiallyClicked(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "PaidBills_Tapped", "Partially Paid Bills from Dashboard");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.PartiallyPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

                Instrumentation.EndCall(callTracker);
            });
        }
        private void unPaidClicked(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "UnpaidBills_Tapped", "Unpaid Bills from Dashboard");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.UnPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

                Instrumentation.EndCall(callTracker);

            });
        }
        private async void Logout_Tapped(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LogoutPageView(AppResources.LogoutConfirmationMessage));
            }
            catch (Exception)
            {


            }
        }
        private void Label_MyBills(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {


                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyBills_Tapped", "All Bills from Dashboard");

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.UnPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

                Instrumentation.EndCall(callTracker);
            });

        }
        private void Label_MyRetuns_Tapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyRetuns_Tapped", "Returns eService");

            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 4);

            });

            Instrumentation.EndCall(callTracker);
        }
        private void Label_MyProfile_Tapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyProfile_Tapped", "My Profile eService");
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayerProfilePageView);
            });


            Instrumentation.EndCall(callTracker);
        }
        private void Aboutus_Tapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Aboutus_Tapped", "About Us");
            viewModel._navigationService.NavigateTo(App.AboutUsPageView);


            Instrumentation.EndCall(callTracker);
        }
        private void Contactus_Tapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Contactus_Tapped", "Contact Us");
            viewModel._navigationService.NavigateTo(App.ContactUsPageView);

            Instrumentation.EndCall(callTracker);
        }
        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "PrivacyPolicy_Tapped", "Privacy Policy");
            viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);

            Instrumentation.EndCall(callTracker);

        }
        private void ChangeLanguage_Tapped(object sender, EventArgs e)
        {
            try
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;

                if (App.IsArabic)
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeLanguage_Tapped", "Language Changed to English");

                    App.IsArabic = false;
                    App.changeFontFamily(App.appObj);

                    var vUpdatedPage = new GAZTNewDesignDashBoardPageView();
                    vUpdatedPage.Padding = safeInsets;

                    viewModel.SelectedCommitmentFilterValue = null;
                    Navigation.InsertPageBefore(vUpdatedPage, this);
                    Navigation.PopAsync();
                    viewModel.NDCommitments = AppResources.NDCommitments;
                    viewModel.ZBills = AppResources.Bills;
                    viewModel.Return = AppResources.Returns;

                    viewModel.AboutUs = AppResources.ZZZAboutUs;
                    viewModel.Contactus = AppResources.ZZZContactus;
                    viewModel.PrivacyandPolicy = AppResources.ZZZPrivacyandPolicy;
                    viewModel.Logout = AppResources.ZLogout;
                    App.HasToRefreshLoaderOnDashboard = true;
                    SetLTRDirection();


                    Instrumentation.EndCall(callTracker);
                }
                else
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeLanguage_Tapped", "Language Changed to Arabic");

                    App.IsArabic = true;
                    App.changeFontFamily(App.appObj);
                    var vUpdatedPage = new GAZTNewDesignDashBoardPageView();
                    vUpdatedPage.Padding = safeInsets;

                    viewModel.SelectedCommitmentFilterValue = null;
                    Navigation.InsertPageBefore(vUpdatedPage, this);
                    Navigation.PopAsync();
                    viewModel.NDCommitments = AppResources.NDCommitments;
                    viewModel.ZBills = AppResources.Bills;
                    viewModel.Return = AppResources.Returns;

                    viewModel.AboutUs = AppResources.ZZZAboutUs;
                    viewModel.Contactus = AppResources.ZZZContactus;
                    viewModel.PrivacyandPolicy = AppResources.ZZZPrivacyandPolicy;
                    viewModel.Logout = AppResources.ZLogout;
                    App.HasToRefreshLoaderOnDashboard = true;
                    SetRTLDirection();

                    Instrumentation.EndCall(callTracker);
                }

                OnAppearing();
            }
            catch (Exception)
            {


            }
        }

        private void RefundRequest_Tapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                //viewModel._navigationService.NavigateTo(App.RefundRequestMenuListPageView);
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });



            Instrumentation.EndCall(callTracker);
        }

        public void SetRTLDirection()
        {
            try
            {
                string langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                // this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.TranslateText = "English";

                viewModel.NextCommitmentsString = AppResources.ZZMyCommitments;
                viewModel.BillString = AppResources.ZZZDBMyPayments;
                viewModel.ReturnString = AppResources.ZZZDBMyReturns;

                viewModel.PaidString = AppResources.Paid + " " + viewModel.PaidBillCount;
                viewModel.UnPaidString = AppResources.UnPaid + " " + viewModel.UnPaidBillCount;
                viewModel.PartiallyPaidString = AppResources.Partiallynewui + " " + viewModel.PartiallyPaidBillCount;
                viewModel.TotalString = AppResources.NDTotalNumberOfBills;
                viewModel.WelcomeText = AppResources.ZZZWelcomeOnLanding;
                viewModel.Rotation = 180;
            }
            catch (Exception)
            {


            }
        }
        public void SetLTRDirection()
        {
            try
            {
                string langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                // this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.TranslateText = "عربي";

                viewModel.NextCommitmentsString = AppResources.ZZMyCommitments;
                viewModel.BillString = AppResources.ZZZDBMyPayments;
                viewModel.ReturnString = AppResources.ZZZDBMyReturns;

                viewModel.PaidString = AppResources.Paid + " " + viewModel.PaidBillCount;
                viewModel.UnPaidString = AppResources.UnPaid + " " + viewModel.UnPaidBillCount;
                viewModel.PartiallyPaidString = AppResources.Partiallynewui + " " + viewModel.PartiallyPaidBillCount;
                viewModel.TotalString = AppResources.NDTotalNumberOfBills;
                viewModel.WelcomeText = AppResources.ZZZWelcomeOnLanding;
                viewModel.Rotation = 0;
            }
            catch (Exception)
            {


            }

        }
        private void TapGestureRecognizer_Tapped_Inbox(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Inbox_Tapped", "Inbox eService");
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    viewModel._navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);
                }
                catch (Exception)
                {


                }

            });

            Instrumentation.EndCall(callTracker);
        }
        private void VATLookUp_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATLookUp_Tapped", "VAT Registration Verification eService");
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATLookUpNewPageView);
            });


            Instrumentation.EndCall(callTracker);
        }
        private void TaxpayerCertificate_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxpayerCertificate_Tapped", "My Certificates eService");
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);
            });


            Instrumentation.EndCall(callTracker);
        }

        private void VATDeregistrationDetails_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATDeregistrationDetails_Tapped", "VAT Deregistration eService");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage(false));
            });

            Instrumentation.EndCall(callTracker);
        }

        private void TinRegistrationDetails_Tapped(object sender, EventArgs e)
        {
            try
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TinRegistrationDetails_Tapped", "Registration Details");

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationDetailsListPageView);
                });


                Instrumentation.EndCall(callTracker);
            }
            catch (Exception)
            {

            }

        }

        private void VatRegistrationTile_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VatRegistrationTile_Tapped", "VAT Registration eService");
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);

            });

            Instrumentation.EndCall(callTracker);
        }


        private void ZakatInstalmentPlan_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ZakatInstalmentPlan_Tapped", "Zakat Instalment eService");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);
            });



            Instrumentation.EndCall(callTracker);
        }
        private void ChnageFillingPeriod_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeFillingPeriod_Tapped", "Change Filing Period eService");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });


            Instrumentation.EndCall(callTracker);
        }
        private void ContractRelease_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ContractRelease_Tapped", "Contract Release eService");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ContractReleaseListPageView);

            });


            Instrumentation.EndCall(callTracker);

        }
        private void Vat_Review_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Vat_Review_Tapped", "Objections eService");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ObjectionsSelectionPageView);
            });


            Instrumentation.EndCall(callTracker);
        }
        private void VATRefundRequest_Tapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });


            Instrumentation.EndCall(callTracker);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void TaxEvasion_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxEvasion_Tapped", "Tax Evasion eService");

            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                //  viewModel._navigationService.NavigateTo(App.TaxEvasionPageWebView);
                viewModel._navigationService.NavigateTo("InquiryAboutAddOrShowReportsPage");
            });

            Instrumentation.EndCall(callTracker);
        }

        private void OnVATNowTapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnVATNowTapped", "VAT Registration eService");
            App.VATType = PageExecutionType.Register;
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
            });

            Instrumentation.EndCall(callTracker);
        }

        private void OnSupportTapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnSupportTapped", "Support");
            viewModel._navigationService.NavigateTo(App.SupportPageView);


            Instrumentation.EndCall(callTracker);
        }

        private void OnZatcaInfoMenuTapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnZatcaInfoTapped", "ZatcaInfo");
            viewModel._navigationService.NavigateTo(App.ZatcaInfoMenuPageView);


            Instrumentation.EndCall(callTracker);
        }

        private void OnZakatNowTapped(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnZakatNowTapped", "Establishment Registration eService");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (App.LoginDataRetrieved.ZkReg == "U")
                {
                    App.ZAKATType = PageExecutionType.Update;
                    viewModel._navigationService.NavigateTo(App.EstablishmentAmendUpdatePage);
                }
                else
                {
                    viewModel._navigationService.NavigateTo(App.EstablishmentRegistrationPage);
                }
            });

            Instrumentation.EndCall(callTracker);
        }

        private void OnApplicationStatus_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnApplicationStatus_Tapped", "Application Status eService");
            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);

            Instrumentation.EndCall(callTracker);
        }

        private void VATAment_Tapped(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATAmend_Tapped", "VAT Amendment eService");

            App.VATType = PageExecutionType.Amend;
            viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView);

            Instrumentation.EndCall(callTracker);
        }

        private void VATReactivation_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATReactivation_Tapped", "VAT Reactivation eService");
            viewModel.IsLoading = true;
            App.VATType = PageExecutionType.Reactivation;
            MainThread.BeginInvokeOnMainThread(() => viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView));

            Instrumentation.EndCall(callTracker);
        }

        private void VATRegistration_Details_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRegistration_Details_Tapped", "VAT Registration Details eService");
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() => viewModel._navigationService.NavigateTo(App.VATRegistrationDisplayDetails));

            Instrumentation.EndCall(callTracker);
        }
        private void OnVATServiceTapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnVATServiceTapped", "VAT Services");
            viewModel._navigationService.NavigateTo(App.VATServicesPageView);

            Instrumentation.EndCall(callTracker);
        }

        private void AccountStatements_Tapped(object sender, EventArgs e)
        {

            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "AccountStatements_Tapped", "Account Statements eService");
            
            viewModel._navigationService.NavigateTo(App.AccountStatementBillsPageView);


            Instrumentation.EndCall(callTracker);
        }

        private void InstalmentPlan_Tapped(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ZakatInstalmentPlan_Tapped", "Zakat Instalment eService");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);
            });


            Instrumentation.EndCall(callTracker);
        }

        private void OnBackTapped(object sender, EventArgs e)
        {
            GoBackStep();
        }
        public void GoBackStep()
        {
        }

        public void SetParentMenuVisible()
        {
        }

        void CommitmentsPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            viewModel.SelectedCommitmentFilterLabelValue = viewModel.SelectedCommitmentFilterValue = e.NewValue.ToString();

        }

        private async void InternalUITesting_Tapped(object sender, EventArgs e)
        {
            try
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new TestPage());
            }
            catch (Exception)
            {


            }
        }

        private async void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {


            if (viewModel.IsMyObligationsClear || viewModel.MenuViewVisible)
            {
                frameToolbar.IsVisible = false;
                btn_frameToolbar.IsVisible = false;
            }
            else
            {
                frameToolbar.IsVisible = true;
                btn_frameToolbar.IsVisible = true;
            }

            var screenWidth = Application.Current.MainPage.Width;
            var btnWidth = btn_frameToolbar.Width;
            var xPosition = screenWidth - btnWidth - 20;
            var scrollView = sender as ScrollView;
            var yPostion = e.ScrollY;

            if (e.ScrollY > 120)
            {
                await frameToolbar.FadeTo(1, 100);
                await btn_frameToolbar.FadeTo(0, 100);
                await btn_frameToolbar.TranslateTo(xPosition - 10, -100, 100);
                viewModel.IsMenuLogoVisible = false;

                //                  await btn_frameToolbar.TranslateTo(100, 0, 200, Easing.CubicInOut);
            }
            else
            {
                await frameToolbar.FadeTo(0, 100);
                await btn_frameToolbar.FadeTo(1, 100);
                await btn_frameToolbar.TranslateTo(scrollView.X, scrollView.Y, 100);
                viewModel.IsMenuLogoVisible = true;

                //await btn_frameToolbar.TranslateTo(0, 0, 200, Easing.CubicInOut);
            }
        }

        private void TapGestureRecognizer_ToolbarMyTax(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            MainThread.BeginInvokeOnMainThread(() =>
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyBills", AppResources.MyBills + " Page");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);


                Instrumentation.EndCall(callTracker);
            });
        }

        private void BillsPayNowTapped(object sender, EventArgs e)
        {
            if (!viewModel.isPayNowTapped)
            {
                viewModel.isPayNowTapped = true;
                Border payNowCard = sender as Border;
                OverduePaymentAndUnSubmittedReturn BModel = (OverduePaymentAndUnSubmittedReturn)payNowCard.BindingContext;

                viewModel.verifyPaymentAndShowBillsPopup(BModel);
            }

        }
        private void OnEduLinkTapped(object sender, EventArgs e)
        {
            var callTracker = Instrumentation.BeginCall("DashboardPageView", "EduLink_Tapped", "Education Link");
            Uri uri = new Uri("https://edujourneys.zatca.gov.sa/home/tracks");
            OpenBrowser(uri);


            Instrumentation.EndCall(callTracker);
        }
        public async void OpenBrowser(Uri uri)
        {
            await Launcher.OpenAsync(uri);
        }

        void GoToExisTax(object sender, EventArgs e)
        {


            var callTracker = Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "GoToExisTax_Tapped", "Exis Tax");
            viewModel._navigationService.NavigateTo("ExciseTax");


            Instrumentation.EndCall(callTracker);

        }

        void OnChatTapped(object sender, EventArgs e)
        {

        }
    }
}
