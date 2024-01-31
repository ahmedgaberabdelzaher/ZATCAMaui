using ZATCAMAUI.Models.MyReportsModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.MyReportsVM;

namespace ZATCAMAUI.Views.NewDesign.MyReports
{
    public partial class MyReportDetailsPage : ContentPage
    {
        MyReportsViewModel viewModel;
        public MyReportDetailsPage(MyReportsModel report)
        {
            try
            {
                viewModel = App.Locator.MyReportsViewModel;
                viewModel.MyReports = report ?? new MyReportsModel();
                BindingContext = viewModel;
                InitializeComponent();
            }
            catch (Exception)
            {

            }


        }
    }
}

