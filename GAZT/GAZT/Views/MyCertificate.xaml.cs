using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace GAZT.Views
{
    public partial class MyCertificate : ContentPage
    {
        // ObservableCollection<String> Items = new ObservableCollection<String>();

        MyCertificateViewModel viewModel;
        List<string> list = new List<string>();
        public MyCertificate()
        {
            try
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel = App.Locator.MyCertificate;
                SetLTR();

                this.BindingContext = viewModel;
            }catch(Exception ex)
            {

            }
            //string str = "abc";
            //Items.Add(str);
            // CardView.ItemsSource = Items;




            viewModel.OnPageLoad();
            if (viewModel.allCertificate != null)
            {
                if (viewModel.allCertificate.ZakatSet != null && viewModel.allCertificate.ZakatSet.results != null && viewModel.allCertificate.ZakatSet.results.Count > 0)
                {
                    Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
                    Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
                    Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
                }
                else if (viewModel.allCertificate.VATSet != null && viewModel.allCertificate.VATSet.results != null && viewModel.allCertificate.VATSet.results.Count > 0)
                {
                    Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
                    Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
                    Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
                }
                else if (viewModel.allCertificate.ExciseSet != null && viewModel.allCertificate.ExciseSet.results != null && viewModel.allCertificate.ExciseSet.results.Count > 0)
                {
                    Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];

                    Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
                    Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
                }
            }
            else
            {
                Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
                Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
                Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            }





            //EXISECertificateList.ItemSelected += (sender, e) =>
            //{
            //    if (e.SelectedItem == null)
            //    {

            //        return;
            //    } ((ListView)sender).SelectedItem = null;
            //};
            CertificateLstET.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            };
            CertificateLstZakat.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            };
            CertificateLstVAT.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
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


        private void ClickGestureRecognizer_ClickedForExcise(object sender, EventArgs e)
        {

            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];


            Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }

        private void ClickGestureRecognizer_ClickedForZakat(object sender, EventArgs e)
        {

            Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];


            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }

        private void ClickGestureRecognizer_ClickedForVAT(object sender, EventArgs e)
        {

            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];


            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }

    }
}
