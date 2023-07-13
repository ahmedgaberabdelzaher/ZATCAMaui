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
            viewModel.PaymentCode = paymentCode;
            BindingContext = viewModel;

            InitializeComponent();


        }

        void PaymentView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            if (e.Url.Contains("EDeclarationPages/EDeclarationStartPage.aspx"))
            {
               viewModel._navigationService.NavigateTo("/Home", "0");

            }
        }
    }
}

