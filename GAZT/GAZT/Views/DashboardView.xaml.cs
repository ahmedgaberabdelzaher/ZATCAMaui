using System;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using GAZT.Helper;
namespace GAZT.Views
{
    public partial class DashboardView : ContentPage
    {
        double DeviceHeight;
        double DeviceWidth;
        ObservableCollection<String> Items = new ObservableCollection<String>();
        public DashboardView()
        {
            InitializeComponent();
            string str = "abc";
            Items.Add(str);
            CardView.ItemsSource = Items;
            EstimateZakatReturns.ItemsSource = Items;
            MyCertificates.ItemsSource = Items;
            MyBillsInvoices.ItemsSource = Items;
            //DeviceWidth = DependencyService.Get<IDeviceInfo>().GetDeviceWidth();
            //DeviceHeight = DependencyService.Get<IDeviceInfo>().GetDeviceHeight();
            //SetEstimateZakatReturnsLayoutHeightWidth();
            //SetMyCertificatesLayoutHeightWidth();
            //SetMyBillsInvoicesLayoutHeightWidth();
        }

        //private void SetEstimateZakatReturnsLayoutHeightWidth()
        //{
           
        //    double HeightWidth = (DeviceWidth*87)/100/3;
        //    EstimateZakatReturns.HeightRequest = HeightWidth;
        //    EstimateZakatReturns.HeightRequest = HeightWidth;

                
        //}

        //private void SetMyCertificatesLayoutHeightWidth()
        //{
        //          double HeightWidth = (DeviceWidth * (87 / 100)) / 3;
        //    MyCertificates.HeightRequest = HeightWidth;
        //    MyCertificates.HeightRequest = HeightWidth;


        //}

        //private void SetMyBillsInvoicesLayoutHeightWidth()
        //{
        //    double HeightWidth = (DeviceWidth * (87 / 100)) / 3;
        //    MyBillsInvoices.HeightRequest = HeightWidth;
        //    MyBillsInvoices.HeightRequest = HeightWidth;
        //}

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
            float YPoint = (deviceHeight * 40 / 100);// deviceHeight - ;
            float Radius = deviceHeight + YPoint;
            path.AddCircle(XPoint, -YPoint, Radius);


            SKPaint paint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColor.Parse("#005e4b"),
            };

            canvas.DrawPath(path, paint);

        }

       

    }
}
