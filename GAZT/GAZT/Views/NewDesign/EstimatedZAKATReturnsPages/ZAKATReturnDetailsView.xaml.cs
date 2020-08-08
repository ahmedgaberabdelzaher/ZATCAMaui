using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    public partial class ZAKATReturnDetailsView : ContentPage
    {
        ZAKATReturnDetailsViewModel viewModel;
        public ZAKATReturnDetailsView(string fbguid)
        {
            InitializeComponent();
            viewModel = App.Locator.ZAKATReturnDetailsView;
            this.BindingContext = viewModel;
            SetLTR();

          viewModel.OnPageLoad(fbguid);

        }

        protected override void OnAppearing()
        {

        date.Text = viewModel.Abrzu;
        }
        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
           this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

    }
}
