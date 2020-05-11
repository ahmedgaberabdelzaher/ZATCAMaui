using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChecKTINStatus_ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.CheckTINStatus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckTINStatusPageView : ContentPage
    {
        ChecKTINStatusViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public CheckTINStatusPageView()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.CheckTINStatusPageView;
            this.BindingContext = viewModel;
            viewModel.IsAndroid = false;
            viewModel.Isios = true;
            ChangeAeroIcon();
            MainLayout.Margin = new Thickness(0, 0, 0, 5);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.OnPageLoad();
            SetLTR();
            manageDeviceVisibility();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                if (App.IsArabic)
                {
                    if (width > height)
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        MainLayout.Margin = new Thickness(40, 0, 40, 5);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        MainLayout.Margin = new Thickness(0, 0, 0, 5);
                    }
                }
                //reconfigure layout
            }
        }
        public void manageDeviceVisibility()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                viewModel.Isios = true;
                viewModel.IsAndroid = false;
            }
            else
            {
                viewModel.Isios = false;
                viewModel.IsAndroid = true;
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
        private void ListTINStatus_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            return;
        }
    }
}