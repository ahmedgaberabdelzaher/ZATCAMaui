using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewBillViewBottomPopUpPageView : PopupPage
    {
        private VatReviewViewModel _viewModel;
        public VatReviewBillViewBottomPopUpPageView()
        {
            InitializeComponent();
            _viewModel = App.Locator.VatReviewView;
            this.BindingContext = _viewModel;
        }

        private void CloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();

        }
    }
}