
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.CreditCarriedPage;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Application = Microsoft.Maui.Controls.Application;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.CreditCarriedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCarriedPageView : ContentPage
    {
        CreditCarriedPageViewModel viewModel;
        public CreditCarriedPageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.CreditCarriedPageView;
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
            catch (Exception)
            {
            }
        }
        
    }
}