using EGAZT.ViewModel.NewDesignViewModel.OnBoardingAnimation;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.OnboardingPages
{
    [Preserve(AllMembers = true)]
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    
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
            viewModel.NextButtonText = AppResources.ZZNext;
            
        }
        private void ChangeLangButton_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (App.IsArabic)   
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignOnBoardingAnimationPageView", "ChangeLangButton_Clicked", "Language Changed to English");
                    App.IsArabic = false;
                    App.changeFontFamily(App.appObj);
                    SetLTRDirection();
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignOnBoardingAnimationPageView", "ChangeLangButton_Clicked", "Language Changed to Arabic");
                    App.IsArabic = true;
                    App.changeFontFamily(App.appObj);
                    SetRTLDirection();
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                viewModel.NextButtonText = AppResources.ZZNext;
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void SetRTLDirection()
        {
            try
            {
                String langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.LanguageText = AppResources.ZZZSetToEn;
                viewModel.test();
                InitializeComponent();
                

            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }
        public void SetLTRDirection()
        {
            try
            {
                String langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.LanguageText = AppResources.ZZZSetToEn;

                viewModel.test();
                InitializeComponent();
               
              
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
    }
}