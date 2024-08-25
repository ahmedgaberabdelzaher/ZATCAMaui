
using ZATCAMAUI.Models.VATRefunds;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds;

namespace ZATCAMAUI.Views.NewDesign.VATRefunds
{
   
    public partial class VATRefundsSuccessPageView : ContentPage
    {
        VATRefundsSuccessPageViewModel viewModel;
        public VATRefundsSuccessPageView(VatRefundDisplayDataModel vatRefundsSummaryData)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsSuccessPageView;
            BindingContext = viewModel;
            viewModel.VatNewReqSummaryData = vatRefundsSummaryData;
        }

     
        void btnGoToDasboard_Clicked(object sender, EventArgs e)
        {
            try
            {
                var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(firstPageToRemove);

                var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(secondPageToRemove);

                var thirdPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(thirdPageToRemove);

                viewModel._navigationService.GoBack();
            }
            catch (Exception)
            {


            }
        }

        void btnRefunds_Clicked(object sender, EventArgs e)
        {
            try
            {
                var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(firstPageToRemove);

                var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(secondPageToRemove);

                viewModel._navigationService.GoBack();
            }
            catch (Exception)
            {


            }
        }
    }
}
