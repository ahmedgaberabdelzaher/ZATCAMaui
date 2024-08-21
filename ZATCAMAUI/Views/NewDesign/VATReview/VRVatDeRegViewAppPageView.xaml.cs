
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VRVatDeRegViewAppPageView : ContentPage
    {
        private VatReviewViewModel viewModel;

        public VRVatDeRegViewAppPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VatReviewView;
            BindingContext = viewModel;
        }

       

        private void VRVatDeRegAttachTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                viewModel.OpenAttachment(e.DataItem as Attachment);
            }
            catch (Exception)
            {


            }
        }

    }
}