using EGAZT.ViewModel.NewDesignViewModel.OnBoardingAnimation;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.OnboardingPages
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignOnBoardingAnimationPageView
    {
        GAZTNewDesignOnBoardingAnimationPageViewModel viewModel = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GAZTNewDesignOnBoardingAnimationPageView" /> class.
        /// </summary>
        public GAZTNewDesignOnBoardingAnimationPageView()
        {
            InitializeComponent();
            
            this.BindingContext = viewModel = App.Locator.GAZTNewDesignOnBoardingAnimationPageView;
        }

        private void ChangeLangButton_Clicked(object sender, System.EventArgs e)
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
        }

        public void SetRTLDirection()
        {
            String langName = "ar-AE";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
           
            this.FlowDirection = FlowDirection.RightToLeft;
            viewModel.LanguageText = AppResources.ZZZSetToEnglish;
        }
        public void SetLTRDirection()
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            this.FlowDirection = FlowDirection.LeftToRight;

            viewModel.LanguageText = AppResources.ZZZSetToArabic;
        }
    }
}