using GAZT.Helper;
using GAZT.Manager;
using GAZT.ViewModel;
using GAZT.Views.NewViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPageView : ContentPage
    {   
        DashboardPageViewModel viewModel;

        #region Constructor
        public DashboardPageView()
        {
            viewModel = App.Locator.DashboardPageView;
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            NavigationPage.SetHasBackButton(this, false);
          
            SetLTR();
           
            this.BindingContext = viewModel;


            viewModel.onPageLoad();

            double deviceWidth = DependencyService.Get<IDeviceInfo>().GetDeviceWidth();
            double deviceHeight = DependencyService.Get<IDeviceInfo>().GetDeviceHeight();

            viewModel.DeviceWidth = deviceWidth;
            viewModel.DeviceHeight = deviceHeight;

            setHeight();

        }
        #endregion


        #region Method

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }


        public void setHeight()
        {
            double value = 0;
            double valueForReturn = 0;
            if (viewModel.DeviceHeight>viewModel.DeviceWidth)
            {
                value = 0.19 * viewModel.DeviceHeight;
                valueForReturn = 0.11 * viewModel.DeviceHeight;
                viewModel.HeightRequestForCollectionView = Convert.ToInt32(value);
                viewModel.HeightRequestForReturnCollectionView= Convert.ToInt32(valueForReturn);
                viewModel.CalendarHeightRequest =Convert.ToInt32(viewModel.DeviceHeight * 0.0468);
            }
           else
            {
                value = 0.19 * viewModel.DeviceWidth;
                valueForReturn = 0.11 * viewModel.DeviceHeight;
                viewModel.HeightRequestForCollectionView = Convert.ToInt32(value);
                viewModel.HeightRequestForReturnCollectionView = Convert.ToInt32(valueForReturn);
                viewModel.CalendarHeightRequest = Convert.ToInt32(viewModel.DeviceWidth * 0.0468);
            }

            viewModel.PaddingHeight = 0.2 * value;

            viewModel.PaddingForCollectionView=new Xamarin.Forms.Thickness(0,viewModel.PaddingHeight, 0, 0);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.IsComingFromDashboardToLogOff = true;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
          
            
        }

        private async void OnLogoutClicked(Object sender, EventArgs e)
        {
            var result = await this.DisplayAlert(AppResources.Alerts, AppResources.LogoutConfirmationMessage,AppResources.ZYes,AppResources.ZNo);
            if (result)
            {
                App.TP = null;
                viewModel.LogOut();
            }
        }
        
        #endregion
    }
}