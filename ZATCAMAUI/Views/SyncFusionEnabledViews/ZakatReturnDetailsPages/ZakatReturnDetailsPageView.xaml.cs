
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatReturnListPages;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatReturnDetailsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnDetailsPageView : ContentPage
    {
        #region Variable
        ZakatReturnDetailsPageViewModel viewModel;
        #endregion

        #region Constructor
        string Fbguid = "";
        public ZakatReturnDetailsPageView(string fbguid)
        {
            try
            {
                viewModel = App.Locator.ZakatReturnDetailsPageView;
                InitializeComponent();
                viewModel.ClearData();
                Fbguid = fbguid;
                BindingContext = viewModel;
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Method
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ZakatReturnListPageView.AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage = true;
            await viewModel.OnPageLoad(Fbguid);
            date.Text = viewModel.Abrzu;
        }

        protected async void OnBillsButtonClicked(object sender, EventArgs e)
        {
            ZakatReturnDetailsPageViewModel.IsAmendButtonClicked = false;
            if (viewModel.ZakatReturnDetails.d.Statusz.Equals("E0001") || viewModel.ZakatReturnDetails.d.Statusz.Equals("IP011"))
            {

                if (App.IsArabic)
                {
                    var result = await DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZCancel, AppResources.ZZZOkayText);
                    if (!result)
                    {
                        await viewModel.OnReleaseOrBillsClicked();
                    }

                }
                else
                {
                    var result = await DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZZOkayText, AppResources.ZZCancel);
                    if (result)
                    {
                        await viewModel.OnReleaseOrBillsClicked();
                    }

                }
            }
            else
            {
                await viewModel.OnReleaseOrBillsClicked();
            }





        }
        private void OnInformationMessageClickedOne(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZZEstimatedSalesInformationText;
            popUp.IsLinkAvailable = false;
            if (App.IsArabic)
            {
                popUp.FlowDirections = "RightToLeft";
            }
            else
            {
                popUp.FlowDirections = "LeftToRight";
            }
            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
        }
        private void OnInformationMessageClickedTwo(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZZCapitalamountasperMCIrecordsMOMRArecordsoranyothersourcethatassisttoidentifythecapitalamount;
            popUp.IsLinkAvailable = false;
            if (App.IsArabic)
            {
                popUp.FlowDirections = "RightToLeft";
            }
            else
            {
                popUp.FlowDirections = "LeftToRight";
            }
            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
        }
        private void OnInformationMessageClickedThree(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZZGreatervalueofEstimatedSales;
            popUp.IsLinkAvailable = false;
            if (App.IsArabic)
            {
                popUp.FlowDirections = "RightToLeft";
            }
            else
            {
                popUp.FlowDirections = "LeftToRight";
            }
            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
        }
        private void OnInformationMessageClickedFour(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZZZakatBaseandwithalowerboundof500SAR;
            popUp.IsLinkAvailable = false;
            if (App.IsArabic)
            {
                popUp.FlowDirections = "RightToLeft";
            }
            else
            {
                popUp.FlowDirections = "LeftToRight";
            }
            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
        }
        #endregion
    }
}