using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.LiveVideoVM;
using MediaManager;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.LiveVideo
{
    public partial class LiveVideoPage : ContentPage
    {
         LiveVideoViewModel viewModel;
        public LiveVideoPage()
        {
            viewModel = App.Locator.liveVideoViewModel;
            CrossMediaManager.Current.Dispose();
            CrossMediaManager.Current.Init();
            BindingContext = viewModel;
            InitializeComponent();
           
        }
    }
}

