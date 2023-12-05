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
using Application = Xamarin.Forms.Application;

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
          //  _ = PageLoad();
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                      
                        TaxTypeDownPicker.HeaderFontFamily = "Somar-SemiBold";
                        TaxTypeDownPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                        TaxTypeDownPicker.SelectedItemFontFamily = "Somar-SemiBold";
                        TaxTypeDownPicker.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy

                 
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        TaxTypeDownPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypeDownPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypeDownPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypeDownPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception)
            {
                
                
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
                    Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch(Exception)
            {
                
                
            }

        }
        public async Task PageLoad()
        {

            try
            {
                    viewModel.IsLoading = true;
                await viewModel.onPageLoad();
                viewModel.SetData();
                viewModel.FilterOnbasisOfChipSelectedItem();
                    viewModel.IsLoading = false;
                
            }
            catch (Exception)
            {
                
                
                viewModel.IsLoading = false;
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await PageLoad();
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;
                if (Device.RuntimePlatform == Device.Android)
                {
                    TaxTypeDownPicker.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
                }
                else
                {
                    TaxTypeDownPicker.BackgroundColor =  (Color)Application.Current.Resources["White"];
                }

                
                if (TaxTypeDownPicker.SelectedItem != null)
                {
                    viewModel.SelectedTaxTypeDropdownItem = (ReturnTypes)TaxTypeDownPicker.SelectedItem;
                }
                else
                {
                    TaxTypeDownPicker.SelectedItem = viewModel.TaxTypeListForDropDown.FirstOrDefault();
                }
            }
            catch(Exception)
            {
                
                
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
            catch (Exception)
            {
                
                
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
            catch (Exception)
            {
                
                
            }
        }

        //private void ListView_Correspondance_ItemTapped(object sender, ItemTappedEventArgs e)
        //{

        //    CorrespondanceModel Correspondence = ((Xamarin.Forms.ListView)sender).SelectedItem as CorrespondanceModel;
        //    viewModel.ShowCorrespondenceDetails(Correspondence);
        //    ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        //}

        private void ListView_Correspondance_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try { 
            CorrespondanceModel Correspondence = e.ItemData as CorrespondanceModel;
            viewModel.ShowCorrespondenceDetails(Correspondence);
            }
            catch (Exception)
            {
                
                
            }

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

        void SfListView_ItemTapped(System.Object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
        }
    }
}