using EGAZT.ViewModel.NewDesignViewModel.Common;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.Common.NativeNafath
{
    public partial class NativeNafathPage : ContentPage
	{
        NativeNafathLoginPageViewModel viewModel;
        public NativeNafathPage()
        {

            InitializeComponent();
            viewModel = App.Locator.NativeNafathLoginPageViewModel;
            BindingContext = viewModel;

        }
    }
}

