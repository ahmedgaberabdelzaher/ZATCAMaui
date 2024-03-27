using RGPopup.Maui.Pages;

using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;

namespace ZATCAMAUI.Views.NewDesign.ContractReleasePages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleaseInfoPopup : PopupPage
    {
        private ContractReleaseViewModel viewModel;

        public ContractReleaseInfoPopup(string Title, string Desc)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            //ChangeAeroIcon();
            viewModel = App.Locator.ContractReleasePageView;
            this.BindingContext = viewModel;
            viewModel.InfoTitle = Title;
            viewModel.InfoDesc = Desc;
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
    }
}