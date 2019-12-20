using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GAZT.Views
{
    public partial class PdfiOSView : ContentPage
    {
        PdfiOSViewModel viewModel;
        public PdfiOSView(string Pdfurl)
        {
            viewModel = App.Locator.PdfiOSView;
            InitializeComponent();
            this.BindingContext = viewModel;
            if (!string.IsNullOrEmpty(Pdfurl))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    webView.Source = Pdfurl;
                });
            }

        }
    }
}
