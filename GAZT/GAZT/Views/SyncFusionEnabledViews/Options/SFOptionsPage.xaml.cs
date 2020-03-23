using GAZT;
using GAZTeServicesApp.ViewModels.LandingPage;
using GAZTeServicesApp.ViewModels.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace GAZTeServicesApp.Views.Options
{
    /// <summary>
    /// Page to show the setting.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFOptionsPageView : ContentPage
    {
        SFOptionsPageViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPageView" /> class.
        /// </summary>
        public SFOptionsPageView()
        {
            InitializeComponent();
            this.BindingContext = viewModel = App.Locator.OptionsPageView;
            SetLTR();
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private async void OnLogoutClicked(Object sender, EventArgs e)
        {
            var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
            if (result)
            {
                App.TP = null;
                viewModel.LogOut();
            }
        }
    }
}