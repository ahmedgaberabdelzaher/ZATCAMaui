using System;
using System.Collections.Generic;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using GAZT.Helper;

namespace GAZT.Views
{
    public partial class OTPView : ContentPage
    {
        double DeviceHeight;
        double DeviceWidth;
        OTPViewModel viewModel;
        public OTPView()
        {
            viewModel = App.Locator.OTPView;
            InitializeComponent();
            this.BindingContext = viewModel;
            DeviceWidth = DependencyService.Get<IDeviceInfo>().GetDeviceWidth();
            DeviceHeight = DependencyService.Get<IDeviceInfo>().GetDeviceHeight();

        }
        private void SetOTPBoxHeightWidth()
        {
            double HTAndWT = DeviceWidth * 65 / 100;
            //OTPOne.HeightRequest = HTAndWT;
            //OTPOne.WidthRequest = HTAndWT;
            //OTPEEntryOne.HeightRequest = HTAndWT;
            //OTPEEntryOne.WidthRequest = HTAndWT;

            //OTPTwo.HeightRequest = HTAndWT;
            //OTPTwo.WidthRequest = HTAndWT;
            //OTPEEntryTwo.HeightRequest = HTAndWT;
            //OTPEEntryTwo.WidthRequest = HTAndWT;

            //OTPThree.HeightRequest = HTAndWT;
            //OTPThree.WidthRequest = HTAndWT;
            //OTPEEntryThree.HeightRequest = HTAndWT;
            //OTPEEntryThree.WidthRequest = HTAndWT;

            //OTPFour.HeightRequest = HTAndWT;
            //OTPFour.WidthRequest = HTAndWT;
            //OTPEEntryFour.HeightRequest = HTAndWT;
            //OTPEEntryFour.WidthRequest = HTAndWT;

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
