using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatyViewModels;

namespace ZATCAMAUI.Views.NewDesign.Zakaty
{
    public partial class AboutZakatyView : BaseContentPage
    {
        AboutZakatyViewModel viewModel;
        public AboutZakatyView()
        {
            viewModel = App.Locator.AboutZakatyViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}

