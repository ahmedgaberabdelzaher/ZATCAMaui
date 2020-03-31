using GAZT.ViewModel.SyncFusionEnabledViewModel.ReturnsPageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
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
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel = App.Locator.ReturnsPageView;
                this.BindingContext = viewModel;
                viewModel.onPageLoad();
                viewModel.TabIndexStatus = Index;
                SetLTR();
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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}