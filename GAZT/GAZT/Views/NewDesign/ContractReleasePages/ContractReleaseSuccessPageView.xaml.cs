using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ContractReleaseViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ContractRelease
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleaseSuccessPageView : ContentPage
    {
        public ContractReleaseSuccessViewModel viewModel;
        
        public ContractReleaseSuccessPageView()
        {
            InitializeComponent();
            
            App.IsArabic = true;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.ContractReleaseSuccessPageView;
            this.BindingContext = viewModel;
        }
        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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
        
        private void Dashboard_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
        }
    }
}