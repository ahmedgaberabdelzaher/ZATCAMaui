using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight.Views;

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
            Thickness th = new Thickness();
            th.Left = 0;
            th.Right = 0;
            th.Top = -40;
            th.Bottom = 0;
          // logo.Margin = th;
            App.IsArabic = true;
            this.BindingContext = viewModel;
            
            //  SetRTLDirection();
            // UserNameMobileNumber.HorizontalTextAlignment = TextAlignment.Start;

            ToolbarItem toolbarItem1 = new ToolbarItem
            {
                Icon = "ic_language.png",
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(() =>
                {
                    //Original Code
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
            if(LanguageToolBarCount == 0)
            {
                LanguageToolBarCount = 1;
                this.ToolbarItems.Add(toolbarItem1);
            }
         
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //viewModel.UserName = String.Empty;
            //viewModel.Password = String.Empty;

            if (App.IsComingFromDashboardToLogOff)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("Alert!", AppResources.LogoutConfirmationMessage, "Yes", "No");
                    if (!result)
                    {
                        viewModel._navigationService.NavigateTo(App.DashboardView);
                    }
                   
                });
            }
            

        }
        //private void OnOnLanguageClickClicked(object sender, EventArgs e)
        //{
        //    if(App.IsArabic)
        //    {
        //        App.IsArabic = false;
        //        SetLTRDirection();
        //    }
        //    else
        //    {
        //        App.IsArabic = true;
        //        SetRTLDirection();
        //    }

        //}

        



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
            float XPoint = DeviceWidth/2;
            float YPoint;
            if (Device.Idiom == TargetIdiom.Phone)
            {
                YPoint = (deviceHeight * 92 / 100);// deviceHeight - ;
            }
            else
            {
                YPoint = (deviceHeight * 160 / 100);// (deviceHeight * 92 / 100);// deviceHeight - ;
            }
            float Radius = deviceHeight+ YPoint;
            //YPoint = YPoint;// + (float)App.NavigationBarHeightt; 
            path.AddCircle(XPoint,-YPoint, Radius);

           
            SKPaint paint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColor.Parse("#005e4b"),
            };

            canvas.DrawPath(path, paint);

        }
       
            //public void Handle_UserNameTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
            //{
            //    var email = this.UserNameTxtBox.Text;

            //    var emailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            //    var TINPattern = @"^([a-zA-Z0-9])$";
            //    if (Regex.IsMatch(email, emailPattern) || Regex.IsMatch(email, TINPattern))
            //    {
            //    }
            //}


        }
    }
