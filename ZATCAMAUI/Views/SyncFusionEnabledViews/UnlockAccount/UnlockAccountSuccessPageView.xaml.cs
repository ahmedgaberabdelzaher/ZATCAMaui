using System.Globalization;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.UnlockAccount
{
    public partial class UnlockAccountSuccessPageView : ContentPage
    {
        UnlockAccountSuccessPageViewModel viewModel;

        public UnlockAccountSuccessPageView(string PasswordChangedSuccessfully)
        {
            InitializeComponent();

            viewModel = App.Locator.UnlockAccountSuccessPageViewModel;
            BindingContext = viewModel;
            viewModel.PasswordChangedSuccessfully = PasswordChangedSuccessfully;
        }

        void btnLogin_Clicked(object sender, EventArgs e)
        {
            viewModel.PopToRootPage();
        }

      

    }
}
