using System;
using System.Collections.Generic;
using GAZT.ViewModel.NewViewModel;
using Xamarin.Forms;

namespace GAZT.Views.NewViews
{
    public partial class AAcknowledgement : ContentPage
    {
        AAcknowledgementViewModel viewModel;
        public AAcknowledgement(string url)
        {
            viewModel = App.Locator.AAcknowledgementView;
          
            InitializeComponent();
            this.BindingContext = viewModel;
            Acknowledgement.Source = url;
        }
    }
}
