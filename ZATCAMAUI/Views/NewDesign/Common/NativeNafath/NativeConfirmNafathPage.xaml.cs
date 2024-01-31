
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common.NativeNafath
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

