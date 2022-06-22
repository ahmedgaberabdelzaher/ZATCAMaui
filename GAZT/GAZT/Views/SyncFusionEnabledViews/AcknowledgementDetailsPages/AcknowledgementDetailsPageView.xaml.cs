using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AcknowledgementDetailsPage_ViewModel;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.AcknowledgementDetails
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcknowledgementDetailsPageView : ContentPage
    {
        #region Variable
        AcknowledgementDetailsPageViewModel viewModel;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public AcknowledgementDetailsPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            ChangeAeroIcon();
            try
            {
                viewModel = App.Locator.AcknowledgementDetailsPageView;
                this.BindingContext = viewModel;
                SetLTR();
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
                    if ((App.ICRStatus == "E0045") && viewModel.VATDeclarationData.d.RefundFg != "1")
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
                        if ((App.ICRStatus == "E0006" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0) || ((App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057") && (Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0)) || (App.ICRStatus == "E0055" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0))
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
                    //if (App.IsArabic)
                    //{
                    //    ReceiptDate = JsonConvert.DeserializeObject<DateTime>(@"""" + viewModel.VATDeclarationData.d.ReceiptDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    //    viewModel.ReceiptDate = UtilityManager.ToArabicDate(ReceiptDate);
                    //}
                    //else
                    //{
                    //    viewModel.ReceiptDate = JsonConvert.DeserializeObject<DateTime>(@"""" + viewModel.VATDeclarationData.d.ReceiptDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        #endregion
        #region Method
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
            catch(Exception ex)
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
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        public void IsCheckedEnable()
        {
            if (App.ICRStatus == "E0006")
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
        protected async void OnVATRefreshButtonClicked(Object sender, EventArgs e)
        {
            await viewModel.OnRefreshClick();
        }
        #endregion
    }
}