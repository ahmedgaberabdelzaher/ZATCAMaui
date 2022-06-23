using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages
{
    public partial class ReportFinancialViolation : ContentPage
    {
        ReportFinancialViolationViewModel viewModel;
        public ReportFinancialViolation()
        {
            viewModel = App.Locator.reportFinancialViolationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
