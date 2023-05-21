using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.VAT;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.VAT
{
    public partial class TaxCalculator : ContentPage
    {
        TaxCalculatorViewModel viewModel;
        public TaxCalculator()
        {
            viewModel = App.Locator.taxCalculatorViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (viewModel.IsConsumerCalc)
            {
                viewModel.Totaltaxablepurchases = ConsumerTotaltaxablepurchases.Text;
                viewModel.CalculateTaxCommand.Execute(null);
            }
        }
    }
}
