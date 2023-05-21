using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.ReportOTPVM;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.ReportOTP
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

