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
            SetLTR();
            // SetPickerFont();
            InitializeComponent();

        }


        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;


            }
            else
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
        }



    }
}
