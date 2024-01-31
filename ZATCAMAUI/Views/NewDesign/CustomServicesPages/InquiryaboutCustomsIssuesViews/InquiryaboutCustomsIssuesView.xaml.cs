using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.InquiryaboutCustomsIssuesViews
{
    public partial class InquiryaboutCustomsIssuesView : ContentPage
    {

        InquiryaboutCustomsIssuesViewModel viewModel;
        public InquiryaboutCustomsIssuesView()
        {
            viewModel = App.Locator.InquiryaboutCustomsIssuesViewModel;
            BindingContext = viewModel;
            var url = PageSettings.GetCustomsIssueUrl();

            InitializeComponent();
            webc.Source = url;
        }

        void BackTapped(object sender, EventArgs e)
        {
            if (webc.CanGoBack)
            {
                webc.GoBack();
                var url = PageSettings.GetCustomsIssueUrl();
                webc.Source = url;
                return;
            }
            viewModel._navigationService.GoBack();
        }
    }
}

