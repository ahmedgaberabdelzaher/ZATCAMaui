using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatForm5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatForm5PageView : ContentPage
    {
        ZakatForm5PageViewModel viewModel;

        public ZakatForm5PageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatForm5PageView;
        }
    }
}