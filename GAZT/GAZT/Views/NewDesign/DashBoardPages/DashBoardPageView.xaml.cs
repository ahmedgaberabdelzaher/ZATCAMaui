using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
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
                if (viewModel != null)
                {
                    viewModel.MenuViewVisible = false;
                    viewModel.HomeViewVisible = true;
                }

                if (App.TP != null)
                    viewModel.TaxPayerProfile = App.TP;

                if (App.IsArabic)
                {
                    // viewModel.TranslateText = AppResources.ZZZSetLanguageText;
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
            catch(Exception ex)
            {

            }
        }

        #region Method

        private void OnHomeTapped(object sender, EventArgs e)
        {
            viewModel.MenuViewVisible = false;
            viewModel.HomeViewVisible = true;
            viewModel.HomeIndicatorColor = Color.DarkGreen;
            viewModel.MenuIndicatorColor = Color.White;
            viewModel.TabbarColor = Color.DarkGray;
            viewModel.StackMenuColor= Color.White;

            //MenuView.IsVisible = false;
            //HomeView.IsVisible = true;
            //HomeIndicator.BackgroundColor = Color.DarkGreen;
            //MenuIndicator.BackgroundColor = Color.White;
            //Tabbar.BorderColor = Color.DarkGray;
            //stackMenu.BackgroundColor = Color.White;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.IsComingFromSleepMode = false;
            if (viewModel != null)
            {
                viewModel.MenuViewVisible = false;
                viewModel.HomeViewVisible = true;
            }

            Task.Run(async () =>
            {
                await LoadData();
                viewModel.IsLoading = false;
            });
        }
        private async Task LoadData()
        {
            try
            {
                App.DisplayProgressView();
                await viewModel.LoadDashboardData();
                Device.BeginInvokeOnMainThread(() => {
                    viewModel.BillCount = string.Empty;
                    viewModel.BillsAndReturnsCommitments = null;
                  
                    viewModel.PopulateBillsInformation();
                    viewModel.PopulateReturnsInformation();
                    viewModel.PopualateCommittmentsInformation();
                    App.HideProgressView();
                });

               // viewModel.PopulateeServicesApplicableToTheTaxPayer();
            }
            catch (Exception ex)
            {
                App.HideProgressView();
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

        private void TappedOnMyBills(object sender, EventArgs e)
        {
           // App.DisplayProgressView();
            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = AppResources.All;
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);


        }

        private void TappedOnMyReturns(object sender, EventArgs e)
        {
            //App.DisplayProgressView();
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 3);
        }

   
        private void TappedOnSignleReturns(object sender, EventArgs e)
        {
           // App.DisplayProgressView();

            string controltype = sender.GetType().ToString();

                Syncfusion.XForms.Cards.SfCardView arrowImage = sender as Syncfusion.XForms.Cards.SfCardView;
            ReturnTypeAndCorrepsondingCount BModel = (ReturnTypeAndCorrepsondingCount)arrowImage.BindingContext;
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

        private void paidClicked(object sender, EventArgs e)
        {
           // App.DisplayProgressView();

            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = AppResources.Paid;
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }

        private void partiallyClicked(object sender, EventArgs e)
        {
          //  App.DisplayProgressView();

            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = AppResources.PartiallyPaid;
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }

        private void unPaidClicked(object sender, EventArgs e)
        {
         //   App.DisplayProgressView();

            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = AppResources.UnPaid;
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
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

        private void Label_MyBills(object sender, EventArgs e)
        {
       //     App.DisplayProgressView();

            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = AppResources.All;
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }

        private void Label_MyRetuns_Tapped(object sender, EventArgs e)
        {
     //       App.DisplayProgressView();

            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 4);
        }
        private void Label_MyProfile_Tapped(object sender, EventArgs e)
        {
   //         App.DisplayProgressView();
            viewModel._navigationService.NavigateTo(App.TaxPayerProfilePageView);
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
    //        App.DisplayProgressView();
            if (App.IsArabic)
            {
                App.IsArabic = false;
                App.changeFontFamily(App.appObj);
                SetLTRDirection();
            }
            else
            {
                App.IsArabic = true;
                App.changeFontFamily(App.appObj);
                SetRTLDirection();
            }

            OnAppearing();
        }

        public void SetRTLDirection()
        {
            String langName = "ar-AE";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
           // InitializeComponent();
            this.FlowDirection = FlowDirection.RightToLeft;
            viewModel.TranslateText = AppResources.ZZZSetToEnglish;

            viewModel.NextCommitmentsString = AppResources.ZZZZNextCommitments;
            viewModel.BillString = AppResources.Bills;
            viewModel.ReturnString = AppResources.NDReturns;

            viewModel.PaidString = AppResources.Paid;
            viewModel.UnPaidString = AppResources.UnPaid;
            viewModel.PartiallyPaidString = AppResources.PartiallyPaid;
            viewModel.TotalString = AppResources.NDTotal;
        }
        public void SetLTRDirection()
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            //InitializeComponent();
            this.FlowDirection = FlowDirection.LeftToRight;
            viewModel.TranslateText = AppResources.ZZZSetToArabic;

            viewModel.NextCommitmentsString = AppResources.ZZZZNextCommitments;
            viewModel.BillString = AppResources.Bills;
            viewModel.ReturnString = AppResources.NDReturns;

            viewModel.PaidString = AppResources.Paid;
            viewModel.UnPaidString = AppResources.UnPaid;
            viewModel.PartiallyPaidString = AppResources.PartiallyPaid;
            viewModel.TotalString = AppResources.NDTotal;
            
        }

        private void TapGestureRecognizer_Tapped_Inbox(object sender, EventArgs e)
        {
         //   App.DisplayProgressView();
            viewModel._navigationService.NavigateTo(App.CorrespondancePageView);
        }
    }
}