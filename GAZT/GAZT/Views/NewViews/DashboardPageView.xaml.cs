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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
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
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
          
            SetLTR();
           
            this.BindingContext = viewModel;
            InitializeViewModel();
            viewModel.onPageLoad();

            double deviceWidth = DependencyService.Get<IDeviceInfo>().GetDeviceWidth();
            double deviceHeight = DependencyService.Get<IDeviceInfo>().GetDeviceHeight();

            viewModel.DeviceWidth = deviceWidth;
            viewModel.DeviceHeight = deviceHeight;

            setHeight();

        }

        public void InitializeViewModel()
        {
            viewModel.IsZakatVisible = false;
            viewModel.IsVATVisible = false;
            viewModel.BillReturn = null;
            viewModel.BillPaid = null;

            viewModel.StartDate = string.Empty;
            viewModel.TinNumber = string.Empty;
            viewModel.TaxPayerName = string.Empty;
            viewModel.EndDate = string.Empty;
            viewModel.HeightRequestForCollectionView = 0;
            viewModel.HeightRequestForReturnCollectionView = 0;
            viewModel.TotalNoOfReturns = "0";
            viewModel.FooterImageInArabic = true;
            viewModel.FooterImageInEnglish = true;
            viewModel.TotalPaidAmount = string.Empty;
            viewModel.PaddingForCollectionView= new Xamarin.Forms.Thickness(0, 0, 0, 0);
            viewModel.FiscalDates = string.Empty;
            viewModel.IsVisibleFiscal = false;
            viewModel.IsVATVisible = false;
            viewModel.IsZakatVisible = false;
            viewModel.VATColumn = 0;
            viewModel.CorresColumn = 1;
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
            var result = await this.DisplayAlert(AppResources.ZLogout,AppResources.LogoutConfirmationMessage, AppResources.ZYes,AppResources.ZNo);
            if (result)
            {
                App.TP = null;
                viewModel.LogOut();
            }
        }
        
        #endregion
    }
}