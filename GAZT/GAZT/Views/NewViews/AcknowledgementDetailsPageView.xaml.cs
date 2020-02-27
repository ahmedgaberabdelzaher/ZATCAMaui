using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
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
            NavigationPage.SetBackButtonTitle(this, "");
            try
            {
                viewModel = App.Locator.AcknowledgementDetailsPageView;
                this.BindingContext = viewModel;
                SetLTR();
                if (vATDeclaration!=null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;
                    viewModel.TPName = App.TP.Name;
                    viewModel.ReturnReferenceNumber = viewModel.VATDeclarationData.d.Fbnum;
                    viewModel.TaxablePeriod = viewModel.VATDeclarationData.d.Perslt;
                    string ReceiptDate;
                    viewModel.SadadNumber = string.Empty;
                    viewModel.IsSadadNumberVisible = false;
                    viewModel.IsButtonVisible = false;
                    if ((App.ICRStatus== "E0045") && viewModel.VATDeclarationData.d.RefundFg != "1")
                    {
                        if (Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0)
                        {
                            viewModel.IsSadadNumberVisible = false;
                            viewModel.IsSadadNoteVisible = false;
                            viewModel.IsRefreshButtonVisible = false;
                            viewModel.IsButtonVisible = true;
                        }
                        else
                        {
                            viewModel.OnRefreshClick();
                        }
                    }
                    else
                    {
                        if ((App.ICRStatus == "E0006" && Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat)<=0) || ((App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057") && (Convert.ToDouble(viewModel.VATDeclarationData.d.NetdueVat) <= 0)))
                        {
                            viewModel.IsSadadNumberVisible = false;
                            viewModel.IsSadadNoteVisible = false;
                            viewModel.IsRefreshButtonVisible = false;
                            viewModel.IsButtonVisible = true;
                        }
                        else
                        {
                            viewModel.IsRefreshButtonVisible = true;
                        }
                    }
                    if (viewModel.VATDeclarationData.d.RefundFg == "1")
                    {
                        viewModel.IsSadadNumberVisible = false;
                        viewModel.IsSadadNoteVisible = false;
                        viewModel.IsRefreshButtonVisible = false;
                        viewModel.IsButtonVisible = true;
                    }
                    
                        
                   
                    

                    if (App.IsArabic)
                    {
                        ReceiptDate = JsonConvert.DeserializeObject<DateTime>(@"""" + viewModel.VATDeclarationData.d.ReceiptDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        viewModel.ReceiptDate = UtilityManager.ToArabicDate(ReceiptDate);
                    }
                    else
                    {
                        viewModel.ReceiptDate = JsonConvert.DeserializeObject<DateTime>(@"""" + viewModel.VATDeclarationData.d.ReceiptDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    }
                }

                
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Method

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

        protected async void OnVATRefreshButtonClicked(Object sender,EventArgs e)
        {
            await viewModel.OnRefreshClick();
        }
        #endregion




    }
}