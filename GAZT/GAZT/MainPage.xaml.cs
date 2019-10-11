using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
namespace GAZT
{
    public partial class MainPage : ContentPage
    {
        SKPaint outlinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 3,
            Color = SKColors.Black
        };

        SKPaint arcPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 15,
            Color = SKColors.Red
        };

        public MainPage()
        {
            InitializeComponent();
        }

        void sliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (canvasView != null)
            {
                canvasView.InvalidateSurface();
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            //SKImageInfo info = args.Info;
            //SKSurface surface = args.Surface;
            //SKCanvas canvas = surface.Canvas;

            //canvas.Clear();

            //SKRect rect = new SKRect(100, 100, info.Height - 100, info.Width - 100);
            //float startAngle = 0;// (float)startAngleSlider.Value;
            //float sweepAngle = 0; //(float)sweepAngleSlider.Value;

            //canvas.DrawOval(rect, outlinePaint);

            //using (SKPath path = new SKPath())
            //{
            //    path.AddArc(rect, startAngle, sweepAngle);
            //    canvas.DrawPath(path, arcPaint);
            //}

            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float radius = Math.Min(info.Width, info.Height) / 4;

            SKPath path = new SKPath
            {
                FillType = SKPathFillType.EvenOdd
            };

            float a = center.X - radius / 2;
            float b = center.Y - radius / 2;
            float r = radius;
            float DeviceWidth = info.Width;
            float deviceHeight = info.Height;
            float XPoint = DeviceWidth / 2;
            float YPoint = (deviceHeight*11/100);// deviceHeight - ;
            float Radius = (deviceHeight*42/100) + YPoint;
            path.AddCircle(XPoint, -YPoint, Radius);
            //path.AddCircle(center.X - radius / 2, center.Y - radius / 2, radius);
            //path.AddCircle(center.X - radius / 2, center.Y + radius / 2, radius);
            //path.AddCircle(center.X + radius / 2, center.Y - radius / 2, radius);
            //path.AddCircle(center.X + radius / 2, center.Y + radius / 2, radius);

            SKPaint paint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color =SKColor.Parse("#005e4b"),
            };

            canvas.DrawPath(path, paint);

            paint.Style = SKPaintStyle.Stroke;
            paint.StrokeWidth = 0;
            paint.Color = SKColors.Green;

            canvas.DrawPath(path, paint);
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
           // AppResources.ResourceManager.ReleaseAllResources();
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
