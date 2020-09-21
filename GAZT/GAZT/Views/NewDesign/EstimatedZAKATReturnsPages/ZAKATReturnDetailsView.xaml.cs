using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GAZT.Manager;
using GAZT.Models;
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
        public static bool IsComingFromAttachmentPage;
        public ZAKATReturnDetailsView(string fbguid)
        {
            InitializeComponent();
            viewModel = App.Locator.ZAKATReturnDetailsView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            if (AttachmentPopUpViewModel.SalesDetailList != null)
                AttachmentPopUpViewModel.SalesDetailList.Clear();
            IsGoingFirstTimeOnAttachmentPage = true;
           Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, " ");
            TotalVATSales.Text = "NA";

            ChangeAeroIcon();
            ZAKATReturnDetailsViewModel.Fbguid = fbguid;
            SetLTR();
            IsComingFromAttachmentPage = false;

           

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
          //  viewModel.ClearData();
            try
            {
                MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToReleaseTheReturn");
                MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToAmendheReturn");
            }
            catch (Exception ex)
            {

            }
              
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //date.Text = viewModel.Abrzu;
            if(ZAKATReturnDetailsView.IsComingFromAttachmentPage == false)
            {
                await viewModel.OnPageLoad(ZAKATReturnDetailsViewModel.Fbguid);
                viewModel.ZAKATReturnsPagName = AppResources.FORM5ReturnDetails;
            }
            else
            {
                IsComingFromAttachmentPage = false;
            }

            if (viewModel.isThresholdValueLessThanTotalVATSales)
            {
                TotalVATSalesInputLayout.ContainerBackgroundColor = Color.FromHex("#FFFFFF");
                AverageNumberOfLabourInputLayout.ContainerBackgroundColor = Color.FromHex("#F3F3F3");
                ImportValueInputLayout.ContainerBackgroundColor = Color.FromHex("#F3F3F3");
                SalesFromPointOfSalesInputLayout.ContainerBackgroundColor = Color.FromHex("#F3F3F3");
                ContractFromETIMADSystemInputLayout.ContainerBackgroundColor = Color.FromHex("#F3F3F3");
                ExportValueInputLayout.ContainerBackgroundColor = Color.FromHex("#F3F3F3");
                PurchaseValueInputLayout.ContainerBackgroundColor = Color.FromHex("#F3F3F3");

                TotalVATSalesApplicableLayout.IsVisible = true;
                TotalVATSalesNotApplicableLayout.IsVisible = false;

                AverageNumberOfLabourApplicableApplicable.IsVisible = false;
                AverageNumberOfLabourNotApplicableLayout.IsVisible = true;

                ImportValueApplicableLayout.IsVisible = false;
                ImportValueNotApplicableLayout.IsVisible = true;

                SalesFromPointOfSalesNotApplicableLayout.IsVisible = true;
                SalesFromPointOfSalesApplicableLayout.IsVisible = false;

                ContractFromETIMADSystemApplicableLayout.IsVisible = false;
                ContractFromETIMADSystemNotApplicableLayout.IsVisible = true;

                ExportValueApplicableLayout.IsVisible = false;
                ExportValueNotApplicableLayout.IsVisible = true;

                PurchaseValueApplicableLayout.IsVisible = false;
                PurchaseValueNotApplicableLayout.IsVisible = true;

                TotalVATSalesInformationFromRelatedPartiesLabel.Text = viewModel.ZakatReturnDetail.TvtslI;
                TotalVATSalesEstimatedSalesLabel.Text = viewModel.ZakatReturnDetail.TvtslE;


                AverageNumberOfLabourInformationFromRelatedPartiesLabel.Text = AppResources.ZNA;
                AverageNumberOfLabourEstmatedSalesLabel.Text = AppResources.ZNA;

                ImportValueInformationFromRelatedPartiesLabel.Text = AppResources.ZNA;
                ImportValueEstimatedSalesLabel.Text = AppResources.ZNA;

                SalesFromPointOfSalesInformationFromRelatedPartiesLabel.Text = AppResources.ZNA;

                ContactFromETIMADSystemInfoInformationFromRelatedPartiesLabel.Text = AppResources.ZNA;
                ContractsFromETIMADSystemEstimatedSalesLabel.Text = AppResources.ZNA;

                PurchaceValueInformationFromRelatedPartiesLabel.Text = AppResources.ZNA;
                PurchaseValueEstmatedSalesLabel.Text = AppResources.ZNA;

                
                //AverageNumberOfLabour.Text = AppResources.ZNA;
                //ImportValue.Text = AppResources.ZNA;
                //SalesFromPointOfSales.Text = AppResources.ZNA;
                //ContractFromETIMADSystem.Text = AppResources.ZNA;
                //ExportValue.Text = AppResources.ZNA;
                //PurchaseValue.Text = AppResources.ZNA;
                //CapitalAmount.Text = AppResources.ZNA;
            }
            else
            {
                TotalVATSalesInputLayout.ContainerBackgroundColor = Color.FromHex("#F3F3F3");
                //TotalVATSales.Text = "ABC";// AppResources.ZNA;
               
                AverageNumberOfLabourInputLayout.ContainerBackgroundColor = Color.FromHex("#FFFFFF");
                ImportValueInputLayout.ContainerBackgroundColor = Color.FromHex("#FFFFFF");
                SalesFromPointOfSalesInputLayout.ContainerBackgroundColor = Color.FromHex("#FFFFFF");
                ContractFromETIMADSystemInputLayout.ContainerBackgroundColor = Color.FromHex("#FFFFFF");
                ExportValueInputLayout.ContainerBackgroundColor = Color.FromHex("#FFFFFF");
                PurchaseValueInputLayout.ContainerBackgroundColor = Color.FromHex("#FFFFFF");

                TotalVATSalesApplicableLayout.IsVisible = false;
                TotalVATSalesNotApplicableLayout.IsVisible = true;

                AverageNumberOfLabourApplicableApplicable.IsVisible = true;
                AverageNumberOfLabourNotApplicableLayout.IsVisible = false;

                ImportValueApplicableLayout.IsVisible = true;
                ImportValueNotApplicableLayout.IsVisible = false;

                SalesFromPointOfSalesNotApplicableLayout.IsVisible = false;
                SalesFromPointOfSalesApplicableLayout.IsVisible = true;

                ContractFromETIMADSystemApplicableLayout.IsVisible = true;
                ContractFromETIMADSystemNotApplicableLayout.IsVisible = false;

                ExportValueApplicableLayout.IsVisible = true;
                ExportValueNotApplicableLayout.IsVisible = false;

                PurchaseValueApplicableLayout.IsVisible = true;
                PurchaseValueNotApplicableLayout.IsVisible = false;

               
                TotalVATSalesInformationFromRelatedPartiesLabel.Text = AppResources.ZNA;
                TotalVATSalesEstimatedSalesLabel.Text = AppResources.ZNA;


                AverageNumberOfLabourInformationFromRelatedPartiesLabel.Text = viewModel.ZakatReturnDetail.LabnoI;
                AverageNumberOfLabourEstmatedSalesLabel.Text = viewModel.ZakatReturnDetail.LabnoE;

                ImportValueInformationFromRelatedPartiesLabel.Text = viewModel.ZakatReturnDetail.ImpvalI;
                ImportValueEstimatedSalesLabel.Text = viewModel.ZakatReturnDetail.ImpvalE;
                SalesFromPointOfSalesInformationFromRelatedPartiesLabel.Text = viewModel.ZakatReturnDetail.PtoslI;
                ContactFromETIMADSystemInfoInformationFromRelatedPartiesLabel.Text = viewModel.ZakatReturnDetail.EtimadI;
                ContractsFromETIMADSystemEstimatedSalesLabel.Text = viewModel.ZakatReturnDetail.Sumcnt;
                ExportValueInformationFromRelatedPartiesLabel.Text = viewModel.ZakatReturnDetail.ExamtI;
                PurchaceValueInformationFromRelatedPartiesLabel.Text = viewModel.ZakatReturnDetail.PramtI;
                PurchaseValueEstmatedSalesLabel.Text = viewModel.ZakatReturnDetail.PramtE;







            }
            getYesCommandToReleaseTheReturn();
            getYesCommandToAmendTheReturn();

            getNoCommand();

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
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

        protected async void OnReleaseBillsButtonClicked(object sender, EventArgs e)
        {
            if (viewModel.ZakatReturnDetails.d.Statusz.Equals("E0001") || viewModel.ZakatReturnDetails.d.Statusz.Equals("IP011"))
            {// Call the Post API to release and if response is true then set the Button Name as bills and after tapping on that user needs to be navigated to Bills page 
                await PopupNavigation.Instance.PushAsync(new ZAKATOkCancelPopUpView(AppResources.ZZDoyouwanttoreleasethedeclaration));

            }
            else
            {
               await viewModel.OnReleaseOrBillsClicked();
            }
            //if (viewModel.ZakatReturnDetails.d.Statusz.Equals("E0001") || viewModel.ZakatReturnDetails.d.Statusz.Equals("IP011"))
            //{
            //    if (App.IsArabic)
            //    {
            //        var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZCancel, AppResources.ZZZOkayText);
            //        if (!result)
            //        {
            //            await viewModel.OnReleaseOrBillsClicked();
            //        }

            //    }
            //    else
            //    {
            //        var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZZOkayText, AppResources.ZZCancel);
            //        if (result)
            //        {
            //            await viewModel.OnReleaseOrBillsClicked();
            //        }

            //    }
            //}
            //else
            //{
            //    await viewModel.OnReleaseOrBillsClicked();
            //}



        }

        public async void getYesCommandToReleaseTheReturn()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToReleaseTheReturn", async (sender, arg) =>
                {
                    await viewModel.OnReleaseOrBillsClicked();
                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void getYesCommandToAmendTheReturn()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToAmendheReturn", async (sender, arg) =>
                {
                    string PostOperationID = viewModel.GetConfirmOperationId();
                    await viewModel.ConfirmClicked(PostOperationID);

                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void getNoCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoReceived", async (sender, arg) =>
                {
                    //await PopupNavigation.Instance.PopAsync();
                    //await viewModel.VATSetReturnVoidAsync();

                });
            }
            catch (Exception ex)
            {

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
            if (viewModel.CheckBoxStatus)
            {


                if (viewModel.IsCurrentZAKATTaxLess)
                {
                    await PopupNavigation.Instance.PushAsync(new ZAKATOkCancelPopUpView(AppResources.ZZDeartaxpayerbasedonthesubmittedamendments));
                    //if (App.IsArabic)
                    //{
                    //    var result = await this.DisplayAlert(AppResources.Alerts, AppResources.ZZDeartaxpayerbasedonthesubmittedamendments, AppResources.ZZCancel, AppResources.ZZZOkayText);
                    //    if (!result)
                    //    {
                    //        await viewModel.ConfirmClicked(PostOperationID);
                    //    }

                    //}
                    //else
                    //{
                    //    var result = await this.DisplayAlert(AppResources.Alerts, AppResources.ZZDeartaxpayerbasedonthesubmittedamendments, AppResources.ZZZOkayText, AppResources.ZZCancel);
                    //    if (result)
                    //    {
                    //        await viewModel.ConfirmClicked(PostOperationID);
                    //    }

                    //}
                }
                else
                {
                    string PostOperationID = viewModel.GetConfirmOperationId();
                    await viewModel.ConfirmClicked(PostOperationID);
                }
            }
            else
            {
                  Device.BeginInvokeOnMainThread(async () => {
                      await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit));

                     // await viewModel._dialogService.ShowMessageBox(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit, AppResources.Alerts);
                    });
               
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

        // Events to handle the Sales Type inputs entry
        private void TotalVATSalesInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (TotalVATSales.Text == "0.00")
                {
                    TotalVATSales.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(TotalVATSales.Text) && TotalVATSales.Text.Contains(","))
                {
                    TotalVATSales.Text = TotalVATSales.Text.Replace(",", "");
                    TotalVATSales.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TotalVATSalesInputUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TotalVATSales.Text))
                {
                    TotalVATSales.Text = "0.00";
                }
                if (!String.IsNullOrEmpty(TotalVATSales.Text) && TotalVATSales.Text.Contains(","))
                {
                    TotalVATSales.Text = UtilityManager.GetCommaSeparatedAmount(TotalVATSales.Text);
                    TotalVATSales.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void AverageNumberOfLabourInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (AverageNumberOfLabour.Text == "0.00")
                {
                    AverageNumberOfLabour.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(AverageNumberOfLabour.Text) && AverageNumberOfLabour.Text.Contains(","))
                {
                    AverageNumberOfLabour.Text = AverageNumberOfLabour.Text.Replace(",", "");
                    AverageNumberOfLabour.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AverageNumberOfLabourInputUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(AverageNumberOfLabour.Text))
                {
                    AverageNumberOfLabour.Text = "0.00";
                }
                if (!String.IsNullOrEmpty(AverageNumberOfLabour.Text) )
                {
                    AverageNumberOfLabour.Text = UtilityManager.GetCommaSeparatedAmount(AverageNumberOfLabour.Text);
                    AverageNumberOfLabour.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        



             private void ImportValueInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (ImportValue.Text == "0.00")
                {
                    ImportValue.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(ImportValue.Text) && ImportValue.Text.Contains(","))
                {
                    ImportValue.Text = ImportValue.Text.Replace(",", "");
                    ImportValue.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ImportValueInputUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ImportValue.Text))
                {
                    ImportValue.Text = "0.00";
                }
                if (!String.IsNullOrEmpty(ImportValue.Text) )
                {
                    ImportValue.Text = UtilityManager.GetCommaSeparatedAmount(ImportValue.Text);
                    ImportValue.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }



        

        


        private void SalesFromPointOfSalesInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (SalesFromPointOfSales.Text == "0.00")
                {
                    SalesFromPointOfSales.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(SalesFromPointOfSales.Text) && SalesFromPointOfSales.Text.Contains(","))
                {
                    SalesFromPointOfSales.Text = SalesFromPointOfSales.Text.Replace(",", "");
                    SalesFromPointOfSales.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SalesFromPointOfSalesUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(SalesFromPointOfSales.Text))
                {
                    SalesFromPointOfSales.Text = "0.00";
                }
                if (!String.IsNullOrEmpty(SalesFromPointOfSales.Text) )
                {
                    SalesFromPointOfSales.Text = UtilityManager.GetCommaSeparatedAmount(SalesFromPointOfSales.Text);
                    SalesFromPointOfSales.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ContractFromETIMADSystemInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (ContractFromETIMADSystem.Text == "0.00")
                {
                    ContractFromETIMADSystem.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(ContractFromETIMADSystem.Text) && ContractFromETIMADSystem.Text.Contains(","))
                {
                    ContractFromETIMADSystem.Text = ContractFromETIMADSystem.Text.Replace(",", "");
                    ContractFromETIMADSystem.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ContractFromETIMADSystemInputFocusedUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ContractFromETIMADSystem.Text))
                {
                    ContractFromETIMADSystem.Text = "0.00";
                }
                if (!String.IsNullOrEmpty(ContractFromETIMADSystem.Text))
                {
                    ContractFromETIMADSystem.Text = UtilityManager.GetCommaSeparatedAmount(ContractFromETIMADSystem.Text);
                    ContractFromETIMADSystem.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ExportValueInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (ExportValue.Text == "0.00")
                {
                    ExportValue.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(ExportValue.Text) && ExportValue.Text.Contains(","))
                {
                    ExportValue.Text = ExportValue.Text.Replace(",", "");
                    ExportValue.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ExportValueUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ExportValue.Text))
                {
                    ExportValue.Text = "0.00";
                }
                if (!String.IsNullOrEmpty(ExportValue.Text) )
                {
                    ExportValue.Text = UtilityManager.GetCommaSeparatedAmount(ExportValue.Text);
                    ExportValue.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void PurchaseValueInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (PurchaseValue.Text == "0.00")
                {
                    PurchaseValue.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(PurchaseValue.Text) && PurchaseValue.Text.Contains(","))
                {
                    PurchaseValue.Text = PurchaseValue.Text.Replace(",", "");
                    PurchaseValue.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void PurchaseValueUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(PurchaseValue.Text))
                {
                    PurchaseValue.Text = "0.00";
                }
                if (!String.IsNullOrEmpty(PurchaseValue.Text) )
                {
                    PurchaseValue.Text = UtilityManager.GetCommaSeparatedAmount(PurchaseValue.Text);
                    PurchaseValue.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void CapitalAmountInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (CapitalAmount.Text == "0.00")
                {
                    CapitalAmount.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(CapitalAmount.Text) && CapitalAmount.Text.Contains(","))
                {
                    CapitalAmount.Text = CapitalAmount.Text.Replace(",", "");
                    CapitalAmount.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void CapitalAmountUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CapitalAmount.Text))
                {
                    CapitalAmount.Text = "0.00";
                }
                if (!String.IsNullOrEmpty(CapitalAmount.Text))
                {
                    CapitalAmount.Text = UtilityManager.GetCommaSeparatedAmount(CapitalAmount.Text);
                    CapitalAmount.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void OnCheckBoxCheckedChanged(Object sender, EventArgs e)
        {
            viewModel.CheckBoxStatus = checkBox.IsChecked;
        }

    }
}