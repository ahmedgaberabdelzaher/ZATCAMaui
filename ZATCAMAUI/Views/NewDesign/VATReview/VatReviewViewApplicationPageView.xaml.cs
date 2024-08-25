
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{
 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewViewApplicationPageView : ContentPage
    {

        private VatReviewViewModel viewModel;

        public VatReviewViewApplicationPageView(VatReviewViewModel VatReviewviewModel)
        {
            InitializeComponent();
            viewModel = VatReviewviewModel;
            this.BindingContext = viewModel;
        }

    }
}