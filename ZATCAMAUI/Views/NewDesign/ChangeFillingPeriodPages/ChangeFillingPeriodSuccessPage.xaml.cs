
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodSuccessPage : ContentPage
    {
        public ChangeFillingPeriodViewModel viewModel;

        public ChangeFillingPeriodSuccessPage()
        {
            InitializeComponent();

            viewModel = App.Locator.ChangeFillingPeriodSuccessPageView;
            BindingContext = viewModel;
        }

    }
}