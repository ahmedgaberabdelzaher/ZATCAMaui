using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATReturnSuccessfullPageView : ContentPage
    {
        #region Variable
        public VATReturnSuccessfullPageViewModel viewModel;
        #endregion
        public VATReturnSuccessfullPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            viewModel = App.Locator.VATReturnSuccessfullPageView;
            this.BindingContext = viewModel;
            SetLTR();
            if (vATDeclaration!=null && vATDeclaration.d!=null)
            {
                viewModel.SadadNumber = string.Empty;
                viewModel.IsSadadNumberVisible = false;
                viewModel.IsButtonVisible = false;
                viewModel.IsAcknowledgementButtonVisible = false;

                viewModel.VATDeclarationData = vATDeclaration;
                viewModel.ReturnReferenceNumber = vATDeclaration.d.Fbnum;
                viewModel.TaxablePeriod = vATDeclaration.d.Perslt;

                if (Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0)
                {
                    viewModel.IsSadadNumberVisible = false;
                    viewModel.IsRefreshButtonVisible = false;
                    viewModel.IsButtonVisible = true;
                    if (vATDeclaration.d.EstimatedFg == "X")
                    {
                        viewModel.IsAcknowledgementButtonVisible = false;
                    }
                    else
                    {
                        viewModel.IsAcknowledgementButtonVisible = true;
                    }
                }
                else
                {
                    RefreshForSadad();
                }

            }

        }

        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        public async void RefreshForSadad()
        {
            try
            {
                Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.OnRefreshClick();
                });
                Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }
        private void SfButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new RefundAccountPopupPageView());
        }

        private async void OnVATRefreshButtonClicked(object sender, EventArgs e)
        {
            await viewModel.OnRefreshClick();
        }

        private void GotodashboardClicked(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
                Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg);
                Xamarin.Forms.Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg1);
            }
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
        }
    }
}