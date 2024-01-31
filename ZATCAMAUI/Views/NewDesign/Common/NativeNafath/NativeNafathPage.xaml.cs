using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common.NativeNafath
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

