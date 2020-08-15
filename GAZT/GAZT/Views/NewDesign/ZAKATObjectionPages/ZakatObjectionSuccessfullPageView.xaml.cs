using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZAKATObjectionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionSuccessfullPageView : ContentPage
    { 
        ZakatObjectionSuccessfullPageViewModel viewModel;
        public ZakatObjectionSuccessfullPageView()
        {
            InitializeComponent();
        viewModel = App.Locator.ZakatObjectionSuccessfullPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);

        }
    }
}