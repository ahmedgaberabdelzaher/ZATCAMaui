using GAZT.ViewModel.SyncFusionEnabledViewModel.ReturnsPageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.SyncFusionEnabledViews.ReturnsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReturnsPageView : ContentPage
    {
        ReturnsPageViewModel viewModel;
        public ReturnsPageView(int Index)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.ReturnsPageView;
                this.BindingContext = viewModel;
                viewModel.onPageLoad();
                viewModel.TabIndexStatus = Index;
            }
            catch (Exception ex)
            {

            }
        }

        private void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    this.Content = null;
        //}
    }
}