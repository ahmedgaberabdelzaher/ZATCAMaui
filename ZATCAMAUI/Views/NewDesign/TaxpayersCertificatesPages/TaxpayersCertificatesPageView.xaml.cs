using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.TaxpayersCertificatesPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayersCertificatesPageView : ContentPage
    {
        TaxpayersCertificatesPageViewModel viewModel;
        public TaxpayersCertificatesPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayersCertificatesPageView;
            BindingContext = viewModel;
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            TaxTypePicker.IsOpen = true;
        }

        private void TaxTypePicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                //TODO
                ReturnTypes selectedReturntype = viewModel.TaxTypeForFilter[e.NewValue];
                viewModel.SelectedTaxTypeForFilter = selectedReturntype;

            }
            catch (Exception)
            {
            }

        }

    }
}