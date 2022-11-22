using EGAZT.ViewModel.NewDesignViewModel.SubmitReport;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.SubmitReport
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