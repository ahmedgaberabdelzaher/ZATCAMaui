using System;
using System.Globalization;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using GAZT.Manager;

namespace GAZT.Views
{
    public partial class LogInView : ContentPage
    {
        LogInViewModel viewModel;
        int LanguageToolBarCount = 0;
        public LogInView()
        {
            viewModel = App.Locator.LogInView;
            InitializeComponent();
            App.IsArabic = true;
            this.BindingContext = viewModel;
            viewModel.PasswordVisibility = true;
            ToolbarItem toolbarItem1 = new ToolbarItem
            {
                Icon = "ic_language.png",
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(() =>
                {
                    if (App.IsArabic)
                    {
                        App.IsArabic = false;
                        SetLTRDirection();
                    }
                    else
                    {
                        App.IsArabic = true;
                        SetRTLDirection();
                    }
                })
            };
            if (LanguageToolBarCount == 0)
            {
                LanguageToolBarCount = 1;
                this.ToolbarItems.Add(toolbarItem1);
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (App.CurrentDropdownTIN != null)
            viewModel.SelectedTinId = App.CurrentDropdownTIN;
            //viewModel.UserName = String.Empty;
            //viewModel.Password = String.Empty;
            //  await WebServiceManager.GetAllGAZTCertificate("EN", "");
            if (App.IsComingFromDashboardToLogOff && !App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Alert!", AppResources.LogoutConfirmationMessage, "Yes", "No");
                    if (!result)
                    {
                        viewModel._navigationService.NavigateTo(App.DashboardView);
                    }
                    else
                    {
                        App.TP = null;
                        viewModel.UserName = string.Empty;
                        viewModel.Password = string.Empty;
                        viewModel.IsVisibleTinIds = false;
                    }
                });
            }
            else if(App.IsSessionExpired)
            {
                await viewModel._dialogService.ShowMessageBox("Your Session has expired,Please Login again", AppResources.Information);
            }
            else
            {

            }
            App.TP = null;
            viewModel.UserName = string.Empty;
            viewModel.Password = string.Empty;
            viewModel.IsVisibleTinIds = false;
        }
        public void OnPasswordVisibilityClicked(object sender, EventArgs args)
        {
            viewModel.PasswordVisibility = !viewModel.PasswordVisibility;
        }
        public void SetRTLDirection()
        {
            String langName = "ar-AE";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            InitializeComponent();

            this.FlowDirection = FlowDirection.RightToLeft;
        }
        public void SetLTRDirection()
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            InitializeComponent();
            this.FlowDirection = FlowDirection.LeftToRight;
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


        //private void ImageEntry_LeftImageClicked(object sender, EventArgs e)
        //{
        //    DefaultEntry.Text = "Working Left Image Entry Clicked";
        //}

        //private void ImageEntry_RightImageClicked(object sender, EventArgs e)
        //{
        //    DefaultEntry.Text = "Working Right Image Entry Clicked";
        //}

        //private void ImageEntry_LeftImageClicked_1(object sender, EventArgs e)
        //{
        //    DefaultEntry.Text = "Working Both Entry Clicked ";
        //}

        //private void ImageEntry_RightImageClicked_1(object sender, EventArgs e)
        //{
        //    DefaultEntry.Text = "Working Both Both Entry Clicked";
        //}
    }
}
