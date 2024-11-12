
using Mopups.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ZakatInstalationModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{
   
    public partial class OldZakatInstalmentPlanPageView : ContentPage
    {



        #region Variable
        OldZakatInstalmentPlanViewModel viewModel;

       
        #endregion

        public OldZakatInstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.OldZakatInstalmentPlanPageView;
                BindingContext = viewModel;
                
            }
            catch (Exception)
            {


                viewModel.IsLoading = false;

            }



        }

       


        private void Frequncy_Selected(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            var selectedItem = e.DataItem as InstalmentAgreementFrequencyModel;
            viewModel.updateInstalmentsOnSlider(selectedItem);
        }

        void outletDecisionOptionsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            ZakatInstalmentPlanModel selectedItem = e.AddedItems[0] as ZakatInstalmentPlanModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
            //            viewModel.ReasonContinueBtnClicked();


            if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 0)
            {
                viewModel.IsZakatSelected = true;
                viewModel.IsIncomeTaxViewEnabled = false;
                viewModel.IsVATAmountVisible = false;
                viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatFinancialCrisis];
            }
            else if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 1)
            {
                viewModel.IsZakatSelected = false;
                viewModel.IsIncomeTaxViewEnabled = true;
                viewModel.IsVATAmountVisible = false;
                viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatDisputeInFavorOfGAZT];
            }
            else
            {
                viewModel.IsZakatSelected = false;
                viewModel.IsIncomeTaxViewEnabled = false;
                viewModel.IsSubIncomeTaxViewEnabled = false;
                viewModel.IsVATAmountVisible = true;
                viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatOtherReason];

            }


        }

        private void Year1Change_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.Year1.Contains("_") || viewModel.Year1.Contains("-") || viewModel.Year1.Contains(",") || viewModel.Year1.Contains(".") || viewModel.Year1.Contains(" "))
            {
                viewModel.Year1 = viewModel.Year1.Replace("_", "").Replace(",", "").Replace(".", "").Replace("-", "").Replace(" ", "");
            }
            if (viewModel.Year2.Contains("_") || viewModel.Year2.Contains("-") || viewModel.Year2.Contains(",") || viewModel.Year2.Contains(".") || viewModel.Year2.Contains(" "))
            {
                viewModel.Year2 = viewModel.Year2.Replace("_", "").Replace(",", "").Replace(".", "").Replace("-", "").Replace(" ", "");
            }
            if (viewModel.Year3.Contains("_") || viewModel.Year3.Contains("-") || viewModel.Year3.Contains(",") || viewModel.Year3.Contains(".") || viewModel.Year3.Contains(" "))
            {
                viewModel.Year3 = viewModel.Year3.Replace("_", "").Replace(",", "").Replace(".", "").Replace("-", "").Replace(" ", "");
            }
        }

        private void Installment_ValueChanged(object sender, ValueChangedEventArgs args)
        {

            try
            {
                var newVal = args.NewValue;
                viewModel.NoOfInstalments = Convert.ToInt32(newVal);
            }
            catch (Exception)
            {


            }
        }

        private void SearchItem_PropertyChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (viewModel.InputData.Length > 0)
                {
                    var itemsSource = viewModel.ZakatInvoicesList.Where(w => w.AIvNoTb.ToString().Contains(viewModel.InputData)).ToList();

                    BillsVATListVIew.ItemsSource = itemsSource;

                    for (int i = 0; i < itemsSource.Count(); i++)
                    {
                        var dataItem = itemsSource[i] as OldResults3;
                        if (viewModel.selectedList.Contains(dataItem))
                        {
                            BillsVATListVIew.SelectedItem = itemsSource[i];
                        }

                    }

                }
                else
                {
                    BillsVATListVIew.ItemsSource = viewModel.ZakatInvoicesList;

                    for (int i = 0; i < viewModel.ZakatInvoicesList.Count; i++)
                    {
                        var dataItem = viewModel.ZakatInvoicesList[i] as OldResults3;
                        if (viewModel.selectedList.Contains(dataItem))
                        {
                            BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[i];
                        }
                    }

                }

            }
            catch (Exception)
            {



            }

        }


        private void listView_SelectionChanging(object sender, Syncfusion.Maui.ListView.ItemSelectionChangingEventArgs e)
        {
            if (App.selectedZakatItem != "")
            {
                e.Cancel = true;
            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<object, bool>(this, "InvoiceBillsLoaded");
            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "SelectedFrequencyType");
            MessagingCenter.Unsubscribe<object, string>(this, "SelectedReason");
            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "PickerSelectedItem");
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                
                //if (DeviceInfo.Platform == DevicePlatform.iOS)
                //{
                //    //iOS stuff
                //    BillsVATListVIew.IsScrollingEnabled = false;
                //    VATInstalmentDisplayDetailsViewPage.IsScrollingEnabled = false;


                //}

            }
            catch (Exception)
            {


            }
        }

        private void calculation_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.calculateYear1Data();

        }
        private void calculation2_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.calculateYear2Data();
        }
        private void calculation3_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.calculateYear3Data();
        }

        public void getActionCommand()
        {
            try
            {


                MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
                {
                    await MopupService.Instance.PopAsync();
                    if (arg != null)
                    {
                        string message = arg;

                        if (App.IsArabic)
                        {
                            ArButtons buttonId = ArButtons.None;
                            if (!string.IsNullOrEmpty(message))
                            {
                                message = message.Replace(" ", "");
                            }
                            Enum.TryParse(message, out buttonId);
                            switch (buttonId)
                            {
                                case ArButtons.إضافةملاحظات:
                                    //viewModel.VATReturnAddNote();
                                    break;
                                case ArButtons.عرضملاحظات:
                                    //  viewModel.VATReturnGetNotes();
                                    break;
                                case ArButtons.المرفقات:
                                    // viewModel.VATViewAttachments();
                                    break;
                                case ArButtons.إلغاء:
                                    viewModel.isDraftClicked = true;
                                   await viewModel.VoidMsg();
                                    viewModel.isDraftClicked = false;
                                    break;
                                case ArButtons.عادةتعيين:
                                    //await viewModel.VATReturnResetAsync();
                                    break;
                                case ArButtons.تعديل:
                                    // await viewModel.VATReturnAmendAsync();
                                    break;
                                case ArButtons.حفظكمسودة:
                                    viewModel.isDraftClicked = true;
                                  await  viewModel.OnSaveDraftClicked();
                                    viewModel.isDraftClicked = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Buttons buttonId = Buttons.None;
                            if (!string.IsNullOrEmpty(message))
                            {
                                message = message.Replace(" ", "");
                            }
                            Enum.TryParse(message, out buttonId);
                            switch (buttonId)
                            {
                                case Buttons.CreateNotes:
                                    //viewModel.VATReturnAddNote();
                                    break;
                                case Buttons.DisplayNotes:
                                    //viewModel.VATReturnGetNotes();
                                    break;
                                case Buttons.Attachments:
                                    // viewModel.VATViewAttachments();
                                    break;
                                case Buttons.Void:
                                    viewModel.isDraftClicked = true;
                                   await viewModel.VoidMsg();
                                    viewModel.isDraftClicked = false;
                                    break;
                                case Buttons.Reset:
                                    //await viewModel.VATReturnResetAsync();
                                    break;
                                case Buttons.Amend:
                                    // await viewModel.VATReturnAmendAsync();
                                    break;
                                case Buttons.SaveasDraft:
                                    viewModel.isDraftClicked = true;
                                   await viewModel.OnSaveDraftClicked();

                                    viewModel.isDraftClicked = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                });
            }
            catch (Exception)
            {


            }
        }

       

        void CashBankText_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.CashBankY1 = CashBankText.Text;
                CashBankText.Text = string.Format("{0:N}", Convert.ToDouble(CashBankText.Text));

                viewModel.StiY1 = ShortTermInvestmentText.Text;
                ShortTermInvestmentText.Text = string.Format("{0:N}", Convert.ToDouble(ShortTermInvestmentText.Text));

                viewModel.DebitorsY1 = DebitorsText.Text;
                DebitorsText.Text = string.Format("{0:N}", Convert.ToDouble(DebitorsText.Text));

                viewModel.InventoryY1 = InventoryText.Text;
                InventoryText.Text = string.Format("{0:N}", Convert.ToDouble(InventoryText.Text));

                viewModel.TcAssetsY1 = TcAssetsText.Text;
                TcAssetsText.Text = string.Format("{0:N}", Convert.ToDouble(TcAssetsText.Text));

                viewModel.ZakatY1 = ZakatText.Text;
                ZakatText.Text = string.Format("{0:N}", Convert.ToDouble(ZakatText.Text));

                viewModel.TcLiabltyY1 = TcLiabilityText.Text;
                TcLiabilityText.Text = string.Format("{0:N}", Convert.ToDouble(TcLiabilityText.Text));

                viewModel.RevenueY1 = RevenueText.Text;
                RevenueText.Text = string.Format("{0:N}", Convert.ToDouble(RevenueText.Text));

                viewModel.NetIncomeY1 = NetIncomeText.Text;
                NetIncomeText.Text = string.Format("{0:N}", Convert.ToDouble(NetIncomeText.Text));

                viewModel.NcFlowY1 = NcFlowText.Text;
                NcFlowText.Text = string.Format("{0:N}", Convert.ToDouble(NcFlowText.Text));



            }
            catch (Exception)
            {

            }
        }


        void calculation2_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.CashBankY2 = CashBank2Text.Text;
                CashBank2Text.Text = string.Format("{0:N}", Convert.ToDouble(CashBank2Text.Text));

                viewModel.StiY2 = Sti2Text.Text;
                Sti2Text.Text = string.Format("{0:N}", Convert.ToDouble(Sti2Text.Text));

                viewModel.DebitorsY2 = Debitors2Text.Text;
                Debitors2Text.Text = string.Format("{0:N}", Convert.ToDouble(Debitors2Text.Text));

                viewModel.InventoryY2 = Inventory2Text.Text;
                Inventory2Text.Text = string.Format("{0:N}", Convert.ToDouble(Inventory2Text.Text));

                viewModel.TcAssetsY2 = TcAssets2Text.Text;
                TcAssets2Text.Text = string.Format("{0:N}", Convert.ToDouble(TcAssets2Text.Text));

                viewModel.ZakatY2 = Zakat2Text.Text;
                Zakat2Text.Text = string.Format("{0:N}", Convert.ToDouble(Zakat2Text.Text));

                viewModel.TcLiabltyY2 = TcLiability2Text.Text;
                TcLiability2Text.Text = string.Format("{0:N}", Convert.ToDouble(TcLiability2Text.Text));

                viewModel.RevenueY2 = Revenue2Text.Text;
                Revenue2Text.Text = string.Format("{0:N}", Convert.ToDouble(Revenue2Text.Text));

                viewModel.NetIncomeY2 = NetIncome2Text.Text;
                NetIncome2Text.Text = string.Format("{0:N}", Convert.ToDouble(NetIncome2Text.Text));

                viewModel.NcFlowY2 = NcFlow2Text.Text;
                NcFlow2Text.Text = string.Format("{0:N}", Convert.ToDouble(NcFlow2Text.Text));



            }
            catch (Exception)
            {

            }
        }

        void calculation3_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.CashBankY3 = CashBank3Text.Text;
                CashBank3Text.Text = string.Format("{0:N}", Convert.ToDouble(CashBank3Text.Text));

                viewModel.StiY3 = Sti3Text.Text;
                Sti3Text.Text = string.Format("{0:N}", Convert.ToDouble(Sti3Text.Text));

                viewModel.DebitorsY3 = Debitors3Text.Text;
                Debitors3Text.Text = string.Format("{0:N}", Convert.ToDouble(Debitors3Text.Text));

                viewModel.InventoryY3 = Inventory3Text.Text;
                Inventory3Text.Text = string.Format("{0:N}", Convert.ToDouble(Inventory3Text.Text));

                viewModel.TcAssetsY3 = TcAssets3Text.Text;
                TcAssets3Text.Text = string.Format("{0:N}", Convert.ToDouble(TcAssets3Text.Text));

                viewModel.ZakatY3 = Zakat3Text.Text;
                Zakat3Text.Text = string.Format("{0:N}", Convert.ToDouble(Zakat3Text.Text));

                viewModel.TcLiabltyY3 = TcLiability3Text.Text;
                TcLiability3Text.Text = string.Format("{0:N}", Convert.ToDouble(TcLiability3Text.Text));

                viewModel.RevenueY3 = Revenue3Text.Text;
                Revenue3Text.Text = string.Format("{0:N}", Convert.ToDouble(Revenue3Text.Text));

                viewModel.NetIncomeY3 = NetIncome3Text.Text;
                NetIncome3Text.Text = string.Format("{0:N}", Convert.ToDouble(NetIncome3Text.Text));

                viewModel.NcFlowY3 = NcFlow3Text.Text;
                NcFlow3Text.Text = string.Format("{0:N}", Convert.ToDouble(NcFlow3Text.Text));



            }
            catch (Exception)
            {

            }
        }


    }
}
