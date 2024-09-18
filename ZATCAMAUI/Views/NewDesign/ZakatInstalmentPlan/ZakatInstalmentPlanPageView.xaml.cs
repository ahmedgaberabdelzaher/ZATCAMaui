
using Mopups.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ZakatInstalationModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{
 
    public partial class ZakatInstalmentPlanPageView : ContentPage
    {
        #region Variable
        ZakatInstalmentPlanViewModel viewModel;

        double totalAmountDue = 0.0;
        #endregion

        public ZakatInstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();



                viewModel = App.Locator.ZakatInstalmentPlanPageView;
                BindingContext = viewModel;
                viewModel.ResetData();
                viewModel.IsZakat = Preferences.Get("isZakat", false);
                viewModel.IsPenaltyVisible = !Preferences.Get("isZakat", false);
                viewModel.showInstructionsDialog();
                _ = GetZakatInstalmentData();
                outletDecisionOptionsListView.SelectedItem = viewModel.OutletDecisionOptions[0];
                frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[0];
                viewModel.setMoreOptioButtons();

            }
            catch (Exception)
            {



            }



        }

        public async Task GetZakatInstalmentData()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;

                });
                await Task.Run(async () =>
                {
                    await viewModel.OnPageLoad();

                });

            }
            catch (Exception)
            {


            }
        }

        private void Frequncy_Selected(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                var selectedItem = e.DataItem as InstalmentAgreementFrequencyModel;
                viewModel.updateInstalmentsOnSlider(selectedItem);
            }
            catch (Exception)
            {


            }
        }
        void outletDecisionOptionsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            try
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
                //if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 2)
                {
                    viewModel.IsZakatSelected = false;
                    viewModel.IsIncomeTaxViewEnabled = false;
                    viewModel.IsSubIncomeTaxViewEnabled = false;
                    viewModel.IsVATAmountVisible = true;
                    viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatOtherReason];

                }
            }
            catch (Exception)
            {


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

        private void DownPayment_ValueChanged(object sender, ValueChangedEventArgs args)
        {
            try
            {
                viewModel.DownPaymentAmount = args.NewValue;
                downPaymentEntry.Text = viewModel.DownPaymentAmount.ToString();
            }
            catch (Exception)
            {


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
        private void SearchItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            try
            {
                if (viewModel.InputData.Length > 0)
                {
                    var itemsSource = viewModel.ZakatInvoicesList.Where(w => w.InvNo.ToString().Contains(viewModel.InputData)).ToList();

                    BillsVATListVIew.ItemsSource = itemsSource;

                    for (int i = 0; i < itemsSource.Count(); i++)
                    {
                        var dataItem = itemsSource[i] as ZakatInvoicesResult;
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
                        var dataItem = viewModel.ZakatInvoicesList[i] as ZakatInvoicesResult;
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

        private async void Bills_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            ZakatInvoicesResult dataItem = e.DataItem as ZakatInvoicesResult;

            totalAmountDue = 0.0;

            try
            {
                if (viewModel.selectedList.Count > 0)
                {
                    if (viewModel.selectedList.Contains(dataItem) && dataItem.InvCb == "")
                    {
                        viewModel.selectedList.Remove(dataItem);
                    }
                    else if (dataItem.InvCb == "X")
                    {
                        BillsVATListVIew.ItemsSource = viewModel.ZakatInvoicesList;
                        for (int i = 0; i < viewModel.selectedList.Count; i++)
                        {
                            if (dataItem.InvNo.Equals(viewModel.selectedList[i].InvNo))
                            {

                                var index1 = viewModel.ZakatInvoicesList.IndexOf(dataItem);
                                BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[index1];
                            }
                        }
                        foreach (var item in viewModel.ZakatInvoicesList)
                        {
                            if (item.InvNo.Equals(dataItem.InvNo))
                            {
                                var index = viewModel.ZakatInvoicesList.IndexOf(dataItem);
                                if (index != -1)
                                {
                                    await Task.Delay(100);
                                    BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[index];
                                }
                            }
                        }


                    }
                    else
                    {
                        viewModel.selectedList.Add(dataItem);
                    }

                }
                else
                {
                    if (dataItem.InvCb == "X")
                    {
                        BillsVATListVIew.ItemsSource = viewModel.ZakatInvoicesList;
                        for (int i = 0; i < viewModel.selectedList.Count; i++)
                        {
                            if(dataItem.InvNo.Equals(viewModel.selectedList[i].InvNo))
                            {

                                var index1 = viewModel.ZakatInvoicesList.IndexOf(dataItem);
                                BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[index1];
                            }
                        }
                        foreach (var item in viewModel.ZakatInvoicesList)
                        {
                            if (item.InvNo.Equals(dataItem.InvNo))
                            {
                                var index = viewModel.ZakatInvoicesList.IndexOf(dataItem);
                                if (index != -1)
                                {
                                    await Task.Delay(100);
                                    BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[index];
                                }
                            }
                        }
                      
                    }
                    else
                    {
                        viewModel.selectedList.Add(dataItem);
                    }
                }

                for (int i = 0; i < viewModel.selectedList.Count; i++)
                {
                    totalAmountDue = totalAmountDue + Convert.ToDouble(viewModel.selectedList[i].DueAmt);
                }

                viewModel.VATBillDueAmount = string.Format("{0:N2}", totalAmountDue) + " " + dataItem.Waers;
                viewModel.MaxAmount = Math.Round(totalAmountDue, 2);
                viewModel.MaxAmountTitle = AppResources.ZakatMax + " " + viewModel.MaxAmount;
                viewModel.DownPaymentAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                viewModel.MinAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                viewModel.MinAmountTitle = AppResources.ZakatMin + " " + viewModel.MinAmount;
            }
            catch (Exception)
            {

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



        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                viewModel.IsZakat = Preferences.Get("isZakat", false);
                viewModel.IsPenaltyVisible = !Preferences.Get("isZakat", false);

                //getActionCommand();
                getYesCommand();
                getNoCommand();



                MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.PopulateAttachments(arg.results);



                        if (viewModel.BankStatementsAttachmentsListViewData != null)
                        {
                            attachmentsListView.ItemsSource = viewModel.BankStatementsAttachmentsListViewData;
                        }
                        if (viewModel.FinanceAttachmentsListViewData != null)
                        {
                            FinancialAttachmentsList.ItemsSource = viewModel.FinanceAttachmentsListViewData;
                        }




                        if (viewModel.AttachmentsListViewData != null && viewModel.FinanceAttachmentsListViewData != null && viewModel.BankStatementsAttachmentsListViewData != null)
                        {
                            viewModel.PopulateSummaryAttachments();
                            SummaryAttachmentsListView.ItemsSource = viewModel.AttachmentsListViewData;
                        }
                    }
                });

                MessagingCenter.Subscribe<object, bool>(this, "InvoiceBillsLoaded", (sender, arg) =>
                {
                    if (viewModel.ZakatInvoicesList != null)
                    {
                        totalAmountDue = 0;
                        for (int i = 0; i < viewModel.ZakatInvoicesList.Count; i++)
                        {
                            var dataItem = viewModel.ZakatInvoicesList[i] as ZakatInvoicesResult;
                            if (viewModel.selectedList.Contains(dataItem))
                            {
                                BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[i];
                                totalAmountDue = totalAmountDue + Convert.ToDouble(viewModel.selectedList[i].DueAmt);
                            }
                        }
                        viewModel.DownPaymentAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                        viewModel.MinAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                        viewModel.MinAmountTitle = AppResources.ZakatMin + " " + viewModel.MinAmount;
                        viewModel.PeriodicInstalment = Math.Round(totalAmountDue - viewModel.MinAmount);


                    }
                });


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
                                    viewModel.VoidMsg();
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
                                    viewModel.OnSaveDraftClicked();
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
                                    viewModel.VoidMsg();
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
                                    viewModel.OnSaveDraftClicked();

                                    viewModel.isDraftClicked = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                });

                MessagingCenter.Subscribe<object, string>(this, "SelectedReason", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {

                            try
                            {

                                switch (arg)
                                {
                                    case "01": { outletDecisionOptionsListView.SelectedItem = viewModel.OutletDecisionOptions[0]; viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatFinancialCrisis]; break; }
                                    case "02": { outletDecisionOptionsListView.SelectedItem = viewModel.OutletDecisionOptions[1]; viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatDisputeInFavorOfGAZT]; break; }
                                    case "03": { outletDecisionOptionsListView.SelectedItem = viewModel.OutletDecisionOptions[2]; viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatOtherReason]; break; }

                                }
                            }
                            catch (Exception)
                            {

                            }
                        });
                    }
                });


               MessagingCenter.Subscribe<object, string>(this, "SelectedFrequencyType", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        switch (arg)
                        {
                            case "01":
                                {
                                    frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[0];
                                    viewModel.updateInstalmentsOnSlider(new InstalmentAgreementFrequencyModel
                                    {
                                        FrequencyOptions = AppResources.ZakatInstalmetMonthly,
                                        IsSelected = true
                                    });
                                    break;
                                }
                            case "02":
                                {
                                    frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[1];
                                    viewModel.updateInstalmentsOnSlider(new InstalmentAgreementFrequencyModel
                                    {
                                        FrequencyOptions = AppResources.ZakatInstalmetQuarterly,
                                        IsSelected = true
                                    });
                                    break;
                                }
                            case "03":
                                {
                                    frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[2];
                                    viewModel.updateInstalmentsOnSlider(new InstalmentAgreementFrequencyModel
                                    {
                                        FrequencyOptions = AppResources.ZakatInstalmetHalfYearly,
                                        IsSelected = true
                                    });
                                    break;
                                }
                            case "04":
                                {
                                    frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[3];
                                    viewModel.updateInstalmentsOnSlider(new InstalmentAgreementFrequencyModel
                                    {
                                        FrequencyOptions = AppResources.ZakatInstalmetYearly,
                                        IsSelected = true
                                    });
                                    break;
                                }
                        }
                    }
                });

                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    //iOS stuff
                    BillsVATListVIew.IsScrollingEnabled = false;
                    VATInstalmentDisplayDetailsViewPage.IsScrollingEnabled = false;


                }

            }
            catch (Exception)
            {


            }
        }

        private void downPaymentEntry_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (double.Parse(downPaymentEntry.Text) > viewModel.MaxAmount)
                {
                    viewModel.ShowDialog(AppResources.ZakatInstalmentCannotExceed + " " + viewModel.MaxAmount);
                    downPaymentEntry.Text = viewModel.MinAmount.ToString();
                    downPaymentSlider.Value = viewModel.MinAmount;
                }
                else if (double.Parse(downPaymentEntry.Text) < viewModel.MinAmount)
                {
                    viewModel.ShowDialog(AppResources.ZakatInstalmentCannotBeLessThan + viewModel.MinAmount);
                    downPaymentEntry.Text = viewModel.MinAmount.ToString();
                    downPaymentSlider.Value = viewModel.MinAmount;
                }
                else if (downPaymentEntry.Text.Length == 0)
                {
                    downPaymentEntry.Text = viewModel.DownPaymentAmount.ToString();
                }
                else
                {
                    viewModel.DownPaymentAmount = Math.Round(double.Parse(downPaymentEntry.Text), 2);
                    downPaymentSlider.Value = viewModel.DownPaymentAmount;
                }
            }
            catch (Exception)
            {

            }
        }

        private void downPaymentEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (downPaymentEntry.Text.Length > 0)
                {
                    if (double.Parse(downPaymentEntry.Text) > viewModel.MaxAmount)
                    {
                        downPaymentEntry.Text = viewModel.DownPaymentAmount.ToString();
                    }
                    else if (double.Parse(downPaymentEntry.Text) < viewModel.MinAmount)
                    {
                        downPaymentEntry.Text = viewModel.DownPaymentAmount.ToString();

                    }
                    else
                    {
                        viewModel.DownPaymentAmount = Math.Round(double.Parse(downPaymentEntry.Text), 2);
                        downPaymentSlider.Value = viewModel.DownPaymentAmount;
                        var dueAmount = viewModel.VATBillDueAmount.Replace("SAR", "");
                        viewModel.PeriodicInstalment = Math.Abs(double.Parse(dueAmount) - double.Parse(downPaymentEntry.Text));
                    }
                }
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
                                    viewModel.VoidMsg();
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
                                    viewModel.OnSaveDraftClicked();
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
                                    viewModel.VoidMsg();
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
                                    viewModel.OnSaveDraftClicked();

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

        public void getYesCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await MopupService.Instance.PopAsync();
                            viewModel.VATSetReturnVoidAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                    }

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
                MessagingCenter.Subscribe<object, string>(this, "NoReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                    }
                });
            }
            catch (Exception)
            {

            }
        }
    }
}
