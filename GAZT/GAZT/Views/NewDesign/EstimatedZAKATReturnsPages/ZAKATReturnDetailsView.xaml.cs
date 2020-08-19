using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    public partial class ZAKATReturnDetailsView : ContentPage
    {
        ZAKATReturnDetailsViewModel viewModel;
        public static string salesType;
        public static bool IsGoingFirstTimeOnAttachmentPage;
        public ZAKATReturnDetailsView(string fbguid)
        {
            InitializeComponent();
            viewModel = App.Locator.ZAKATReturnDetailsView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            if (AttachmentPopUpViewModel.SalesDetailList != null)
                AttachmentPopUpViewModel.SalesDetailList.Clear();
            IsGoingFirstTimeOnAttachmentPage = true;
            viewModel.ClearData();
            viewModel.Fbguid = fbguid;
            SetLTR();

            viewModel.OnPageLoad(fbguid);

        }

        protected override void OnAppearing()
        {

            //date.Text = viewModel.Abrzu;
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected async void OnReleaseBillsButtonClicked(object sender, EventArgs e)
        {
            if (viewModel.ZakatReturnDetails.d.Statusz.Equals("E0001") || viewModel.ZakatReturnDetails.d.Statusz.Equals("IP011"))
            {
                if (App.IsArabic)
                {
                    var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZCancel, AppResources.ZZZOkayText);
                    if (!result)
                    {
                        await viewModel.OnReleaseOrBillsClicked();
                    }

                }
                else
                {
                    var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZZOkayText, AppResources.ZZCancel);
                    if (result)
                    {
                        await viewModel.OnReleaseOrBillsClicked();
                    }

                }
            }
            else
            {
                await viewModel.OnReleaseOrBillsClicked();
            }



        }
        private void OnEditClicked(object sender, EventArgs e)
        {

            viewModel.isLabelVisible = false;
            viewModel.isEditVisible = true;
            viewModel.IsEditTextVisible = false;
            viewModel.SetSubmitButtonVisibility = true;
            viewModel.SetConfirmButtonVisibility = false;
            viewModel.SetEditImage();
            //  PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));

        }

        private void OnTotalVATSaleEditImageClicked(object sender, EventArgs e)
        {
            if(viewModel.isThresholdValueLessThanTotalVATSales)
            {
                salesType = "TotalVATSales";
                PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));

            }
        }

        private void OnAverageNumberLabourEditImageClicked(object sender, EventArgs e)
        {
            salesType = "AverageNumberLabour";
            PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
        }

        private void OnImportValueEditImageClicked(object sender, EventArgs e)
        {
            salesType = "ImportValue";
            PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
        }

        private void OnImportFromPointOfSalesEditImageClicked(object sender, EventArgs e)
        {
            salesType = "ImportFromPointOfSales";
            PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
        }

        private void OnContactFromETIMADSystemEditImageClicked(object sender, EventArgs e)
        {
            salesType = "ContactFromETIMADSystem";
            PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
        }

        private void OnExportValueEditImageClicked(object sender, EventArgs e)
        {
            salesType = "ExportValue";
            PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
        }

        private void OnPurchaseValueEditImageClicked(object sender, EventArgs e)
        {
            salesType = "PurchaseValue";
            PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
        }

        private void OnCapitalAmountEditImageClicked(object sender, EventArgs e)
        {
            salesType = "CapitalAmount";
            PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
        }

        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            string PostOperationID = viewModel.GetConfirmOperationId();
            if (viewModel.IsCurrentZAKATTaxLess)
            {
                if (App.IsArabic)
                {
                    var result = await this.DisplayAlert(AppResources.Alerts, AppResources.ZZDeartaxpayerbasedonthesubmittedamendments, AppResources.ZZCancel, AppResources.ZZZOkayText);
                    if (!result)
                    {
                        await viewModel.ConfirmClicked(PostOperationID);
                    }

                }
                else
                {
                    var result = await this.DisplayAlert(AppResources.Alerts, AppResources.ZZDeartaxpayerbasedonthesubmittedamendments, AppResources.ZZZOkayText, AppResources.ZZCancel);
                    if (result)
                    {
                        await viewModel.ConfirmClicked(PostOperationID);
                    }

                }
            }
            else
            {
                await viewModel.ConfirmClicked(PostOperationID);
            }


        }

        private void OnEstimatedSalesForTheFiscalYearInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZEstimatedSalesInformationText));
        }

        private void OnZAKATAmountInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZakatBaseandwithalowerboundof500SAR));
        }

        private void OnZAKATBaseAmountInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGreatervalueofEstimatedSales));
        }

        private void OnCapitalAmountInformationClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZCapitalamountasperMCIrecordsMOMRArecordsoranyothersourcethatassisttoidentifythecapitalamount));
        }


        private void OnTotalVATSalesInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZTotalsalesinVATreturns));
        }

        private void OnAverageNumberOfLabourInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZAveragenumberoflaborsx6000SAR));
        }

        private void OnImportValueInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZImportsvaluex115));
        }

        //private void OnImportFromPointOfSalesInfoClicked(object sender, EventArgs e)
        //{
        //    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZThesumofsalesthroughpointsofsalecontractsinETIMADplatformthevalueofexports));
        //}

        private void OnContactFromETIMADSystemInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZThesumofsalesthroughpointsofsalecontractsinETIMADplatformthevalueofexports));
        }

        //private void OnExportInfoClicked(object sender, EventArgs e)
        //{
        //    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZThesumofsalesthroughpointsofsalecontractsinETIMADplatformthevalueofexports));
        //}

        private void OnPurchaseInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPurchasesvaluex115));
        }

        private void OnCapitalAmountInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZCapitalamountasperMCIrecordsMOMRArecordsoranyothersourcethatassisttoidentifythecapitalamount));
        }


    }
}