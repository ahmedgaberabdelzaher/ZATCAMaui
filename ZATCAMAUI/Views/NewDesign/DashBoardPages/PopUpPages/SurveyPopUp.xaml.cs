using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages.PopUpPages
{
    public partial class SurveyPopUp : PopupPage
    {
       GAZTNewDesignDashBoardPageViewModel viewModel;
        public SurveyPopUp()
        {
            viewModel = App.Locator.GAZTNewDesignDashBoardPageView;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}

