using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZAKATObjectionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionSuccessfullPageView : ContentPage
    { 
        ZakatObjectionSuccessfullPageViewModel viewModel;
        public ZakatObjectionSuccessfullPageView(ZakatReturnDetailsD ZakatReturnDetail)
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatObjectionSuccessfullPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            SetLTR();
            viewModel.OnPageLoad(ZakatReturnDetail);
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnReturnClicked(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(pg);
        }
    }
}