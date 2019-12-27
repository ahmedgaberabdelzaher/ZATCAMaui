using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace GAZT.Views
{
    public partial class MyCertificate : ContentPage
    {
       // ObservableCollection<String> Items = new ObservableCollection<String>();

        MyCertificateViewModel viewModel;
        List<string> list = new List<string>();
        public MyCertificate()
        {

            viewModel = App.Locator.MyCertificate;
            InitializeComponent();
            //string str = "abc";
            //Items.Add(str);
           // CardView.ItemsSource = Items;
            viewModel.OnPageLoad();
            SetLTR();
            this.BindingContext = viewModel;
            //EXISECertificateList.ItemSelected += (sender, e) =>
            //{
            //    if (e.SelectedItem == null)
            //    {

            //        return;
            //    } ((ListView)sender).SelectedItem = null;
            //};
            CertificateLst.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
            };

            //VATCertificateList.ItemSelected += (sender, e) =>
            //{
            //    if (e.SelectedItem == null)
            //    {
            //        return;
            //    } ((ListView)sender).SelectedItem = null;
            //};
            //ZAKATCertificateList.ItemSelected += (sender, e) =>
            //{
            //    if (e.SelectedItem == null)
            //    {
            //        return;
            //    } ((ListView)sender).SelectedItem = null;
            //};
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        //void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        //{
        //    SKImageInfo info = args.Info;
        //    SKSurface surface = args.Surface;
        //    SKCanvas canvas = surface.Canvas;

        //    canvas.Clear();

        //    SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
        //    float radius = Math.Min(info.Width, info.Height) / 4;
        //    SKPath path = new SKPath
        //    {
        //        FillType = SKPathFillType.EvenOdd,
        //    };
        //    float a = center.X - radius / 2;
        //    float b = center.Y - radius / 2;
        //    float r = radius;
        //    float DeviceWidth = info.Width;
        //    float deviceHeight = info.Height;
        //    float XPoint = DeviceWidth / 2;
        //    float YPoint;
        //    if (Device.Idiom == TargetIdiom.Phone)
        //    {
        //        YPoint = (deviceHeight * 92 / 100);// deviceHeight - ;
        //    }
        //    else
        //    {
        //        YPoint = (deviceHeight * 160 / 100);// (deviceHeight * 92 / 100);// deviceHeight - ;
        //    }
        //    float Radius = deviceHeight + YPoint;
        //    path.AddCircle(XPoint, -YPoint, Radius);
        //    SKPaint paint = new SKPaint()
        //    {
        //        Style = SKPaintStyle.StrokeAndFill,
        //        Color = SKColor.Parse("#005e4b"),
        //    };
        //    canvas.DrawPath(path, paint);
        //}

        protected void OnTaxPayerClicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.TaxPayerProfileView);

        }

    }
}
