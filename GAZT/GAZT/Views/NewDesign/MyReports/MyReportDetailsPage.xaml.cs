using EGAZT.Models.MyReportsModel;
using EGAZT.ViewModel.NewDesignViewModel.MyReportsVM;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.MyReports
{
    public partial class MyReportDetailsPage : ContentPage
    {
        MyReportsViewModel viewModel;
        public MyReportDetailsPage(MyReportsModel report)
        {
            try
            {
                viewModel = App.Locator.myReportsViewModel;
                viewModel.MyReports = report ?? new MyReportsModel();
                BindingContext = viewModel;
                InitializeComponent();
            }
            catch (System.Exception ex)
            {

            }
           
            
        }
    }
}

