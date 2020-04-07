using System;
using System.Collections.Generic;
using GAZT.ViewModel.NewViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace GAZT.Views.NewViews
{
    public partial class AAcknowledgement : ContentPage
    {
        AAcknowledgementViewModel viewModel;
        public AAcknowledgement(string url)
        {
            viewModel = App.Locator.AAcknowledgementView;
          
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            Acknowledgement.Source = url;
        }
    }
}
