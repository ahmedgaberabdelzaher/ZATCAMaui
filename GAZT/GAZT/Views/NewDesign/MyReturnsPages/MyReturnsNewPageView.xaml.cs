using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.DashBoardPages;
using EGAZT.Views.NewDesign.PaymentOptions;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.MyReturnsNewPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignMyReturnsNewPageView : ContentPage
    {
        GAZTNewDesignMyReturnsNewPageViewModel viewModel;
        public GAZTNewDesignMyReturnsNewPageView(int Index)
        {
            InitializeComponent();
            
           // App.DisplayProgressView();

            viewModel = App.Locator.GAZTNewDesignMyReturnsNewPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.Index = Index;
            viewModel.PopulateReturnTypeList();
            viewModel.PopulateDataInChips();
            viewModel.SelectedReturnTypeForFilter = viewModel.ReturnTypeForFilter.FirstOrDefault();
            TaxTypePicker.SelectedItem = viewModel.ReturnTypeForFilter.FirstOrDefault();
            viewModel.SelectedChipFilterItem = null;
            ListView_Returns.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)e.Item;
                viewModel.SelectedListItem = SelectedItem;
               
                if (e.Item == null)
                {
                    return;
                } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            };
        SetPickerFont();
          //  App.HideProgressView();
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                         
                                TaxTypePicker.HeaderFontFamily = "SSTArabic-Medium";
                                TaxTypePicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                TaxTypePicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                TaxTypePicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy

                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        TaxTypePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected async override void OnAppearing()
        {

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            try
            {
                VATDeclarationAttachmentPageViewModel.isToBeFilled = true;
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });

                if (Device.RuntimePlatform == Device.Android)
                {
                    TaxTypePicker.BackgroundColor = Color.FromHex("#f7f7f7");
                }
                else
                {
                    TaxTypePicker.BackgroundColor = Color.FromHex("#FFFFFF");
                }
                await viewModel.OnPageLoad();
                if (TaxTypePicker.SelectedItem != null)
                {
                    viewModel.SelectedReturnTypeForFilter = (ReturnTypes)TaxTypePicker.SelectedItem;
                }
                else
                {
                    TaxTypePicker.SelectedItem = viewModel.ReturnTypeForFilter.FirstOrDefault();
                }
                
                viewModel.SelectedChipFilterItem = null;
               // viewModel.FilterAllData();
                if (viewModel.Index == 0)
                {
                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("Submitted")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("Submitted")).FirstOrDefault();

                }
                if (viewModel.Index == 1)
                {
                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                }
                if (viewModel.Index == 2)
                {
                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("OverDue")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("OverDue")).FirstOrDefault();

                }
                if (viewModel.Index == 5)
                {
                    TaxTypePicker.SelectedItem = (ReturnTypes)viewModel.ReturnTypeForFilter.Where(x => x.Id == "01").FirstOrDefault();
                    viewModel.SelectedReturnTypeForFilter = (ReturnTypes)TaxTypePicker.SelectedItem;

                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                }
                if (viewModel.Index == 6)
                {
                    TaxTypePicker.SelectedItem = (ReturnTypes)viewModel.ReturnTypeForFilter.Where(x => x.Id == "02").FirstOrDefault();
                    viewModel.SelectedReturnTypeForFilter = (ReturnTypes)TaxTypePicker.SelectedItem;

                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                }

                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch(Exception ex)
            {

            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Card_Payment", async (sender, arg) =>
                {
                    Console.WriteLine("Card Payment Clicked");
                    await viewModel.MadaPaymentSelected();
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
                    Console.WriteLine("Applea pay Clicked");
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
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
            TaxTypePicker.IsOpen = true;
        }

        private void TaxTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
                TaxTypePicker.SelectedItem = selectedReturntype;//Fbnum
                viewModel.SelectedReturnTypeForFilter = selectedReturntype;

            }
            catch (Exception ex)
            { 
            
            }
        }

        private void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                ChipModel selectedReturntype = (ChipModel)e.AddedItem;
                ChipGroup_statusFilter.SelectedItem = selectedReturntype;//Fbnum
                viewModel.SelectedChipFilterItem = selectedReturntype;
            }
            catch (Exception ex)
            { 
            
            }
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void payNow_Tapped(object sender, EventArgs e)
        {
            
            MyReturnsResult SelectedItem = (MyReturnsResult)viewModel.ListToDisplay[0];

            await viewModel.DoValidatePayment(SelectedItem.Fbnum);

            
        }
    }
}