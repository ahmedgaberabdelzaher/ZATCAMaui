using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages
{

    public interface IBaseUrl { string Get(); }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerCorrespondanceDetailPageView : ContentPage
    {
        TaxpayerCorrespondanceDetailPageViewModel viewModel;
        public TaxpayerCorrespondanceDetailPageView(CorrespondanceModel CorrModel)
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayerCorrespondanceDetailPageView;
            BindingContext = viewModel;
            viewModel.CorrModel = CorrModel;
        }


      
    }
}