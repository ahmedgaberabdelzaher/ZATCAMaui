using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace EGAZT.Views.NewDesign.TahqaqViews
{
    public partial class E_InvoicesScan : ContentPage
    {
        TahqaqScanPageViewModel viewModel;
        ZXingScannerView zxing;
        public E_InvoicesScan()
        {
            viewModel = App.Locator.tahqaqScanPageViewModel;
            BindingContext = viewModel;
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () =>
                {
                   
                    viewModel.Result = result;
                     viewModel.ScanEnvoiceQrCommand.Execute(null);
                });

            InitializeComponent();
            
            MainGrid.Children.Add(zxing);
        }
        protected override async void OnAppearing()
        {
           PermissionStatus granted = await Permissions.CheckStatusAsync<Permissions.Camera>();
             if (granted != PermissionStatus.Granted)
             {
                 _ = await Permissions.RequestAsync<Permissions.Camera>();
             }
            zxing.IsScanning = true;
            /*  var  scanPage = new ZXingScannerPage();
                 scanPage.OnScanResult += (result) =>
                 {
                     scanPage.IsScanning = false;

                     Device.BeginInvokeOnMainThread(async () =>
                     {
                         await Navigation.PopAsync();
                         await DisplayAlert("Scanned Barcode", result.Text, "OK");
                     });
                 };

                 await Navigation.PushAsync(scanPage);
             */
            base.OnAppearing();
            // await viewModel.CheckQr()
           // ;           // zxing.IsScanning = true;
        }
    }
}
