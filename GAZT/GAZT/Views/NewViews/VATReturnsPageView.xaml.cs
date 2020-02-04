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

        private void SetLTR()
        {


            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public async void IntilizeAsync()
        {
            await viewModel.pageLoad();
            viewModel.ListOfActionButtonsApplicable = new List<string>();
            await viewModel.SetButtons(viewModel.VATDeclarationData);

            onPageLoadCalculation();
            
            
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
           if (AddNotePageViewModel.IsComingFromNotePage==true && !string.IsNullOrEmpty(AddNotePageViewModel.NoteString))
            {
               
                if(App.ICRStatus == "E0001")
                {
                    SetNote();
                }
                if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
                {
                    Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                    if(note!=null)
                    {
                        foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                        {
                            item.Strline = AddNotePageViewModel.NoteString;
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

            objNote.Notenoz = "00" + (count + 1).ToString();
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
            objNote.Noteno = "00" + (count + 1).ToString();
           // objNote.Lineno = 0;
            //objNote.ElemNo = 0;
            objNote.Strdt = string.Empty;
            objNote.Strtime = string.Empty;
            objNote.Sect = "VAT Return General Note";
            objNote.Strline = AddNotePageViewModel.NoteString;
            viewModel.VATDeclarationData.d.NOTESSet.results.Add(objNote);
            AddNotePageViewModel.IsComingFromNotePage = false;
            AddNotePageViewModel.NoteString = string.Empty;
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
                if (current.pageName == "Instrunction")
                {
                    viewModel.InstrunctionClicked();
                    setColor(previous, current);
                }
                else if (current.pageName == "TaxPayer Details")
                {
                    if (viewModel.IsDeclarationCheckedForInstruction == true )
                    {
                        viewModel.TaxpayerDetailsClicked();
                        setColor(previous, current);
                    }
                    //else
                    //{
                    //    viewModel.PageSelectedItem = viewModel.VatTabbledPageList[0];
                    //}
                }
                else if (current.pageName == "VAT Return Form")
                {
                    if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true)
                    {
                        viewModel.VATReturnFormClicked();
                        setColor(previous, current);
                    }
                    //else
                    //{
                    //    viewModel.PageSelectedItem = viewModel.VatTabbledPageList[0];
                    //}
                }
                else if (current.pageName == "Summary")
                {
                    if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true)
                    {
                        viewModel.SummaryClicked();
                        setColor(previous, current);
                    }
                    //else
                    //{
                    //    viewModel.PageSelectedItem = viewModel.VatTabbledPageList[0];
                    //}
                }

            }

        }

        private void chkDeclaration_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (viewModel.IsDeclarationChecked == false)
            {
                Resources["searchBarStyleForDeclarationChb"] = App.Current.Resources["GAZTGrayLabelStyleForCaptionFont"];
            }
            else
            {
                Resources["searchBarStyleForDeclarationChb"] = App.Current.Resources["GAZTGoldLabelStyleForCaptionFont"];
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
            
            String  action = await DisplayActionSheet("ActionSheet", "Cancel", null, viewModel.ListOfActionButtonsApplicable.ToArray());


            Buttons buttonId = Buttons.None;
            Enum.TryParse(action, out buttonId);
            
            switch (buttonId)
            {
                case Buttons.Createnotes:
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
                default:
                    break;
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAmount(object sender, EventArgs e)
        {
            viewModel.StdsalesVat = viewModel.StandardRatedSalesVatAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.StdsalesAdj);
            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
            viewModel.TotalsalesVat = viewModel.StdsalesVat;
        }

        private void ClickGestureRecognizer_ClickedForAllAmount(object sender, TextChangedEventArgs e)
        {
            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt,viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
        }

        private void ClickGestureRecognizer_ClickedForVatAdjustment(object sender, TextChangedEventArgs e)
        {
            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);

        }

        private void ClickGestureRecognizer_ClickedForVatAmountForPurchase(object sender, TextChangedEventArgs e)
        {
            viewModel.StdpurchasesVat = viewModel.StandardRatedDomesticPurchaseVatAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.StdpurchaseAdj);
            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

        }

        private void ClickGestureRecognizer_ClickedForVatPaidatcustoms(object sender, TextChangedEventArgs e)
        {
            if(viewModel.ResponseVATDeclarationD.TpregFg=="X")
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
            viewModel.ImportsaccVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ImportsaccAdj);

            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

        }

        private void ClickGestureRecognizer_ClickedForAllPurchaseAmount(object sender, TextChangedEventArgs e)
        {
            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);

        }

        private void ClickGestureRecognizer_ClickedForAllPurchaseAdjustment(object sender, TextChangedEventArgs e)
        {
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
        }

        private void ClickGestureRecognizer_ClickedForAllPurchaseVatAmount(object sender, TextChangedEventArgs e)
        {
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
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnStandardRatedSalesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardRatedSalesAdjustment;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnPrivateHealthcareAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipPrivateHealthcareAmount;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnPrivateHealthcareAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipPrivateHealthcareAdjustment;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnZerorateddomesticsalesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZerorateddomesticsalesAmount;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnZerorateddomesticsalesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZerorateddomesticsalesAdjustment;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

       

        private void OnExportsAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExportsAmount;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnExportsAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExportsAdjustment;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnExemptAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptAdjustment;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnStandardrateddomesticpurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAmount;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatImportsVatPaidatcustomsAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAmount;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatImportsVatPaidatcustomsAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAdjustment;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatZeroRatedPurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZerorateddomesticsalesAmount;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatExemptPurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptpurchasesAmount;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatExemptPurchasesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptpurchasesAdjustment;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatcreditcarriedforwardClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipcreditcarriedforwardfrompreviousperiod;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnVatNetdueClicked(object sender, EventArgs e)
        {

            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipNetVATdue;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnExemptAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptAmount;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }

        private void OnStandardrateddomesticpurchasesAdjustmentClicked(object sender, EventArgs e)
        {

            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAdjustment;
            popUp.IsFaqAvailable = false;
            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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

     
    
