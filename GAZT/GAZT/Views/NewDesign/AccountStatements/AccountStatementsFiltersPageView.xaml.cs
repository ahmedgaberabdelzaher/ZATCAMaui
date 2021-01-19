using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    [Preserve(AllMembers = true)]
    public partial class AccountStatementsFiltersPageView : PopupPage
    {
        AccountStatementsFiltersPageViewModel viewModel;

        public AccountStatementsFiltersPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.AccountStatementsFiltersPageView;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.PopulateFiltersData();
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }

        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        void SortAscending_Tapped(System.Object sender, System.EventArgs e)
        {
        }

        void SortDescending_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}
