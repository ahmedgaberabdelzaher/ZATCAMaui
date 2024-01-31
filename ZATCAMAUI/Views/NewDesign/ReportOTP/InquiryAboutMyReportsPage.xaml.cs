using ZATCAMAUI.ViewModel.NewDesignViewModel.ReportOTPVM;

namespace ZATCAMAUI.Views.NewDesign.ReportOTP
{
    public partial class InquiryAboutMyReportsPage : ContentPage
    {
        ReportOTPViewModel viewModel;
        public InquiryAboutMyReportsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ReportOTPViewModel;
            BindingContext = viewModel;
        }
    }
}

