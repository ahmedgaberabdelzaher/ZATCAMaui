
using Mopups.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATInstalmentModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;

namespace ZATCAMAUI.Views.NewDesign.VatInstalmentPlan
{
    public partial class VatInstalmentPlanPageView : ContentPage
    {
        #region Variable

        VATInstalmentPlanViewModel viewModel;


        #endregion

        public VatInstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.VatInstalmentPlanPageView;
                BindingContext = viewModel;

                viewModel.ResetData();

                Task.Run(async () =>
                {
                    viewModel.IsLoading = true;
                    await GetVAtInstalmentData();
                    viewModel.IsLoading = false;
                });

                viewModel.setMoreOptioButtons();

            }
            catch (Exception)
            {


            }
        }

        private void SetItemsSelected()
        {
            if (viewModel.VatInstalments != null && viewModel.VatInstalments.d != null)
            {

                if (viewModel.SelectedBillsList != null)
                {

                    viewModel.selectedList = viewModel.SelectedBillsList.Where(w => w.Xsele.Contains("X")).ToList();



                    for (int i = 0; i < viewModel.selectedList.Count; i++)
                    {
                        var dataItem = viewModel.selectedList[i] as VATResults4;
                        int index = viewModel.SelectedBillsList.IndexOf(dataItem);
                        if (index != -1)
                        {
                            BillsVATListVIew.SelectedItem = viewModel.SelectedBillsList[index];
                        }
                    }
                }



                double dueAmount = 0.0;
                for (int i = 0; i < viewModel.selectedList.Count; i++)
                {
                    if (viewModel.selectedList[i] != null)
                    {
                        dueAmount = dueAmount + Convert.ToDouble(viewModel.selectedList[i].Betrh);
                    }
                    else
                    {
                        viewModel.selectedList.Remove(viewModel.selectedList[i]);
                    }
                }


                if (viewModel.selectedList.Count > 0)
                {

                    viewModel.TotalAmountSAR = string.Format("{0:N2}", dueAmount) + " " + viewModel.selectedList[0].Waers;
                    viewModel.EnableBillsContinue();

                }
            }







        }
        public async Task GetVAtInstalmentData()
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
                    Bills_Auto_select();

                });
            }
            catch (Exception)
            {


            }
        }

        private void Bills_Auto_select()
        {
            if (!viewModel.IsViewEnable)
            {
                return;
            }


            try
            {
                var itemsSource = viewModel.VatInstalments.d.VTIASet.ToList();

                itemsSource.ForEach(i => i.IsArabic = App.IsArabic);

                BillsVATListVIew.ItemsSource = itemsSource;
                Console.WriteLine(itemsSource.Count);
                Double dueAmount = 0.0;
                foreach (var dataItem in viewModel.VatInstalments.d.VTIASet)
                {
                    if (dataItem.Xsele == "X")
                    {
                        viewModel.selectedList.Add(dataItem);
                        dueAmount = dueAmount + Convert.ToDouble(dataItem.Betrh);
                    }
                    viewModel.TotalAmountSAR = string.Format("{0:N2}", dueAmount) + " " + viewModel.currencyUnits;
                }
                //    for (int i = 0; i < itemsSource.Count-1; i++)
                //{
                //    var dataItem = itemsSource[i] as VATResults4;
                //    Console.WriteLine(dataItem);
                //    if (dataItem.Xsele == "X")
                //    {
                //        //BillsVATListVIew.SelectedItem = itemsSource[i];
                //        viewModel.selectedList.Add(dataItem);
                //        dueAmount = dueAmount + Convert.ToDouble(viewModel.selectedList[i].Betrh);
                //        //viewModel. [i] = false;
                //    }
                //    viewModel.TotalAmountSAR = string.Format("{0:N2}", dueAmount) + " " + dataItem.Waers;
                //}
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }
        private void DownPayment_ValueChanged(object sender, ValueChangedEventArgs args)
        {
            viewModel.DownPaymentAmount = args.NewValue;


        }

        private void Installment_ValueChanged(object sender, ValueChangedEventArgs args)
        {
            var newVal = args.NewValue;
            viewModel.NoOfInstalments = Convert.ToInt32(newVal);


        }
        private void SearchItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsEnabled = true;
            try
            {
                if (viewModel.InputData.Length > 0)
                {
                    var itemsSource = viewModel.SelectedBillsList.Where(w => w.SadadNo.Contains(viewModel.InputData)).ToList();
                    BillsVATListVIew.ItemsSource = itemsSource;


                    for (int i = 0; i < itemsSource.Count; i++)
                    {
                        var dataItem = itemsSource[i] as VATResults4;
                        if (viewModel.selectedList.Contains(dataItem))
                        {
                            BillsVATListVIew.SelectedItem = itemsSource[i];
                        }

                    }


                }
                else
                {
                    BillsVATListVIew.ItemsSource = viewModel.SelectedBillsList;


                    for (int i = 0; i < viewModel.SelectedBillsList.Count; i++)
                    {
                        var dataItem = viewModel.SelectedBillsList[i] as VATResults4;
                        if (viewModel.selectedList.Contains(dataItem))
                        {
                            BillsVATListVIew.SelectedItem = viewModel.SelectedBillsList[i];
                        }

                    }

                }

            }
            catch (Exception)
            {


            }
        }

        private void Bills_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {

            if (!viewModel.IsViewEnable)
            {
                return;
            }
            var dataItem = e.DataItem as VATResults4;



            try
            {
                if (viewModel.selectedList.Count > 0)
                {
                    if (viewModel.selectedList.Contains(dataItem))
                    {

                        viewModel.selectedList.Remove(dataItem);
                    }
                    else
                    {
                        viewModel.selectedList.Add(dataItem);
                    }

                }
                else
                {
                    viewModel.selectedList.Add(dataItem);

                }



                double dueAmount = 0.0;
                for (int i = 0; i < viewModel.selectedList.Count; i++)
                {
                    if (viewModel.selectedList[i] != null)
                    {
                        dueAmount = dueAmount + Convert.ToDouble(viewModel.selectedList[i].Betrh);
                    }
                    else
                    {
                        viewModel.selectedList.Remove(viewModel.selectedList[i]);
                    }

                }

                viewModel.TotalAmountSAR = string.Format("{0:N2}", dueAmount) + " " + dataItem.Waers;

                viewModel.EnableBillsContinue();
            }

            catch (Exception)
            {


            }
        }
     
        private void listView_SelectionChanging(object sender, Syncfusion.Maui.ListView.ItemSelectionChangingEventArgs e)
        {
            if (!viewModel.IsViewEnable)
            {
                e.Cancel = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, bool>(this, "TermsContinue");
            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<object, bool>(this, "TermsContinueSecond");
            MessagingCenter.Unsubscribe<object, bool>(this, "InstructionsContinue");
            MessagingCenter.Unsubscribe<object, string>(this, "RejectScenario");
            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "Notes");


        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                getYesCommand();
                getNoCommand();

                viewModel.FirstTerms = false;
                viewModel.SecondTerms = false;


                MessagingCenter.Subscribe<object, string>(this, "RejectScenario", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        SetItemsSelected();
                    }
                });
               MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {



                        viewModel.PopulateAttachments(arg.results);
                        if (viewModel.AttachmentsListViewData != null)
                        {
                            attachmentsListView.ItemsSource = viewModel.AttachmentsListViewData;
                            SummaryattachmentsListView.ItemsSource = viewModel.AttachmentsListViewData;
                        }
                    }
                });
                MessagingCenter.Subscribe<object, bool>(this, "TermsContinueSecond", (sender, arg) =>
                {
                    if (arg == true)
                    {
                        viewModel.SecondTerms = true;
                    }
                });

                MessagingCenter.Subscribe<object, bool>(this, "InstructionsContinue", (sender, arg) =>
                {
                    if (arg == true)
                    {
                        viewModel.FirstTerms = true;
                    }
                });

                MessagingCenter.Subscribe<object, string>(this, "Notes", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.NotesText = arg;
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
                                    viewModel.VATReturnAddNote();
                                    break;
                                case ArButtons.عرضملاحظات:
                                    viewModel.VATReturnGetNotes();
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
                                    viewModel.VATReturnAddNote();
                                    break;
                                case Buttons.DisplayNotes:
                                    viewModel.VATReturnGetNotes();
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




                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    //iOS stuff
                    BillsVATListVIew.IsScrollingEnabled = false;
                    StatementListView.IsScrollingEnabled = false;
                }
                else if (DeviceInfo.Platform == DevicePlatform.Android)
                {

                }

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
