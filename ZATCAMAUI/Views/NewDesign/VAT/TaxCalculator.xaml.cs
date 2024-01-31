using ZATCAMAUI.ViewModel.NewDesignViewModel.VAT;

namespace ZATCAMAUI.Views.NewDesign.VAT
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

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (viewModel.IsConsumerCalc)
            {
                viewModel.Totaltaxablepurchases = ConsumerTotaltaxablepurchases.Text;
                viewModel.CalculateTaxCommand.Execute(null);
            }
        }
    }
}
