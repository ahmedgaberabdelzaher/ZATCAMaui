using EGAZT.ViewModel.SyncFusionEnabledViewModel.MyReturnsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage_ViewModel;
using GAZT.Models;
using Syncfusion.ListView.XForms;
using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.MyReturnsPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReturnsPageView : ContentPage
    {
        MyReturnsPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public static String ReturnPeriod = ""; 
        public MyReturnsPageView(int Index)
        {
            viewModel = App.Locator.MyReturnsPageView;
              On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            //0 On<Xamarin.Forms.PlatformConfiguration.iOS>().
            InitializeComponent();
            ParentContainerForOTP.Padding = new Thickness(0, 0, 0, 0);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            viewModel.TabIndexStatus = Index;
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            ReturnsVATSubmited.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)e.ItemData;
                viewModel.GetVATAllReturnsAsync(SelectedItem);
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsVATNonSubmited.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)e.ItemData;
                viewModel.GetVATAllReturnsAsync(SelectedItem);
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsVATOverDue.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)e.ItemData;
                viewModel.GetVATAllReturnsAsync(SelectedItem);
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsZakatSubmited.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = e.ItemData as MyReturnsResult;
                if (SelectedItem.Fbtyp.Equals("FZ12"))
                {
                    ZakatReturnListPageViewModel.ReturnPeriod = SelectedItem.Abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")) +" - " + SelectedItem.Abrzo.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")); ;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        App.IsZakatLoadingFromMyReturns = true;
                        viewModel._navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedItem.Fbguid);
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsZakatNonSubmited.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)e.ItemData;
                if (SelectedItem.Fbtyp.Equals("FZ12"))
                {
                    ZakatReturnListPageViewModel.ReturnPeriod = SelectedItem.Abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")) + " - " + SelectedItem.Abrzo.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")); ;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        App.IsZakatLoadingFromMyReturns = true;
                        viewModel._navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedItem.Fbguid);
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsZakatOverDue.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)e.ItemData;
                if (SelectedItem.Fbtyp.Equals("FZ12"))
                {
                    ZakatReturnListPageViewModel.ReturnPeriod = SelectedItem.Abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")) + " - " + SelectedItem.Abrzo.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")); ;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        App.IsZakatLoadingFromMyReturns = true;
                        viewModel._navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedItem.Fbguid);
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                    });
                }
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsETSubmited.ItemTapped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () => {
                    viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                });
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsETNonSubmited.ItemTapped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () => {
                    viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                });
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsETOverDue.ItemTapped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () => {
                    viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                });
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsWHSubmited.ItemTapped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () => {
                    viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                });
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsWHNonSubmited.ItemTapped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () => {
                    viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                });
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
            ReturnsWHOverDue.ItemTapped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () => {
                    viewModel._dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                });
                if (e.ItemData == null)
                {
                    return;
                } ((SfListView)sender).SelectedItem = null;
            };
        }
        void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            // Process changes
            var displayInfo = e.DisplayInfo;
            if (displayInfo.Orientation.Equals("Landscape"))
            {
            //    ReturnsVATSubmited.WidthRequest = displayInfo.Width - 100;// new Thickness(0, 5, 0, 0);
            //    ReturnsZakatSubmited.WidthRequest = displayInfo.Width - 100;//  new Thickness(0, 5, 0, 0);
            //    ReturnsETSubmited.WidthRequest = displayInfo.Width - 100;//  new Thickness(0, 5, 0, 0);
            //    ReturnsWHSubmited.WidthRequest = displayInfo.Width - 100;//  new Thickness(0, 5, 0, 0);
            //    ReturnsVATNonSubmited.WidthRequest = displayInfo.Width - 100;//  new Thickness(0, 5, 0, 0);
            //    ReturnsZakatNonSubmited.WidthRequest = displayInfo.Width - 100;// new Thickness(0, 5, 0, 0);
            //    ReturnsETNonSubmited.WidthRequest = displayInfo.Width - 100;// new Thickness(0, 5, 0, 0);
            //    ReturnsWHNonSubmited.WidthRequest = displayInfo.Width - 100;//  new Thickness(0, 5, 0, 0);
            //    ReturnsVATOverDue.WidthRequest = displayInfo.Width - 100;// new displayInfo.Width - 50;// Thickness(0, 5, 0, 0);
            //    ReturnsZakatOverDue.WidthRequest = displayInfo.Width - 100;// displayInfo.Width - 50;// new Thickness(0, 5, 0, 0);
            //    ReturnsETOverDue.WidthRequest = displayInfo.Width - 100;// new Thickness(0, 5, 0, 0);
            //    ReturnsETOverDue.WidthRequest = displayInfo.Width - 100; ;
            }
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.FDirection = FlowDirection.LeftToRight;
                viewModel.IsArabic = false;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.FDirection = FlowDirection.RightToLeft;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    viewModel.IsArabic = false;
                }
                else
                {
                    viewModel.IsArabic = true;
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnPageLoad();

            // var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            //// safeInsets.Left = 24;
            // safeInsets.Right = 24;
            // this.Padding = safeInsets;
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
                        ParentContainerForOTP.Padding = new Thickness(40, 0, 40, 0);
                        //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        //var safeInsets = On<iOS>().SafeAreaInsets();
                        //ReturnsVATSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsZakatSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsETSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsWHSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsVATNonSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsZakatNonSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsETNonSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsWHNonSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsVATOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsZakatOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsETOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsETOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsVATSubmitedExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsETSubmitedExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsWHSubmitedExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsVATNonSubmitedExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsZakatNonSubmitedExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsETNonSubmitedExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsWHNonSubmitedExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsVATOverDueExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsZakatOverDueExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsETOverDueExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsWHOverDueExpander.Margin = new Thickness(0, 5, 80, 0);
                        //ReturnsZakatSubmitedExpander.Margin = new Thickness(0, 5, 80, 0);
                        //BPicker.Margin = new Thickness(20, 0, 60, 0);
                        //FrmLicenseIssuedBy.Margin = new Thickness(20, 0, 80, 5);
                        //var safeInsets = On<iOS>().SafeAreaInsets();
                        //safeInsets.Left = -80;
                        //Padding = safeInsets;
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        ParentContainerForOTP.Padding = new Thickness(0, 0, 0, 0);
                        //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        //ReturnsVATSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsZakatSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsETSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsWHSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsVATNonSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsZakatNonSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsETNonSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsWHNonSubmited.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsVATOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsZakatOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsETOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsETOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ReturnsVATSubmitedExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsETSubmitedExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsWHSubmitedExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsVATNonSubmitedExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsZakatNonSubmitedExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsETNonSubmitedExpander.Margin = new Thickness(0, 5, 65, 0);
                        //ReturnsWHNonSubmitedExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsVATOverDueExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsZakatOverDueExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsETOverDueExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsWHOverDueExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ReturnsZakatSubmitedExpander.Margin = new Thickness(0, 5, 5, 0);
                        //ICRList.Margin = new Thickness(0, 5, 60, 0);
                        //ZakatICRListSubmitted.Margin = new Thickness(0, 5, 0, 0);
                        //ICRListNon.Margin = new Thickness(0, 5, 0, 0);
                        //ZakatICRListOverDue.Margin = new Thickness(0, 5, 0, 0);
                        //ZakatICRListNonSubmitted.Margin = new Thickness(0, 5, 0, 0);
                        //ICRListOver.Margin = new Thickness(0, 5, 0, 0);
                    }
                }
                //reconfigure layout
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
    }
}