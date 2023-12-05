using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using EGAZT.Views.SyncFusionEnabledViews.ZakatReturnList;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.ZakatReturnDetails_View
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnDetailsPageView : ContentPage
    {
        #region Variable
        ZakatReturnDetailsPageViewModel viewModel;
        #endregion
        #region Property
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
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                SetLTR();
                this.BindingContext = viewModel;
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                ChangeAeroIcon();
            }
                catch(Exception)
            {
            }
        }
        #endregion
        #region Method
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ZakatReturnListPageView.AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage = true;
            await viewModel.OnPageLoad(Fbguid);
            date.Text = viewModel.Abrzu;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected async void OnBillsButtonClicked(object sender, EventArgs e)
        {
            ZakatReturnDetailsPageViewModel.IsAmendButtonClicked = false;
            if (viewModel.ZakatReturnDetails.d.Statusz.Equals("E0001") || viewModel.ZakatReturnDetails.d.Statusz.Equals("IP011"))
            {
                //var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZZOkayText, AppResources.ZZCancel);
                //if (result)
                //{
                //    await viewModel.OnReleaseOrBillsClicked();
                //}

                if (App.IsArabic)
                {
                    var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZCancel, AppResources.ZZZOkayText);
                    if (!result)
                    {
                        await viewModel.OnReleaseOrBillsClicked();
                    }

                }
                else
                {
                    var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZZOkayText, AppResources.ZZCancel);
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