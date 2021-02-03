using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReturnNewSuccessPageView : ContentPage
    {
        public VatReturnNewSuccessViewModel viewModel;
        public VatReturnNewSuccessPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();

            viewModel = App.Locator.VatReturnNewSuccessView;
            this.BindingContext = viewModel;
            SetLTR();
            ChangeAeroIcon();
            //  On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            if (vATDeclaration != null && vATDeclaration.d != null)
            {
                viewModel.SadadNumber = string.Empty;
                viewModel.IsSadadNumberVisible = false;
                viewModel.IsButtonVisible = false;
                viewModel.IsAcknowledgementButtonVisible = false;
                viewModel.IsCreditCarriedTextVisible = false;
                viewModel.VATDeclarationData = vATDeclaration;
                viewModel.ReturnReferenceNumber = vATDeclaration.d.Fbnum;
                viewModel.TaxablePeriod = vATDeclaration.d.Perslt;

                //if (Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0)
                //{
                //    viewModel.IsSadadNumberVisible = false;
                //    viewModel.IsRefreshButtonVisible = false;
                //    viewModel.IsButtonVisible = true;
                //    if (vATDeclaration.d.EstimatedFg == "X")
                //    {
                //        viewModel.IsAcknowledgementButtonVisible = false;
                //    }
                //    else
                //    {
                //        viewModel.IsAcknowledgementButtonVisible = true;
                //    }
                //}
                //else
                //{
                //    RefreshForSadad();
                //}

                if ((App.ICRStatus == "E0045") && viewModel.VATDeclarationData.d.RefundFg != "1")
                {
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
                else
                {
                    if ((App.ICRStatus == "E0006" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0) || ((App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057") && (Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0)) || (App.ICRStatus == "E0055" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0))
                    {
                        viewModel.IsSadadNumberVisible = false;
                        viewModel.IsRefreshButtonVisible = false;
                        viewModel.IsButtonVisible = true;
                        viewModel.IsCreditCarriedTextVisible = true;
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
                        if ((App.ICRStatus == "E0006" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) > 0) || (App.ICRStatus == "E0056" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) > 0) || (App.ICRStatus == "E0001" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) > 0) || (App.ICRStatus == "E0013" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) > 0))
                        {
                            RefreshForSadad();
                        }
                        else
                        {
                            viewModel.IsRefreshButtonVisible = true;
                        }
                    }
                }
                if (viewModel.VATDeclarationData.d.RefundFg == "1")
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


            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;

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
        private void GotoreturnClicked(object sender, EventArgs e)
        {
            //PopupNavigation.Instance.PushAsync(new RefundAccountPopupPageView());
        }
        private void OnCopySadadNumberButtonClicked(object sender, EventArgs e)
        {
            //PopupNavigation.Instance.PushAsync(new RefundAccountPopupPageView());
        }

        private async void OnVATRefreshButtonClicked(object sender, EventArgs e)
        {
            await viewModel.OnRefreshClick();
        }

    }
}