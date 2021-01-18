using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AmendSalesDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SalesDetailsPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.SalesDetailsView
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesDetailsPageView : ContentPage
    {
        #region Variable
        SalesDetailsPageViewModel viewModel;
        ZakatReturnDetailsD ZakatReturnDetail = null;
        string fbNum;
        int selectedIndex = -1;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public SalesDetailsPageView(ZakatReturnDetails ZakatReturnDetail)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            try
            {
                viewModel = App.Locator.SalesDetailsPageView;
                viewModel.zakatReturnDetailsD = ZakatReturnDetail;
                viewModel.zakatReturnDetailsDToCompare = ZakatReturnDetail;
                SalesDetailsPageViewModel.RetGuid = ZakatReturnDetail.d.ReturnIdz;
                fbNum = ZakatReturnDetail.d.Fbnum;
               // AmendSalesDetailsPageViewModel.SelectedSalesDetails = new SalesDetails();
                //viewModel.EstimatedZAKATSADADNumber.ObjectionInvoiceVisibility = false;
                //viewModel.EstimatedZAKATSADADNumber.AmendInvoiceVisibility = false;
                SetLTR();
                viewModel.ClearData();
                viewModel.onPageLoad();
                viewModel.ZakatReturnDetail = ZakatReturnDetail;
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                ChangeAeroIcon();
            }
            catch(Exception ex)
            {
            }
            this.BindingContext = viewModel;
          //  viewModel.HideInvoicePopUp();
            SalesDetails.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };
        }
        #endregion
        #region Method
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
            try
            {
                if (AmendSalesDetailsPageViewModel.SelectedSalesDetails != null && AmendSalesDetailsPageViewModel.SelectedSalesDetails.ComingFromAmendEditMode && AmendSalesDetailsPageViewModel.IsSaveButtonPressed)
                {
                    AmendSalesDetailsPageViewModel.IsSaveButtonPressed = false;
                    SetUpdatedDataToObject();
                    viewModel.SetUpdatedDataToZAKATEstimated(selectedIndex);
                   // UpdateTheAttachmentPostData();
                   //viewModel.SetChangedDataToTheList();
                    //viewModel.SetChangedDataToTheList(selectedIndex);
                    //   viewModel.SetChangedValueToUploadAttachment();//Called to Highlight the required document
                }
            }
            catch(Exception ex)
            {
            }
        }
        private void SetUpdatedDataToObject()
        {
            try
            {
                int index;
                if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("1"))
                {
                     index = 0;
                    // viewModel.zakatReturnDetailsD.d.TvtslE = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[0].InformationFromPartieToCompare = viewModel.SalesDetailsList[0].NewValue = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue == "" ? viewModel.SalesDetailsList[index].InformationFromPartie : AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;// AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[0].ChangeReason = AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason;
                    viewModel.SalesDetailsList[0].estimateZakatAttachment = AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment;
                    viewModel.SalesDetailsList[0].IsOldValueChanged = IsOldValueChanged(index);
                    selectedIndex = 0;
                    //IsChangeReasonEntered(index);
                    //IsAttachmentAttached(index);
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("2"))
                {
                    index = 1;
                    //   viewModel.zakatReturnDetailsD.d.LabnoE = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[1].InformationFromPartieToCompare = viewModel.SalesDetailsList[1].NewValue = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue == "" ? viewModel.SalesDetailsList[index].InformationFromPartie : AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;// AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[1].ChangeReason = AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason;
                    viewModel.SalesDetailsList[1].estimateZakatAttachment = AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment;
                    viewModel.SalesDetailsList[1].IsOldValueChanged = IsOldValueChanged(index);
                    selectedIndex = 1;
                    //IsChangeReasonEntered(index);
                    //IsAttachmentAttached(index);
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("3"))
                {
                    index = 2;
                    // viewModel.zakatReturnDetailsD.d.ImpvalE = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[2].InformationFromPartieToCompare = viewModel.SalesDetailsList[2].NewValue = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue == "" ? viewModel.SalesDetailsList[index].InformationFromPartie : AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;// AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[2].ChangeReason = AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason;
                    viewModel.SalesDetailsList[2].estimateZakatAttachment = AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment;
                    viewModel.SalesDetailsList[2].IsOldValueChanged = IsOldValueChanged(index);
                    selectedIndex = 2;
                    //IsChangeReasonEntered(index);
                    //IsAttachmentAttached(index);
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("4"))
                {
                    index = 3;
                    // viewModel.zakatReturnDetailsD.d.TvtslResn = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[3].InformationFromPartieToCompare  = viewModel.SalesDetailsList[3].NewValue = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue == "" ? viewModel.SalesDetailsList[index].InformationFromPartie : AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;// AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[3].ChangeReason = AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason;
                    viewModel.SalesDetailsList[3].estimateZakatAttachment = AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment;
                    viewModel.SalesDetailsList[3].IsOldValueChanged = IsOldValueChanged(index);
                    selectedIndex = 3;
                    //IsChangeReasonEntered(index);
                    //IsAttachmentAttached(index);
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("5"))
                {
                    index = 4;
                    //  viewModel.zakatReturnDetailsD.d.Estsl = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[4].InformationFromPartieToCompare = viewModel.SalesDetailsList[4].NewValue = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue == "" ? viewModel.SalesDetailsList[index].InformationFromPartie : AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;// AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[4].ChangeReason = AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason;
                    viewModel.SalesDetailsList[4].estimateZakatAttachment = AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment;
                    viewModel.SalesDetailsList[4].IsOldValueChanged = IsOldValueChanged(index);
                    selectedIndex = 4;
                    //IsChangeReasonEntered(index);
                    //IsAttachmentAttached(index);
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("6"))
                {
                    index = 5;
                    // viewModel.zakatReturnDetailsD.d.ExamtI = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[5].InformationFromPartieToCompare = viewModel.SalesDetailsList[5].NewValue = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue == "" ? viewModel.SalesDetailsList[index].InformationFromPartie : AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue ;
                    viewModel.SalesDetailsList[5].ChangeReason = AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason;
                    viewModel.SalesDetailsList[5].estimateZakatAttachment = AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment;
                    viewModel.SalesDetailsList[5].IsOldValueChanged = IsOldValueChanged(index);
                    selectedIndex = 5;
                    //IsChangeReasonEntered(index);
                    //IsAttachmentAttached(index);
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("7"))
                {
                    index = 6;
                    // viewModel.zakatReturnDetailsD.d.PramtE = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[6].InformationFromPartieToCompare = viewModel.SalesDetailsList[6].NewValue = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue == "" ? viewModel.SalesDetailsList[index].InformationFromPartie : AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;// AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[6].ChangeReason = AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason;
                    viewModel.SalesDetailsList[6].estimateZakatAttachment = AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment;
                    viewModel.SalesDetailsList[6].IsOldValueChanged = IsOldValueChanged(index);
                    selectedIndex = 6;
                    //IsChangeReasonEntered(index);
                    //IsAttachmentAttached(index);
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("8"))
                {
                    index = 7;
                    viewModel.SalesDetailsList[7].InformationFromPartieToCompare = viewModel.SalesDetailsList[7].NewValue = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue == "" ?viewModel.SalesDetailsList[index].InformationFromPartie : AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;// AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                    viewModel.SalesDetailsList[7].ChangeReason = AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason;
                    viewModel.SalesDetailsList[7].estimateZakatAttachment = AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment;
                    viewModel.SalesDetailsList[7].IsOldValueChanged = IsOldValueChanged(index);
                    selectedIndex = 7;
                    //IsChangeReasonEntered(index);
                    //IsAttachmentAttached(index);
                    // Missing need to check and assign the value
                    ///viewModel.zakatReturnDetailsD.TvtslResn = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
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
        //protected async void OnRefreshButtonClicked(Object sender, EventArgs e)
        //{
        //    //  viewModel.CheckBoxStatus = checkBox.IsChecked;
        //   // await viewModel.GetSADADNumber();
        //}
        protected async void OnCloseButtonClicked(Object sender, EventArgs e)
         {
            viewModel.InvoicePopUpVisibility = false;
            viewModel._navigationService.GoBack();
        }
        protected async void OnOnInvoiceClicked(Object sender, EventArgs e)
        {
            //viewModel.InvoicePopUpVisibility = false;
            //viewModel._navigationService.GoBack();
        }
        protected async void OnEditImageClicked(Object sender, EventArgs e)
        {
            Image EditImage = sender as Image;
            SalesDetails selectedSalesDetails = (SalesDetails)EditImage.BindingContext;
            AmendSalesDetailsPageViewModel.fbNum = fbNum;
            if (!viewModel.ConfirmButtonVisibility)
            {
                double d = Convert.ToDouble(viewModel.zakatReturnDetailsD.d.TvtslI);
                double d1 = Convert.ToDouble(viewModel.zakatReturnDetailsD.d.ThresholdSet.results[0].Value);
                bool IsThresholdGreaterLessVATAmount = d1 < d;
                if (Convert.ToDouble(viewModel.zakatReturnDetailsD.d.TvtslI) > Convert.ToDouble(viewModel.zakatReturnDetailsD.d.ThresholdSet.results[0].Value))
                {
                    if ((selectedSalesDetails != null) && (viewModel.SubmitButtonVisibility))
                    {
                        if (selectedSalesDetails.SalesType.Equals("Total VAT Sales") || selectedSalesDetails.SalesType.Equals("إجمالي مبيعات القيمة المضافة"))
                        {
                            viewModel._navigationService.NavigateTo(App.AmendSalesDetailsPageView, selectedSalesDetails);
                        }
                    }
                }
                else
                {
                    if ((selectedSalesDetails != null) && (viewModel.SubmitButtonVisibility))
                    {
                        if (selectedSalesDetails  != null && !selectedSalesDetails.SalesType.Equals("Total VAT Sales") || selectedSalesDetails.SalesType.Equals("إجمالي مبيعات القيمة المضافة"))
                        {
                            viewModel._navigationService.NavigateTo(App.AmendSalesDetailsPageView, selectedSalesDetails);
                        }
                    }
                }
            }
            else
            {
                viewModel.ShowOnlyInfoIcon();
            }
        }
        protected async void OnConfirmButtonClicked(Object sender, EventArgs e)
        {
            if(viewModel.IsCurrentZAKATTaxLess)
            {
                //var result = await this.DisplayAlert(AppResources.Alerts, AppResources.ZZDeartaxpayerbasedonthesubmittedamendments, AppResources.ZZZOkayText, AppResources.ZZCancel);
                //if (result)
                //{
                //    await viewModel.OnConfirmClicked("S");//Passing S if Existing ZAKAT is greater than new one 
                //}


                if (App.IsArabic)
                {
                    var result = await this.DisplayAlert(AppResources.Alerts, AppResources.ZZDeartaxpayerbasedonthesubmittedamendments, AppResources.ZZCancel, AppResources.ZZZOkayText);
                    if (!result)
                    {
                        await viewModel.OnConfirmClicked("S");//Passing S if Existing ZAKAT is greater than new one 
                    }

                }
                else
                {
                    var result = await this.DisplayAlert(AppResources.Alerts, AppResources.ZZDeartaxpayerbasedonthesubmittedamendments, AppResources.ZZZOkayText, AppResources.ZZCancel);
                    if (result)
                    {
                        await viewModel.OnConfirmClicked("S");//Passing S if Existing ZAKAT is greater than new one 
                    }

                }

            }
            else
            {
                await viewModel.OnConfirmClicked("I");
            }
        }
        private void OnInformationMessageClicked(object sender, EventArgs e)
        {
            Label InfoImage = sender as Label;
            SalesDetails estimateZakatAttachment = (SalesDetails)InfoImage.BindingContext;
          string informationMessage =   GetInformationMessage(Convert.ToInt32(estimateZakatAttachment.SelectedEditFieldId));
            PopUp popUp = new PopUp();
            popUp.isFontSet = true;
            popUp.Message = informationMessage;// "Total sales in VAT returns after adjustment during the financial year (excluding any amount under objection, reassessed value but still in the legal period for objection, or penalties";
            popUp.IsLinkAvailable = false;
            if (App.IsArabic)
            {
                popUp.FlowDirections = "RightToLeft";
            }
            else
            {
                popUp.FlowDirections = "LeftToRight";
            }
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }
        private void OnEstmatedSalesInfoMessageClicked(object sender, EventArgs e)
        {
            string informationMessage = AppResources.ZZEstimatedSalesInformationText;
            PopUp popUp = new PopUp();
            popUp.Message = informationMessage;// "Total sales in VAT returns after adjustment during the financial year (excluding any amount under objection, reassessed value but still in the legal period for objection, or penalties";
            popUp.IsLinkAvailable = false;
            if (App.IsArabic)
            {
                popUp.FlowDirections = "RightToLeft";
            }
            else
            {
                popUp.FlowDirections = "LeftToRight";
            }
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }
        private bool IsOldValueChanged(int index)
        {
            if(viewModel.SalesDetailsList[index].InformationFromPartie.Equals(viewModel.SalesDetailsDataList[index].InformationFromPartieToCompare))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        //public void IsChangeReasonEntered(int SalesDetailIndex)
        //{
        //    if(AmendSalesDetailsPageViewModel.SelectedSalesDetails.IsReasonRequird)
        //    {
        //        viewModel.SalesDetailsList[SalesDetailIndex].IsReasonRequird = false;
        //            AmendSalesDetailsPageViewModel.SelectedSalesDetails.IsReasonRequird = false;
        //    }
        //}
        //public void IsAttachmentAttached(int SalesDetailIndex)
        //{
        //    if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.IsAttachmentRequired)
        //    {
        //        viewModel.SalesDetailsList[SalesDetailIndex].IsAttachmentRequired = false;
        //        AmendSalesDetailsPageViewModel.SelectedSalesDetails.IsAttachmentRequired = false;
        //    }
        //}
        private string GetInformationMessage(int selectedId)
        {
            string informationMessage = "";
            switch (selectedId)
            {
                case 1:
                    {
                        informationMessage = AppResources.ZZTotalsalesinVATreturns;
                        break;
                    }
                case 2:
                    {
                        informationMessage = AppResources.ZZAveragenumberoflaborsx6000SAR;
                        break;
                    }
                case 3:
                    {
                        informationMessage = AppResources.ZZImportsvaluex115;
                        break;
                    }
                case 4:
                    {
                        informationMessage = AppResources.ZZThesumofsalesthroughpointsofsalecontractsinETIMADplatformthevalueofexports;
                        break;
                    }
                case 5:
                    {
                        informationMessage = AppResources.ZZThesumofsalesthroughpointsofsalecontractsinETIMADplatformthevalueofexports;
                        break;
                    }
                case 6:
                    {
                        informationMessage = AppResources.ZZThesumofsalesthroughpointsofsalecontractsinETIMADplatformthevalueofexports;
                        break;
                    }
                case 7:
                    {
                        informationMessage = AppResources.ZZPurchasesvaluex115;
                        break;
                    }
                case 8:
                    {
                        informationMessage = AppResources.ZZCapitalamountasperMCIrecordsMOMRArecordsoranyothersourcethatassisttoidentifythecapitalamount;
                        break;
                    }
            }
            return informationMessage;
        }
        private void UpdateTheAttachmentPostData()
        {
            if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment.Count > 0)
            {
                for (int i = 0; i < AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment.Count; i++)
                {
                    AmendSalesDetailsPageViewModel.SelectedSalesDetails.estimateZakatAttachment[i].AttBy = String.Empty;
                }
            }
        }
    }
}