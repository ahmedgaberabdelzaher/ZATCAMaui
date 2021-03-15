using EGAZT.Enums;
using EGAZT.Models.EnumModels;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.PaymentOptions;
using EGAZT.Views.NewDesign.VATDeRegistration;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfChart.XForms;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.Cards;
using Syncfusion.XForms.ProgressBar;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static EGAZT.ViewModel.NewDesignViewModel.GAZTNewDesignDashBoardPageViewModel;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.DashBoardPages
{       
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignDashBoardPageView : ContentPage
    {
        #region Variable

        GAZTNewDesignDashBoardPageViewModel viewModel;
        private bool isTimerOff = false;

        #endregion

        private bool isFirstTime = true;
        public GAZTNewDesignDashBoardPageView()
        {
            try
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Constructor", AppResources.Dashboard);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                viewModel = App.Locator.GAZTNewDesignDashBoardPageView;
                this.BindingContext = viewModel;
                viewModel.MyObligationAmount = 0.0;

                viewModel.MyObligationAmountCommas = "";
                viewModel.IsPendingBillsVisible = false;
                viewModel.IsInstalmentPlanVisible = false;
                viewModel.IsMyObligationsClear = false;
                viewModel.MenuViewVisible = false;
                viewModel.IfnotRegInVATAndZakat = false;
                viewModel.IsBodyMyTaxVisible = false;

                if(viewModel.AccountStatementsList != null) {

                    viewModel.AccountStatementsList.Clear();
                }

                
                //InstalmentsCollectionView.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
                //{
                //    ItemSpacing = 10
                //};




                SetLTR();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void Vat_Registration_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRegistration_Details_Tapped", "VAT Registration Details eService");
            viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void General_Services_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "GeneralServices_Tapped", "General Services");
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.GeneralServicesListPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void btnCommitmentsPickerClicked(object sender, System.EventArgs e)
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

                Colors.Add(Color.Green);

                Colors.Add(Color.Purple);

                Colors.Add(Color.Red);

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
            this.Padding = safeInsets;

            viewModel.NextCommitmentsString = AppResources.ZZZZNextCommitments;
            viewModel.PaidString = AppResources.Paid + " " + viewModel.PaidBillCount;
            viewModel.UnPaidString = AppResources.UnPaid + " " + viewModel.UnPaidBillCount;
            viewModel.PartiallyPaidString = AppResources.Partiallynewui + " " + viewModel.PartiallyPaidBillCount;
            viewModel.TotalString = AppResources.NDTotalNumberOfBills;

          

           
            ChangeArrowDirection();
            MessagingCenter.Subscribe<Object>(this, "UpdateProgressBar", (sender) =>
            {
                RangeColorCollection rangeColors = new RangeColorCollection();
                rangeColors.Add(new RangeColor() { Color = Color.FromHex("006450"), IsGradient = false, Start = 0, End = viewModel.CreditAmountStartProgressBar });
                rangeColors.Add(new RangeColor() { Color = Color.FromHex("AA0C19"), IsGradient = false, Start = viewModel.CreditAmountStartProgressBar, End = 100 });
                //         Device.BeginInvokeOnMainThread(() => TaxBalanceProgress.RangeColors = rangeColors);
            });
            isTimerOff = false;
            StartTimer();
            viewModel.IsLoading = false;

           

            if (isFirstTime)
            {

                await frameToolbar.FadeTo(0, 0);
                await btn_frameToolbar.FadeTo(1, 0);
                isFirstTime = false;

                /*if (viewModel.IsMyObligationsClear)
                {
                    //part.IsVisible = false;
                    btn_frameToolbar.IsVisible = false;
                }
                else
                {
                    //part.IsVisible = true;
                    btn_frameToolbar.IsVisible = true;
                }*/
            }


            if (App.isMybillsRefresh) {

                await viewModel.GetBillsAndReturns();
                await viewModel.GetAccountStatments();
            }


            try
            {
                MessagingCenter.Subscribe<object, string>(this, "MultipleBillsContinue", async (sender, arg) =>
                {
                    Console.WriteLine("MultipleBillsContinue Clicked");

                    viewModel.showPaymentOptions();

                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

            try
            {
                MessagingCenter.Subscribe<Object, string>(this, "Card_Payment", async (sender, arg) =>
                {
                    Console.WriteLine("Card Payment Clicked");

                    viewModel.MadaPaymentSelected();

                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

            try
            {
                MessagingCenter.Subscribe<Object, string>(this, "Apple_Pay", async (sender, arg) =>
                {
                    viewModel.ApplePaySelected();

                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

            try
            {
                MessagingCenter.Subscribe<Object, string>(this, "SADAD", async (sender, arg) =>
                {

                    Console.WriteLine("SADAD Clicked");
                    viewModel.SadadPaymentSelected();
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            try
            {
                MessagingCenter.Subscribe<App, string>(this, "DashboardApplePayData", async (sender, arg) =>
                {

                    viewModel.ApplePayTokenData = arg.ToString();

                    await viewModel.UpdateApplePayPaymentGuid();


                });

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            /*                            TaxTypePicker.HeaderFontFamily = "SSTArabic-Medium";
                                                        TaxTypePicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                                        TaxTypePicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                                        TaxTypePicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy*/

                            CommitmentsPicker.HeaderFontFamily = "SSTArabic-Medium";
                            CommitmentsPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            CommitmentsPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                            CommitmentsPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {

                            /*  TaxTypePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                              TaxTypePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                              TaxTypePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                              TaxTypePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
  */
                            CommitmentsPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CommitmentsPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CommitmentsPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CommitmentsPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        private void OnDataLoad()
        {
            if (App.HasToRefreshLoaderOnDashboard == true)
            {
                App.IsComingFromSleepMode = false;


                LoadData();
                try
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
                            }
                        }
                        if (App.LoginDataRetrieved.ZkReg == "X" && App.LoginDataRetrieved.VtReg == "X")
                        {
                            viewModel.IfRegInZakat = true;
                            refundreqMenu.IsVisible = refundreqMenuBox.IsVisible = true;
                            fillingMenu.IsVisible = fillingMenuBox.IsVisible = true;


                        }

                        if ((App.LoginDataRetrieved.VtSignup == "X" || App.LoginDataRetrieved.ZkSignup == "X") && (App.LoginDataRetrieved.ZkReg == string.Empty && App.LoginDataRetrieved.VtReg == string.Empty))
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
            else
            {

            }
        }

        private void StartTimer()
        {
            int counter = 120;
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                counter = counter - 1;
                if (counter == 0)
                {
                    counter = 120;
                    App.HasToRefreshLoaderOnDashboard = true;
                    OnDataLoad();
                }
                return !isTimerOff;
            });
        }

        public void RefreshDashboardCommand()
        {
            if (viewModel.MenuViewVisible)
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnMenuTapped", AppResources.ZZZMenu + " Page");
                viewModel.MenuViewVisible = true;
                viewModel.HomeViewVisible = false;
                viewModel.AccountStatementVisible = false;
                viewModel.LiveChatVisible = false;
                viewModel.HomeIndicatorColor = Color.White;
                viewModel.MenuIndicatorColor = Color.FromHex("#006450");
                viewModel.StackMenuColor = Color.Transparent;
                viewModel.TabbarColor = Color.Transparent;
                viewModel.IsToolbarTaxVisible = false;

                frameToolbar.IsVisible = false;

                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
            else
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnHomeTapped", "Home Page");
                viewModel.MenuViewVisible = false;
                viewModel.HomeViewVisible = true;
                viewModel.AccountStatementVisible = false;
                viewModel.LiveChatVisible = false;
                viewModel.HomeIndicatorColor = Color.DarkGreen;
                viewModel.MenuIndicatorColor = Color.White;
                viewModel.TabbarColor = Color.DarkGray;
                viewModel.StackMenuColor = Color.White;

                frameToolbar.IsVisible = true;

                viewModel.IsToolbarTaxVisible = true;

                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<GAZTNewDesignDashBoardPageView, string>(this, "StartTimerForDashboard");
            MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToLogout");
            MessagingCenter.Unsubscribe<object, string>(this, "NoPressedToLogout");
            MessagingCenter.Unsubscribe<Object>(this, "UpdateProgressBar");
            MessagingCenter.Unsubscribe<Object, string>(this, "Card_Payment");
            MessagingCenter.Unsubscribe<Object, string>(this, "Apple_Pay");
            MessagingCenter.Unsubscribe<Object, string>(this, "SADAD");
            MessagingCenter.Unsubscribe<App, string>(this, "DashboardApplePayData");
            MessagingCenter.Unsubscribe<object, string>(this, "MultipleBillsContinue");



            isTimerOff = true;
        }
        private async void LoadData()
        {
            try
            {
                viewModel.IsLoading = true;
                await viewModel.LoadDashboardData();
                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.BillCount = string.Empty;
                    viewModel.BillsAndReturnsCommitments = new List<OverduePaymentAndUnSubmittedReturn>();
                    viewModel.PopulateBillsInformation();
                    viewModel.PopulateReturnsInformation();
                    viewModel.PopualateCommittmentsInformation();
                    /*  try
                      {
                          if (viewModel.BillsAndReturnsCommitments != null)
                          {
                              if (viewModel.BillsAndReturnsCommitments.Count > 0)
                              {
                                  CollectionView_Commitment.ScrollTo(0);
                              }

                          }
                      }
                      catch (Exception ex)
                      {
                          Console.Write(ex.ToString());
                          Console.Write(ex.StackTrace.ToString());
                      }*/
                    viewModel.IsLoading = false;
                });
            }
            finally { viewModel.IsLoading = false; }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        #endregion

        private void OnHomeTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnHomeTapped", "Home Page");
            viewModel.MenuViewVisible = false;
            viewModel.HomeViewVisible = true;
            viewModel.AccountStatementVisible = false;
            viewModel.LiveChatVisible = false;
            viewModel.HomeIndicatorColor = Color.DarkGreen;
            viewModel.MenuIndicatorColor = Color.White;
            viewModel.TabbarColor = Color.DarkGray;
            viewModel.StackMenuColor = Color.White;

            frameToolbar.IsVisible = true;

            viewModel.IsToolbarTaxVisible = true;

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (true)
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnMenuTapped", AppResources.ZZZMenu + " Page");
                viewModel.MenuViewVisible = true;
                viewModel.HomeViewVisible = false;
                viewModel.AccountStatementVisible = false;
                viewModel.LiveChatVisible = false;
                viewModel.HomeIndicatorColor = Color.White;
                viewModel.MenuIndicatorColor = Color.FromHex("#006450");
                viewModel.StackMenuColor = Color.Transparent;
                viewModel.TabbarColor = Color.Transparent;
                viewModel.IsToolbarTaxVisible = false;

                frameToolbar.IsVisible = false;

                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
            else
            {
                PopupNavigation.Instance.PushAsync(new InfoPopUpPage());
            }
        }
        private void TappedOnMyBills(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyBills", AppResources.MyBills + " Page");
                BillInfo billInfo = new BillInfo();
                //billInfo.BillTypeName = AppResources.UnPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            });
        }
        private void TappedOnMyReturns(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyReturns", AppResources.Returns + " Page");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 3);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            });
        }
        /* private void TappedOnSignleReturns(object sender, EventArgs e)
         {
             string controltype = sender.GetType().ToString();

             Syncfusion.XForms.Cards.SfCardView arrowImage = sender as Syncfusion.XForms.Cards.SfCardView;
             ReturnTypeAndCorrepsondingCount BModel = (ReturnTypeAndCorrepsondingCount)arrowImage.BindingContext;
             if (BModel.ReturnTypeName == AppResources.Submitted)
             {
                 viewModel.IsLoading = true;
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Submitted Return from Dashboard");
                     viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 0);
                     AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                 });
             }
             if (BModel.ReturnTypeName == AppResources.UnSubmitted)
             {
                 viewModel.IsLoading = true;
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Unsubmitted Return from Dashboard");
                     viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 1);
                     AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                 });
             }
             if (BModel.ReturnTypeName == AppResources.OverDue)
             {
                 viewModel.IsLoading = true;
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Overdue Return from Dashboard");
                     viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 2);
                     AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                 });

             }

         }*/

        private void TappedOnUnSubmitted(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Unsubmitted Return from Dashboard");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 1);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);

            });
        }
        private void TappedOnSubmitted(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Submitted Return from Dashboard");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 0);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            });
        }
        private void TappedOnOverDue(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Overdue Return from Dashboard");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 2);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);

            });
        }
        private void paidClicked(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "PaidBills_Tapped", "Paid Bills from Dashboard");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.Paid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            });
        }

        private async void OnQuickActionClicked(object sender, EventArgs e)
        {
            try
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnQuickAction_Tapped", "Quick Actions");
                await PopupNavigation.Instance.PushAsync(new QuickActionPopUpPageView());
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void OnAccountStatementsClicked(object sender, EventArgs e)
        {
            try
            {/*
                viewModel.MenuViewVisible = false;
                viewModel.HomeViewVisible = false;
                viewModel.AccountStatementVisible = true;
                viewModel.LiveChatVisible = false;
                viewModel.IsToolbarTaxVisible = false;
*/
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "AccountStatements_Tapped", "Account Statements eService");
                viewModel._navigationService.NavigateTo(App.AccountStatementsPageView);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void OnLiveChatClicked(object sender, EventArgs e)
        {
            try
            {
                /* viewModel.MenuViewVisible = false;
                 viewModel.HomeViewVisible = false;
                 viewModel.AccountStatementVisible = false;
                 viewModel.LiveChatVisible = true;
                 viewModel.IsToolbarTaxVisible = false;*/

                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnSupportTapped", "Support");
                App.isFromDashboard = true;
                viewModel._navigationService.NavigateTo(App.SupportPageView);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void partiallyClicked(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "PaidBills_Tapped", "Partially Paid Bills from Dashboard");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.PartiallyPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            });
        }
        private void unPaidClicked(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "UnpaidBills_Tapped", "Unpaid Bills from Dashboard");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.UnPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);

            });
        }
        private async void Logout_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LogoutPageView(AppResources.LogoutConfirmationMessage));
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        private void Label_MyBills(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyBills_Tapped", "All Bills from Dashboard");

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.UnPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            });

        }
        private void Label_MyRetuns_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyRetuns_Tapped", "Returns eService");

            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 4);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void Label_MyProfile_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyProfile_Tapped", "My Profile eService");
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayerProfilePageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void Aboutus_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Aboutus_Tapped", "About Us");
            viewModel._navigationService.NavigateTo(App.AboutUsPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void Contactus_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Contactus_Tapped", "Contact Us");
            viewModel._navigationService.NavigateTo(App.ContactUsPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "PrivacyPolicy_Tapped", "Privacy Policy");
            viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

        }
        private void ChangeLanguage_Tapped(object sender, EventArgs e)
        {
            try
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;

                if (App.IsArabic)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeLanguage_Tapped", "Language Changed to English");

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
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeLanguage_Tapped", "Language Changed to Arabic");

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
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }

                OnAppearing();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void RefundRequest_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.RefundRequestMenuListPageView);
            });

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        public void SetRTLDirection()
        {
            try
            {
                String langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
               // this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.TranslateText = "English";

                viewModel.NextCommitmentsString = AppResources.ZZZZNextCommitments;
                viewModel.BillString = AppResources.ZZZDBMyPayments;
                viewModel.ReturnString = AppResources.ZZZDBMyReturns;

                viewModel.PaidString = AppResources.Paid + " " + viewModel.PaidBillCount;
                viewModel.UnPaidString = AppResources.UnPaid + " " + viewModel.UnPaidBillCount;
                viewModel.PartiallyPaidString = AppResources.Partiallynewui + " " + viewModel.PartiallyPaidBillCount;
                viewModel.TotalString = AppResources.NDTotalNumberOfBills;
                viewModel.WelcomeText = AppResources.ZZZWelcomeOnLanding;
                viewModel.Rotation = 180;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        public void SetLTRDirection()
        {
            try
            {
                String langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
               // this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.TranslateText = "عربي";

                viewModel.NextCommitmentsString = AppResources.ZZZZNextCommitments;
                viewModel.BillString = AppResources.ZZZDBMyPayments;
                viewModel.ReturnString = AppResources.ZZZDBMyReturns;

                viewModel.PaidString = AppResources.Paid + " " + viewModel.PaidBillCount;
                viewModel.UnPaidString = AppResources.UnPaid + " " + viewModel.UnPaidBillCount;
                viewModel.PartiallyPaidString = AppResources.Partiallynewui + " " + viewModel.PartiallyPaidBillCount;
                viewModel.TotalString = AppResources.NDTotalNumberOfBills;
                viewModel.WelcomeText = AppResources.ZZZWelcomeOnLanding;
                viewModel.Rotation = 0;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }
        private void TapGestureRecognizer_Tapped_Inbox(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Inbox_Tapped", "Inbox eService");
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    viewModel._navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);
                }
                catch (Exception)
                {

                }

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void VATLookUp_Tapped(System.Object sender, System.EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATLookUp_Tapped", "VAT Registration Verification eService");
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATLookUpNewPageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void TaxpayerCertificate_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxpayerCertificate_Tapped", "My Certificates eService");
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void VATDeregistrationDetails_Tapped(object sender, EventArgs e)
        {

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATDeregistrationDetails_Tapped", "VAT Deregistration eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage());
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void TinRegistrationDetails_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TinRegistrationDetails_Tapped", "Registration Details");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ZakatRegistrationDetailsListPageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void VatRegistrationTile_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VatRegistrationTile_Tapped", "VAT Registration eService");
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }


        private void ZakatInstalmentPlan_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ZakatInstalmentPlan_Tapped", "Zakat Instalment eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);
            });

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void ChnageFillingPeriod_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeFillingPeriod_Tapped", "Change Filing Period eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void ContractRelease_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ContractRelease_Tapped", "Contract Release eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ContractReleaseListPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

        }
        private void Vat_Review_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Vat_Review_Tapped", "Objections eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ObjectionsSelectionPageView);
            });

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void VATRefundRequest_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void TaxEvasion_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxEvasion_Tapped", "Tax Evasion eService");

            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxEvasionPageWebView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnVATNowTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnVATNowTapped", "VAT Registration eService");
            App.VATType = PageExecutionType.Register;
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnSupportTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnSupportTapped", "Support");
            viewModel._navigationService.NavigateTo(App.SupportPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnZakatNowTapped(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnZakatNowTapped", "Establishment Registration eService");
            Device.BeginInvokeOnMainThread(() =>
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
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnApplicationStatus_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnApplicationStatus_Tapped", "Application Status eService");
            viewModel._navigationService.NavigateTo(App.FormBundleStatusPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void VATAment_Tapped(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATAmend_Tapped", "VAT Amendment eService");

            App.VATType = PageExecutionType.Amend;
            viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView);

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void VATReactivation_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATReactivation_Tapped", "VAT Reactivation eService");
            viewModel.IsLoading = true;
            App.VATType = PageExecutionType.Reactivation;
            Device.BeginInvokeOnMainThread(() => viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView));
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void VATRegistration_Details_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRegistration_Details_Tapped", "VAT Registration Details eService");
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() => viewModel._navigationService.NavigateTo(App.VATRegistrationDisplayDetails));
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void OnVATServiceTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnVATServiceTapped", "VAT Services");
            viewModel._navigationService.NavigateTo(App.VATServicesPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void AccountStatements_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "AccountStatements_Tapped", "Account Statements eService");
            viewModel._navigationService.NavigateTo(App.AccountStatementsPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void InstalmentPlan_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ZakatInstalmentPlan_Tapped", "Zakat Instalment eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
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

        /*        private void btnTaxTypePickerClicked(object sender, System.EventArgs e)
                {
                    TaxTypePicker.IsOpen = true;
                }*/

        void CommitmentsPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedCommitmentFilterLabelValue = viewModel.SelectedCommitmentFilterValue = e.NewValue.ToString();

        }

        private async void InternalUITesting_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new TestPage());
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
            var scrollView = sender as Xamarin.Forms.ScrollView;
            var yPostion = e.ScrollY;

            if (e.ScrollY > 120)
            {
                await frameToolbar.FadeTo(1, 600);
                await btn_frameToolbar.FadeTo(0, 600);
                await btn_frameToolbar.TranslateTo(xPosition - 10, -100, 400);

                //                  await btn_frameToolbar.TranslateTo(100, 0, 200, Easing.CubicInOut);
            }
            else
            {
                await frameToolbar.FadeTo(0, 600);
                await btn_frameToolbar.FadeTo(1, 600);
                await btn_frameToolbar.TranslateTo(scrollView.X, scrollView.Y, 400);

                //await btn_frameToolbar.TranslateTo(0, 0, 200, Easing.CubicInOut);
            }
        }

        private void TapGestureRecognizer_ToolbarMyTax(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyBills", AppResources.MyBills + " Page");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            });
        }

        private async void BillsPayNowTapped(object sender, EventArgs e)
        {

            SfBorder payNowCard = sender as SfBorder;
            OverduePaymentAndUnSubmittedReturn BModel = (OverduePaymentAndUnSubmittedReturn)payNowCard.BindingContext;
            Console.WriteLine("Clicked on: Amount: " + BModel.Amount + " ,FbNum: " + BModel.Fbnum);
            //viewModel.DoValidatePayment(BModel.Fbnum, BModel.Amount);

            viewModel.verifyPaymentAndShowBillsPopup(BModel);
            /*if (BModel.MadabutFg == "X")
                {
                    PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));

                }
                else
                {
                    PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, BModel.OpenliMsg));
                }
                viewModel.selectedFbNum = BModel.Fbnum;
                viewModel.selectedSadadNo = BModel.Sopbel;
                viewModel.selectedAmount = BModel.Amount;

                viewModel.selectedTaxablePeriod = BModel.Persl;*/

        }
    }
}
