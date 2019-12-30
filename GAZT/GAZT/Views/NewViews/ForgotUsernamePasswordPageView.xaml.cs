using GAZT.ViewModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;
using GAZT.ViewModel.NewViewModel;
using GAZT.Manager;

namespace GAZT.Views.NewViews
{
    public partial class ForgotUsernamePasswordPageView : ContentPage
    {
        ObservableCollection<String> Items = new ObservableCollection<String>();

        ForgotUsernamePasswordPageViewModel viewModel;
        public ForgotUsernamePasswordPageView()
        {
            viewModel = App.Locator.ForgotUsernamePasswordPageView;
            InitializeComponent();
            SetLTR();
            string str = "abc";
            Items.Add(str);
            App.IsComingFromDashboardToLogOff = false;
           // CardView.ItemsSource = Items;
            try
            {

                this.BindingContext = viewModel;
                viewModel.OnPageLoad();
            }
            catch (Exception ex)
            {

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

        protected void OnSelectedTaxPAyerType(object sender, EventArgs e)
        {

        }

        protected void OnSelectedForgetType(object sender, EventArgs e)
        {

        }

        protected void OnUserNameUnFocussed(object sender, EventArgs e)
        {
            //if(!string.IsNullOrEmpty(UserName.Text))
            //{
                string userName = UserName.Text;
                bool IsValiedEmailAddress = UtilityManager.IsValidEmailAddress(userName);
                if (!IsValiedEmailAddress)
                {
                    viewModel.IsVisibleTinIds = false;
                }
                viewModel.SetTinsListLayoutVisibility(IsValiedEmailAddress);
            //}



        }
        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.NewPassword = "";
            viewModel.ConfirmPassword = "";
            viewModel.EnteredOTP = "";
            viewModel.IDNumber = "";
            viewModel.NewPasswordLayoutVisibility = false;
            viewModel.OTPLayoutVisibility = false;
            viewModel.NavigateToLoginLinkVisibility = false;
            viewModel.EnteredCaptchaValue  = "";
            viewModel.IsVisibleTinIds = false;
            viewModel.IsIDTypeVisible = false;
            //viewModel.Captcha = "";


            //viewModel.IsTaxPayerTypeEnable = true;
            //viewModel.IsForgotUserNameWithIndividual = true;
            //viewModel.IsForgotPassword = false;
            //viewModel.IsForgotUserNameWithCorporate = false;

        }
    }
}
