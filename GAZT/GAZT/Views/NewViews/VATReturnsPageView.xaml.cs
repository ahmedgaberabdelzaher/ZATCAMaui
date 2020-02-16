using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATReturnsPageView : ContentPage
    {

        #region Variable
        public VATReturnsPageViewModel viewModel;


        #endregion

        #region Property
        #endregion

        #region Constructor
        public VATReturnsPageView(VATDeclaration _vATDeclarationInfo)
        {
            viewModel = App.Locator.AAcknowledgement;
            //Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
            //Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            //Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            //Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            //Resources["searchBarStyleForDeclarationChb"]= App.Current.Resources["GAZTGrayLabelStyleForCaptionFont"];
            //Resources["searchBarStyleForClearificationChb"] = App.Current.Resources["GAZTGrayLabelStyleForCaptionFont"];



            InitializeComponent();


            viewModel = App.Locator.AAcknowledgement;
            this.BindingContext = viewModel;
            SetLTR();

            if (_vATDeclarationInfo.d != null)
            {
                viewModel.VATDeclarationData = _vATDeclarationInfo;
            }
            viewModel.IsFirstTimeGet = true;
            AddNotePageViewModel.NoteString = string.Empty;
            viewModel.IsRefundVisible = false;
            viewModel.IsGetAcknowledgementClicked = false;
            viewModel.IsVisibleDropdownForRefund = false;
            setAllCheckbox(false);
            Attachmentlist.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                //if (sender is ListView lv) lv.SelectedItem = null;
            };


            IntilizeAsync();

            viewModel.IsMainButtonEnabled = false;
            // Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorGreen"];
        }
        #endregion

        #region Method
        public void setAllCheckbox(bool bValue)
        {
            viewModel.IsDeclarationCheckedForInstruction = bValue;
            viewModel.IsDeclarationCheckedForSummary = bValue;
            viewModel.IsCheckedTaxPayerDetailsInfo = bValue;
        }
        private void SetLTR()
        {


            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public async void IntilizeAsync()
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async () =>
            {


                await viewModel.pageLoad();
                if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                {
                    viewModel.ManageEnabledProperty(false);
                    viewModel.IsCheckedTaxPayerDetailsInfo = true;
                    viewModel.ButtonName = AppResources.ZVatDownloadForm;
                    viewModel.IsMainButtonEnabled = false;
                }
                else
                {
                    viewModel.ManageEnabledProperty(true);
                    if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
                    {
                        viewModel.NavigationSetupForDraft();
                    }

                }



                viewModel.ListOfActionButtonsApplicable = new List<string>();
                await viewModel.SetButtons(viewModel.VATDeclarationData);
                if (App.CheckTINStatusPageView != "0045")
                {
                    onPageLoadCalculation();
                }

            });
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });

            //onPageLoadCalculation();
        }


        //private void ClickGestureRecognizer_ClickedForCustomLabel(object sender, EventArgs e)
        //{
        //    Label labelInstrunction = (Label)sender;
        //    if(labelInstrunction.Text== "Instrunction")
        //    {
        //        labelInstrunction.Style = (Style)App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
        //    }
        //    else if(labelInstrunction.Text == "TaxPayer Details")
        //    {
        //        labelInstrunction.Style = (Style)App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
        //    }
        //    else if (labelInstrunction.Text == "VAT Return Form")
        //    {
        //        labelInstrunction.Style = (Style)App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
        //    }
        //    else if (labelInstrunction.Text == "Summary")
        //    {
        //        labelInstrunction.Style = (Style)App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
        //    }


        //}

        //private void OnDeleteAttachmentClicked(object sender, EventArgs e)
        //{
        //    Image arrowImage = sender as Image;
        //    Attachment attachment = (Attachment)arrowImage.BindingContext;
        //    var results =   WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename,  viewModel.VATDeclarationData.d.ReturnIdz);
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (AddNotePageViewModel.IsComingFromNotePage == true && !string.IsNullOrEmpty(AddNotePageViewModel.NoteString))
            {

                if (App.ICRStatus == "E0001")
                {
                    SetNote();
                }
                if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
                {
                    Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                    if (note != null)
                    {
                        foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                        {
                            item.Strline = AddNotePageViewModel.NoteString;
                            item.Tdline = AddNotePageViewModel.NoteString;
                        }
                        AddNotePageViewModel.IsComingFromNotePage = false;
                        AddNotePageViewModel.NoteString = string.Empty;
                    }
                    else
                    {
                        SetNote();
                    }



                }
            }
        }

        public void SetNote()
        {
            viewModel.VATDeclarationData.d.NOTESSet.results = new List<Note>();

            Note objNote = new Note();

            int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
            string Url = Constants.QABaseUrlForODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/NOTESSet('00" + (count + 1).ToString() + "')";
            objNote.__metadata = new Metadata2();
            objNote.__metadata.id = Url;
            objNote.__metadata.uri = Url;
            objNote.__metadata.type = "ZDP_VATR_M_SRV.NOTES";

            objNote.Notenoz = (count + 1).ToString();
            objNote.DataVersionz = "00000";
            objNote.Refnamez = String.Empty;
            objNote.XInvoicez = String.Empty;
            objNote.XObsoletez = string.Empty;
            objNote.Rcodez = "VATR";
            objNote.ByPusrz = string.Empty;
            objNote.Tdformat = string.Empty;
            objNote.Tdline = string.Empty;
            objNote.Erfusrz = viewModel.VATDeclarationData.d.Gpartz;
            objNote.ByGpartz = viewModel.VATDeclarationData.d.Gpartz;
            objNote.Namez = viewModel.VATDeclarationData.d.Tpnm;
            objNote.AttByz = "TP";
            objNote.Noteno = (count + 1).ToString();
            objNote.Lineno = 1;
            objNote.ElemNo = 0;
            objNote.Strdt = string.Empty;
            objNote.Strtime = string.Empty;
            objNote.Sect = "VAT Return General Note";
            objNote.Strline = AddNotePageViewModel.NoteString;
            objNote.Tdline = AddNotePageViewModel.NoteString;
            viewModel.VATDeclarationData.d.NOTESSet.results.Add(objNote);
            AddNotePageViewModel.IsComingFromNotePage = false;
            //AddNotePageViewModel.NoteString = string.Empty;
        }

        private void ClickGestureRecognizer_ClickedForInstructions(object sender, EventArgs e)
        {

            Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];


            Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
        }

        private void ClickGestureRecognizer_ClickedForTPDetails(object sender, EventArgs e)
        {
            Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];

            Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
        }
        private void ClickGestureRecognizer_ClickedForVATReturnForm(object sender, EventArgs e)
        {
            Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];

            Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
        }
        private void ClickGestureRecognizer_ClickedForSummary(object sender, EventArgs e)
        {
            Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];


            Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
        }

        #endregion

        public void OnPageSelected(object sender, SelectionChangedEventArgs e)
        {
            // CollectionView pagename =(CollectionView)sender;




            VATDeclarationTabbedPageName previous = (e.PreviousSelection.FirstOrDefault() as VATDeclarationTabbedPageName);
            VATDeclarationTabbedPageName current = (e.CurrentSelection.FirstOrDefault() as VATDeclarationTabbedPageName);

            //Set the current to the color you want
            //if (current != null)
            //{
            //    current.TextColor = Color.FromHex("#c49b2d");
            //}

            //if (previous != null)
            //{
            //    //Reset the previous to defaulr color
            //    previous.TextColor = Color.FromHex("#FFFFFF");
            //}

            if (current != null)
            {
                if (current.pageName == "Instruction" || current.pageName== "التعليمات")
                {

                    viewModel.InstrunctionClicked();

                    if (viewModel.IsDeclarationCheckedForInstruction && !((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (viewModel.IsAmendClicked == false)))
                    {
                        viewModel.IsMainButtonEnabled = true;
                    }
                    else
                    {
                        viewModel.IsMainButtonEnabled = false;
                    }
                    // BtnNextStep.IsEnabled = false;

                    //if (App.ICRStatus == "E0045")
                    //{
                    //    viewModel.IsDeclarationCheckedForInstruction = true;
                    //    viewModel.IsDeclarationCheckedForSummary = true;
                    //    viewModel.IsCheckedTaxPayerDetailsInfo = true;
                    //    chkDeclaration.IsChecked = true;
                    //}
                    //else
                    //{
                    //    viewModel.IsDeclarationCheckedForInstruction = false;
                    //    chkDeclaration.IsChecked = false;
                    //}
                    setColor(previous, current);
                    viewModel.IsFirstTimeGet = false;
                }
                else if (current.pageName == "TaxPayer Details" || current.pageName== "تفاصيل المكلف")
                {
                    bool value = viewModel.IsCheckedDraftMode();
                    bool Tvalue = viewModel.IsTabbedValid("02");
                    if (value && Tvalue)
                    {
                        if (viewModel.IsFirstTimeGet)
                        {
                            viewModel.IsDeclarationCheckedForInstruction = true;
                        }
                    }
                    if (viewModel.IsDeclarationCheckedForInstruction == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
                    {
                        if (viewModel.IsDeclarationCheckedForInstruction == true)
                        {
                            viewModel.TaxpayerDetailsClicked();
                            setColor(previous, current);
                        }
                    }
                    else
                    {
                        viewModel.IsDeclarationCheckedForInstruction = false;
                        viewModel.IsMainButtonEnabled = false;
                        // BtnNextStep.IsEnabled = false;
                        //  chkClearification.IsChecked = false;
                    }
                    viewModel.IsFirstTimeGet = false;
                }
                else if (current.pageName == "VAT Return Form" || current.pageName== "نموذج الإقرار الضريبي")
                {

                    bool value = viewModel.IsCheckedDraftMode();
                    bool Tvalue = viewModel.IsTabbedValid("03");
                    if (value && Tvalue)
                    {
                        if (viewModel.IsFirstTimeGet)
                        {
                            viewModel.IsDeclarationCheckedForInstruction = true;
                            viewModel.IsCheckedTaxPayerDetailsInfo = true;
                        }
                    }
                    if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
                    {
                        if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true)
                        {
                            viewModel.VATReturnFormClicked();
                            setColor(previous, current);
                        }
                    }
                    viewModel.IsFirstTimeGet = false;
                    //else
                    //{
                    //    viewModel.PageSelectedItem = viewModel.VatTabbledPageList[0];
                    //}
                }
                else if (current.pageName == "Summary" || current.pageName== "ملخص")
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(LabelTotalsalesAmt.Text) && !string.IsNullOrEmpty(LabelTotalsalesAdj.Text))
                        {
                            CheckSixaSixb(Convert.ToDecimal(LabelTotalsalesAmt.Text), Convert.ToDecimal(LabelTotalsalesAdj.Text));
                        }

                        if (!string.IsNullOrEmpty(LabelTotalsalesAmt.Text) && !string.IsNullOrEmpty(LabelTotalpurchaseAmt.Text))
                        {
                            CheckSixaTweveb(Convert.ToDecimal(LabelTotalsalesAmt.Text), Convert.ToDecimal(LabelTotalpurchaseAmt.Text));
                        }

                        if (!string.IsNullOrEmpty(LabelTotalpurchaseAmt.Text) && !string.IsNullOrEmpty(LabelTotalpurchaseAdj.Text))
                        {
                            CheckTweveaTweveb(Convert.ToDecimal(LabelTotalpurchaseAmt.Text), Convert.ToDecimal(LabelTotalpurchaseAdj.Text));
                        }
                        if (!string.IsNullOrEmpty(LabelTotaldueVat.Text) && !string.IsNullOrEmpty(EntryPreperiodcorr.Text))
                        {
                            CheckThirteenaFouteenb(Convert.ToDecimal(LabelTotaldueVat.Text), Convert.ToDecimal(EntryPreperiodcorr.Text));
                        }
                    }
                    catch
                    {

                    }
                    if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
                    {
                        bool value = viewModel.IsCheckedDraftMode();
                        bool Tvalue = viewModel.IsTabbedValid("04");
                        if (value && Tvalue)
                        {
                            if (viewModel.IsFirstTimeGet)
                            {
                                viewModel.IsDeclarationCheckedForInstruction = true;
                                viewModel.IsCheckedTaxPayerDetailsInfo = true;
                            }
                        }
                        if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true)
                        {
                            viewModel.SummaryClicked();
                            setColor(previous, current);
                        }
                    }
                    else
                    {
                        viewModel.IsMainButtonEnabled = false;

                        viewModel.IsMainButtonEnabled = false;
                        chkDeclarationForSummary.IsChecked = false;
                    }
                    viewModel.IsFirstTimeGet = false;
                }

            }

        }

        private void chkDeclaration_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (viewModel.IsDeclarationChecked == false)
            {
                Resources["searchBarStyleForDeclarationChb"] = App.Current.Resources["GAZTGrayLabelStyleForCaptionFont"];
                // BtnNextStep.IsEnabled = false;
            }
            else
            {
                Resources["searchBarStyleForDeclarationChb"] = App.Current.Resources["GAZTGoldLabelStyleForCaptionFont"];
                //  BtnNextStep.IsEnabled = true;
            }

            if (chkDeclaration.IsChecked == true)
            {

                viewModel.IsMainButtonEnabled = false;
                //  BtnNextStep.IsEnabled = true;
            }
            else
            {
                //  BtnNextStep.IsEnabled = false;
            }
        }

        public void setColor(VATDeclarationTabbedPageName previous, VATDeclarationTabbedPageName current)
        {
            if (current != null)
            {
                current.TextColor = Color.FromHex("#c49b2d");
            }

            if (previous != null)
            {
                //Reset the previous to defaulr color
                previous.TextColor = Color.FromHex("#FFFFFF");
            }
        }
        private async void onMoreOptionClicked(object sender, EventArgs e)
        {

            String action = await DisplayActionSheet("ActionSheet",AppResources.ZZCancel, null, viewModel.ListOfActionButtonsApplicable.ToArray());


            if (App.IsArabic)
            {
                ArButtons buttonId = ArButtons.None;
                if (!string.IsNullOrEmpty(action))
                {
                    action = action.Replace(" ", "");
                }
                Enum.TryParse(action, out buttonId);

                switch (buttonId)
                {
                    case ArButtons.إضافةملاحظات:
                        viewModel.VATReturnAddNote();
                        break;
                    case ArButtons.عرضملاحظات:
                        viewModel.VATReturnGetNotes();
                        break;
                    case ArButtons.المرفقات:
                        viewModel.VATViewAttachments();
                        break;
                    case ArButtons.إبطال:
                        await viewModel.VATSetReturnVoidAsync();
                        break;
                    case ArButtons.عادةتعيين:
                        await viewModel.VATReturnResetAsync();
                        break;
                    case ArButtons.تعديل:
                        await viewModel.VATReturnAmendAsync();
                        break;
                    case ArButtons.حفظ:
                        await viewModel.OnSaveDraftClicked();
                        break;
                    default:
                        break;
                }
            }

            else
            {
                Buttons buttonId = Buttons.None;
                if (!string.IsNullOrEmpty(action))
                {
                    action = action.Replace(" ", "");
                }
                Enum.TryParse(action, out buttonId);

                switch (buttonId)
                {
                    case Buttons.CreateNotes:
                        viewModel.VATReturnAddNote();
                        break;
                    case Buttons.DisplayNotes:
                        viewModel.VATReturnGetNotes();
                        break;
                    case Buttons.Attachments:
                        viewModel.VATViewAttachments();
                        break;
                    case Buttons.Void:
                        await viewModel.VATSetReturnVoidAsync();
                        break;
                    case Buttons.Reset:
                        await viewModel.VATReturnResetAsync();
                        break;
                    case Buttons.Amend:
                        await viewModel.VATReturnAmendAsync();
                        break;
                    case Buttons.Save:
                        await viewModel.OnSaveDraftClicked();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAmount(object sender, EventArgs e)
        {
            CheckMandetoryFields();
            viewModel.StdsalesVat = viewModel.StandardRatedSalesVatAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.StdsalesAdj);
            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
            viewModel.TotalsalesVat = viewModel.StdsalesVat;
        }

        private void ClickGestureRecognizer_ClickedForAllAmount(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
        }

        private void ClickGestureRecognizer_ClickedForVatAdjustment(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);

        }

        private void ClickGestureRecognizer_ClickedForVatAmountForPurchase(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
            viewModel.StdpurchasesVat = viewModel.StandardRatedDomesticPurchaseVatAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.StdpurchaseAdj);
            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

        }

        private void ClickGestureRecognizer_ClickedForVatPaidatcustoms(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
            if (viewModel.ResponseVATDeclarationD.TpregFg == "X")
            {
                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
            }
            else
            {
                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);

            }
            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

            //   viewModel.ResponseVATDeclarationD.ImportspaidVat=viewModel.
        }

        private void ClickGestureRecognizer_ClickedForVatAccounted(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
            viewModel.ImportsaccVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ImportsaccAdj);

            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

        }

        private void ClickGestureRecognizer_ClickedForAllPurchaseAmount(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);

        }

        private void ClickGestureRecognizer_ClickedForAllPurchaseAdjustment(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
        }

        private void ClickGestureRecognizer_ClickedForAllPurchaseVatAmount(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
            viewModel.TotalpurchaseVat = viewModel.TotalVatAmount(viewModel.StdpurchasesVat, viewModel.ImportspaidVat, viewModel.ImportsaccVat);
        }

        public async void onPageLoadCalculation()
        {
            viewModel.StdsalesVat = viewModel.StandardRatedSalesVatAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.StdsalesAdj);
            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
            viewModel.TotalsalesVat = viewModel.ResponseVATDeclarationD.StdsalesVat;
            viewModel.StdpurchasesVat = viewModel.StandardRatedDomesticPurchaseVatAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.StdpurchaseAdj);
            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
            if (viewModel.ResponseVATDeclarationD.TpregFg == "X")
            {
                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
            }
            else
            {
                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);

            }
            viewModel.ImportsaccVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ImportsaccAdj);
            viewModel.TotalpurchaseVat = viewModel.TotalVatAmount(viewModel.StdpurchasesVat, viewModel.ImportspaidVat, viewModel.ImportsaccVat);

        }

        private void OnStandardRatedSalesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardRatedSalesAmount;
            String MessageWithPercent = popUp.Message.Replace("5%", viewModel.VATRate002);
            popUp.Message = MessageWithPercent;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnStandardRatedSalesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardRatedSalesAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnPrivateHealthcareAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipPrivateHealthcareAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Link";
            popUp.Link = "https://www.uqn.gov.sa/articles/1515222747471373200/";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnPrivateHealthcareAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipPrivateHealthcareAdjustment;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Link";
            popUp.Link = "https://www.uqn.gov.sa/articles/1515222747471373200/";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnZerorateddomesticsalesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZerorateddomesticsalesAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnZerorateddomesticsalesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZerorateddomesticsalesAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }



        private void OnExportsAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExportsAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnExportsAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExportsAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnExemptAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnStandardrateddomesticpurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatImportsVatPaidatcustomsAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatImportsVatPaidatcustomsAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatZeroRatedPurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZeroratedpurchasesAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatExemptPurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptpurchasesAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatExemptPurchasesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptpurchasesAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatcreditcarriedforwardClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipcreditcarriedforwardfrompreviousperiod;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatNetdueClicked(object sender, EventArgs e)
        {

            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipNetVATdue;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnExemptAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnStandardrateddomesticpurchasesAdjustmentClicked(object sender, EventArgs e)
        {

            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatImportsSubjectToVatAccountedAmountClickedNew(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = "Click here to open FAQ URL";
            popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatImportsSubjectToVatAccountedAdjustmentClickedNew(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatImportsSubjectToVatAccountedVatAmountClickedNew(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATaccountedVatAmount;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatZeroRatedPurchasesAdjustmentClickedNew(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZeroratedpurchasesAdjustment;
            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatcreditcarriedforwardFromPreviousPeriodClicked(object sender, EventArgs e)
        {
            // < 5,000 >
            PopUp popUp = new PopUp();

            popUp.Message = AppResources.ZToolTipCorrectionsfrompreviousperiod;
            String MessageWithPositiveValue = popUp.Message.Replace("<5,000>", viewModel.CorrectionPeriodAmount);
            String MessageWithNegativeValue = popUp.Message.Replace("<-5,000>", MessageWithPositiveValue);
            popUp.Message = MessageWithNegativeValue;

            popUp.IsLinkAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        public void CheckMandetoryFields()
        {
            if (TabVatReturn.IsVisible == true)
            {
                bool IsAllEntered = true;
                if (string.IsNullOrEmpty(EntryVatAmount.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryStdsalesVat.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntrySalesGccAmt.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntrySalesGccAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryZerosalesAmt.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryZerosalesAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryExportsAmt.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryExportsAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryExemptsalesAmt.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryExemptsalesAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryStdpurchaseAmt.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryStdpurchaseAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryStdpurchasesVat.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportspaidAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportspaidVat.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportsaccAmt.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportsaccAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportsaccVat.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryZeropurchaseAmt.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryZeropurchaseAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryPreperiodcorr.Text))
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryCreditVat.Text))
                {
                    IsAllEntered = false;
                }
                //if (string.IsNullOrEmpty(EntryNetdueVat.Text))
                //{
                //    IsAllEntered = false;
                //}
                if (IsAllEntered == false)
                {

                    viewModel.IsMainButtonEnabled = false;
                    // BtnNextStep.IsEnabled = false;
                }
                else
                {
                    // viewModel.IsDeclarationCheckedForInstruction = false;
                    viewModel.IsMainButtonEnabled = true;
                    //  BtnNextStep.IsEnabled = true;
                }
            }
        }

        private void EntryPreperiodcorr_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
        }

        private void EntryCreditVat_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
        }

        private void EntryNetdueVat_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
        }

        private void chkDeclarationForSummary_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (viewModel.IsDeclarationCheckedForSummary != true)
            {
                if (TabSummarry.IsVisible == true)
                {
                    CheckBox CheckSummary = (CheckBox)sender;
                    if (CheckSummary.IsChecked == true)
                    {

                        viewModel.IsMainButtonEnabled = true;
                        //  BtnNextStep.IsEnabled = true;
                    }
                    else
                    {

                        viewModel.IsMainButtonEnabled = false;
                        // BtnNextStep.IsEnabled = false;
                    }
                }
            }
            else
            {
                ValidationsForVATRefund();
            }
        }

        private void chkClearification_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (TabTaxPayerDetails.IsVisible == true)
            {
                CheckBox CheckSummary = (CheckBox)sender;
                if (CheckSummary.IsChecked == true)
                {

                    viewModel.IsMainButtonEnabled = true;
                    //  BtnNextStep.IsEnabled = true;
                }
                else
                {

                    viewModel.IsMainButtonEnabled = false;
                    //  BtnNextStep.IsEnabled = false;
                }
            }
        }

        private void EntryVatAmount_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryVatAmount.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAmount.Text != "." && EntryVatAdjustmentWithSAR.Text != ".")
            {
                CheckOneaOneb(Convert.ToDecimal(EntryVatAmount.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR.Text));

            }
        }

        private void EntryVatAdjustmentWithSAR_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryVatAmount.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAmount.Text != "." && EntryVatAdjustmentWithSAR.Text != ".")
            {
                CheckOneaOneb(Convert.ToDecimal(EntryVatAmount.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR.Text));

            }
        }

        public void CheckOneaOneb(decimal EntryVatAmount, decimal EntryVatAdjustmentWithSAR)
        {


            if (EntryVatAmount == 0 && EntryVatAdjustmentWithSAR > 0)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;

                Pop.Message = AppResources.ZZValidationMessage02_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;

                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }
            else
            {

                string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

                //  decimal PercentageValue = (EntryVatAmount / 100) * Convert.ToDecimal(Percentage);

                if (((Convert.ToDecimal(Percentage) / 100) * EntryVatAmount) + EntryVatAmount < EntryVatAdjustmentWithSAR)
                {
                    PopUp Pop = new PopUp();
                    Pop.IsLinkAvailable = false;

                    Pop.Message = string.Format(AppResources.ZZValidationMessage01_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);

                    PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                }
            }
        }

        private void EntrySalesGccAmt_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && !string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAmt.Text != "." && EntrySalesGccAdj.Text != ".")
            {
                CheckTwoaTwob(Convert.ToDecimal(EntrySalesGccAmt.Text), Convert.ToDecimal(EntrySalesGccAdj.Text));

            }
        }

        private void EntrySalesGccAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && !string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAmt.Text != "." && EntrySalesGccAdj.Text != ".")
            {
                CheckTwoaTwob(Convert.ToDecimal(EntrySalesGccAmt.Text), Convert.ToDecimal(EntrySalesGccAdj.Text));

            }
        }


        public void CheckTwoaTwob(decimal EntrySalesGccAmt, decimal EntrySalesGccAdj)
        {


            if (EntrySalesGccAmt == 0 && EntrySalesGccAdj > 0)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = AppResources.ZZValidationMessage04_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }
            else
            {

                string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

                // decimal PercentageValue = (EntrySalesGccAmt / 100) * Convert.ToDecimal(Percentage);

                if (((Convert.ToDecimal(Percentage) / 100) * EntrySalesGccAmt) + EntrySalesGccAmt < EntrySalesGccAdj)
                {
                    PopUp Pop = new PopUp();
                    Pop.IsLinkAvailable = false;
                    Pop.Message = string.Format(AppResources.ZZValidationMessage03_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                }
            }
        }

        private void EntryZerosalesAmt_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && !string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAmt.Text != "." && EntryZerosalesAdj.Text != ".")
            {
                CheckTwoaTwob(Convert.ToDecimal(EntryZerosalesAmt.Text), Convert.ToDecimal(EntryZerosalesAdj.Text));

            }
        }

        private void EntryZerosalesAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && !string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAmt.Text != "." && EntryZerosalesAdj.Text != ".")
            {
                CheckTwoaTwob(Convert.ToDecimal(EntryZerosalesAmt.Text), Convert.ToDecimal(EntryZerosalesAdj.Text));

            }
        }

        public void CheckThreeaThreeb(decimal EntryZerosalesAmt, decimal EntryZerosalesAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //  decimal PercentageValue = (EntryZerosalesAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * EntryZerosalesAmt) + EntryZerosalesAmt < EntryZerosalesAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage06_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        private void EntryExportsAmt_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && !string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAmt.Text != "." && EntryExportsAdj.Text != ".")
            {
                CheckFouraFourb(Convert.ToDecimal(EntryExportsAmt.Text), Convert.ToDecimal(EntryExportsAdj.Text));

            }
        }

        private void EntryExportsAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && !string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAmt.Text != "." && EntryExportsAdj.Text != ".")
            {
                CheckFouraFourb(Convert.ToDecimal(EntryExportsAmt.Text), Convert.ToDecimal(EntryExportsAdj.Text));

            }
        }

        public void CheckFouraFourb(decimal EntryExportsAmt, decimal EntryExportsAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            // decimal PercentageValue = (EntryExportsAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * EntryExportsAmt) + EntryExportsAmt < EntryExportsAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage08_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        private void EntryExemptsalesAmt_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && !string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAmt.Text != "." && EntryExemptsalesAdj.Text != ".")
            {
                CheckFouraFourb(Convert.ToDecimal(EntryExemptsalesAmt.Text), Convert.ToDecimal(EntryExemptsalesAdj.Text));

            }
        }

        private void EntryExemptsalesAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && !string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAmt.Text != "." && EntryExemptsalesAdj.Text != ".")
            {
                CheckFiveaFiveb(Convert.ToDecimal(EntryExemptsalesAmt.Text), Convert.ToDecimal(EntryExemptsalesAdj.Text));

            }
        }

        public void CheckFiveaFiveb(decimal EntryExemptsalesAmt, decimal EntryExemptsalesAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //  decimal PercentageValue = (EntryExemptsalesAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * EntryExemptsalesAmt) + EntryExemptsalesAmt < EntryExemptsalesAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage10_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        public void CheckSixaSixb(decimal LabelTotalsalesAmt, decimal LabelTotalsalesAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //   decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalsalesAmt) + LabelTotalsalesAmt < LabelTotalsalesAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage11_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        private void EntryStdpurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(LabelTotalsalesAmt.Text) && EntryStdpurchaseAmt.Text != "." && LabelTotalsalesAmt.Text != ".")
            {
                CheckSevenaSixa(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(LabelTotalsalesAmt.Text));

            }
            if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) && EntryStdpurchaseAmt.Text != "." && EntryStdpurchaseAdj.Text != ".")
            {
                CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(EntryStdpurchaseAdj.Text));

            }
        }

        private void EntryStdpurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) && EntryStdpurchaseAmt.Text != "." && EntryStdpurchaseAdj.Text != ".")
            {
                CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(EntryStdpurchaseAdj.Text));

            }


        }

        public void CheckSevenaSixa(decimal EntryStdpurchaseAmt, decimal LabelTotalsalesAmt)
        {


            if (EntryStdpurchaseAmt > LabelTotalsalesAmt)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = AppResources.ZZValidationMessage12_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        public void CheckSevenaSevenb(decimal EntryStdpurchaseAmt, decimal EntryStdpurchaseAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //  decimal PercentageValue = (EntryStdpurchaseAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * EntryStdpurchaseAmt) + EntryStdpurchaseAmt < EntryStdpurchaseAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage13_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        private void EntryZVatAmountWithSAR_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryZVatAmountWithSAR.Text != "." && EntryImportspaidAdj.Text != ".")
            {
                CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR.Text), Convert.ToDecimal(EntryImportspaidAdj.Text));

            }
        }

        private void EntryImportspaidAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryZVatAmountWithSAR.Text != "." && EntryImportspaidAdj.Text != ".")
            {
                CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR.Text), Convert.ToDecimal(EntryImportspaidAdj.Text));

            }
        }

        public void CheckEightaEightb(decimal EntryZVatAmountWithSAR, decimal EntryImportspaidAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //  decimal PercentageValue = (EntryZVatAmountWithSAR / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * EntryZVatAmountWithSAR) + EntryZVatAmountWithSAR < EntryImportspaidAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage14_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        private void EntryImportsaccAmt_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAmt.Text != "." && EntryImportsaccAdj.Text != ".")
            {
                CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt.Text), Convert.ToDecimal(EntryImportsaccAdj.Text));

            }
        }

        private void EntryImportsaccAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAmt.Text != "." && EntryImportsaccAdj.Text != ".")
            {
                CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt.Text), Convert.ToDecimal(EntryImportsaccAdj.Text));

            }
        }

        public void CheckNineaNineb(decimal EntryImportsaccAmt, decimal EntryImportsaccAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //  decimal PercentageValue = (EntryImportsaccAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * EntryImportsaccAmt) + EntryImportsaccAmt < EntryImportsaccAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage15_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        private void EntryZeropurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && !string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAmt.Text != "." && EntryZeropurchaseAdj.Text != ".")
            {
                CheckTenaTenb(Convert.ToDecimal(EntryZeropurchaseAmt.Text), Convert.ToDecimal(EntryZeropurchaseAdj.Text));

            }
        }

        private void EntryZeropurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && !string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAmt.Text != "." && EntryZeropurchaseAdj.Text != ".")
            {
                CheckTenaTenb(Convert.ToDecimal(EntryZeropurchaseAmt.Text), Convert.ToDecimal(EntryZeropurchaseAdj.Text));

            }
        }

        public void CheckTenaTenb(decimal EntryZeropurchaseAmt, decimal EntryZeropurchaseAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //   decimal PercentageValue = (EntryZeropurchaseAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * EntryZeropurchaseAmt) + EntryZeropurchaseAmt < EntryZeropurchaseAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage16_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        private void EntryExemptpurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAmt.Text != "." && EntryExemptpurchaseAdj.Text != ".")
            {
                CheckTenaTenb(Convert.ToDecimal(EntryExemptpurchaseAmt.Text), Convert.ToDecimal(EntryExemptpurchaseAdj.Text));

            }
        }

        private void EntryExemptpurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAmt.Text != "." && EntryExemptpurchaseAdj.Text != ".")
            {
                CheckTenaTenb(Convert.ToDecimal(EntryExemptpurchaseAmt.Text), Convert.ToDecimal(EntryExemptpurchaseAdj.Text));

            }
        }

        public void CheckElevenaElevenb(decimal EntryExemptpurchaseAmt, decimal EntryExemptpurchaseAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //  decimal PercentageValue = (EntryExemptpurchaseAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * EntryExemptpurchaseAmt) + EntryExemptpurchaseAmt < EntryExemptpurchaseAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage17_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        public void CheckSixaTweveb(decimal LabelTotalsalesAmt, decimal LabelTotalpurchaseAmt)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //  decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalsalesAmt) + LabelTotalsalesAmt < LabelTotalpurchaseAmt)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = AppResources.ZZValidationMessage18_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        public void CheckTweveaTweveb(decimal LabelTotalpurchaseAmt, decimal LabelTotalpurchaseAdj)
        {
            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

            //  decimal PercentageValue = (LabelTotalpurchaseAmt / 100) * Convert.ToDecimal(Percentage);

            if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalpurchaseAmt) + LabelTotalpurchaseAmt < LabelTotalpurchaseAdj)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = string.Format(AppResources.ZZValidationMessage19_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }

        }

        public void CheckThirteenaFouteenb(decimal LabelTotaldueVat, decimal EntryPreperiodcorr)
        {
            try
            {
                string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();

                //  decimal PercentageValue = (LabelTotaldueVat / 100) * Convert.ToDecimal(Percentage);

                if (((Convert.ToDecimal(Percentage) / 100) * LabelTotaldueVat) + LabelTotaldueVat < EntryPreperiodcorr)
                {
                    //PopUp Pop = new PopUp();
                    //Pop.IsLinkAvailable = false;
                    //Pop.Message = string.Format(AppResources.ZZValidationMessage20_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                }
            }
            catch
            {

            }

        }

        private void EntryPreperiodcorr_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryPreperiodcorr.Text) && EntryPreperiodcorr.Text != ".")
                {
                    string MinValue = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "001").Select(x => x.MinVal).FirstOrDefault();
                    string MaxValue = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "001").Select(x => x.MaxVal).FirstOrDefault();
                    if (Convert.ToDecimal(EntryPreperiodcorr.Text) <= Convert.ToDecimal(MinValue) || Convert.ToDecimal(EntryPreperiodcorr.Text) >= Convert.ToDecimal(MaxValue))
                    {
                        viewModel._dialogService.ShowMessage(string.Format(AppResources.ZZGeneralMessage_IfCorrectionsGreaterThanEqualToMAxValueAndLessThanEqualToMinValue, MaxValue, MinValue), AppResources.Information);
                    }
                }

            }
            catch
            {

            }
        }


        private void Iban_Changed(object sender, TextChangedEventArgs e)
        {
            bool a = false;
            string allowedchar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (!string.IsNullOrEmpty(viewModel.IbanNumberText))
            {
                if (!viewModel.IbanNumberText.All(allowedchar.Contains))
                {
                    viewModel.IbanNumberText = viewModel.IbanNumberText.Remove(viewModel.IbanNumberText.Length - 1);
                }
                else
                {



                    if (viewModel.IbanNumberText.Length > 24)
                    {
                        viewModel.IbanNumberText = viewModel.IbanNumberText.Remove(viewModel.IbanNumberText.Length - 1);
                    }


                    a = UtilityManager.IsIBANValid(viewModel.IbanNumberText);

                }
                viewModel.IsIBANValid = a;
            }
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }





        private void onDropdownButtonClicked(object sender, EventArgs e)
        {

        }

        public void ValidationsForVATRefund()
        {
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    if (viewModel.IsCheckedRefund == true)
                    {
                        if (!string.IsNullOrEmpty(viewModel.IbanNumberText) && viewModel.SelectedIBANType != null && viewModel.SelectedIBANIDNumber != null && viewModel.IsDeclarationCheckedForSummary != false)
                        {
                            viewModel.IsMainButtonEnabled = true;
                        }
                        else
                        {
                            viewModel.IsMainButtonEnabled = false;
                        }
                    }
                    else
                    {
                        if (viewModel.SelectedIBAN != null && viewModel.SelectedIBANType != null && viewModel.SelectedIBANIDNumber != null && viewModel.IsDeclarationCheckedForSummary != false)
                        {
                            viewModel.IsMainButtonEnabled = true;
                        }
                        else
                        {
                            viewModel.IsMainButtonEnabled = false;
                        }
                    }
                }
            }
        }

        private void BPicker2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }

        private void BPicker1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }

        private void BPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }

        private void Unfocused_IBAN(object sender, FocusEventArgs e)
        {
            try
            {
                var response = WebServiceManager.GAZTCheckIBAN(viewModel.IbanNumberText);
                if (response != null)
                {
                    viewModel.IsIBANValid = true;
                }
                else
                {
                    viewModel.IsIBANValid = false;
                }
            }
            catch (Exception ex)
            {
                viewModel.IsIBANValid = false;
            }

        }


    }
    //private void ICvalidation_Clicked(object sender, EventArgs e)
    //{
    //   //if(TabInstruction.IsVisible==true)
    //   // {
    //   //     if(chkDeclaration.IsChecked==false)
    //   //     {
    //   //         Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorRed"];
    //   //     }
    //   //     else
    //   //     {
    //   //         Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorGreen"];

    //   //     }
    //   // }
    //   //if(TabTaxPayerDetails.IsVisible==true)
    //   // {
    //   //     if (chkClearification.IsChecked == false)
    //   //     {
    //   //         Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorRed"];
    //   //     }
    //   //     else
    //   //     {
    //   //         Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorGreen"];

    //   //     }

    //   // }
    //}

}



