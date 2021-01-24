using EGAZT.Models.Form5Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatForm5
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatAcknowledgmentPageView : ContentPage
    {
        ZakatAcknowledgmentPageViewModel viewModel;

        public ZakatAcknowledgmentPageView(List<Result_9> AknowledgementList)
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatAcknowledgmentPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
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
            this.Padding = safeInsets;

            //App.IsComingFromSleepMode = false;
            //if (viewModel != null)
            //{
            //    viewModel.IsLoading = false;

            //}

            Task.Run(async () =>
            {
                await LoadData();

                //if (viewModel != null)
                //{
                //    viewModel.IsLoading = false;
                //    viewModel.NextText = AppResources.ZZNext;
                //    viewModel.setCurrentTab();
                //}
            });
        }
        private async Task LoadData()
        {
            //try
            //{
            //    //App.DisplayProgressView();
            //    await Task.Run(() =>
            //    {
            //        viewModel.IsLoading = true;
            //    });
                viewModel.LoadZakatForm5_ACK_Data();
            //    //Device.BeginInvokeOnMainThread(() => {

            //    //    ////App.HideProgressView();
            //    //    //});
            //    //    await Task.Run(() =>
            //    //    {
            //    //        viewModel.IsLoading = false;
            //    //    });

            //    //}
            //    await Task.Run(() =>
            //    {
            //        viewModel.IsLoading = false;
            //    });
            //}
            //catch (Exception ex)
            //{
            //    await Task.Run(() =>
            //    {
            //        viewModel.IsLoading = false;
            //    });
            //    // App.HideProgressView();
            //}
        }
        public async Task IntialiseAsync()
        {
            //try
            //{
             viewModel.LoadZakatForm5_ACK_Data();
            //    if (viewModel.ZakatForm5DataResult != null)
            //    {
            //        // BPicker.SelectedIndex = 14;
            //    }
            //}
            //catch (Exception e)
            //{
            //}
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
        }
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
               // Image_backArrow.Rotation = 0;
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
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
                Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg);
            }
            viewModel._navigationService.GoBack();
        }
    }
}