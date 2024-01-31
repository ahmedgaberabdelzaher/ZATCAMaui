using ZATCAMAUI.ViewModel.NewDesignViewModel.SubmitReport;

namespace ZATCAMAUI.Views.NewDesign.SubmitReport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportSuccessPage : ContentPage
    {
        SubmitReportViewModel viewModel;
        public ReportSuccessPage()
        {
            viewModel = App.Locator.SubmitReportViewModel;
            BindingContext = viewModel;
            InitializeComponent();

        }
    }
}