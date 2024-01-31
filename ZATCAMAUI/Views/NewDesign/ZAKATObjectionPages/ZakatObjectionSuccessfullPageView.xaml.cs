using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using Page = Microsoft.Maui.Controls.Page;

namespace ZATCAMAUI.Views.NewDesign.ZAKATObjectionPages
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
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            // Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            SetLTR();
            ChangeAeroIcon();

            viewModel.ClearData();
            _ = viewModel.OnPageLoad(ZakatReturnDetail);
            _zakatReturnDetail = ZakatReturnDetail;

            ToolbarItem Refresh = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(() =>
                {
                  
                })
            };
            ToolbarItems.Add(Refresh);
            Refresh.SetBinding(MenuItem.IconImageSourceProperty, new Binding("RefreshIconImageSource"));

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
            catch (Exception)
            {

            }

        }


        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnReturnClicked(object sender, EventArgs e)
        {
            Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(pg);
            viewModel._navigationService.GoBack();
        }

        private async void OnRefreshButtonClicked(object sender, EventArgs e)
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
            catch (Exception)
            {

            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}