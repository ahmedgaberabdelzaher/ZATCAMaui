using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common.NativeNafath
{
    public partial class NativeNafathPage : ContentPage
    {
        NativeNafathLoginPageViewModel viewModel;
        public NativeNafathPage(string pageName)
        {

            InitializeComponent();
            viewModel = App.Locator.NativeNafathLoginPageViewModel;
            viewModel.PageName = pageName;
            BindingContext = viewModel;

        }
    }
}

