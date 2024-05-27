using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateManagerDetailsPopUp : PopupPage

    {
        UpdateManagerViewModel viewModel;
        public UpdateManagerDetailsPopUp()
        {
            InitializeComponent();
            viewModel = App.Locator.UpdateManagerPopUp;
            this.BindingContext = viewModel;

            viewModel.LoadManagerDetails();
        }
        async void OnUpdateBtnClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
               await viewModel.PrepareDataForSubmit();
            }
            catch (Exception)
            {
                viewModel.IsLoading = false;
            }
        }
        async void OnBackArrowTapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
            catch (Exception)
            {
                viewModel.IsLoading = false;
            }
        }
        
        void SfButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void OnDiscardBtnClicked(System.Object sender, System.EventArgs e)
        {
            await viewModel.LoadManagerDetails();
        }
    }
}

