using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.AccountStatements
{ 
    public partial class AccountStatementsFiltersPageView : PopupPage
    {
        AccountStatementsFiltersPageViewModel viewModel;
        public AccountStatementsFiltersPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.AccountStatementsFiltersPageView;
            ChangeAeroIcon();
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            viewModel.PopulateFiltersData();
        }

        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
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
