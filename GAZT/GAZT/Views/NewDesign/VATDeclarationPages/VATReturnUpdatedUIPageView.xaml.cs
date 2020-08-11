using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignVATReturnUpdatedUIPageView : ContentPage
    {
        #region Variable
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel viewModel;
        #endregion

        #region Constructor
        public GAZTNewDesignVATReturnUpdatedUIPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignVATReturnUpdatedUIPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            //if (_vATDeclarationInfo.d != null)
            //{
            //    viewModel.VATDeclarationData = _vATDeclarationInfo;
            //}



        }

        #endregion

        #region Method

        public void checkNewFormorOld()
        {

            if (App.ICRStatus == "E0001" || App.ICRStatus == "E0013")
            {
                viewModel.IsEnableSwitchToggledFor15PercentChange = true;
            }
            else
            {
                if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0056" || App.ICRStatus == "E0006") && viewModel.VATDeclarationData.d.Yesno == "X")
                {
                    viewModel.IsEnableSwitchToggledFor15PercentChange = false;
                }
                else
                {
                    if (App.ICRStatus == "E0056")
                    {
                        viewModel.IsEnableSwitchToggledFor15PercentChange = true;
                    }
                    else
                    {
                        viewModel.IsEnableSwitchToggledFor15PercentChange = false;
                    }
                }
                if (App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
                {
                    viewModel.IsEnableSwitchToggledFor15PercentChange = false;
                }
            }


            if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null && viewModel.VATDeclarationData.d.GoliveFg == "X")
            {
                viewModel.IsFifteenPercentChange = true;
                viewModel.IsNewReturn = true;
                ShowHideContent(viewModel.IsNewReturn);
                SetNewVATRate();
            }
            else
            {
                viewModel.IsFifteenPercentChange = false;
                viewModel.IsNewReturn = false;
                ShowHideContent(viewModel.IsNewReturn);
            }

        }
        public void ShowHideContent(bool IsNewReturn)
        {
            //Test Checked In VAT15Change
            if (IsNewReturn == true)
            {
                viewModel.IsPrevReturn = false;
                viewModel.IsNewReturn = true;
                if (viewModel.VATDeclarationData.d.Yesno == "X")
                {
                    viewModel.IsFifteenPersenctVisible = true;
                    viewModel.IsFivePersenctVisible = true;
                    viewModel.IsSwitchToggledFor15PercentChange = true;
                    viewModel.IsYesChecked = true;
                }
                else
                {
                    viewModel.IsFifteenPersenctVisible = true;
                    viewModel.IsFivePersenctVisible = false;
                    viewModel.IsSwitchToggledFor15PercentChange = false;
                    viewModel.IsNoChecked = true;
                }
            }
            else
            {
                viewModel.IsPrevReturn = true;
                viewModel.IsNewReturn = false;
                viewModel.IsFifteenPersenctVisible = false;
                viewModel.IsFivePersenctVisible = false;
            }
        }
        public void SetNewVATRate()
        {

            if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null)
            {
                if (viewModel.VATDeclarationData.d.VATPERITEMSet != null && viewModel.VATDeclarationData.d.VATPERITEMSet.results != null)
                {
                    Result6 Rate002For15Percent = viewModel.VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                    Result6 Rate003For5Percent = viewModel.VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();

                    if (Rate002For15Percent != null)
                    {
                        viewModel.VATRate002For15Percent = Rate002For15Percent.Rate;
                    }
                    if (Rate003For5Percent != null)
                    {
                        viewModel.VATRate003For5Percent = Rate003For5Percent.Rate;
                    }
                }
            }




        }
        public async Task IntilizeAsync()
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });

            await Task.Run(async () =>
            {
                try
                {
                    await viewModel.pageLoad();
                    viewModel.ListOfActionButtonsApplicable = new List<string>();
                    await viewModel.SetButtons(viewModel.VATDeclarationData);
                    if (App.CheckTINStatusPageView != "0045")
                    {
                        await onPageLoadCalculation();
                    }
                    
                }
                catch(Exception ex)
                {

                }
            });

            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        public async Task onPageLoadCalculation()
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

        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
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
        #endregion

        private void btnprimary_Clicked(object sender, EventArgs e)
        {

        }

        private void OnStandardRatedTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView());
        }

        private void OnDomesticRatedTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AdjustmentPopupPageView());
        }

        private void OnInCTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InstructionPopUp());
        }

        private void OnSummaryInCTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new SummaryInstruction());
        }

        private void OnYesTapped(object sender, EventArgs e)
        {
            NoImg.Source = "";
            YesImg.Source = "";
  //          YesLbl.
        }

        private void OnNoTapped(object sender, EventArgs e)
        {
            NoImg.Source = "";
            YesImg.Source = "";
//            NoLbl.TextColor = "";
        }
    }
}