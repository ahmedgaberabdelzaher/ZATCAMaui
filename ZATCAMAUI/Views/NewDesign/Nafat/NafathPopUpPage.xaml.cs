
using Mopups.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Nafat;

namespace ZATCAMAUI.Views.NewDesign.Nafat
{
    public partial class NafathPopUpPage : PopupPage
    {
        NafathPopupPageViewModel viewModel;
        public NafathPopUpPage()
        {
            InitializeComponent();
            viewModel = App.Locator.NafathPopupPageViewModel;
            BindingContext = viewModel;
        }



    }
}

