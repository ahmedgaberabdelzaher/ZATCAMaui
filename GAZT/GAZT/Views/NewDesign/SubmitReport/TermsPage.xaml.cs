using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.SubmitReport;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.SubmitReport
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

