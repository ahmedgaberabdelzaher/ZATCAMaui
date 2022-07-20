using System;
using System.Collections.Generic;
using System.Diagnostics;
using EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

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
            PermissionStatus granted = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (granted != PermissionStatus.Granted)
            {
                _ = await Permissions.RequestAsync<Permissions.Camera>();
            }

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
