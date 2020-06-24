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
    public partial class VATRegistrationSuccessfullPageView : ContentPage
    {
        VATRegistrationSuccessfullPageViewModel viewModel;
        public VATRegistrationSuccessfullPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationSuccessfullPageView;
            this.BindingContext = viewModel;
        }

        private void btnDashboard_Clicked(object sender, EventArgs e)
        {

        }
    }
}