using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.Models.VATReviewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewBillViewBottomPopUpPageView : PopupPage
    {
        private VatReviewViewModel _viewModel;
        VATObjectionFormViewBillModel billFormModel = new VATObjectionFormViewBillModel();
        public VatReviewBillViewBottomPopUpPageView(VATObjectionFormViewBillModel _billDetails)
        {
            InitializeComponent();
            _viewModel = App.Locator.VatReviewView;
            this.BindingContext = _viewModel;
            this.billFormModel = _billDetails;

        }

        private void CloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();

        }

        public void onAppering()
        {
            DocumentNumber.Text = this.billFormModel.DocumentNumber;
            SadadNumber.Text = this.billFormModel.SadadNumber;
            DateofPenality.Text = this.billFormModel.DateofPenality;
            DescriptionOfPenality.Text = this.billFormModel.DescriptionOfPenality;
            Periodkey.Text = this.billFormModel.Periodkey;
            StartDate.Text = this.billFormModel.StartDate;
            EndDate.Text = this.billFormModel.EndDate;
            DueDate.Text = this.billFormModel.DueDate;
            Amount.Text = this.billFormModel.Amount;
        }
    }
}