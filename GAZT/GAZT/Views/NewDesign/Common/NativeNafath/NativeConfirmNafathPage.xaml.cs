
using EGAZT.ViewModel.NewDesignViewModel.Common;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.Common.NativeNafath
{	
	public partial class NativeConfirmNafathPage : ContentPage
	{
        NativeNafathLoginPageViewModel viewModel;
        public NativeConfirmNafathPage()
        {

            InitializeComponent();
            viewModel = App.Locator.NativeNafathLoginPageViewModel;
            BindingContext = viewModel;

        }
    }
}

