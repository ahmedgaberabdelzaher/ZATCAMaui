using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreatedPageView : ContentPage
    {
        AccountCreatedPageViewModel viewModel;
        public AccountCreatedPageView()
        {
            viewModel = App.Locator.AccountCreatedPageView;
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetLTR();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void Checked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);
        }
    }
}