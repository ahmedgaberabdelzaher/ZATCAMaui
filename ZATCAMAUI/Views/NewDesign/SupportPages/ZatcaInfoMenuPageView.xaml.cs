
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.SupportPageVM;
namespace ZATCAMAUI.Views.NewDesign.SupportPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZatcaInfoMenuPageView : ContentPage
    {
        ZatcaInfoMenuPageViewModel viewModel;
        public ZatcaInfoMenuPageView()
        {

            InitializeComponent();

            viewModel = App.Locator.ZatcaInfoMenuPageView;
            BindingContext = viewModel;

            viewModel.setMenuTab();
        }

        protected override bool OnBackButtonPressed()
        {
            GoToBackStep();
            return true;
        }
        private void OnBackArrowTapped(object sender, EventArgs e)
        {

            GoToBackStep();
        }

        public void GoToBackStep()
        {

            viewModel.IsLoading = false;
            viewModel.ChcekCurrentTabCustoms();

        }



        private void OnMenu1Tapped(object sender, EventArgs e)
        {
            viewModel.setMenu1Tab();
            if (!PageSettings.IsIncludeTarrif)
            {
                if (App.IsArabic)
                {

                    CustomsTraffis.Source = ZATCAConstants.ZAtcaCustomsTarrifsAr;
                }
                else
                {
                    CustomsTraffis.Source = ZATCAConstants.ZAtcaCustomsTarrifsAr;
                }
            }
        }

        private void OnMenu2Tapped(object sender, EventArgs e)
        {
            viewModel.setMenu2Tab();
            if (!PageSettings.IsIncludeInquiryVisible)
            {
                if (App.IsArabic)
                {

                    CustomsView.Source = ZATCAConstants.ZAtcaCustomsdeclarationsAr;
                }
                else
                {
                    CustomsView.Source = ZATCAConstants.ZAtcaCustomsdeclarationsAr;
                }

            }


        }

        private void CustomsView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            try
            {
                if (e.Url.Contains(ZATCAConstants.ZAtcaCustomsdeclarationsAr) || e.Url.Contains(ZATCAConstants.ZAtcaCustomsdeclarationsEN))
                {
                    viewModel.IsLoading = false;
                }
                else
                {
                    viewModel.IsLoading = true;
                }
            }
            catch (Exception)
            {


            }
        }

        private void CustomsView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }


        private void CustomsTraffis_Navigating(object sender, WebNavigatingEventArgs er)
        {
            try
            {
                if (er.Url.Contains(ZATCAConstants.ZAtcaCustomsTarrifsAr) || er.Url.Contains(ZATCAConstants.ZAtcaCustomsTarrifsEN))
                {
                    viewModel.IsLoading = false;
                }
                else
                {
                    viewModel.IsLoading = true;
                }
            }
            catch (Exception)
            {


            }
        }

        private void CustomsTraffis_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }

    }
}
