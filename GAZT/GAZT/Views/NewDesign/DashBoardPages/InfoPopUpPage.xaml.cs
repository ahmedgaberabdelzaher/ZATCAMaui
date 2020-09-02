using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Manager;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPopUpPage : PopupPage
    {

        GAZTNewDesignDashBoardPageViewModel viewModel;
        public InfoPopUpPage()
        {

            viewModel = App.Locator.InfoPopUpPage;
            this.BindingContext = viewModel;
            SetLTR();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void OnVATNowTapped(object sender, EventArgs e)
        {

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

        private void btnDashboard_Clicked(object sender, EventArgs e)
        {

        }

        private async  void btnLogout_Clicked(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZNo, AppResources.ZYes);
                if (!result)
                {
                    App.TP = null;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PopAsync();
                        await LogOutFromPopup();
                    });
             
                }
            }
            else
            {
                var result = await this.DisplayAlert(AppResources.ZLogout, AppResources.LogoutConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
                if (result)
                {
                    App.TP = null;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PopAsync();
                        await LogOutFromPopup();
                    });



                }
            }
        }//  viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);

        public async Task LogOutFromPopup()
        {
            await Task.Run(() =>
            {
                App.DisplayProgressView();
            });
            if (App.TP != null)
                App.TP = null;
            if (App.PreviousIsArabic)
            {
                String langName = "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                String langName = "en-US";
                AppResources.Culture = new CultureInfo(langName);
            }

            try
            {
                await WebServiceManager.GAZTLogOff();
            }
            catch
            {

            }

            await Task.Run(() =>
            {
                App.HideProgressView();
            });

            //var _navigation = Application.Current.MainPage.Navigation;
            //foreach (var item in _navigation.NavigationStack)
            //{
            //    if (item.GetType().Name == App.GAZTNewDesignOnBoardingAnimationPageView)
            //    {
            //        _navigation.RemovePage(item);
            //        break;
            //    }
            //}

            App.IsLogOut = true;
            App.IsLoginCalled = false;
            App.IsSamlApiCalledAndroid = false;

            try
            {
                App.httpClientHandler = new HttpClientHandler();
                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
            }
            catch (Exception ex)
            {

            }

            //_navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);

            // await _navigation.PopToRootAsync();
            //_navigation.NavigationStack.ToList().Clear();
          //  viewModel._navigationService.GoBack();
          viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
           
        }

        private void OnZakatNowTapped(object sender, EventArgs e)
        {

        }
    }
}