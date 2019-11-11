using GAZT.Helper;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace GAZT.Views
{
    public partial class OTPView : ContentPage
    {
        double DeviceHeight;
        double DeviceWidth;
        OTPViewModel viewModel;
        public OTPView(bool IsComingFromLogIn)
        {

            App.IsOTPiew = true;
            viewModel = App.Locator.OTPView;
            InitializeComponent();
            // viewModel.ClearData();
            viewModel.IsComingFromLogIn = IsComingFromLogIn;
            SetLTR();
            this.BindingContext = viewModel;
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

       

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

           
          
        //}

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
                    string cnt = entryTwo.Text;
                    if (cnt.Length == 1)
                    {
                        entrytThree.Unfocus();
                        entryFour.Focus();
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
                    string cnt = entrytThree.Text;
                    if (cnt.Length == 1)
                    {
                        entrytThree.Unfocus();
                        entryFour.Focus();
                    }
                    if (cnt.Length > 1)
                    {
                        entryFour.Unfocus();
                    }
                });
            });



        }

        //protected async override void OnAppearing()
        //{
        //    await Task.Run(() =>
        //    {
        //        Task.Delay(100);
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            entry.Focus();
        //        });
        //    });
        //}
        //private async void OnTextChangedOne(Object sender, EventArgs e)
        //{
        //    string s = entry.Text;
        //    await Task.Run(() =>
        //    {
        //        Task.Delay(100);
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            string cnt = entry.Text;
        //            if (cnt.Length == 1)
        //            {
        //                entry.Unfocus();
        //                entryTwo.Focus();
        //            }

        //        });
        //    });



        //}




        //private async void OnTextChangedTwo(Object sender, EventArgs e)
        //{
        //    string s = entry.Text;
        //    await Task.Run(() =>
        //    {
        //        Task.Delay(100);
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            string cnt = entryTwo.Text;
        //            if (cnt.Length == 1)
        //            {
        //                entryTwo.Unfocus();
        //                entrytThree.Focus();
        //            }
        //        });
        //    });



        //}



        //private async void OnTextChangedThree(Object sender, EventArgs e)
        //{
        //    string s = entry.Text;
        //    await Task.Run(() =>
        //    {
        //        Task.Delay(100);
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            string cnt = entryTwo.Text;
        //            if (cnt.Length == 1)
        //            {
        //                entrytThree.Unfocus();
        //                entryFour.Focus();
        //            }
        //        });
        //    });



        //}



        //private async void OnTextChangedFour(Object sender, EventArgs e)
        //{
        //    string s = entry.Text;
        //    await Task.Run(() =>
        //    {
        //        Task.Delay(100);
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            string cnt = entrytThree.Text;
        //            if (cnt.Length == 1)
        //            {
        //                entrytThree.Unfocus();
        //                entryFour.Focus();
        //            }
        //            if (cnt.Length > 1)
        //            {
        //                entryFour.Unfocus();
        //            }
        //        });
        //    });



      //  }
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
