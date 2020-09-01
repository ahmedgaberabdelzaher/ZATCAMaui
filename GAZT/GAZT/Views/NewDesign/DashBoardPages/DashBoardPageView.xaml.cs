using EGAZT.ViewModel.NewDesignViewModel;
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

                viewModel = App.Locator.GAZTNewDesignDashBoardPageView;
                this.BindingContext = viewModel;
                SetLTR();
                if (viewModel != null)
                {
                    viewModel.MenuViewVisible = false;
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
            App.IsComingFromSleepMode = false;
            if (viewModel != null)
            {
                viewModel.IsLoading = false;
               // viewModel.MenuViewVisible = false;
               // viewModel.HomeViewVisible = true;
                //viewModel.StackMenuColor = Color.White;
                //viewModel.TabbarColor = Color.DarkGray;
                //viewModel.HomeIndicatorColor= Color.DarkGreen;
            }

            Task.Run(async () =>
            {
                await LoadData();

                if (viewModel != null)
                    viewModel.IsLoading = false;
            });
            try
            {
                if (App.LoginDataRetrieved.VtReg == null)
                { 
                    viewModel.IsVatRegistrationTileVisible = true;
                }
                else if (App.LoginDataRetrieved.VtReg != "X")
                {
                    viewModel.IsVatRegistrationTileVisible = true;
                }

                if (App.LoginDataRetrieved.ZkReg == null)
                {
                    viewModel.IsEstablishmentRegistrationTileVisible = true;
                }
                else if (App.LoginDataRetrieved.ZkReg != "X")
                {
                    viewModel.IsEstablishmentRegistrationTileVisible = true;
                }

            }
            catch
            {
                viewModel.IsVatRegistrationTileVisible = true;
            }
            ChangeArrowDirection();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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
                Device.BeginInvokeOnMainThread( () =>
                {
                    viewModel.BillCount = string.Empty;
                    viewModel.BillsAndReturnsCommitments = null;

                    viewModel.PopulateBillsInformation();
                    viewModel.PopulateReturnsInformation();
                    viewModel.PopualateCommittmentsInformation();
                  
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

        public void setDashBoardVisible()
        {
            viewModel.MenuViewVisible = false;
            viewModel.HomeViewVisible = true;
            viewModel.HomeIndicatorColor = Color.DarkGreen;
            viewModel.MenuIndicatorColor = Color.White;
            viewModel.TabbarColor = Color.DarkGray;
            viewModel.StackMenuColor = Color.White;
        }
        private void OnHomeTapped(object sender, EventArgs e)
        {
            viewModel.MenuViewVisible = false;
            viewModel.HomeViewVisible = true;
            viewModel.HomeIndicatorColor = Color.DarkGreen;
            viewModel.MenuIndicatorColor = Color.White;
            viewModel.TabbarColor = Color.DarkGray;
            viewModel.StackMenuColor = Color.White;

            //MenuView.IsVisible = false;
            //HomeView.IsVisible = true;
            //HomeIndicator.BackgroundColor = Color.DarkGreen;
            //MenuIndicator.BackgroundColor = Color.White;
            //Tabbar.BorderColor = Color.DarkGray;
            //stackMenu.BackgroundColor = Color.White;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!viewModel.IsVatRegistrationTileVisible)
            { 
            viewModel.MenuViewVisible = true;
            viewModel.HomeViewVisible = false;
            viewModel.HomeIndicatorColor = Color.White;

            viewModel.MenuIndicatorColor = Color.DarkGreen;
            viewModel.StackMenuColor = Color.Transparent;
            viewModel.TabbarColor = Color.Transparent;
                //HomeView.IsVisible = false;
                //MenuView.IsVisible = true;
                //HomeIndicator.BackgroundColor = Color.White;
                //MenuIndicator.BackgroundColor = Color.DarkGreen;
                //stackMenu.BackgroundColor=Tabbar.BorderColor = Color.Transparent;
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

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

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

                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 3);

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


                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.Submitted + " from Dashboard");
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

                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.UnSubmitted + " from Dashboard");
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
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", AppResources.OverDue + " from Dashboard");
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

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.Paid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

            });

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

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.PartiallyPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

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


                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.UnPaid;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

            });
        }
        private async void Logout_Tapped(System.Object sender, System.EventArgs e)
        {
            //    App.DisplayProgressView();

            if (App.IsArabic)
            {
                var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZNo, AppResources.ZYes);
                if (!result)
                {
                    App.TP = null;
                    await viewModel.LogOut();
                }
            }
            else
            {
                var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
                if (result)
                {
                    App.TP = null;
                    await viewModel.LogOut();
                }
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

                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

            });

        }
        private async void Label_MyRetuns_Tapped(object sender, EventArgs e)
        {
            //       App.DisplayProgressView();   await Task.Run(() =>



            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 4);

            });
        }
        private void Label_MyProfile_Tapped(object sender, EventArgs e)
        {
            //         App.DisplayProgressView();

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayerProfilePageView);
            });
        }
        private void Aboutus_Tapped(object sender, EventArgs e)
        {
            //       App.DisplayProgressView();
            viewModel._navigationService.NavigateTo(App.AboutUsPageView);
        }
        private void Contactus_Tapped(object sender, EventArgs e)
        {
            //      App.DisplayProgressView();
            viewModel._navigationService.NavigateTo(App.ContactUsPageView);
        }
        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            //        App.DisplayProgressView();
             viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);

        }
        private void ChangeLanguage_Tapped(object sender, EventArgs e)
        {
            try
            {
                //        App.DisplayProgressView();
                if (App.IsArabic)
                {
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
                }
                else
                {
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

                viewModel.PaidString = AppResources.Paid;
                viewModel.UnPaidString = AppResources.UnPaid;
                viewModel.PartiallyPaidString = AppResources.Partiallynewui;
                viewModel.TotalString = AppResources.NDTotal;
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

                viewModel.PaidString = AppResources.Paid;
                viewModel.UnPaidString = AppResources.UnPaid;
                viewModel.PartiallyPaidString = AppResources.Partiallynewui;
                viewModel.TotalString = AppResources.NDTotal;
                viewModel.WelcomeText = AppResources.ZZZWelcomeOnLanding;
                viewModel.Rotation = 0;
            }
            catch (Exception ex)
            {

            }

        }
        private async void TapGestureRecognizer_Tapped_Inbox(object sender, EventArgs e)
        {
            //   App.DisplayProgressView();
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);

            });
           
        }
        private async void VATLookUp_Tapped(System.Object sender, System.EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATLookUpNewPageView);

            });

        }
        private async void TaxpayerCertificate_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

            });

        }

        private async void VATDeregistrationDetails_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});
            Device.BeginInvokeOnMainThread(() =>
            {
                PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage());
            });
        }

        private async void TinRegistrationDetails_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ZakatRegistrationDetailsListPageView);
            });
        }

        private async  void VatRegistrationTile_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);

            });
        }

        private async void ZakatInstalmentPlan_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);

            });
        }
        private async void ChnageFillingPeriod_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);

            });
        }
        private async void ContractRelease_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.ContractReleaseListPageView);

            });
        }

        private async void VATRefundRequest_Tapped(object sender, EventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsListPageView);
            });
        }
        protected override bool OnBackButtonPressed()
        {
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

        private async  void TaxEvasion_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);

            });
        }

        private async  void OnVATNowTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationPageView);

            });
           
        }

        private void OnSupportTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SupportPageView);
        }

        private void OnZakatNowTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.EstablishmentRegistrationPage);
        }
    }
}