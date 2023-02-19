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

        protected override void OnDisappearing()
        {
            App.Locator.StateManager.DeleteItem("inquireDeclaration");
            viewModel.Inquire = new TravelerDeclarationResponse();
            viewModel.InquireList = new ObservableCollection<BottomSheetModel>();
            viewModel.DetailsTotalFeesList = new ObservableCollection<BottomSheetModel>();
            base.OnDisappearing();
        }
    }
}

