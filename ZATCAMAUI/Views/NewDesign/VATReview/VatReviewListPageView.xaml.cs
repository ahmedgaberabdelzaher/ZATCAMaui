using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class VatReviewListPageView : ContentPage
    {
        private VatReviewListViewModel _viewModel;

        public VatReviewListPageView()
        {
            InitializeComponent();


            _viewModel = App.Locator.VatReviewListView;

            BindingContext = _viewModel;

        }

      

    }
}