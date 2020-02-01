using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
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
            Attachmentlist.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                if (sender is ListView lv) lv.SelectedItem = null;
            };
            

            IntilizeAsync();


            viewModel.IsMainButtonEnabled = false;
            
            
            // viewModel.PageSelectedItems = viewModel.VatTabbledPageList[0];
            

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
            onPageLoadCalculation();
            if (App.ICRStatus != "E0013")
            {
                viewModel.IsCheckedTaxPayerDetailsInfo = false;
                viewModel.IsDeclarationCheckedForInstruction = false;
                viewModel.IsDeclarationCheckedForSummary = false;
            }
            
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

        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            Image arrowImage = sender as Image;
            Attachment attachment = (Attachment)arrowImage.BindingContext;
            var results =  await WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename,  viewModel.VATDeclarationData.d.ReturnIdz);
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
            var action = await DisplayActionSheet("ActionSheet", "Cancel", null,"Add Note", "View Notes","Attachments", "Set Return Void", "Reset Return");
            switch (action)
            {
                case "Add Note":
                    viewModel.VATReturnAddNote();
                    break;
                case "View Notes":
                    viewModel.VATReturnGetNotes();
                    break;
                case "Attachments":
                    viewModel.VATViewAttachments();
                    break;
                case "Set Return Void":
                    viewModel.SetVATReturnVoid();
                    break;
                case "Reset Return":
                    viewModel.VATReturnReset();
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

      
        }

     
    }
