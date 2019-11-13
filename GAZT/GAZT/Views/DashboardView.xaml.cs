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
        DashboardViewModel viewModel;
        ObservableCollection<String> Items = new ObservableCollection<String>();
        public DashboardView()
        {

            viewModel = App.Locator.DashboardView;
            InitializeComponent();
            SetLTR();
            this.BindingContext = viewModel;
            string str = "abc";
            Items.Add(str);
            CardView.ItemsSource = Items;


        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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

        //protected override bool OnBackButtonPressed()
        //{
        //    Device.BeginInvokeOnMainThread(async () => {
        //        var result = await this.DisplayAlert("Alert!", "Do you really want to Logout?", "Yes", "No");
        //        if (result) viewModel._navigationService.NavigateTo(App.LoginView); ; // or anything else
        //    });

        //    return true;
        //}

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.IsComingFromDashboardToLogOff = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            for (int index = 0; index < Navigation.NavigationStack.Count; index++)
            {
                Page pg = Navigation.NavigationStack[index];
                if (pg.GetType() == typeof(OTPView))
                {
                    Navigation.RemovePage(pg);
                }
            }
        }

    }
}
