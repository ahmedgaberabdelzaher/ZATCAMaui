using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATCreditCarriedForwardPopUpPageView : PopupPage
    {
        VATCreditCarriedForwardPopUpPageViewModel viewModel;
        public VATCreditCarriedForwardPopUpPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            viewModel = App.Locator.VATCreditCarriedForwardPopUpPageView;
            BindingContext = viewModel;
            if (vATDeclaration.data != null)
            {
                viewModel.VATDeclarationData = vATDeclaration;
            }
            if (viewModel.VATDeclarationData.data != null && viewModel.VATDeclarationData.data.CFSet != null && viewModel.VATDeclarationData.data.ADRSet.Count != 0)
            {
                if (viewModel.VATDeclarationData.data.CFSet.Count() != 0)
                {
                    viewModel.CreditCarriedsList = viewModel.VATDeclarationData.data.CFSet;
                    viewModel.IsListViewVisible = true;
                    viewModel.IsNoDataLabelVisible = false;
                }
                else
                {
                    viewModel.IsListViewVisible = false;
                    viewModel.IsNoDataLabelVisible = true;
                }
            }
        }
        private async void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                await MopupService.Instance.PopAsync();
            }
            catch(Exception)
            {
                
            }
        }
    }
}