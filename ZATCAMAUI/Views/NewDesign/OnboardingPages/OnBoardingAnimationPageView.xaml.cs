using System.Globalization;
using AppDynamics.Agent;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.OnboardingPages
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignOnBoardingAnimationPageView : ContentPage
    {
        GAZTNewDesignOnBoardingAnimationPageViewModel viewModel = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GAZTNewDesignOnBoardingAnimationPageView" /> class.
        /// </summary>
        public GAZTNewDesignOnBoardingAnimationPageView()
        {
            InitializeComponent();

            this.BindingContext = viewModel = App.Locator.GAZTNewDesignOnBoardingAnimationPageView;
            viewModel.NextButtonText = AppResources.ZZNext;

        }
        private void ChangeLangButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (App.IsArabic)
                {


                    var callTracker =Instrumentation.BeginCall("GAZTNewDesignOnBoardingAnimationPageView", "ChangeLangButton_Clicked", "Language Changed to English");
                    App.IsArabic = false;
                    App.changeFontFamily(App.appObj);
                    SetLTRDirection();


                   Instrumentation.EndCall(callTracker);
                }
                else
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignOnBoardingAnimationPageView", "ChangeLangButton_Clicked", "Language Changed to Arabic");
                    App.IsArabic = true;
                    App.changeFontFamily(App.appObj);
                    SetRTLDirection();

                    Instrumentation.EndCall(callTracker);
                }
                viewModel.NextButtonText = AppResources.ZZNext;
            }
            catch (Exception)
            {


            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (App.IsArabic)
                {
                    App.changeFontFamily(App.appObj);
                    SetRTLDirection();
                }
                else
                {
                    App.changeFontFamily(App.appObj);
                    SetLTRDirection();
                }
                viewModel.NextButtonText = AppResources.ZZNext;
            }
            catch (Exception)
            {


            }
        }

        public void SetRTLDirection()
        {
            try
            {
                string langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                this.FlowDirection = FlowDirection.RightToLeft;
                //viewModel.LanguageText = AppResources.ZZZSetToEn;
                viewModel.test();
                InitializeComponent();


            }
            catch (Exception)
            {


            }

        }
        public void SetLTRDirection()
        {
            try
            {
                string langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                this.FlowDirection = FlowDirection.LeftToRight;
                //viewModel.LanguageText = AppResources.ZZZSetToEn;

                viewModel.test();
                InitializeComponent();


            }
            catch (Exception)
            {


            }
        }
    }
}