using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    public partial class ZAKATReturnDetailsView : ContentPage
    {
        ZAKATReturnDetailsViewModel viewModel;
        public ZAKATReturnDetailsView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZAKATReturnDetailsView;
        }

    }
}
