
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
            BindingContext = viewModel;

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


        protected async Task OnRefreshButtonClicked()
        {
            try
            {
                if (viewModel.IsrefreshEnabled)
                {
                    await viewModel.OnPageLoad(_zakatReturnDetail);
                }
               
            }
            catch (Exception)
            {

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
                
            }
            catch (Exception)
            {

            }

        }
    }
}