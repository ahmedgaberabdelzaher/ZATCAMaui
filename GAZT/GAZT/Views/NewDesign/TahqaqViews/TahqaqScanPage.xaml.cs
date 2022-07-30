using System;
using System.Collections.Generic;
using System.Diagnostics;
using EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace EGAZT.Views.NewDesign.TahqaqViews
{
    public partial class TahqaqScanPage : ContentPage
    {
        TahqaqScanPageViewModel viewModel;
        public TahqaqScanPage()
        {
            viewModel = App.Locator.tahqaqScanPageViewModel;
            BindingContext = viewModel;
            InitializeComponent();
           
           /* zxing.OnScanResult += (result) => Device.BeginInvokeOnMainThread(() => {
                Debug.WriteLine(result.Text);
            });*/
        }
        protected override async void OnAppearing()
        {
           /* PermissionStatus granted = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (granted != PermissionStatus.Granted)
            {
                _ = await Permissions.RequestAsync<Permissions.Camera>();
            }
           */
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
;           // zxing.IsScanning = true;
        }/*
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            zxing.IsScanning = false;
        }*/
    }
}
