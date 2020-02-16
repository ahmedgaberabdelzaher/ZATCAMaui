using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
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
            viewModel = App.Locator.BillDetailsPageView;
            viewModel.ZakatReturnDetail = ZakatReturnDetail;
            SetLTR();
            viewModel.zakatReturnDetailsD = ZakatReturnDetail;
            viewModel.OnPageLoad();
            this.BindingContext = viewModel;
            ToolbarItem Refresh = new ToolbarItem
            {
                Icon = "ic_refresh.png",
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

        //  Refresh.SetBinding(ToolbarItem.IconImageSourceProperty, new Binding("RefreshIconImageSource"));


        }
        #endregion

        #region Method
        protected async Task OnRefreshButtonClicked()
        {
            //if(!viewModel.IsSADADNumberGenerated)
            //{
                await viewModel.OnPageLoad();
            //}
        }

        protected  void OnDownLoadInvoiceClicked()
        {
         viewModel.GetPdfUrl();
        }
        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        #endregion

    }
}