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
            // PaymentView.Source = PageSettings.GetCustomsPaymentUrl() + "9c602810-f4d5-4838-8103-0ad1323cfcc4";
            var lang = App.IsArabic ? "ar" : "en";
         // PaymentView.Source = PageSettings.GetCustomsPaymentUrl() + "9cf1e0f2-e49f-4447-8a65-267797ce1720"+ "?local="+lang;

        }
    }
}

