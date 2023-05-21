using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.ReportOTPVM;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.ReportOTP
{
    public partial class InquiryAboutAddOrShowReportsPage : ContentPage
    {
        ReportOTPViewModel viewModel;
        public InquiryAboutAddOrShowReportsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ReportOTPViewModel;
            BindingContext = viewModel;
        }
    }
}

