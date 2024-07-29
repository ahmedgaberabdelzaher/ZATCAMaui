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
            if (vATDeclaration.d != null)
            {
                viewModel.VATDeclarationData = vATDeclaration;
            }
            if (viewModel.VATDeclarationData.d != null && viewModel.VATDeclarationData.d.CFSet.results != null && viewModel.VATDeclarationData.d.ADRSet.results.Count != 0)
            {
                if (viewModel.VATDeclarationData.d.CFSet.results.Count() != 0)
                {
                    viewModel.CreditCarriedsList = viewModel.VATDeclarationData.d.CFSet.results;
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