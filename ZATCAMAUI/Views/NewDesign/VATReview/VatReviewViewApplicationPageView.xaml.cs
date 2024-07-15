
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{
 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewViewApplicationPageView : ContentPage
    {

        private VatReviewViewModel viewModel;

        public VatReviewViewApplicationPageView(VatReviewViewModel VatReviewviewModel)
        {
            InitializeComponent();

            
            ChangeAeroIcon();
            viewModel = VatReviewviewModel;
            this.BindingContext = viewModel;
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
    }
}