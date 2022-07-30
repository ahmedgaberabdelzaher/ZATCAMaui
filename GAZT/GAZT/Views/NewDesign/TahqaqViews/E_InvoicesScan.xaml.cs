using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace EGAZT.Views.NewDesign.TahqaqViews
{
    public partial class E_InvoicesScan : ContentPage
    {
        TahqaqScanPageViewModel viewModel;
        public E_InvoicesScan()
        {
            viewModel = App.Locator.tahqaqScanPageViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
     
    }
}
