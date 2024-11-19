
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.VATReviewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewSuccessPageView : ContentPage
    {
        public VatReviewViewModel viewModel;
        public VATObjectionSummaryModel modelclass3;
        public VatReviewSuccessPageView(VATObjectionSummaryModel modelclass)
        {
            InitializeComponent();


            viewModel = App.Locator.VatReviewSuccessView;

            BindingContext = viewModel;
            VATApplicationID.Text = modelclass.d.Fbnumx;
            viewModel.VATReferanceNumber = modelclass.d.Fbnumx;
            modelclass3 = modelclass;

        }


        private async void VatReview_Tapped(object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewListPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewSuccessPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            await viewModel._navigationService.NavigateTo(App.VatReviewListPageView);
        }
    }
}