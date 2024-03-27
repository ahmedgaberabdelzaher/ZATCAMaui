using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages
{
    public partial class InquiryAboutCustomsDeclaration : ContentPage
    {
        InquiryAboutCustomsDeclarationViewModel viewModel;
        public InquiryAboutCustomsDeclaration()
        {
            viewModel = App.Locator.InquiryAboutCustomsDeclarationViewModel;
            BindingContext = viewModel;
            InitializeComponent();

        }


    }
}
