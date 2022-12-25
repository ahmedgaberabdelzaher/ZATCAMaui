using System;
using System.Collections.Generic;
using EGAZT.AppConfigurations;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.Common
{
    public partial class PaymentWebView : BaseContentPage
    {
        CustomsPaymentViewModel viewModel;
        public PaymentWebView(string paymentCode)
        {
            viewModel = App.Locator.CustomsPaymentViewModel;
            BindingContext = viewModel;
            InitializeComponent();
            PaymentView.Source = PageSettings.GetCustomsPaymentUrl() + paymentCode;

        }
    }
}

