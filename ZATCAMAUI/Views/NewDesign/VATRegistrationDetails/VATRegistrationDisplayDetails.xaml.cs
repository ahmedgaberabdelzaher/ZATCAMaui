
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATRegistrationDetails
{

    public partial class VATRegistrationDisplayDetails : ContentPage
    {
        VATRegistrationDisplayDetailsPageViewModel viewModel;
        public VATRegistrationDisplayDetails()
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationDisplayDetails;
            BindingContext = viewModel;
            Task.Run(async () =>
            {
                await GetVatRegistrationData();
            });
        }


        public async Task GetVatRegistrationData()
        {
            try
            {
                await viewModel.onPageLoad();
            }
            catch (Exception)
            {


            }
        }
    }
}
