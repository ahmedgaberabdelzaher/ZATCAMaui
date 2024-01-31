using ZATCAMAUI.ViewModel.NewDesignViewModel.ReportOTPVM;

namespace ZATCAMAUI.Views.NewDesign.ReportOTP
{
    public partial class ReportOTPPage : ContentPage
    {
        ReportOTPViewModel viewModel;
        public ReportOTPPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ReportOTPViewModel;
            BindingContext = viewModel;
        }
    }
}

