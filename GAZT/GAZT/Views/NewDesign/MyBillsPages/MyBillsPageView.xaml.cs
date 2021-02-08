using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.PaymentOptions;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.MyBillsPages
{
    [Preserve(AllMembers = true)]
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
            this.BindingContext = viewModel;
            try
            {

                viewModel.onPageLoad(billInfo);
                viewModel.PopulateReturnTypeList();
                viewModel.PopulateDataInChips();
                viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal);


                if (billInfo != null)
                {
                    if (billInfo.BillTypeName.Equals(AppResources.Paid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.Paid)).FirstOrDefault();

                    }
                    if (billInfo.BillTypeName.Equals(AppResources.UnPaid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.UnPaid)).FirstOrDefault();
                        ChipGroup_statusFilter.SelectedChipTextColor = Color.FromHex("#AA0C19");
                        ChipGroup_statusFilter.SelectedChipBackgroundColor = Color.FromHex("#f6e6e8");
                    }
                    if (billInfo.BillTypeName.Equals(AppResources.PartiallyPaid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.PartiallyPaid)).FirstOrDefault();
                        ChipGroup_statusFilter.SelectedChipTextColor = Color.FromHex("#D99A29");
                        ChipGroup_statusFilter.SelectedChipBackgroundColor = Color.FromHex("#fbf4e9");
                        viewModel.FilterIfTypeAndStausFilterSelected();
                    }
                    ChipGroup_statusFilter.SelectedItem = viewModel.SelectedChipFilterItem;
                    viewModel.SelectionColor = Color.AliceBlue;
                }
                else
                {
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist[0];
                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist[0];
                }

            }
            catch (Exception ex)
            {

            }
            ChangeAeroIcon();
            SetLTR();


            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            Bills.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };

            //  App.HideProgressView();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Card_Payment", async (sender, arg) =>
                {
                    Console.WriteLine("Card Payment Clicked");

                    viewModel.MadaPaymentSelected();

                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Apple_Pay", async (sender, arg) =>
                {
                    Console.WriteLine("Apple pay Clicked");
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "SADAD", async (sender, arg) =>
                {

                    Console.WriteLine("SADAD Clicked");
                    viewModel.SadadPaymentSelected();
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

            }
            catch (Exception ex)
            {
                //scrollView.ScrollToAsync(0, 500, true);
            }
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
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        private void btn_Clicked(object sender, System.EventArgs e)
        {
            MessagingCenter.Subscribe<GAZTNewDesignMyBillsPageView, ReturnTypes>(this, "pickerNew", (a, arg) =>
            {
                viewModel.SelectedTaxTypeForFilter = arg;
                MessagingCenter.Unsubscribe<GAZTNewDesignMyBillsPageView, ReturnTypes>(this, "pickerNew");
            });
            PopupNavigation.Instance.PushAsync(new NewPopupPageView(viewModel.TaxTypeForFilter, viewModel.SelectedTaxTypeForFilter), false);
        }


        private void chipgroup_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                ChipModel selectedReturntype = (ChipModel)e.AddedItem;
                //ChipGroup_statusFilter.SelectedItem = selectedReturntype;
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (selectedReturntype.Text == AppResources.UnPaid)
                    {
                        ChipGroup_statusFilter.SelectedChipTextColor = Color.FromHex("#AA0C19");
                        ChipGroup_statusFilter.SelectedChipBackgroundColor = Color.FromHex("#f6e6e8");
                    }
                    else if (selectedReturntype.Text == AppResources.Partiallynewui)
                    {
                        ChipGroup_statusFilter.SelectedChipTextColor = Color.FromHex("#D99A29");
                        ChipGroup_statusFilter.SelectedChipBackgroundColor = Color.FromHex("#fbf4e9");
                    }
                });
                viewModel.SelectedChipFilterItem = selectedReturntype;
            }
            catch (Exception ex)
            {

            }
            //
        }

        private async void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
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



                    await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception ex)
            {

            }

        }
        private async void payNow_Tapped(object sender, EventArgs eventArgs)
        {
            //var dataItem = e.Item as MyBills;
            StackLayout payNowCard = sender as StackLayout;
            MyBills BModel = (MyBills)payNowCard.BindingContext;
            Console.WriteLine("Clicked on: Amount: " + BModel.TestDueAmount + " ,FbNum: " + BModel.Fbnum);

            if (!String.IsNullOrEmpty(BModel.Fbnum))
            {


                if (BModel.MadabutFg == "X")
                {
                    PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));

                    //viewModel.DoValidatePayment(fbNum: BModel.Fbnum,BModel.Status);
                }
                else
                {
                    PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ""));
                }
                viewModel.selectedFbNum = BModel.Fbnum;
                viewModel.selectedSadadNo = BModel.VTRE2;

              //  PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));

                //viewModel.DoValidatePayment(BModel.Fbnum,BModel.TestDueAmount);
            }

        }
    }
}