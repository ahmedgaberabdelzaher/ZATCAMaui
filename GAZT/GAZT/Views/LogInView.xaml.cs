using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
namespace GAZT.Views
{
    public partial class LogInView : ContentPage
    {
        public LogInView()
        {
            InitializeComponent();
           // UserNameMobileNumber.HorizontalTextAlignment = TextAlignment.Start;
        }
        //private void OnArClicked(object sender, EventArgs e)
        //{
        //    String langName = "ar-AE";
        //    CultureInfo ci = new CultureInfo(langName);
        //    AppResources.Culture = ci;
        //    // AppResources.ResourceManager.ReleaseAllResources();
        //    this.FlowDirection = FlowDirection.RightToLeft;
        //}
        //private void OnEnClicked(object sender, EventArgs e)
        //{
        //    String langName = "en-US";
        //    CultureInfo ci = new CultureInfo(langName);
        //    AppResources.Culture = ci;
        //    // AppResources.ResourceManager.ReleaseAllResources();
        //    this.FlowDirection = FlowDirection.LeftToRight;
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
            float XPoint = DeviceWidth/2;
            float YPoint = (deviceHeight*40/100);// deviceHeight - ;
            float Radius = deviceHeight+ YPoint;
            path.AddCircle(XPoint,-YPoint, Radius);

           
            SKPaint paint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColor.Parse("#005e4b"),
            };

            canvas.DrawPath(path, paint);

        }
    }
}
