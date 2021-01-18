using System;
using System.Collections.ObjectModel;
using System.Text;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeMobileNumberPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using EGAZT.Views.SyncFusionEnabledViews.InternationalMobileNumber;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.ChangeMobileNumber
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMobileNumberPageView : ContentPage
    {

        #region Variable
        private double width = 0;
        private double height = 0;
        ChangeMobileNumberPageViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

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
                ArIntnlCodes.Margin = new Thickness(15, -12, 10, -12);
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
                ArIntnlCodes.Margin = new Thickness(15, -12, 10, -12);
            }

            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedItem", (sender, arg) =>
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
            try
            {
                mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }
        private void MobileCodes_Clicked(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PushAsync(new InternationalCodeSearchPage(mobileData));

        }

        private void EntryMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {
            StringBuilder Message = new StringBuilder();
            PopUp popUp = new PopUp();
            if (!string.IsNullOrEmpty(MobileNumber.Text))
            {
               
                if (MobileNumber.Text.Substring(0, 1) == "0")
                {
                    Message.Append(AppResources.ZZMobilenumberCannotStartWith0);
                }
                if (MobileNumber.Text.Length < 9)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);
                    }
                    Message.Append(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
                }
                if (Message.Length > 0)
                {
                    popUp.Message = Message.ToString();
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    NewMobileNumberLayout.HasError = true;
                    MobileNumber.Text = string.Empty;
                }
                else
                {
                    NewMobileNumberLayout.HasError = false;
                }
            }
            else
            {
                Message.Append(AppResources.EnterValidMobileNumber);
                popUp.Message = Message.ToString();
                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

            }
        }

        #endregion
    }
}