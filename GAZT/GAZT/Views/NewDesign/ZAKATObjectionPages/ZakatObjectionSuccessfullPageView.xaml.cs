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
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace EGAZT.Views.NewDesign.ZAKATObjectionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionSuccessfullPageView : ContentPage
    { 
        ZakatObjectionSuccessfullPageViewModel viewModel;
        ZakatReturnDetailsD _zakatReturnDetail;

        public ZakatObjectionSuccessfullPageView(ZakatReturnDetailsD ZakatReturnDetail)
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatObjectionSuccessfullPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
           // Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            SetLTR();
            ChangeAeroIcon();

            viewModel.ClearData();
            viewModel.OnPageLoad(ZakatReturnDetail);
            this._zakatReturnDetail = ZakatReturnDetail;

            ToolbarItem Refresh = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(async () =>
                {
                    await OnRefreshButtonClicked();
                    // viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                })
            };
            this.ToolbarItems.Add(Refresh);
            Refresh.SetBinding(ToolbarItem.IconImageSourceProperty, new Binding("RefreshIconImageSource"));

        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        protected async Task OnRefreshButtonClicked()
        {
            try
            {
                if (viewModel.IsrefreshEnabled)
                {
                    await viewModel.OnPageLoad(_zakatReturnDetail);
                }
                else
                {
                    // put Mesage already latest SADADID available
                }
            }
            catch (Exception ex)
            {

            }

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
            Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(pg);
            viewModel._navigationService.GoBack();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            //Navigation.RemovePage(pg);
        }
    }
}