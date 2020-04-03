using GAZT;
using GAZT.Models;
using GAZTeServicesApp.ViewModels.LandingPage;
using GAZTeServicesApp.ViewModels.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
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
        public SFOptionsPageView(ComingToOptionScreenFrom comingToOption)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel = App.Locator.OptionsPageView;
            viewModel.IsComingFrom = comingToOption;
            VisibleTaxPayerProfile();
            if (App.IsArabic)
            {
                viewModel.TranslateText = AppResources.ZZZSetLanguageText;
            }
            else
            {
                viewModel.TranslateText = AppResources.ZZZSetLanguageText;
            }

            //if (Device.Idiom == TargetIdiom.Tablet)
            //{
            //    ContentLayout.HeightRequest = 900;
            //}

            SetLTR();
        }
        //public void AddTapGestures()
        //{
        //    var tap1 = new TapGestureRecognizer();
        //    var tap2 = new TapGestureRecognizer();
        //    tap1.Tapped += (s, e) => LanguageChanged();
        //    tap2.Tapped += (s, e) => LanguageChanged();
        //    lblLanguage1.GestureRecognizers.Add(tap1);
        //    lblLanguage2.GestureRecognizers.Add(tap2);
        //}
        public void VisibleTaxPayerProfile()
        {
            if (viewModel.IsComingFrom == ComingToOptionScreenFrom.IsAnonymousPage)
            {
                viewModel.IsTaxPayerProfileVisible = false;
            }
            else
            {
                viewModel.IsTaxPayerProfileVisible = true;
            }
            InitializeComponent();
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
      
        public void SetRTLDirection()
        {
            String langName = "ar-AE";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            InitializeComponent();
            this.FlowDirection = FlowDirection.RightToLeft;
            viewModel.TranslateText = AppResources.ZZZSetToEnglish;
        }
        public void SetLTRDirection()
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            InitializeComponent();
            this.FlowDirection = FlowDirection.LeftToRight;
            viewModel.TranslateText = AppResources.ZZZSetToArabic;
        }

        //private void LanguageChanged()
        //{
        //    if (App.IsArabic)
        //    {

        //        App.IsArabic = false;
        //        App.changeFontFamily(App.appObj);
        //        SetLTRDirection();

        //    }
        //    else
        //    {

        //        App.IsArabic = true;
        //        App.changeFontFamily(App.appObj);
        //        SetRTLDirection();

        //    }
        //}

        private void LanguageClicked(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {

                App.IsArabic = false;
                App.changeFontFamily(App.appObj);
                SetLTRDirection();

            }
            else
            {

                App.IsArabic = true;
                App.changeFontFamily(App.appObj);
                SetRTLDirection();

            }

            if (viewModel.IsComingFrom == ComingToOptionScreenFrom.IsDashboardPage)
            {
                viewModel._navigationService.NavigateTo(App.SFLandingPageView);
            }
            else
            {
                viewModel._navigationService.NavigateTo(App.SFAnonymousLandingPageView);
            }
        }
    }
}