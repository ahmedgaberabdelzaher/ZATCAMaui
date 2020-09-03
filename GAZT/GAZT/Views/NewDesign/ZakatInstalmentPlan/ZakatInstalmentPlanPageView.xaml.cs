using System;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
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

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ZakatInstalmentPlanPageView;
                this.BindingContext = viewModel;

                viewModel.showInstructionsDialog();
                GetZakatInstalmentData();
                frequencyOptionsListView.SelectedItem = viewModel.ZakatAgreementOptions[0];
                viewModel.ResetData();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private async void Bills_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            ZakatInvoicesResult dataItem = e.ItemData as ZakatInvoicesResult;

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
                            var index1 = viewModel.ZakatInvoicesList.IndexOf(item => item.InvNo.Equals(viewModel.selectedList[i].InvNo));
                            BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[index1];
                        }

                        var index = viewModel.ZakatInvoicesList.IndexOf(item => item.InvNo.Equals(dataItem.InvNo));
                        if (index != -1)
                        {
                            await Task.Delay(100);
                            BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[index];
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
                            var index1 = viewModel.ZakatInvoicesList.IndexOf(item => item.InvNo.Equals(viewModel.selectedList[i].InvNo));
                            BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[index1];
                        }

                        var index = viewModel.ZakatInvoicesList.IndexOf(item => item.InvNo.Equals(dataItem.InvNo));
                        if (index != -1)
                        {
                            await Task.Delay(100);
                            BillsVATListVIew.SelectedItem = viewModel.ZakatInvoicesList[index];
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
                viewModel.DownPaymentAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                viewModel.MinAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);

            }
            catch (Exception ex)
            {

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


        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {

                        // viewModel.VatInstalments.d.AttachmentSet.results = arg.results;
                        viewModel.PopulateAttachments(arg.results);


                    }
                });

                Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.PopulateAttachments(arg.results);
                    }
                });

                Xamarin.Forms.MessagingCenter.Subscribe<object, bool>(this, "InvoiceBillsLoaded", (sender, arg) =>
                {
                    if (arg != null)
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
                        viewModel.PeriodicInstalment = Math.Round(totalAmountDue - viewModel.MinAmount);


                    }
                });

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
    }
}
