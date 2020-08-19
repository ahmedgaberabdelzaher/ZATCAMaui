using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatForm5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatAcknowledgmentPageView : ContentPage
    {
        ZakatAcknowledgmentPageViewModel viewModel;
        public ZakatAcknowledgmentPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatAcknowledgmentPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }
    }
}