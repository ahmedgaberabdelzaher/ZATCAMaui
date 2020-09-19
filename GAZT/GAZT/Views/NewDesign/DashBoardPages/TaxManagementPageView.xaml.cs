using EGAZT.ViewModel.NewDesignViewModel.DashBoardPageViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxManagementPageView : ContentPage
    {
        TaxManagementPageViewModel viewModel;
        public TaxManagementPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxManagementPageView;
            BindingContext = viewModel;
        }

        private void OnRealStateTapped(object sender, EventArgs e)
        {

        }

        private void OnTaxEvasionTapped(object sender, EventArgs e)
        {

        }

        private void OnFillingFrequencyTapped(object sender, EventArgs e)
        {

        }

        private void OnVATRegVerTapped(object sender, EventArgs e)
        {

        }

        private void OnReqForRullingTapped(object sender, EventArgs e)
        {

        }

        private void OnZakatTaxCertificateTapped(object sender, EventArgs e)
        {

        }

        private void OnContractReleaseTapped(object sender, EventArgs e)
        {

        }

        private void OnVATRegDetailsTapped(object sender, EventArgs e)
        {

        }
        private void OnVATCertificateTapped(object sender, EventArgs e)
        {

        }
    }
}