using System;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    public partial class OldZakatInstalmentPlanPageView : ContentPage
    {



        #region Variable
        OldZakatInstalmentPlanViewModel viewModel;

        double totalAmountDue = 0.0;
        #endregion

        public OldZakatInstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();

               // Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);



                viewModel = App.Locator.OldZakatInstalmentPlanPageView;
                this.BindingContext = viewModel;
                viewModel.ResetData();
                viewModel.IsZakat = Preferences.Get("isZakat", false);
                viewModel.IsPenaltyVisible = !Preferences.Get("isZakat", false);
                viewModel.showInstructionsDialog();
                GetZakatInstalmentData();
                outletDecisionOptionsListView.SelectedItem = viewModel.OutletDecisionOptions[0];
                frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[0];
                viewModel.setMoreOptioButtons();

            }
            catch (Exception ex)
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
            catch (Exception ex)
            {

            }
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
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        private void Frequncy_Selected(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selectedItem = e.ItemData as InstalmentAgreementFrequencyModel;
            viewModel.updateInstalmentsOnSlider(selectedItem);
        }
        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
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
            viewModel.DownPaymentAmount = args.NewValue;
            downPaymentEntry.Text = viewModel.DownPaymentAmount.ToString();
        }

        private void Installment_ValueChanged(object sender, ValueChangedEventArgs args)
        {
            var newVal = args.NewValue;
            viewModel.NoOfInstalments = Convert.ToInt32(newVal);
        }
        private void SearchItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private async void Bills_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            OldResults3 dataItem = e.ItemData as OldResults3;

            if (App.selectedZakatItem == "")
            {

                totalAmountDue = 0.0;



                try
                {


                    if (viewModel.selectedList.ToList().Exists(item => item.AIvNoTb == dataItem.AIvNoTb))
                    {

                        viewModel.selectedList.Remove(dataItem);
                      
                    }
                    else {

                        viewModel.selectedList.Add(dataItem);
                    }


                    //if (viewModel.selectedList.Contains(dataItem))
                    //{
                    //    viewModel.selectedList.Remove(dataItem);
                    //}
                    //else
                    //{
                    //    viewModel.selectedList.Add(dataItem);
                    //}




                    for (int i = 0; i < viewModel.selectedList.Count; i++)
                    {
                        totalAmountDue = totalAmountDue + Convert.ToDouble(viewModel.selectedList[i].ADueAmtTb);

                    }



                    viewModel.VATBillDueAmount = string.Format("{0:N2}", totalAmountDue);
                    viewModel.TotalAmountSAR = string.Format("{0:N2}", totalAmountDue);
                    viewModel.MaxAmount = Math.Round(totalAmountDue, 2);
                    viewModel.MaxAmountTitle = AppResources.ZakatMax + " " + viewModel.MaxAmount;
                    viewModel.DownPaymentAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                    viewModel.MinAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                    viewModel.MinAmountTitle = AppResources.ZakatMin + " " + viewModel.MinAmount;

                }
                catch (Exception ex)
                {



                }
            }
        }

        private void listView_SelectionChanging(object sender, Syncfusion.ListView.XForms.ItemSelectionChangingEventArgs e)
        {
            if (App.selectedZakatItem != "")
            {
                e.Cancel = true;
            }
        }

        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            //PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(VatInstalments.d.AttachmentSet.results,WhichAttachment.VATInstalment,viewModel.VatInstalments.d.ReturnIdz));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<object, bool>(this, "InvoiceBillsLoaded");
            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "SelectedFrequencyType");
            MessagingCenter.Unsubscribe<object, string>(this, "SelectedReason");
            MessagingCenter.Unsubscribe<object, Attachments>(this, "PickerSelectedItem");




        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                viewModel.IsZakat = Preferences.Get("isZakat", false);
                viewModel.IsPenaltyVisible = !Preferences.Get("isZakat", false);

                //getActionCommand();
                getYesCommand();
                getNoCommand();



                MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
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
                    if (arg != null && viewModel.ZakatInvoicesList != null && App.selectedZakatItem != "")
                    {
                        totalAmountDue = 0;
                        for (int i = 0; i < viewModel.ZakatInvoicesList.Count; i++)
                        {
                            var dataItem = viewModel.ZakatInvoicesList[i];


                            if (viewModel.selectedList.ToList().Exists(item => item.AIvNoTb == dataItem.AIvNoTb))
                            {

                                BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[i];
                                totalAmountDue = totalAmountDue + Convert.ToDouble(viewModel.selectedList[i].ADueAmtTb);

                            }
                          
                        }
                        //viewModel.DownPaymentAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                        viewModel.MinAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                        viewModel.MinAmountTitle = AppResources.ZakatMin + " " + viewModel.MinAmount;
                        viewModel.PeriodicInstalment = Math.Round(totalAmountDue - viewModel.MinAmount);


                    }
                });






                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                    viewModel.YesNoPickerModel = arg;
                    viewModel.updatePicker();
                    // Console.WriteLine(arg);

                });



                MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
                {
                    await PopupNavigation.Instance.PopAsync();
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

                MessagingCenter.Subscribe<object, string>(this, "SelectedReason", (sender, arg) => {
                    if (arg != null)
                    {
                        Device.BeginInvokeOnMainThread(() => {

                            try
                            {

                                switch (arg)
                                {
                                    case "1": { outletDecisionOptionsListView.SelectedItem = viewModel.OutletDecisionOptions[0]; viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatFinancialCrisis]; break; }
                                    case "2": { outletDecisionOptionsListView.SelectedItem = viewModel.OutletDecisionOptions[1]; viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatDisputeInFavorOfGAZT]; break; }
                                    case "3": { outletDecisionOptionsListView.SelectedItem = viewModel.OutletDecisionOptions[2]; viewModel.IDType = viewModel.IDTypeDictionary[AppResources.ZakatOtherReason]; break; }

                                }
                            }
                            catch (Exception ex)
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
                            case "1":
                                {
                                    frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[0];
                                    viewModel.updateInstalmentsOnSlider(new InstalmentAgreementFrequencyModel
                                    {
                                        FrequencyOptions = AppResources.ZakatInstalmetMonthly,
                                        IsSelected = true
                                    });
                                    break;
                                }
                            case "2":
                                {
                                    frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[1];
                                    viewModel.updateInstalmentsOnSlider(new InstalmentAgreementFrequencyModel
                                    {
                                        FrequencyOptions = AppResources.ZakatInstalmetQuarterly,
                                        IsSelected = true
                                    });
                                    break;
                                }
                            case "3":
                                {
                                    frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[2];
                                    viewModel.updateInstalmentsOnSlider(new InstalmentAgreementFrequencyModel
                                    {
                                        FrequencyOptions = AppResources.ZakatInstalmetHalfYearly,
                                        IsSelected = true
                                    });
                                    break;
                                }
                            case "4":
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

                if (Device.RuntimePlatform == Device.iOS)
                {
                    //iOS stuff
                    BillsVATListVIew.IsScrollingEnabled = false;
                    VATInstalmentDisplayDetailsViewPage.IsScrollingEnabled = false;


                }
                else if (Device.RuntimePlatform == Device.Android)
                {

                }

            }
            catch (Exception ex)
            {
            }
        }

        private void downPaymentEntry_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (Double.Parse(downPaymentEntry.Text) > viewModel.MaxAmount)
                {
                    viewModel.showDialog(AppResources.ZakatInstalmentCannotExceed + " " + viewModel.MaxAmount);
                    downPaymentEntry.Text = viewModel.MinAmount.ToString();
                    downPaymentSlider.Value = viewModel.MinAmount;
                }
                else if (Double.Parse(downPaymentEntry.Text) < viewModel.MinAmount)
                {
                    viewModel.showDialog(AppResources.ZakatInstalmentCannotBeLessThan + viewModel.MinAmount);
                    downPaymentEntry.Text = viewModel.MinAmount.ToString();
                    downPaymentSlider.Value = viewModel.MinAmount;
                }
                else if (downPaymentEntry.Text.Length == 0)
                {
                    downPaymentEntry.Text = viewModel.DownPaymentAmount.ToString();
                }
                else
                {
                    viewModel.DownPaymentAmount = Math.Round(Double.Parse(downPaymentEntry.Text), 2);
                    downPaymentSlider.Value = viewModel.DownPaymentAmount;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void downPaymentEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (downPaymentEntry.Text.Length > 0)
                {
                    if (Double.Parse(downPaymentEntry.Text) > viewModel.MaxAmount)
                    {
                        downPaymentEntry.Text = viewModel.DownPaymentAmount.ToString();
                    }
                    else if (Double.Parse(downPaymentEntry.Text) < viewModel.MinAmount)
                    {
                        downPaymentEntry.Text = viewModel.DownPaymentAmount.ToString();

                    }
                    else
                    {
                        viewModel.DownPaymentAmount = Math.Round(Double.Parse(downPaymentEntry.Text), 2);
                        downPaymentSlider.Value = viewModel.DownPaymentAmount;
                        var dueAmount = viewModel.VATBillDueAmount.Replace("SAR", "");
                        viewModel.PeriodicInstalment = Math.Abs(double.Parse(dueAmount) - double.Parse(downPaymentEntry.Text));
                    }
                }
            }
            catch (Exception ex)
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

        public async void getActionCommand()
        {
            try
            {


                MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
                {
                    await PopupNavigation.Instance.PopAsync();
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
            catch (Exception ex)
            {

            }
        }

        public async void getYesCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await PopupNavigation.Instance.PopAsync();
                            viewModel.VATSetReturnVoidAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                    }

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
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                    }

                    //await PopupNavigation.Instance.PopAsync();
                    // await viewModel.VATSetReturnVoidAsync();
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
