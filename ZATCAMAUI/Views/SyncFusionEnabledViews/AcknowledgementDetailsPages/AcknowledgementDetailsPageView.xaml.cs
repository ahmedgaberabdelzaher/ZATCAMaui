using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Newtonsoft.Json;
using System.Globalization;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AcknowledgementDetailsPage;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.AcknowledgementDetailsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcknowledgementDetailsPageView : ContentPage
    {
        #region Variable
        AcknowledgementDetailsPageViewModel viewModel;
        #endregion

        #region Constructor
        public AcknowledgementDetailsPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            NavigationPage.SetBackButtonTitle(this, "");
            ChangeAeroIcon();
            try
            {
                viewModel = App.Locator.AcknowledgementDetailsPageView;
                BindingContext = viewModel;
                if (vATDeclaration != null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;
                    viewModel.TPName = App.TP.Name;
                    viewModel.ReturnReferenceNumber = viewModel.VATDeclarationData.d.Fbnum;
                    viewModel.TaxablePeriod = viewModel.VATDeclarationData.d.Perslt;
                    string ReceiptDate;
                    viewModel.SadadNumber = string.Empty;
                    viewModel.IsSadadNumberVisible = false;
                    viewModel.IsButtonVisible = false;
                    viewModel.IsAcknowledgementButtonVisible = false;
                    if (App.ICRStatus == "E0045" && viewModel.VATDeclarationData.d.RefundFg != "1")
                    {
                        if (Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0)
                        {
                            viewModel.IsSadadNumberVisible = false;
                            viewModel.IsSadadNoteVisible = false;
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
                        if (App.ICRStatus == "E0006" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0 || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057") && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0 || App.ICRStatus == "E0055" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0)
                        {
                            viewModel.IsSadadNumberVisible = false;
                            viewModel.IsSadadNoteVisible = false;
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
                            if (App.ICRStatus == "E0006" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) > 0 || App.ICRStatus == "E0056" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) > 0 || App.ICRStatus == "E0001" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) > 0 || App.ICRStatus == "E0013" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) > 0)
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
                        viewModel.IsSadadNoteVisible = false;
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
                    viewModel.ReceiptDate = JsonConvert.DeserializeObject<DateTime>(@"""" + viewModel.VATDeclarationData.d.ReceiptDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region Method
        public async void RefreshForSadad()
        {
            try
            {
                await Task.Run(() =>
                  {
                      viewModel.IsLoading = true;
                  });
                await Task.Run(async () =>
                {
                    await viewModel.OnRefreshClick();
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
        public void IsCheckedEnable()
        {
            if (App.ICRStatus == "E0006")
            {
            }
        }
       
        protected async void OnVATRefreshButtonClicked(object sender, EventArgs e)
        {
            await viewModel.OnRefreshClick();
        }
        #endregion
    }
}