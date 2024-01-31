using ZATCAMAUI.ViewModel.NewDesignViewModel.SubmitReport;

namespace ZATCAMAUI.Views.NewDesign.SubmitReport
{
    public partial class TermsPage : ContentPage
    {
        SubmitReportViewModel viewModel;
        public TermsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.SubmitReportViewModel;
            BindingContext = viewModel;
        }
    }
}

