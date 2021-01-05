using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.MyBills_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Syncfusion.DataSource.Extensions;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.MyBillsView
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBillsView : ContentPage
    {
        MyBillsViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public MyBillsView(BillInfo billInfo = null)
        {
            //Resources["searchBarStyleForAll"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            //Resources["searchBarStyleForPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            //Resources["searchBarStyleForUnPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            //Resources["searchBarStyleForPartiallyPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            InitializeComponent();
            ParentContainer.Margin = new Thickness(0, 0, 0, 5);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            ChangeAeroIcon();
            viewModel = App.Locator.MyBillsView;
            try
            {
                SetLTR();
                this.BindingContext = viewModel;
                GetBillsReturnsAsync(billInfo);
            }
            catch (Exception ex)
            {
            }
            Bills.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };
            BillsPaid.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };
            BillsPartiallyPaid.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };
            BillsUnPaid.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };
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
                        ParentContainer.Margin = new Thickness(40, 0, 40, 5);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        ParentContainer.Margin = new Thickness(0, 0, 0, 5);
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
        public void GetBillsReturnsAsync(BillInfo billInfo)
        {
            //Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;
            //});
             viewModel.onPageLoad(billInfo);
            //Task.Run(() =>
            // {
            //     viewModel.IsLoading = false;
            // });
        }
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float radius = Math.Min(info.Width, info.Height) / 4;
            SKPath path = new SKPath
            {
                FillType = SKPathFillType.EvenOdd,
            };
            float a = center.X - radius / 2;
            float b = center.Y - radius / 2;
            float r = radius;
            float DeviceWidth = info.Width;
            float deviceHeight = info.Height;
            float XPoint = DeviceWidth / 2;
            float YPoint;
            if (Device.Idiom == TargetIdiom.Phone)
            {
                YPoint = (deviceHeight * 92 / 100);// deviceHeight - ;
            }
            else
            {
                YPoint = (deviceHeight * 160 / 100);// (deviceHeight * 92 / 100);// deviceHeight - ;
            }
            float Radius = deviceHeight + YPoint;
            path.AddCircle(XPoint, -YPoint, Radius);
            SKPaint paint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColor.Parse("#005e4b"),
            };
            canvas.DrawPath(path, paint);
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private async void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsLoading = true;
            });
            var dataItem = e.Item as MyBills;
            await Clipboard.SetTextAsync(dataItem.VTRE2);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                //viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + Environment.NewLine + " "+ text, "Copied");
                   viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
                //DisplayAlert("Success", string.Format("Your copied text is({0})", text), "OK");
                //if (App.IsArabic)
                //{
                //    CultureInfo culture = CultureInfo.CreateSpecificCulture("ar-AE");
                //    PopUp popUp = new PopUp();
                //    StringBuilder SB = new StringBuilder();
                //    // SB.Append(AppResources.ZSadadInvoiceNumber);
                //    //SB.Append(Environment.NewLine);
                //    // SB.AppendFormat(culture, AppResources.ZSadadInvoiceNumber + Environment.NewLine + " " + text);
                //    // AlertDialog dialog = new RightJustifyAlertDialog(this);
                //    this.DisplayAlert(AppResources.Copied, AppResources.ZSadadInvoiceNumber + Environment.NewLine + " " + text, AppResources.CancelText);

                //    // viewModel._dialogService.ShowMessageBox(SB.ToString(), AppResources.Copied);
                //}


                //popUp.Message = SB.ToString();
                //popUp.IsLinkAvailable = true;

                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsLoading = false;
            });
        }
        private void Chart_AnnotationClicked(object sender, Syncfusion.SfChart.XForms.ChartAnnotationClickedEventArgs e)
        {
        }
        private void Chart_LegendItemClicked(object sender, Syncfusion.SfChart.XForms.ChartLegendItemClickedEventArgs e)
        {
        }
        private void Chart_SelectionChanged(object sender, Syncfusion.SfChart.XForms.ChartSelectionEventArgs e)
        {
            SfChart SfChartM = sender as SfChart;
            //MyBillsChartModel KeywordSelect = (MyBillsChartModel)SfChartM.BindingContext;
            if (e.SelectedDataPointIndex > -1)
            {
                IList items = e.SelectedSeries.ItemsSource as IList;
                MyBillsChartModel selectedDatapoint = items[e.SelectedDataPointIndex] as MyBillsChartModel;
                viewModel.MyBills = null;
                if (selectedDatapoint.BillType == AppResources.Paid)
                {
                    viewModel.MyBills = viewModel.MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToObservableCollection();
                }
                if (selectedDatapoint.BillType == AppResources.UnPaid)
                {
                    viewModel.MyBills = viewModel.MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToObservableCollection();
                }
                if (selectedDatapoint.BillType == AppResources.PartiallyPaid)
                {
                    viewModel.MyBills = viewModel.MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToObservableCollection();
                }
                //await Navigation.PushModalAsync(new SecondaryPage(selectedDatapoint));
            }
            else
            {
                viewModel.MyBills = viewModel.MyBillsOriginal;
            }
        }
        private void ShowSelectedBillsType()
        {
        }
        private void simTab_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
        }
    }
}
