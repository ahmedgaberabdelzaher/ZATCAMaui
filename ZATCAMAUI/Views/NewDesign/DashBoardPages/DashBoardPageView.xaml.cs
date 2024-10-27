using AppDynamics.Agent;
using Syncfusion.Maui.Picker;
using Application = Microsoft.Maui.Controls.Application;
using ScrollView = Microsoft.Maui.Controls.ScrollView;
using ZATCAMAUI.ViewModel.NewDesignViewModel.DashBoardPageViewModel;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignDashBoardPageView : ContentPage
    {
        #region Variable

        GAZTNewDesignDashBoardPageViewModel viewModel;


        #endregion
      
        public GAZTNewDesignDashBoardPageView(bool isMenu = false)
        {
            try
            {
                InitializeComponent();


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
                    viewModel.VatProfitGoodsTileTxt = AppResources.VATProfitDeregisterTile;
                }
                else
                {
                    viewModel.VatProfitGoodsTileTxt = AppResources.ZProfitgoodsSCSRTile;
                }

                //ends 


                MessagingCenter.Subscribe<object>(this, "HideProfitGoods", async (sender) =>
                {

                    viewModel.istileUpdated = false;

                   await viewModel.OnDataLoad();
                });
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


            }
            catch (Exception)
            {


            }
        }


        #region Method

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.isFirstTime)
            {

                await frameToolbar.FadeTo(0, 0);
                await btn_frameToolbar.FadeTo(1, 0);
                viewModel.isFirstTime = false;
            }
        }

        private void btnCommitmentsPickerClicked(object sender, TappedEventArgs e)
        {
            CommitmentsPicker.IsOpen = true;
        }

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


            viewModel.isTimerOff = true;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        void CommitmentsPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            viewModel.SelectedCommitmentFilterLabelValue = viewModel.CommitmentsListFilter[e.NewValue];
            viewModel.SelectedCommitmentFilterValue = viewModel.CommitmentsListFilter[e.NewValue];

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
            }
            else
            {
                await frameToolbar.FadeTo(0, 100);
                await btn_frameToolbar.FadeTo(1, 100);
                await btn_frameToolbar.TranslateTo(scrollView.X, scrollView.Y, 100);
                viewModel.IsMenuLogoVisible = true;
            }
        }

        #endregion

    }
}
