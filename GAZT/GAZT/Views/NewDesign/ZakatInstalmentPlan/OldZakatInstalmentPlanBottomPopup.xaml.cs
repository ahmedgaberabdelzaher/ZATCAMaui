
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OldZakatInstalmentPlanBottomPopup : PopupPage
    {


        OldZakatInstalmentPlanViewModel viewModel;
        public OldZakatInstalmentPlanBottomPopup()
        {
            InitializeComponent();
            viewModel = App.Locator.OldZakatInstalmentPlanPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }



        //private void Close_Tapped(object sender, EventArgs e)
        //{
        //    PopupNavigation.Instance.PopAsync();
        //}
    }
}