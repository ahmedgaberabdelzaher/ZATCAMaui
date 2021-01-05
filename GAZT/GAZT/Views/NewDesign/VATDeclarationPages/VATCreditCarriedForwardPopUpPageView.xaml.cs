using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATCreditCarriedForwardPopUpPageView : PopupPage
    {
        VATCreditCarriedForwardPopUpPageViewModel viewModel;
        public VATCreditCarriedForwardPopUpPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.VATCreditCarriedForwardPopUpPageView;
            BindingContext = viewModel;
            SetLTR();
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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private async void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch(Exception ex)
            {

            }
        }
    }
}