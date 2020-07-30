using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
using System.Collections.Generic;
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


                if (App.TP != null)
                    viewModel.TaxPayerProfile = App.TP;

                MenuView.IsVisible = false;
                HomeView.IsVisible = true;
            }
            catch(Exception ex)
            {

            }
        }

        #region Method

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            HomeView.IsVisible = false;
            MenuView.IsVisible = true;
            HomeIndicator.BackgroundColor = Color.White;
            MenuIndicator.BackgroundColor = Color.DarkGreen;
            Tabbar.BorderColor = Color.Transparent;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetLTR();
            App.IsComingFromSleepMode = false;

            MenuView.IsVisible = false;
            HomeView.IsVisible = true;            

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
                await viewModel.LoadDashboardData();

                viewModel.PopulateBillsInformation();
                viewModel.PopulateReturnsInformation();
                viewModel.PopualateCommittmentsInformation();
               // viewModel.PopulateeServicesApplicableToTheTaxPayer();
            }
            catch (Exception ex)
            {
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
            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = "Paid";
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }

        private void TappedOnMyReturns(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 3);
        }

        private void TappedOnSignleReturns(object sender, EventArgs e)
        {
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
            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = "Paid";
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }

        private void partiallyClicked(object sender, EventArgs e)
        {
            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = "Partial";
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }

        private void unPaidClicked(object sender, EventArgs e)
        {
            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = "Unpaid";
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }
    }
}