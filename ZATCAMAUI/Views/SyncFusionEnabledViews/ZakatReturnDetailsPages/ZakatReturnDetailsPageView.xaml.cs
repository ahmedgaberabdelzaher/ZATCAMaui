
using RGPopup.Maui.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatReturnListPages;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
using Application = Microsoft.Maui.Controls.Application;

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
                On<iOS>().SetUseSafeArea(true);
                BindingContext = viewModel;
                NavigationPage.SetBackButtonTitle(this, "");
                ChangeAeroIcon();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Method
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
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
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }
        #endregion
    }
}