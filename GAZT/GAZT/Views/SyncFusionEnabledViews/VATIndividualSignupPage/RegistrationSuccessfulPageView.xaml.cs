using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationSuccessfulPageView : ContentPage
    {
        RegistrationSuccessfulPageViewModel viewModel;
        public RegistrationSuccessfulPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.RegistrationSuccessfulPageView;
            this.BindingContext = viewModel;
        }

        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {

        }
    }
}