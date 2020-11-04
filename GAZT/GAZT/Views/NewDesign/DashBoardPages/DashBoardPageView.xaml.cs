using EGAZT.Enums;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.TAXEvasionPages;
using EGAZT.Views.NewDesign.VATDeRegistration;
using EGAZT.Views.NewDesign.ZakatDeregistration;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static EGAZT.ViewModel.NewDesignViewModel.GAZTNewDesignDashBoardPageViewModel;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignDashBoardPageView : ContentPage
    {
        #region Variable
        GAZTNewDesignDashBoardPageViewModel viewModel;
        #endregion

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
                SetLTR();
                if (viewModel != null)
                {
                    viewModel.MenuViewVisible = false;
                    viewModel.TaxpayerName = string.Empty;
                    viewModel.HomeViewVisible = true;
                    viewModel.IsVatRegistrationTileVisible = false;
                }

                if (App.TP != null)
                    viewModel.TaxPayerProfile = App.TP;

                if (App.IsArabic)
                {
                    // viewModel.TranslateText = AppResources.ZZZSetLanguageText; ;
                    viewModel.TranslateText = AppResources.ZZZChangetoLanguage;
                }
                else
                {
                    // viewModel.TranslateText = AppResources.ZZZSetLanguageText;
                    viewModel.TranslateText = AppResources.ZZZChangetoLanguage;
                }

                viewModel.MenuViewVisible = false;
                viewModel.HomeViewVisible = true;
            }
            catch (Exception ex)
            {

            }
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnDataLoad();
            RefreshDashboardCommand();
            getYesCommandToLogout();
            getNoCommandToLogout();
            viewModel.NextCommitmentsString = AppResources.ZZZZNextCommitments;
            viewModel.PaidString = AppResources.Paid + " " + viewModel.PaidBillCount;
            viewModel.UnPaidString = AppResources.UnPaid + " " + viewModel.UnPaidBillCount;
            viewModel.PartiallyPaidString = AppResources.Partiallynewui + " " + viewModel.PartiallyPaidBillCount;
            viewModel.TotalString = AppResources.NDTotalNumberOfBills;
            ChangeArrowDirection();
        }
        public async void getYesCommandToLogout()
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

            }
        }

        public async void getNoCommandToLogout()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoPressedToLogout", async (sender, arg) =>
                {
                });
            }
            catch (Exception ex)
            {

            }
        }
        public void OnDataLoad()
        {
            if (viewModel != null)
            {
                viewModel.IsLoading = false;
                // viewModel.MenuViewVisible = false;
                // viewModel.HomeViewVisible = true;
                //viewModel.StackMenuColor = Color.White;
                //viewModel.TabbarColor = Color.DarkGray;
                //viewModel.HomeIndicatorColor= Color.DarkGreen;
            }
            if (App.HasToRefreshLoaderOnDashboard == true)
            {
                App.IsComingFromSleepMode = false;


                Task.Run(async () =>
                {
                    await LoadData();

                    if (viewModel != null)
                        viewModel.IsLoading = false;
                });
                try
                {
                    //if (App.LoginDataRetrieved.VtReg == null)
                    //{
                    //    viewModel.IsVatRegistrationTileVisible = true;
                    //}
                    //else if (App.LoginDataRetrieved.VtReg != "X")
                    //{
                    //    viewModel.IsVatRegistrationTileVisible = true;
                    //}

                    //if (App.LoginDataRetrieved.ZkReg == null)
                    //{
                    //    viewModel.IsEstablishmentRegistrationTileVisible = true;
                    //}
                    //else if (App.LoginDataRetrieved.ZkReg != "X")
                    //{
                    //    viewModel.IsEstablishmentRegistrationTileVisible = true;
                    //}


                    //New Code For Manage VAT Registration and Zakat Registration Tile

                    if (App.LoginDataRetrieved != null)
                    {

                        if (App.LoginDataRetrieved.ZkReg == "X")
                        {
                            viewModel.IsEstablishmentRegistrationTileVisible = false;
                            viewModel.IsVatRegistrationTileVisible = true;
                            viewModel.IsRegistrationDetailsTileVisible = true;
                        }
                        else if(App.LoginDataRetrieved.ZkReg == "U")
                        {
                            viewModel.IsEstablishmentRegistrationTileVisible = true;
                            viewModel.IsRegistrationDetailsTileVisible = false;

                        }
                        else if(App.LoginDataRetrieved.ZkReg == "N")
                        {
                            viewModel.IsEstablishmentRegistrationTileVisible = false;
                            viewModel.IsRegistrationDetailsTileVisible = false;
                        }

                        if (App.LoginDataRetrieved.VtReg == "X")
                        {
                            viewModel.IsVatRegistrationTileVisible = false;
                        }
                        else if (App.LoginDataRetrieved.VtReg == "R")
                        {
                            viewModel.IsVatRegistrationTileVisible = false;
                            viewModel.IfSignUpnNotRegInVATShowVATServie = true;
                        }

                        if (App.LoginDataRetrieved.ZkSignup == "X")
                        {
                            if (App.LoginDataRetrieved.ZkReg == string.Empty)
                            {
                                viewModel.IsVatRegistrationTileVisible = false;
                                viewModel.IsEstablishmentRegistrationTileVisible = true;
                            }

                        }
                        else if(App.LoginDataRetrieved.VtSignup == "X")
                        {
                            if (App.LoginDataRetrieved.VtReg == string.Empty)
                            {
                                viewModel.IsEstablishmentRegistrationTileVisible = false;
                                viewModel.IsVatRegistrationTileVisible = true;
                                viewModel.IfSignUpnNotRegInVAT = true;
                            }
                        }

                        
                        if((App.LoginDataRetrieved.VtSignup == "X" || App.LoginDataRetrieved.ZkSignup == "X") && (App.LoginDataRetrieved.ZkReg == string.Empty && App.LoginDataRetrieved.VtReg == string.Empty))
                        {
                            viewModel.IfnotRegInVATAndZakat = false;
                        }
                        if(App.LoginDataRetrieved.VtReg == "X")
                        {
                            viewModel.IfnotRegInVATAndZakat = true;
                            viewModel.IfSignUpnNotRegInVATShowVATServie = true;
                            viewModel.IfSignUpnNotRegInVAT = false;
                        }
                        else if(App.LoginDataRetrieved.VtReg == string.Empty)
                        {
                            viewModel.IfSignUpnNotRegInVATShowVATServie = false;
                            viewModel.IfSignUpnNotRegInVAT = true;
                        }else if(App.LoginDataRetrieved.ZkReg == "X")
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
                App.StartTimer(0, 2, 0);
            }
            else
            {

            }
        }

        public async void RefreshDashboardCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "StartTimerForDashboard", async (sender, arg) =>
                {
                    OnDataLoad();
                });
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, string>(this, "StartTimerForDashboard");
            MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToLogout");
            MessagingCenter.Unsubscribe<object, string>(this, "NoPressedToLogout");
        }
        private async Task LoadData()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;

                });
                await viewModel.LoadDashboardData();
                Device.BeginInvokeOnMainThread(() =>
               {
                   viewModel.BillCount = string.Empty;
                   viewModel.BillsAndReturnsCommitments = null;

                   viewModel.PopulateBillsInformation();
                   viewModel.PopulateReturnsInformation();
                   viewModel.PopualateCommittmentsInformation();
                   try
                   {
                       if (viewModel.BillsAndReturnsCommitments != null)
                       {
                           if (viewModel.BillsAndReturnsCommitments.Count > 0)
                           {
                               CollectionView_Commitment.ScrollTo(0);
                           }

                       }
                   }
                   catch(Exception ex)
                   {
                   }
                   
                   viewModel.IsLoading = false;

               });

                // viewModel.PopulateeServicesApplicableToTheTaxPayer();
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;

                });
            }
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
            viewModel.HomeIndicatorColor = Color.DarkGreen;
            viewModel.MenuIndicatorColor = Color.White;
            viewModel.TabbarColor = Color.DarkGray;
            viewModel.StackMenuColor = Color.White;

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

            //MenuView.IsVisible = false;
            //HomeView.IsVisible = true;
            //HomeIndicator.BackgroundColor = Color.DarkGreen;
            //MenuIndicator.BackgroundColor = Color.White;
            //Tabbar.BorderColor = Color.DarkGray;
            //stackMenu.BackgroundColor = Color.White;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //if (!viewModel.IsVatRegistrationTileVisible || !viewModel.IsEstablishmentRegistrationTileVisible)
            if(true)
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnMenuTapped", AppResources.ZZZMenu + " Page");

                //viewModel._navigationService.NavigateTo(App.TaxManagementPageView);
                viewModel.MenuViewVisible = true;
                viewModel.HomeViewVisible = false;
                viewModel.HomeIndicatorColor = Color.White;

                viewModel.MenuIndicatorColor = Color.FromHex("#006450");
                viewModel.StackMenuColor = Color.Transparent;
                viewModel.TabbarColor = Color.Transparent;

                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
            else
            {
                PopupNavigation.Instance.PushAsync(new InfoPopUpPage());
            }
        }
        private async void TappedOnMyBills(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyBills", AppResources.MyBills + " Page");

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);

            });
            // App.DisplayProgressView();


        }
        private async void TappedOnMyReturns(object sender, EventArgs e)
        {
            //App.DisplayProgressView();
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyReturns", AppResources.Returns + " Page");
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 3);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);

            });

        }
        private async void TappedOnSignleReturns(object sender, EventArgs e)
        {
            // App.DisplayProgressView();

            string controltype = sender.GetType().ToString();

            Syncfusion.XForms.Cards.SfCardView arrowImage = sender as Syncfusion.XForms.Cards.SfCardView;
            ReturnTypeAndCorrepsondingCount BModel = (ReturnTypeAndCorrepsondingCount)arrowImage.BindingContext;
            if (BModel.ReturnTypeName == AppResources.Submitted)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;

                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Submitted Return from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 0);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                });
            }
            if (BModel.ReturnTypeName == AppResources.UnSubmitted)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;

                });
                Device.BeginInvokeOnMainThread(() =>
                {

                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Unsubmitted Return from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 1);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                });
            }
            if (BModel.ReturnTypeName == AppResources.OverDue)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;

                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Overdue Return from Dashboard");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 2);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                });

            }

        }
        private async void paidClicked(object sender, EventArgs e)
        {
            // App.DisplayProgressView();

            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
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
            catch (Exception Ex)
            {

            }

        }

        private async void partiallyClicked(object sender, EventArgs e)
        {
            //  App.DisplayProgressView();
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "PaidBills_Tapped", "Partially Paid Bills from Dashboard");
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.PartiallyPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            });
        }
        private async void unPaidClicked(object sender, EventArgs e)
        {
            //   App.DisplayProgressView();
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
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
                //    App.DisplayProgressView();

                //if (App.IsArabic)
                //{
                //    var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZNo, AppResources.ZYes);
                //    if (!result)
                //    {
                //        App.TP = null;
                //        await viewModel.LogOut();
                //    }
                //}
                //else
                //{
                //    var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
                //    if (result)
                //    {
                //        App.TP = null;
                //        await viewModel.LogOut();
                //    }
                //}
                PopupNavigation.Instance.PushAsync(new LogoutPageView(AppResources.LogoutConfirmationMessage));
            }
            catch(Exception ex)
            {

            }
        }
        private async void Label_MyBills(object sender, EventArgs e)
        {
            //     App.DisplayProgressView();
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyBills_Tapped", "All Bills from Dashboard");

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);

            });

        }
        private async void Label_MyRetuns_Tapped(object sender, EventArgs e)
        {
            //       App.DisplayProgressView();   await Task.Run(() =>

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyRetuns_Tapped", "Returns eService");

            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 4);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private async void Label_MyProfile_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyProfile_Tapped", "My Profile eService");
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
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
                //        App.DisplayProgressView();
                if (App.IsArabic)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeLanguage_Tapped", "Language Changed to English");

                    App.IsArabic = false;
                    App.changeFontFamily(App.appObj);
                    SetLTRDirection();
                    var vUpdatedPage = new GAZTNewDesignDashBoardPageView();
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

                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeLanguage_Tapped", "Language Changed to Arabic");

                    App.IsArabic = true;
                    App.changeFontFamily(App.appObj);
                    SetRTLDirection();
                    var vUpdatedPage = new GAZTNewDesignDashBoardPageView();
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
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }

                OnAppearing();
            }
            catch (Exception ex)
            {

            }
        }
        public void SetRTLDirection()
        {
            try
            {
                String langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                // InitializeComponent();
                this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.TranslateText = AppResources.ZZZSetToEnglish;

                viewModel.NextCommitmentsString = AppResources.ZZZZNextCommitments;
                viewModel.BillString = AppResources.ZZZDBMyPayments;
                viewModel.ReturnString = AppResources.ZZZDBMyReturns;

                viewModel.PaidString = AppResources.Paid+" "+viewModel.PaidBillCount;
                viewModel.UnPaidString = AppResources.UnPaid+" "+viewModel.UnPaidBillCount;
                viewModel.PartiallyPaidString = AppResources.Partiallynewui+" "+viewModel.PartiallyPaidBillCount;
                viewModel.TotalString = AppResources.NDTotalNumberOfBills;
                viewModel.WelcomeText = AppResources.ZZZWelcomeOnLanding;
                viewModel.Rotation = 180;
            }
            catch (Exception ex)
            {

            }
        }
        public void SetLTRDirection()
        {
            try
            {
                String langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                //InitializeComponent();
                this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.TranslateText = AppResources.ZZZSetToArabic;

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

            }

        }
        private async void TapGestureRecognizer_Tapped_Inbox(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Inbox_Tapped", "Inbox eService");
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private async void VATLookUp_Tapped(System.Object sender, System.EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATLookUp_Tapped", "VAT Registration Verification eService");
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATLookUpNewPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private async void TaxpayerCertificate_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxpayerCertificate_Tapped", "My Certificates eService");
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void VATDeregistrationDetails_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATDeregistrationDetails_Tapped", "VAT Deregistration eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage());
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void TinRegistrationDetails_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TinRegistrationDetails_Tapped", "Registration Details");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ZakatRegistrationDetailsListPageView);
            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

        }

        private async void VatRegistrationTile_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VatRegistrationTile_Tapped", "VAT Registration eService");
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        //private async void Vat_Registration_Details_Tapped(object sender, EventArgs e)
        //{
        //    await Task.Run(() =>
        //    {
        //        viewModel.IsLoading = true;

        //    });
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //         viewModel._navigationService.NavigateTo(App.VATRegistrationDisplayDetails);

        //    });

        //}

        private async void ZakatInstalmentPlan_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ZakatInstalmentPlan_Tapped", "Zakat Instalment eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);
            });

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private async void ChnageFillingPeriod_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeFillingPeriod_Tapped", "Change Filing Period eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private async void ContractRelease_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ContractRelease_Tapped", "Contract Release eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ContractReleaseListPageView);

            });
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

        }
        private async void Vat_Review_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Vat_Review_Tapped", "Objections eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ObjectionsSelectionPageView);
            });

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private async void VATRefundRequest_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }


        protected override bool OnBackButtonPressed()
        {

            //if (VATServices.IsVisible)
            //{
            //    GoBackStep();
            //}


            return true;

            //if (viewModel.MenuViewVisible)
            //{
            //    setDashBoardVisible();
            //    return true;
            //}
            //else
            //{ 
            //    return true;
            //}
        }

        private async void TaxEvasion_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxEvasion_Tapped", "Tax Evasion eService");

            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                // viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);
                viewModel._navigationService.NavigateTo(App.TaxEvasionPageWebView);

            });

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void OnVATNowTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnVATNowTapped", "VAT Registration eService");
            App.VATType = PageExecutionType.Register;
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
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

        private async void OnZakatNowTapped(object sender, EventArgs e)
        {
            await Task.Run(() => viewModel.IsLoading = true);
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnZakatNowTapped", "Establishment Registration eService");
            Device.BeginInvokeOnMainThread(() =>
            {
                if (App.LoginDataRetrieved.ZkReg=="U")
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

        private async void VATAment_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() => viewModel.IsLoading = true);

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATAmend_Tapped", "VAT Amendment eService");

            App.VATType = PageExecutionType.Amend;
            viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView);

            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void VATReactivation_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATReactivation_Tapped", "VAT Reactivation eService");
            await Task.Run(() => viewModel.IsLoading = true);
            App.VATType = PageExecutionType.Reactivation;
            Device.BeginInvokeOnMainThread(() => viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView));
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private async void VATRegistration_Details_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRegistration_Details_Tapped", "VAT Registration Details eService");
            await Task.Run(() => viewModel.IsLoading = true);
            Device.BeginInvokeOnMainThread(() => viewModel._navigationService.NavigateTo(App.VATRegistrationDisplayDetails));
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        private void OnVATServiceTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnVATServiceTapped", "VAT Services");
            viewModel._navigationService.NavigateTo(App.VATServicesPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            //ParentMenu.IsVisible = false;
            //ButtomTab.IsVisible = false;

            //VATServices.IsVisible = true;
            //MenuTitleName.Text = AppResources.NDVATServices;
        }

        private void AccountStatements_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "AccountStatements_Tapped", "Account Statements eService");
            viewModel._navigationService.NavigateTo(App.AccountStatementsPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnBackTapped(object sender, EventArgs e)
        {
            GoBackStep();
        }
        public void GoBackStep()
        {
            //if (ParentMenu.IsVisible)
            //{
            //    viewModel.MenuViewVisible = false;
            //    viewModel.HomeViewVisible = true;
            //}
            //else if (VATServices.IsVisible)
            //{
            //    SetParentMenuVisible();
            //}
        }

        public void SetParentMenuVisible()
        {
            //VATServices.IsVisible = false;
            //ParentMenu.IsVisible = true;
            //MenuTitleName.Text = AppResources.NDTaxManagement;
            //ButtomTab.IsVisible = true;
        }
    }
}