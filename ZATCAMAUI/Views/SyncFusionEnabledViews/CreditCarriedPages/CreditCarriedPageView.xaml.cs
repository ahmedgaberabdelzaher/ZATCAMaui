
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
                On<iOS>().SetUseSafeArea(true);
                ChangeAeroIcon();
                viewModel = App.Locator.CreditCarriedPageView;
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
            catch (Exception)
            {
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
    }
}