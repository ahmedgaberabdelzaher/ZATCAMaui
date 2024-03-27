

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Behaviors;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages
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
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            viewModel.ClearData();
            ElevenDotTwoDecimalPlacesAndNoNegativeValue.IsValiedNumber = true;
            if (AttachmentPopUpViewModel.SalesDetailList != null)
                AttachmentPopUpViewModel.SalesDetailList.Clear();
            IsGoingFirstTimeOnAttachmentPage = true;
            NavigationPage.SetBackButtonTitle(this, " ");
            TotalVATSales.Text = "NA";

            ChangeAeroIcon();
            ZAKATReturnDetailsViewModel.Fbguid = fbguid;
            IsComingFromAttachmentPage = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //  viewModel.ClearData();
            //            viewModel.DesClaimerVisibility = false;
            try
            {
                MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToReleaseTheReturn");
                MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToAmendheReturn");
                MessagingCenter.Unsubscribe<object, string>(this, "Card_Payment");
                MessagingCenter.Unsubscribe<object, string>(this, "Apple_Pay");
                MessagingCenter.Unsubscribe<object, string>(this, "SADAD");
                MessagingCenter.Unsubscribe<App, string>(this, "ApplePayData");

            }
            catch (Exception)
            {
                scrollView.ScrollToAsync(0, 500, true);



            }

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            //date.Text = viewModel.Abrzu;
            if (IsComingFromAttachmentPage == false)
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
                TotalVATSalesInputLayout.ContainerBackground = (Color)Application.Current.Resources["White"];
                AverageNumberOfLabourInputLayout.ContainerBackground = (Color)Application.Current.Resources["ContainerBackgroundColor"];
                ImportValueInputLayout.ContainerBackground = (Color)Application.Current.Resources["ContainerBackgroundColor"];
                SalesFromPointOfSalesInputLayout.ContainerBackground = (Color)Application.Current.Resources["ContainerBackgroundColor"];
                ContractFromETIMADSystemInputLayout.ContainerBackground = (Color)Application.Current.Resources["ContainerBackgroundColor"];
                ExportValueInputLayout.ContainerBackground = (Color)Application.Current.Resources["ContainerBackgroundColor"];
                PurchaseValueInputLayout.ContainerBackground = (Color)Application.Current.Resources["ContainerBackgroundColor"];

                TVSA.IsVisible = true;
                TVSNA.IsVisible = false;



                ANOLA.IsVisible = false;
                ANOLNA.IsVisible = true;

                IVA.IsVisible = false;
                IVNA.IsVisible = true;

                POSA.IsVisible = false;
                POSNA.IsVisible = true;

                ETIMADA.IsVisible = false;
                ETIMADNA.IsVisible = true;

                EVA.IsVisible = false;
                EVNA.IsVisible = true;

                PVA.IsVisible = false;
                REVA.IsVisible = false;
                PVNA.IsVisible = true;
                REVNA.IsVisible = true;



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
            }
            else
            {
                TotalVATSalesInputLayout.ContainerBackground = (Color)Application.Current.Resources["ContainerBackgroundColor"];
                //TotalVATSales.Text = "ABC";// AppResources.ZNA;

                AverageNumberOfLabourInputLayout.ContainerBackground = (Color)Application.Current.Resources["White"];
                ImportValueInputLayout.ContainerBackground = (Color)Application.Current.Resources["White"];
                SalesFromPointOfSalesInputLayout.ContainerBackground = (Color)Application.Current.Resources["White"];
                ContractFromETIMADSystemInputLayout.ContainerBackground = (Color)Application.Current.Resources["White"];
                ExportValueInputLayout.ContainerBackground = (Color)Application.Current.Resources["White"];
                PurchaseValueInputLayout.ContainerBackground = (Color)Application.Current.Resources["White"];
                RealEstateValueInputLayout.ContainerBackground = (Color)Application.Current.Resources["White"];

                TVSA.IsVisible = false;
                TVSNA.IsVisible = true;


                ANOLA.IsVisible = true;
                ANOLNA.IsVisible = false;

                IVA.IsVisible = true;
                IVNA.IsVisible = false;

                POSA.IsVisible = true;
                POSNA.IsVisible = false;

                ETIMADA.IsVisible = true;
                ETIMADNA.IsVisible = false;

                EVA.IsVisible = true;
                EVNA.IsVisible = false;

                PVA.IsVisible = true;
                REVA.IsVisible = true;
                PVNA.IsVisible = false;
                REVNA.IsVisible = false;
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



            }
            getYesCommandToReleaseTheReturn();
            getYesCommandToAmendTheReturn();

            getNoCommand();


            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Card_Payment", async (sender, arg) =>
                {
                    viewModel.MadaPaymentSelectedAsync();
                });
            }
            catch (Exception)
            {


            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Apple_Pay", async (sender, arg) =>
                {
                    viewModel.ApplePaySelected();
                });
            }
            catch (Exception)
            {


            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "SADAD", async (sender, arg) =>
                {
                    viewModel.gotoSuccessPage();
                });
            }
            catch (Exception)
            {


            }

            try
            {
                MessagingCenter.Subscribe<App, string>(this, "ApplePayData", async (sender, arg) =>
                {

                    viewModel.ApplePayTokenData = arg.ToString();

                    await viewModel.UpdateApplePayPaymentGuid();


                });

            }
            catch (Exception)
            {


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



        }

        public void getYesCommandToReleaseTheReturn()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToReleaseTheReturn", async (sender, arg) =>
                {
                    await viewModel.OnReleaseOrBillsClicked();
                });
            }
            catch (Exception)
            {

            }
        }

        public void getYesCommandToAmendTheReturn()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToAmendheReturn", async (sender, arg) =>
                {
                    string PostOperationID = viewModel.GetConfirmOperationId();
                    await viewModel.ConfirmClicked(PostOperationID);

                });
            }
            catch (Exception)
            {

            }
        }

        public void getNoCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoReceived", (sender, arg) =>
                {
                    //await PopupNavigation.Instance.PopAsync();
                    //await viewModel.VATSetReturnVoidAsync();

                });
            }
            catch (Exception)
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

        }

        private async void OnAmendClick(object sender, EventArgs e)
        {
            viewModel.SetLayoutVisibilityAfterTappingOnAmendButton();

            //await scrollView.ScrollToAsync(0, ScrollToPosition.Start, true);
            ZAKATReturnDetailsViewModel.IsBillsButtonTapped = false;
            await scrollView.ScrollToAsync(0, (double)ScrollToPosition.Start, true);



        }

        private void OnBillsDetailsClicked(object sender, EventArgs e)
        {

            MainThread.BeginInvokeOnMainThread(() =>
            {
                App.ZakatReturnBilldetails = true;
                viewModel._navigationService.NavigateTo(App.ZakatReturnDetailsSuccessfullPageView, viewModel.ZakatReturnDetail);

            });

        }

        private void OnTotalVATSaleEditImageClicked(object sender, EventArgs e)
        {
            if (viewModel.isThresholdValueLessThanTotalVATSales)
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
        private void OnRealEstateValueEditImageClicked(object sender, EventArgs e)
        {
            salesType = "RealEstateValue";
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
                   
                }
                else
                {
                    string PostOperationID = viewModel.GetConfirmOperationId();
                    await viewModel.ConfirmClicked(PostOperationID);
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit));

                    // await viewModel._dialogService.ShowMessageBox(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit, AppResources.Alerts);
                });

            }



        }

        private void OnEstimatedSalesForTheFiscalYearInfoClicked(object sender, EventArgs e)
        {
            if (viewModel.IsRealEstateViewVisible == false)
            {
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZEstimatedSalesInformationText));
            }
            else
            {
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZEstimatedSalesInformationTextWithRealEstate));
            }
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
        private void OnRealEstateInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZRealEstateWithinPeriod));
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
                if (!string.IsNullOrEmpty(TotalVATSales.Text) && TotalVATSales.Text.Contains(","))
                {
                    TotalVATSales.Text = TotalVATSales.Text.Replace(",", "");
                    TotalVATSales.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(TotalVATSales.Text) && TotalVATSales.Text.Contains(","))
                {
                    TotalVATSales.Text = UtilityManager.GetCommaSeparatedAmount(TotalVATSales.Text);
                    TotalVATSales.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(AverageNumberOfLabour.Text) && AverageNumberOfLabour.Text.Contains(","))
                {
                    AverageNumberOfLabour.Text = AverageNumberOfLabour.Text.Replace(",", "");
                    AverageNumberOfLabour.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(AverageNumberOfLabour.Text))
                {
                    AverageNumberOfLabour.Text = UtilityManager.GetCommaSeparatedAmount(AverageNumberOfLabour.Text);
                    AverageNumberOfLabour.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(ImportValue.Text) && ImportValue.Text.Contains(","))
                {
                    ImportValue.Text = ImportValue.Text.Replace(",", "");

/* Unmerged change from project 'ZATCAMAUI (net7.0-ios)'
Before:
                    ImportValue.TextColor = (Color)App.Current.Resources["Primary"];;
After:
                    ImportValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"];;
*/
                    ImportValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(ImportValue.Text))
                {
                    ImportValue.Text = UtilityManager.GetCommaSeparatedAmount(ImportValue.Text);

/* Unmerged change from project 'ZATCAMAUI (net7.0-ios)'
Before:
                    ImportValue.TextColor = (Color)App.Current.Resources["Primary"];;
After:
                    ImportValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"];;
*/
                    ImportValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(SalesFromPointOfSales.Text) && SalesFromPointOfSales.Text.Contains(","))
                {
                    SalesFromPointOfSales.Text = SalesFromPointOfSales.Text.Replace(",", "");
                    SalesFromPointOfSales.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(SalesFromPointOfSales.Text))
                {
                    SalesFromPointOfSales.Text = UtilityManager.GetCommaSeparatedAmount(SalesFromPointOfSales.Text);
                    SalesFromPointOfSales.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(ContractFromETIMADSystem.Text) && ContractFromETIMADSystem.Text.Contains(","))
                {
                    ContractFromETIMADSystem.Text = ContractFromETIMADSystem.Text.Replace(",", "");
                    ContractFromETIMADSystem.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(ContractFromETIMADSystem.Text))
                {
                    ContractFromETIMADSystem.Text = UtilityManager.GetCommaSeparatedAmount(ContractFromETIMADSystem.Text);
                    ContractFromETIMADSystem.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(ExportValue.Text) && ExportValue.Text.Contains(","))
                {
                    ExportValue.Text = ExportValue.Text.Replace(",", "");
                    ExportValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(ExportValue.Text))
                {
                    ExportValue.Text = UtilityManager.GetCommaSeparatedAmount(ExportValue.Text);
                    ExportValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(PurchaseValue.Text) && PurchaseValue.Text.Contains(","))
                {
                    PurchaseValue.Text = PurchaseValue.Text.Replace(",", "");
                    PurchaseValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(PurchaseValue.Text))
                {
                    PurchaseValue.Text = UtilityManager.GetCommaSeparatedAmount(PurchaseValue.Text);
                    PurchaseValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
            {
            }
        }
        private void RealEstateValueInputFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (RealEstateValue.Text == "0.00")
                {
                    RealEstateValue.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(RealEstateValue.Text) && RealEstateValue.Text.Contains(","))
                {
                    RealEstateValue.Text = RealEstateValue.Text.Replace(",", "");
                    RealEstateValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
            {


            }
        }

        private void RealEstateValueUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(RealEstateValue.Text))
                {
                    RealEstateValue.Text = "0.00";
                }
                if (!string.IsNullOrEmpty(RealEstateValue.Text))
                {
                    RealEstateValue.Text = UtilityManager.GetCommaSeparatedAmount(RealEstateValue.Text);
                    RealEstateValue.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(CapitalAmount.Text) && CapitalAmount.Text.Contains(","))
                {
                    CapitalAmount.Text = CapitalAmount.Text.Replace(",", "");
                    CapitalAmount.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
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
                if (!string.IsNullOrEmpty(CapitalAmount.Text))
                {
                    CapitalAmount.Text = UtilityManager.GetCommaSeparatedAmount(CapitalAmount.Text);
                    CapitalAmount.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
                }
            }
            catch (Exception)
            {
            }
        }

        protected void OnCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            viewModel.CheckBoxStatus = checkBox.IsChecked;
        }

        private void PayNowButtonClicked(object sender, EventArgs e)
        {

        }
    }
}