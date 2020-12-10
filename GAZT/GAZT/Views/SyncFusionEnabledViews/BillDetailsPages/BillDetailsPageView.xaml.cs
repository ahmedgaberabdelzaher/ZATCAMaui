using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.BillDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SalesDetailsPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;
namespace EGAZT.Views.SyncFusionEnabledViews.BillDetails
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillDetailsPageView : ContentPage
    {
        #region Variable
        BillDetailsPageViewModel viewModel;
        #endregion
        #region Constructor
        public BillDetailsPageView(ZakatReturnDetailsD ZakatReturnDetail)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.BillDetailsPageView;
            viewModel.ZakatReturnDetail = ZakatReturnDetail;
            SetLTR();
            viewModel.zakatReturnDetailsD = ZakatReturnDetail;
            viewModel.ClearData();
            viewModel.OnPageLoad();
            ChangeAeroIcon();
            this.BindingContext = viewModel;
            NavigationPage.SetBackButtonTitle(this, "");
            ToolbarItem Refresh = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(async() =>
                {
                   await OnRefreshButtonClicked();
                   // viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                })
            };
            this.ToolbarItems.Add(Refresh);
            ToolbarItem Download = new ToolbarItem
            {
                Icon = "ic_download.png",
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(() =>
                {
                    OnDownLoadInvoiceClicked();
                    // viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                })
            };
            this.ToolbarItems.Add(Download);
         Refresh.SetBinding(ToolbarItem.IconImageSourceProperty, new Binding("RefreshIconImageSource"));
        }
        #endregion
        #region Method
        protected async Task OnRefreshButtonClicked()
        {
            if(viewModel.IsrefreshEnabled)
            {
                await viewModel.OnPageLoad();
            }
            else
            {
           // put Mesage already latest SADADID available
            }
        }
        protected  void OnDownLoadInvoiceClicked()
        {
         viewModel.GetPdfUrl();
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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            int RemovedPageCount = 0;
            if(SalesDetailsPageViewModel.IsComingFromSalesDetailsPage)
            {
                SalesDetailsPageViewModel.IsComingFromSalesDetailsPage = false;
                for (int index = Navigation.NavigationStack.Count - 2; index > 3; index--)
                {
                    RemovedPageCount++;
                    Xamarin.Forms.Page pg = Navigation.NavigationStack[index];
                    Navigation.RemovePage(pg);
                    if (RemovedPageCount == 2)
                        break;
                }
            }
        }
        public async void OnCopySadadNumberButtonClicked(object sender, EventArgs args)
        {
            await Clipboard.SetTextAsync(viewModel.EstimatedZAKATSADADNumber.Sopbel);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                //viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + Environment.NewLine + " "+ text, "Copied");
               await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber  + " " + text, AppResources.Copied);
                //DisplayAlert("Success", string.Format("Your copied text is({0})", text), "OK");
                //PopUp popUp = new PopUp();
                //StringBuilder SB = new StringBuilder();
                //SB.Append(AppResources.ZSadadInvoiceNumber);
                //SB.Append(Environment.NewLine);
                //SB.Append(text);
                //popUp.Message = SB.ToString();
                //popUp.IsLinkAvailable = true;

                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
            }
        }
        #endregion
    }
}