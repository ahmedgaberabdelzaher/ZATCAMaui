
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATIndividualSignupPageView : ContentPage
    {
        VATIndividualSignupPageViewModel viewModel;
        public VATIndividualSignupPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATIndividualSignupPageView;
            BindingContext = viewModel;
           
        }

    }
}