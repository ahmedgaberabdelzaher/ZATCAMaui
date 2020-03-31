using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMobileNumberPageView : ContentPage
    {
        #region Variable
        ChangeMobileNumberPageViewModel viewModel;
        #endregion
        #region Constructor
        public ChangeMobileNumberPageView()
        {
            viewModel = App.Locator.ChangeMobileNumberPageView;
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.OnPageLoad();


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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ChangeAeroIcon();
            // Task.Delay(20000);
            viewModel.NewMobile =string.Empty;
            

          

        }
        #endregion
    }
}