using Mopups.Services;
using System.Collections.ObjectModel;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.MyBillsPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignMyBillsPageView : ContentPage
    {
        GAZTNewDesignMyBillsPageViewModel viewModel;
        public GAZTNewDesignMyBillsPageView(BillInfo billInfo = null)
        {
            InitializeComponent();

            //  App.DisplayProgressView();

            if (viewModel != null) return;
            viewModel = App.Locator.GAZTNewDesignMyBillsPageView;
            BindingContext = viewModel;
            try
            {
                viewModel.PopulateFilterDropdown();
                viewModel.onPageLoad(billInfo);
                viewModel.PopulateDataInChips();
                //viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal);

                if (viewModel.MyBillsOriginal != null)
                {
                    viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal.Where(x => x.Status != "Paid"));

                }
                if (billInfo != null && billInfo.BillTypeName != null)
                {
                    if (billInfo.BillTypeName.Equals(AppResources.Paid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.Paid)).FirstOrDefault();

                    }
                    if (billInfo.BillTypeName.Equals(AppResources.UnPaid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.UnPaid)).FirstOrDefault();
                        ChipGroup_statusFilter.SelectedChipTextColor = (Color)Application.Current.Resources["Secondary"];
                        ChipGroup_statusFilter.SelectedChipBackground = (Color)Application.Current.Resources["ChipPaidColor"];
                        viewModel.FilterIfTypeAndStausFilterSelected(false);

                    }
                    if (billInfo.BillTypeName.Equals(AppResources.PartiallyPaid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.PartiallyPaid)).FirstOrDefault();
                        ChipGroup_statusFilter.SelectedChipTextColor = (Color)Application.Current.Resources["Secondary"];
                        ChipGroup_statusFilter.SelectedChipBackground = (Color)Application.Current.Resources["ChipUnPaidColor"];
                        viewModel.FilterIfTypeAndStausFilterSelected(false);
                    }
                    ChipGroup_statusFilter.SelectedItem = viewModel.SelectedChipFilterItem;
                    viewModel.SelectionColor = Colors.AliceBlue;
                }
                else
                {
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist[0];
                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist[0];
                }

            }
            catch (Exception)
            {


            }


            NavigationPage.SetBackButtonTitle(this, "");
            Bills.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is ListView lv) lv.SelectedItem = null;
            };

            //  App.HideProgressView();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                viewModel.isPayNowTapped = false;

                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                {
                    viewModel.PickerModel = arg;
                    viewModel.updatePicker();
                });

                MessagingCenter.Subscribe<object, string>(this, "MultipleBillsContinue", (sender, arg) =>
                {


                    viewModel.showPaymentOptions();
                    viewModel.isPayNowTapped = false;

                });
                MessagingCenter.Subscribe<object, string>(this, "Card_Payment", (sender, arg) =>
                {

                    viewModel.MadaPaymentSelected();
                    viewModel.isPayNowTapped = false;

                });

                MessagingCenter.Subscribe<object, string>(this, "Apple_Pay", async (sender, arg) =>
                {
                    await viewModel.ApplePaySelected();
                    viewModel.isPayNowTapped = false;
                });

                MessagingCenter.Subscribe<object, string>(this, "SADAD", async (sender, arg) =>
                {
                    await viewModel.SadadPaymentSelected();
                    viewModel.isPayNowTapped = false;
                });

                MessagingCenter.Subscribe<App, string>(this, "ApplePayData", async (sender, arg) =>
                {

                    viewModel.ApplePayTokenData = arg.ToString();

                    await viewModel.UpdateApplePayPaymentGuid();


                });
            }
            catch (Exception)
            {

            }




        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                MessagingCenter.Unsubscribe<object, string>(this, "Card_Payment");
                MessagingCenter.Unsubscribe<object, string>(this, "Apple_Pay");
                MessagingCenter.Unsubscribe<object, string>(this, "SADAD");
                MessagingCenter.Unsubscribe<App, string>(this, "ApplePayData");
                MessagingCenter.Unsubscribe<object, string>(this, "MultipleBillsContinue");
                MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");

            }
            catch (Exception)
            {
            }
        }



        private void btn_Clicked(object sender, EventArgs e)
        {

            viewModel.showPickerDialog();

        }




        private void chipgroup_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangedEventArgs e)
        {
            try
            {

                ChipModel selectedReturntype = (ChipModel)e.AddedItem;
                //ChipGroup_statusFilter.SelectedItem = selectedReturntype;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (selectedReturntype.Text == AppResources.UnPaid)
                    {
                        ChipGroup_statusFilter.SelectedChipTextColor = (Color)Application.Current.Resources["ErrorColor"];
                        ChipGroup_statusFilter.SelectedChipBackground = (Color)Application.Current.Resources["ChipPaidColor"];
                    }
                    else if (selectedReturntype.Text == AppResources.PartiallyPaid)
                    {
                        ChipGroup_statusFilter.SelectedChipTextColor = (Color)Application.Current.Resources["Partial"];
                        ChipGroup_statusFilter.SelectedChipBackground = (Color)Application.Current.Resources["PartialBg"];

                    }
                });

                //viewModel.AmountTitle = AppResources.MyBillsTotalUnPaidAmount;
                viewModel.SelectedChipFilterItem = selectedReturntype;
            }
            catch (Exception)
            {


            }
            //
        }

        private async void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = true;
                });
                var dataItem = e.Item as MyBills;
                await Clipboard.SetTextAsync(dataItem.VTRE2);
                if (Clipboard.HasText)
                {
                    var text = await Clipboard.GetTextAsync();
                    //await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();



                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZSadadInvoiceNumber + " " + text;
                    if (App.IsArabic)
                    {
                        headerAmountInfo.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        headerAmountInfo.FlowDirections = "LeftToRight";
                    }



                    headerWithInfos.Add(headerAmountInfo);




                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.Copied;



                    await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                }
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {


            }

        }
        private void payNow_Tapped(object sender, EventArgs eventArgs)
        {
            if (!viewModel.isPayNowTapped)
            {
                viewModel.isPayNowTapped = true;
                StackLayout payNowCard = sender as StackLayout;
                MyBills BModel = (MyBills)payNowCard.BindingContext;

                viewModel.verifyPaymentAndShowBillsPopup(BModel);
            }

        }
    }
}