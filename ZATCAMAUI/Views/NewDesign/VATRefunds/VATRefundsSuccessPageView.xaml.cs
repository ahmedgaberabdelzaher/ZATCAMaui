using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models.VATRefunds;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds;

namespace ZATCAMAUI.Views.NewDesign.VATRefunds
{
   
    public partial class VATRefundsSuccessPageView : ContentPage
    {
        VATRefundsSuccessPageViewModel viewModel;
        public VATRefundsSuccessPageView(VatRefundDisplayDataModel vatRefundsSummaryData)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsSuccessPageView;
            // ChangeAeroIcon();
            SetLTR();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            viewModel.VatNewReqSummaryData = vatRefundsSummaryData;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
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

        void btnGoToDasboard_Clicked(object sender, EventArgs e)
        {
            try
            {
                var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(firstPageToRemove);

                var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(secondPageToRemove);

                var thirdPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(thirdPageToRemove);

                viewModel._navigationService.GoBack();
            }
            catch (Exception)
            {


            }
        }

        void btnRefunds_Clicked(object sender, EventArgs e)
        {
            try
            {
                var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(firstPageToRemove);

                var secondPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(secondPageToRemove);

                viewModel._navigationService.GoBack();
            }
            catch (Exception)
            {


            }
        }
    }
}
