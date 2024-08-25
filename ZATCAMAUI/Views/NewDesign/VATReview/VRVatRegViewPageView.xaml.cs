
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VRVatRegViewPageView : ContentPage
    {
        private VatReviewViewModel viewModel;
        public VRVatRegViewPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VatReviewView;
            BindingContext = viewModel;
        }

    

    }
}