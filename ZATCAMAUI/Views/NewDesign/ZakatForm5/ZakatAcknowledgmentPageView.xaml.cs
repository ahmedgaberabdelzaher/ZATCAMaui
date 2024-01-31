using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using ZATCAMAUI.Models.Form5Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ZakatForm5
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatAcknowledgmentPageView : ContentPage
    {
        ZakatAcknowledgmentPageViewModel viewModel;

        public ZakatAcknowledgmentPageView(List<Result_9> AknowledgementList)
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatAcknowledgmentPageView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            viewModel.AknowledgementDataList = AknowledgementList;
            ChangeAeroIcon();
            SetLTR();
            IntialiseAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;


            Task.Run(() =>
            {
                LoadData();

            });
        }
        private void LoadData()
        {
            
            viewModel.LoadZakatForm5_ACK_Data();
            
        }
        public void IntialiseAsync()
        {
           
            viewModel.LoadZakatForm5_ACK_Data();
           
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
            }
        }
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
                // Image_backArrow.Rotation = 0;
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
                //  Image_backArrow.Rotation = 180;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void OnFinishedTapped(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
                Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg);
            }
            viewModel._navigationService.GoBack();
        }
    }
}