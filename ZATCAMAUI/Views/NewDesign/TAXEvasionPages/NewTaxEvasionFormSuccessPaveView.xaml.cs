using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel;

namespace ZATCAMAUI.Views.NewDesign.TAXEvasionPages
{
    public partial class NewTaxEvasionFormSuccessPaveView : ContentPage
    {
        NewTaxEvasionFormPageViewModel viewModel;
        public NewTaxEvasionFormSuccessPaveView()
        {
            InitializeComponent();
            viewModel = App.Locator.NewTaxEvasionFormSuccessPaveView;
            BindingContext = viewModel;
            SetLTR();

            if (App.TP != null)
            {
                if (!string.IsNullOrEmpty(App.TP.Tin))
                {
                    btnDashboard.Text = AppResources.ZZZZGotoDashboard;
                }
                else
                {
                    btnDashboard.Text = AppResources.NDBacktoLoginnew;
                }
            }
            else
                btnDashboard.Text = AppResources.NDBacktoLoginnew;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override bool OnBackButtonPressed() => true;


        private void btnDashboard_Clicked(object sender, EventArgs e)
        {
            if (App.TP != null)
            {
                if (!string.IsNullOrEmpty(App.TP.Tin))
                {
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                }
                else
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                }
            }
            else
                viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
        }

        private void OnGotoReportPageClicked(object sender, EventArgs e)
        {
            try
            {
                if (Navigation.NavigationStack.Count > 0)
                {
                    Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    Navigation.RemovePage(pg);
                }
                viewModel._navigationService.GoBack();
            }
            catch (Exception)
            {


            }

        }


    }
}
