using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.CreditCarriedPage_ViewModel;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.CreditCarried
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCarriedPageView : ContentPage
    {
        CreditCarriedPageViewModel viewModel;
        public CreditCarriedPageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                ChangeAeroIcon();
                viewModel = App.Locator.CreditCarriedPageView;
                this.BindingContext = viewModel;
                SetLTR();
                if (vATDeclaration.d!=null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;
                }
                if (viewModel.VATDeclarationData.d!=null && viewModel.VATDeclarationData.d.CFSet.results != null && viewModel.VATDeclarationData.d.ADRSet.results.Count != 0)
                {
                   if(viewModel.VATDeclarationData.d.CFSet.results.Count()!=0)
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
            catch(Exception)
            {
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}