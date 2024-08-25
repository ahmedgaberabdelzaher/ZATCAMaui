using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.eDeclarations
{
    public partial class CreateE_Declaration : ContentPage
    {
        E_DeclerationViewModel viewModel;

        public CreateE_Declaration(string title)
        {
            viewModel = App.Locator.eDeclerationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
            header.TitleText = title;
            if (title == AppResources.eDeclaration)
            {
                wbview.Source = PageSettings.GetNewEDeclarationLinks();
            }
            else if (title == AppResources.Transactiondescription)
            {
                wbview.Source = PageSettings.GetTawreedLinks();
            }
            else if (title == AppResources.CustomFeesCalculator)
            {
                wbview.Source = PageSettings.GetCustomFeesCalcLink();
            }
        }

    }
}
