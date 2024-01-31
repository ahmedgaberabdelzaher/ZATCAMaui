using ZATCAMAUI.ViewModel.NewDesignViewModel.ReportOTPVM;

namespace ZATCAMAUI.Views.NewDesign.ReportOTP
{
    public partial class InquiryAboutAddOrShowReportsPage : ContentPage
    {
        ReportOTPViewModel viewModel;
        public InquiryAboutAddOrShowReportsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ReportOTPViewModel;
            BindingContext = viewModel;
        }
    }
}

