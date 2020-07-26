using EGAZT.ViewModel.SyncFusionEnabledViewModel.StylesTestUi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.StylesTestUi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StyleTestUIPageView : ContentPage
    {
        StyleTestUIPageViewModel viewModel;
        public StyleTestUIPageView()
            
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.StyleTestUIPageView;
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                this.BindingContext = viewModel;
            }
            catch(Exception ex)
            {

            }
        }
    }
}