using System;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeMobileNumberPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.InternationalMobileNumber;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.ChangeMobileNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMobileNumberPageView : ContentPage
    {
        #region Variable
        private double width = 0;
        private double height = 0;
        ChangeMobileNumberPageViewModel viewModel;
        #endregion
        #region Constructor
        public ChangeMobileNumberPageView()
        {
            viewModel = App.Locator.ChangeMobileNumberPageView;
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.TxtCountryCode = "+966";
            if (Device.RuntimePlatform == Device.Android)
            {
                IntnlCodes.Margin = new Thickness(0);
                ArIntnlCodes.Margin = new Thickness(0);

            }
            else
            {
                IntnlCodes.Margin = new Thickness(5, -12, 10, -12);
                ArIntnlCodes.Margin = new Thickness(5, -12, 10, -12);
            }

            viewModel.OnPageLoad();
        }
        #endregion
        #region Method
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    this.BackgroundImageSource = "sf_LoginBackgroundLand.png";
                }
                else
                {
                    this.BackgroundImageSource = "sf_LoginBackground.png";
                    //  outerStack.Orientation = StackOrientation.Vertical;
                }
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ChangeAeroIcon();
            // Task.Delay(20000);
            viewModel.NewMobile =string.Empty;
            if (Device.RuntimePlatform == Device.Android)
            {
                IntnlCodes.Margin = new Thickness(0);
                ArIntnlCodes.Margin = new Thickness(0);

            }
            else
            {
                IntnlCodes.Margin = new Thickness(5, -12, 10, -12);
                ArIntnlCodes.Margin = new Thickness(5, -12, 10, -12);
            }

            MessagingCenter.Subscribe<InternationalMobileNumberCodePages, string>(this, "SelectedItem", (sender, arg) =>
            {
                if(App.IsArabic)
                {
                    ArIntnlCodes.Text = arg;
                }
                else
                {
                    IntnlCodes.Text = arg;
                }
           
               
                viewModel.TxtCountryCode = arg;
            });

           

        }
        private void MobileCodes_Clicked(object sender, EventArgs e)
        {

            viewModel._navigationService.NavigateTo(App.InternationalMobileNumberCodePages);


        }
        #endregion
    }
}