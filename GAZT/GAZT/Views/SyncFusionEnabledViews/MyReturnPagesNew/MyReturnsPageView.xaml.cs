using GAZT.ViewModel.SyncFusionEnabledViewModel.MyReturnsPageViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.SyncFusionEnabledViews.MyReturnPagesNew
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReturnsPageView : ContentPage
    {
        MyReturnsPageViewModel viewModel;
        public MyReturnsPageView(int Index)
        {
            viewModel = App.Locator.MyReturnsPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            InitializeComponent();
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            viewModel.OnPageLoad();
            viewModel.TabIndexStatus = Index;
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.FDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.FDirection = FlowDirection.RightToLeft;
            }
        }
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
    }
}