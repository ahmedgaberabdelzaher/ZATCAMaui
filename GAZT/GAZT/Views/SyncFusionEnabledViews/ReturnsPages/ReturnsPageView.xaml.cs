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
        public ReturnsPageView()
        {
            try
            {
                InitializeComponent();
                this.BindingContext = viewModel = App.Locator.ReturnsPageView;
                viewModel.onPageLoad();
            }
            catch (Exception ex)
            {

            }
        }

    }
}