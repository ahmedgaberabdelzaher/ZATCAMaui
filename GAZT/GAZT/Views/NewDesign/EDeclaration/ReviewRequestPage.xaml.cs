using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.Controls;
using System.Collections.ObjectModel;

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

        protected override bool OnBackButtonPressed()
        {

            viewModel.BackMethod();
            return true;
        }
    }
}

