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
            SetLTR();
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

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            }
            else
            {
                FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            }
        }
    }
}
