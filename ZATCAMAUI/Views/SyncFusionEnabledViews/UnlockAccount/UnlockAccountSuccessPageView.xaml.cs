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
            ChangeAeroIcon();
        }

        void btnLogin_Clicked(object sender, EventArgs e)
        {
            viewModel.PopToRootPage();
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
