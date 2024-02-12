
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ContactUsPage;

namespace ZATCAMAUI.Views.NewDesign.HomePages
{
    public partial class ContactUs : BaseContentPage
    {
        ContactUsPageViewModel viewModel;
        public ContactUs()
        {
            viewModel = App.Locator.ContactUsPageView;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
