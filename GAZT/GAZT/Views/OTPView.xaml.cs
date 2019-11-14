using GAZT.Helper;
using GAZT.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace GAZT.Views
{
   
    public partial class OTPView : ContentPage
    {
        double DeviceHeight;
        double DeviceWidth;
        OTPViewModel viewModel;
        public OTPView(NavigateToOtp e)
        {

            App.IsOTPiew = true;
            viewModel = App.Locator.OTPView;
            InitializeComponent();
            
            
            // viewModel.ClearData();
            
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.IsComingFrom = e;
            if (e == NavigateToOtp.IsMobile)
            {
                viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode;
                viewModel.OTPSentOnThisMobileNumber = App.TP.NewMobile;
                viewModel.OTPSentOnThis = viewModel.OTPSentOnThisMobileNumber;
                var MobileNumber = viewModel.OTPSentOnThis;

                var firstDigits = MobileNumber.Substring(0, 2);
                var lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);

                var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);

                var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                viewModel.OTPSentOnThis = maskedCardNumberWithSpaces;
            }
            else if(e == NavigateToOtp.IsEmail)
            {
                viewModel.OTPSentOnThisText = AppResources.EnterVerificationCodeForEmail;
                viewModel.OTPSentOnThisEmail = App.TP.NewEmail;
                viewModel.OTPSentOnThis = viewModel.OTPSentOnThisEmail;
            }
            else if(e == NavigateToOtp.IsLogin)
            {
                viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode;
                viewModel.OTPSentOnThisMobileNumber = App.TP.Mobile;
                viewModel.OTPSentOnThis = viewModel.OTPSentOnThisMobileNumber;
                var MobileNumber = viewModel.OTPSentOnThis;

                var firstDigits = MobileNumber.Substring(0, 2);
                var lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);

                var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);

                var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                viewModel.OTPSentOnThis = maskedCardNumberWithSpaces;
            }
            DeviceWidth = DependencyService.Get<IDeviceInfo>().GetDeviceWidth();
            DeviceHeight = DependencyService.Get<IDeviceInfo>().GetDeviceHeight();


        }
       
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App.IsOTPiew = true;
            await Task.Run(() =>
            {

                Task.Delay(100);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    entry.Focus();
                });
            });
        }
        private async void OnTextChangedOne(Object sender, EventArgs e)
        {
            string s = entry.Text;
            await Task.Run(() =>
            {
                Task.Delay(100);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string cnt = entry.Text;
                    if (cnt.Length == 1)
                    {
                        entry.Unfocus();
                        entryTwo.Focus();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(entry.Text))
                            entry.Text = entry.Text.Substring(0, 1);
                    }

                });
            });



        }




        private async void OnTextChangedTwo(Object sender, EventArgs e)
        {
            string s = entry.Text;
            await Task.Run(() =>
            {
                Task.Delay(100);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string cnt = entryTwo.Text;
                    if (cnt.Length == 1)
                    {
                        entryTwo.Unfocus();
                        entrytThree.Focus();
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(entryTwo.Text))
                        entryTwo.Text = entryTwo.Text.Substring(0, 1);
                    }
                });
            });



        }



        private async void OnTextChangedThree(Object sender, EventArgs e)
        {
            string s = entry.Text;
            await Task.Run(() =>
            {
                Task.Delay(100);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string cnt = entrytThree.Text;
                    if (cnt.Length == 1)
                    {
                        entrytThree.Unfocus();
                        entryFour.Focus();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(entrytThree.Text))
                            entrytThree.Text = entrytThree.Text.Substring(0, 1);
                    }
                });
            });



        }



        private async void OnTextChangedFour(Object sender, EventArgs e)
        {
            string s = entry.Text;
            await Task.Run(() =>
            {
                Task.Delay(100);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string cnt = entryFour.Text;
                    if (cnt.Length == 1)
                    {
                       
                        entryFour.Unfocus();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(entryFour.Text))
                            entryFour.Text = entryFour.Text.Substring(0, 1);
                        entryFour.Focus();
                    }
                   
                });
            });



        }

        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.ClearData();
            App.IsOTPiew = false;
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
    }
}
