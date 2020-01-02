using GAZT.ViewModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;


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
    }
}