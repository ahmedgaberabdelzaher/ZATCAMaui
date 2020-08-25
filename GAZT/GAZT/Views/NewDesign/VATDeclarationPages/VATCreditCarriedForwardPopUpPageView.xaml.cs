using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATCreditCarriedForwardPopUpPageView : PopupPage
    {
        VATCreditCarriedForwardPopUpPageViewModel viewModel;
        public VATCreditCarriedForwardPopUpPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATCreditCarriedForwardPopUpPageView;
            BindingContext = viewModel;
        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}