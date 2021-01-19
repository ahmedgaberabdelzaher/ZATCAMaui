using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerCorrespondancePages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerCorrespondancePageView : ContentPage
    {
        TaxpayerCorrespondancePageViewModel viewModel;
        public TaxpayerCorrespondancePageView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayerCorrespondancePageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            viewModel.SelectedChipFilterItemList = new List<ChipModel>();
            PopulateReturnTypeList();
            viewModel.PopulateFilterDropdownList();
            viewModel.PopulateDataInChips();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            SetPickerFont();
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                      
                        TaxTypeDownPicker.HeaderFontFamily = "SSTArabic-Medium";
                        TaxTypeDownPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                        TaxTypeDownPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                        TaxTypeDownPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy

                 
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        TaxTypeDownPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypeDownPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypeDownPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypeDownPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        break;
                }
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
            try
            {
                if (App.IsArabic)
                {
                    Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                    Resources["ImageReverse"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                    Resources["ImageReverse"] = Resources["ArrowImageForEnglishStyle"];
                }
                  
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }
        public async Task PageLoad()
        {

            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await viewModel.onPageLoad();
                viewModel.SetData();
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                viewModel.IsLoading = true;
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;
                if (Device.RuntimePlatform == Device.Android)
                {
                    TaxTypeDownPicker.BackgroundColor = Color.FromHex("#f7f7f7");
                }
                else
                {
                    TaxTypeDownPicker.BackgroundColor = Color.FromHex("#FFFFFF");
                }

                await PageLoad();
                if (TaxTypeDownPicker.SelectedItem != null)
                {
                    viewModel.SelectedTaxTypeDropdownItem = (ReturnTypes)TaxTypeDownPicker.SelectedItem;
                }
                else
                {
                    TaxTypeDownPicker.SelectedItem = viewModel.TaxTypeListForDropDown.FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
        }

        public void PopulateReturnTypeList()
        {
            try
            {
                List<ReturnTypes> ReturnTypesList = new List<ReturnTypes>
                {
                    new ReturnTypes {Id = "00",TaxType = AppResources.VRAll},
                    new ReturnTypes {Id = "01",TaxType = AppResources.ZakatnewUi},
                    new ReturnTypes {Id = "02",TaxType = AppResources.VatReturns},
                    new ReturnTypes {Id = "03",TaxType = AppResources.ETReturns},
            };
                viewModel.TaxTypeListForDropDown = new List<ReturnTypes>();
                viewModel.TaxTypeListForDropDown = ReturnTypesList;


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }


        }
        private void TaxTypeDownPicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
                TaxTypeDownPicker.SelectedItem = selectedReturntype;//Fbnum
                viewModel.SelectedTaxTypeDropdownItem = selectedReturntype;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void ListView_Correspondance_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            CorrespondanceModel Correspondence = ((Xamarin.Forms.ListView)sender).SelectedItem as CorrespondanceModel;
            viewModel.ShowCorrespondenceDetails(Correspondence);
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

     

        private async  void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangingEventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            ChipModel addeditemtype = (ChipModel)e.AddedItem;
            ChipModel removedItem = (ChipModel)e.RemovedItem;

            if (addeditemtype != null)
            {
                viewModel.SelectedChipFilterItemList.Add(addeditemtype);
            }
            if (removedItem != null)
            {
                viewModel.SelectedChipFilterItemList.Remove(removedItem);
            }
            viewModel.FilterOnbasisOfChipSelectedItem();
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private void btn_TaxTypeClicked(object sender, EventArgs e)
        {
            TaxTypeDownPicker.IsOpen = true;
        }
    }
}