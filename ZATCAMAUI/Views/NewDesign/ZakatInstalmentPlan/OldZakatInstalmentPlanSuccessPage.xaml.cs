using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class OldZakatInstalmentPlanSuccessPage : ContentPage
    {
        OldZakatInstalmentPlanViewModel viewModel;
        public OldZakatInstalmentPlanSuccessPage()
        {
            InitializeComponent();

            viewModel = App.Locator.OldZakatInstalmentPlanSuccessPageView;
            BindingContext = viewModel;


            viewModel.SuccessMessage = AppResources.VatInstalmentPlanSubmittedSuccess;

        }

        private async void Instalment_copy_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ZakatReferanceNumber != null)
                {


                    await Clipboard.SetTextAsync(viewModel.ZakatReferanceNumber);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        await viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);

                    }

                }
            }
            catch (Exception)
            {



            }
        }
    }

}