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
