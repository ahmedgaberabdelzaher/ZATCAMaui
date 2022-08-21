using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign
{
    public partial class Home : BaseContentPage
    {
        HomeViewModel viewModel;
        public Home(string tab)
        {
            viewModel = App.Locator.homeViewModel;
            BindingContext = viewModel;
            viewModel.CurrentTab = int.Parse(tab);
            InitializeComponent();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html= @"<iframe  src=https://www.youtube.com/embed/eJ6ZMd4sVrI frameborder=0 allow=accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture allowfullscreen></iframe>";
            //var html = @"<iframe src=https://www.youtube.com/embed/eJ6ZMd4sVrI  frameborder=0 allow=accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture allowfullscreen></iframe>";
            youtubeView.Source = htmlSource;
        }
    }
}
