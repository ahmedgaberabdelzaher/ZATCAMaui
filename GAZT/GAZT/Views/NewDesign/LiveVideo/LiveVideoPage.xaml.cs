using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.LiveVideoVM;
using MediaManager;
using MediaManager.Library;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.LiveVideo
{
    public partial class LiveVideoPage : ContentPage
    {
        LiveVideoViewModel viewModel;
        public LiveVideoPage()
        {
            InitializeComponent();
            viewModel = App.Locator.LiveVideoViewModel;
            viewModel.CurrentTab = 3;
            BindingContext = viewModel;
        }
    }
}

