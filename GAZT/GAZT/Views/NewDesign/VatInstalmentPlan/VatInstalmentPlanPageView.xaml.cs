using System;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models.VATInstalationModels;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using VATResults4 = EGAZT.Models.VATInstalationModels.VATResults4;

namespace EGAZT.Views.NewDesign.VatInstalmentPlan
{
    public partial class VatInstalmentPlanPageView : ContentPage
    {



        #region Variable
        VATInstalmentPlanViewModel viewModel;

        double totalAmount = 0.0;
        #endregion

        public VatInstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.VatInstalmentPlanPageView;
                this.BindingContext = viewModel;

                viewModel.ResetData();

                Task.Run(async () =>
                {
                    viewModel.IsLoading = true;
                    await GetVAtInstalmentData();

                });

                viewModel.setMoreOptioButtons();

            }
            catch (Exception ex)
            {

            }
        }


        private void SetItemsSelected()
        {
            if (viewModel.VatInstalments != null && viewModel.VatInstalments.d != null)
            {

                if(viewModel.SelectedBillsList != null) {

                    viewModel.selectedList = viewModel.SelectedBillsList.Where(w => w.Xsele.Contains("X")).ToList();



                    for (int i = 0; i < viewModel.selectedList.Count; i++)
                    {
                        var dataItem = viewModel.selectedList[i] as VATResults4;
                        int index = viewModel.SelectedBillsList.IndexOf(w => w.SadadNo.Contains(dataItem.SadadNo));
                        if (index != -1)
                        {
                            BillsVATListVIew.SelectedItem = viewModel.SelectedBillsList[index];
                        }
                    }
                }



                Double dueAmount = 0.0;
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

                });
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bills_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

            if (!viewModel.IsViewEnable)
            {
                return;
            }
            var dataItem = e.ItemData as VATResults4;

            totalAmount = 0.0;

            try
            {
                if (viewModel.selectedList.Count > 0)
                {
                    if (viewModel.selectedList.Contains(dataItem))
                    {
                        // totalAmount -= Convert.ToDouble(dataItem.Betrh);
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



                Double dueAmount = 0.0;
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

            catch (Exception ex)
            {

            }
        }
        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            // PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(viewModel.VatInstalments.d.AttachmentSet.results,WhichAttachment.VATInstalment,viewModel.VatInstalments.d.ReturnIdz));
        }

        private void listView_SelectionChanging(object sender, Syncfusion.ListView.XForms.ItemSelectionChangingEventArgs e)
        {
            if (!viewModel.IsViewEnable)
            {
                e.Cancel = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, Boolean>(this, "TermsContinue");
            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<object, Boolean>(this, "TermsContinueSecond");
            MessagingCenter.Unsubscribe<object, Boolean>(this, "InstructionsContinue");
            MessagingCenter.Unsubscribe<object, string>(this, "RejectScenario");
            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");


        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                getYesCommand();
                getNoCommand();

                viewModel.FirstTerms = false;
                viewModel.SecondTerms = false;


                Xamarin.Forms.MessagingCenter.Subscribe<object, string>(this, "RejectScenario", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        SetItemsSelected();
                    }
                });
                Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
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
                Xamarin.Forms.MessagingCenter.Subscribe<object, Boolean>(this, "TermsContinueSecond", (sender, arg) =>
                {
                    if (arg != null && arg == true)
                    {
                        viewModel.SecondTerms = true;
                    }
                });

                Xamarin.Forms.MessagingCenter.Subscribe<object, Boolean>(this, "InstructionsContinue", (sender, arg) =>
                {
                    if (arg != null && arg == true)
                    {
                        viewModel.FirstTerms = true;
                    }
                });

                Xamarin.Forms.MessagingCenter.Subscribe<object, Boolean>(this, "TermsContinue", (sender, arg) =>
                {
                    if (arg != null)
                    {

                        //viewModel.EnableSucessScreenAsync();
                    }
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




                if (Device.RuntimePlatform == Device.iOS)
                {
                    //iOS stuff
                    BillsVATListVIew.IsScrollingEnabled = false;
                    StatementListView.IsScrollingEnabled = false;
                }
                else if (Device.RuntimePlatform == Device.Android)
                {

                }

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
