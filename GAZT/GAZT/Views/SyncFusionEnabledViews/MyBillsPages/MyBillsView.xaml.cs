using GAZT.Models;
using GAZT.ViewModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBillsView : ContentPage
    {
        MyBillsViewModel viewModel;
        public MyBillsView()
        {
            Resources["searchBarStyleForAll"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            Resources["searchBarStyleForPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForUnPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForPartiallyPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel = App.Locator.MyBillsView;
           
         
            Bills.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                if (sender is ListView lv) lv.SelectedItem = null;
            };
            try
            {
                SetLTR();
                this.BindingContext = viewModel;

                viewModel.onPageLoad();

            }
            catch (Exception ex)
            {

            }
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

        private void ClickGestureRecognizer_ClickedForAll(object sender, EventArgs e)
        {

            Resources["searchBarStyleForPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForUnPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForPartiallyPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];


            Resources["searchBarStyleForAll"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }

        private void ClickGestureRecognizer_ClickedForPaid(object sender, EventArgs e)
        {
            Resources["searchBarStyleForAll"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForUnPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForPartiallyPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];

            Resources["searchBarStyleForPaid"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }
        private void ClickGestureRecognizer_ClickedForUnPaid(object sender, EventArgs e)
        {
            Resources["searchBarStyleForAll"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForPartiallyPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];

            Resources["searchBarStyleForUnPaid"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }
        private void ClickGestureRecognizer_ClickedForPartiallyPaid(object sender, EventArgs e)
        {
            Resources["searchBarStyleForAll"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForUnPaid"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
           

            Resources["searchBarStyleForPartiallyPaid"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }
        protected void OnTaxPayerClicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.TaxPayerProfileView);

        }

        private async void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as MyBills;
            await Clipboard.SetTextAsync(dataItem.VTRE2);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + Environment.NewLine + " "+ text, "Copied");

                //DisplayAlert("Success", string.Format("Your copied text is({0})", text), "OK");
            }
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
                    viewModel.MyBills = viewModel.MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList();
                }
                if (selectedDatapoint.BillType == AppResources.UnPaid)
                {
                    viewModel.MyBills = viewModel.MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList();
                }
                if (selectedDatapoint.BillType == AppResources.PartiallyPaid)
                {
                    viewModel.MyBills = viewModel.MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList();
                }
                //await Navigation.PushModalAsync(new SecondaryPage(selectedDatapoint));
            }
            else
            {
                viewModel.MyBills = viewModel.MyBillsOriginal;

            }


        }
    }
    }
