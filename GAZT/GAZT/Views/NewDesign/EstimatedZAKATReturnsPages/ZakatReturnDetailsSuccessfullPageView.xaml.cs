using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnDetailsSuccessfullPageView : ContentPage
    {
        ZakatReturnDetailsSuccessfullPageViewModel viewModel;
        public ZakatReturnDetailsSuccessfullPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatReturnDetailsSuccessfullPageView;
            this.BindingContext = viewModel;
        }
    }
}