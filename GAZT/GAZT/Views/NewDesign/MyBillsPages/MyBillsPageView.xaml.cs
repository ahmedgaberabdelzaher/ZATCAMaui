using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
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
                if (billInfo != null)
                {
                    if (billInfo.BillTypeName.Equals(AppResources.Paid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.Paid)).FirstOrDefault();
                        viewModel.FilterIfTypeAndStausFilterSelected();
                    }
                    if (billInfo.BillTypeName.Equals(AppResources.UnPaid))
                    {
                        viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals(AppResources.UnPaid)).FirstOrDefault();
                        viewModel.FilterIfTypeAndStausFilterSelected();
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
                ChipGroup_statusFilter.SelectedItem = null;
                viewModel.SelectedChipFilterItem = null;          
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
                viewModel.SelectionColor = Color.AliceBlue;
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
                    await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);

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