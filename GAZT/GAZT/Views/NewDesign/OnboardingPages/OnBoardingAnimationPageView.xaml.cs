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
            viewModel.NextButtonText = AppResources.ZZNext;
            
        }
        private void ChangeLangButton_Clicked(object sender, System.EventArgs e)
        {
            try
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
                viewModel.NextButtonText = AppResources.ZZNext;
            }
            catch(Exception ex)
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
                    App.IsArabic = false;
                    App.changeFontFamily(App.appObj);
                    SetRTLDirection(); 
                }
                else
                {
                    App.IsArabic = true;
                    App.changeFontFamily(App.appObj);
                    SetLTRDirection();
                }
                viewModel.NextButtonText = AppResources.ZZNext;
            }
            catch (Exception ex)
            {

            }
        }

        public void SetRTLDirection()
        {
            try
            {
                viewModel.LanguageText = AppResources.ZZZSetToEnglish;
                String langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.test();
                InitializeComponent();
                

                //viewModel._navigationService.GoBack();
                //viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
            }
            catch(Exception ex)
            {

            }

        }
        public void SetLTRDirection()
        {
            try
            {
                viewModel.LanguageText = AppResources.ZZZSetToArabic;
                String langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                this.FlowDirection = FlowDirection.LeftToRight;
              
                viewModel.test();
                InitializeComponent();
               
                //viewModel._navigationService.GoBack();
                //viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
            }
            catch(Exception ex)
            {

            }
        }
    }
}