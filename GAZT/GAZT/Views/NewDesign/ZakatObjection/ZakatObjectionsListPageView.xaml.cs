using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZAKATObjectionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionsListPageView : ContentPage
    {
        ZakatObjectionsListViewModel viewModel;
        public ZakatObjectionsListPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatObjectionListView;
            this.BindingContext = viewModel;
        }
    }
}