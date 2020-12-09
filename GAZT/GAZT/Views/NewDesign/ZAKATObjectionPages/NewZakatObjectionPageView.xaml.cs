using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZAKATObjectionPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewZakatObjectionPageView : ContentPage
    {
        NewZakatObjectionPageViewModel viewModel;
        public NewZakatObjectionPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.NewZakatObjectionPageView;
            this.BindingContext = viewModel;
        }
    }
}