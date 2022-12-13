using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class ReviewRequestPage : ContentPage
    {
        ReviewRequestViewModel viewModel;
        public ReviewRequestPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ReviewRequestViewModel;
            BindingContext = viewModel;
        }
    }
}

