using GAZT.Helper;
using GAZT.ViewModel;
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

        public void setHeight()
        {
            double value = 0;
           if (viewModel.DeviceHeight>viewModel.DeviceWidth)
            {
                value = 0.11 * viewModel.DeviceHeight;
                viewModel.HeightRequestForCollectionView = Convert.ToInt32(value);
                viewModel.CalendarHeightRequest =Convert.ToInt32(viewModel.DeviceHeight * 0.0468);
            }
           else
            {
                value = 0.11 * viewModel.DeviceWidth;
                viewModel.HeightRequestForCollectionView = Convert.ToInt32(value);
                viewModel.CalendarHeightRequest = Convert.ToInt32(viewModel.DeviceWidth * 0.0468);
            }

            viewModel.PaddingHeight = 0.2 * value;

            viewModel.PaddingForCollectionView=new Xamarin.Forms.Thickness(0,viewModel.PaddingHeight, 0, 0);
        }

       
        #endregion
    }
}