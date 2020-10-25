using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.MyBillsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignMyBillsPageView : ContentPage
    {
        GAZTNewDesignMyBillsPageViewModel viewModel;
        public GAZTNewDesignMyBillsPageView(BillInfo billInfo = null)
        {
            InitializeComponent();
            
          //  App.DisplayProgressView();

            viewModel = App.Locator.GAZTNewDesignMyBillsPageView;
            this.BindingContext = viewModel;
            try
            {
                
                viewModel.onPageLoad(billInfo);
                viewModel.PopulateReturnTypeList();
                viewModel.PopulateDataInChips();
                viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal);
                viewModel.SelectedChipFilterItem = null;

                ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist[0];
                viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist[0];

                if (billInfo != null)
                {
                    if (billInfo.BillTypeName.Equals(AppResources.Paid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.Paid)).FirstOrDefault();
                       
                    }
                    if (billInfo.BillTypeName.Equals(AppResources.UnPaid))
                    {
                      viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.UnPaid)).FirstOrDefault();
                       
                    }
                    if (billInfo.BillTypeName.Equals(AppResources.PartiallyPaid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.PartiallyPaid)).FirstOrDefault();
                        viewModel.FilterIfTypeAndStausFilterSelected();
                    }
                    ChipGroup_statusFilter.SelectedItem = viewModel.SelectedChipFilterItem;
                    viewModel.SelectionColor = Color.AliceBlue;
                }

            }
            catch(Exception ex)
            { 
            
            }
          SetPickerFont();
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            Bills.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };

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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                TaxTypePicker.BackgroundColor = Color.FromHex("#f7f7f7");
            }
            else
            {
                TaxTypePicker.BackgroundColor = Color.FromHex("#FFFFFF");
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
                TaxTypePicker.SelectedItem = selectedReturntype;
                viewModel.SelectedTaxTypeForFilter = selectedReturntype;
               // ChipGroup_statusFilter.SelectedItem = null;
                //viewModel.SelectedChipFilterItem = null;          
            }
            catch(Exception ex)
            { 
            
            }
            
        }

        private void chipgroup_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                ChipModel selectedReturntype = (ChipModel)e.AddedItem;
                ChipGroup_statusFilter.SelectedItem = selectedReturntype;
                viewModel.SelectedChipFilterItem = selectedReturntype;
                //viewModel.SelectionColor = Color.AliceBlue;
            }
            catch (Exception ex)
            { 
            
            }
          //
        }

        private async void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {try
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



                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
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
    }
}