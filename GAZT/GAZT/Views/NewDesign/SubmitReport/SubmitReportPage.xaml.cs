using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.SubmitReport;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.SubmitReport
{
    public partial class SubmitReportPage : ContentPage
    {
        SubmitReportViewModel viewModel;
        public SubmitReportPage()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.submitReportViewModel;
                BindingContext = viewModel;
            }
            catch(Exception ex)
            {

            }
            

        }
    }
}

