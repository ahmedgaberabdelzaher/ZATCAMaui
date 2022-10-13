using System;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.SubmitReport;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.SubmitReport
{
    public partial class SubmitReportPage : ContentPage
    {
        SubmitReportViewModel viewModel;
        public SubmitReportPage()
        {
            InitializeComponent();
            viewModel = App.Locator.SubmitReportViewModel;
            BindingContext = viewModel;
        }
    }
}

