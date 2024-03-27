using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerCorrespondancePageView : ContentPage
    {
        TaxpayerCorrespondancePageViewModel viewModel;
        public TaxpayerCorrespondancePageView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayerCorrespondancePageView;
            BindingContext = viewModel;
            ChangeAeroIcon();
            viewModel.SelectedChipFilterItemList = new List<ChipModel>();
            PopulateReturnTypeList();
            viewModel.PopulateFilterDropdownList();
            viewModel.PopulateDataInChips();
            On<iOS>().SetUseSafeArea(true);
            NavigationPage.SetBackButtonTitle(this, "");
            SetPickerFont();
            //  _ = PageLoad();
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {
                            TaxTypeDownPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypeDownPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypeDownPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypeDownPicker.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy


                        }
                        break;
                    case Device.Android:
                        TaxTypeDownPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TaxTypeDownPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TaxTypeDownPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TaxTypeDownPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        break;
                }
            }
            catch (Exception)
            {


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
            catch (Exception)
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
                Padding = safeInsets;
                if (Device.RuntimePlatform == Device.Android)
                {
                    TaxTypeDownPicker.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
                }
                else
                {
                    TaxTypeDownPicker.BackgroundColor = (Color)Application.Current.Resources["White"];
                }


                //if (TaxTypeDownPicker.SelectedItem != null)
                if(TaxTypeDownPicker.Columns[0].SelectedIndex != 0)
                {
                    //viewModel.SelectedTaxTypeDropdownItem = (ReturnTypes)TaxTypeDownPicker.SelectedItem;
                    viewModel.SelectedTaxTypeDropdownItem = viewModel.TaxTypeListForDropDown[TaxTypeDownPicker.Columns[0].SelectedIndex];


                }
                else
                {
                    TaxTypeDownPicker.Columns[0].SelectedIndex = 0;
                    //TaxTypeDownPicker.SelectedItem = viewModel.TaxTypeListForDropDown.FirstOrDefault();
                }
            }
            catch (Exception)
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
        private void TaxTypeDownPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                //TODO
                ReturnTypes selectedReturntype = viewModel.TaxTypeListForDropDown[e.NewValue];
                //ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
                //TaxTypeDownPicker.SelectedItem = selectedReturntype;//Fbnum
                viewModel.SelectedTaxTypeDropdownItem = selectedReturntype;
            }
            catch (Exception)
            {


            }
        }

        private void ListView_Correspondance_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                CorrespondanceModel Correspondence = e.DataItem as CorrespondanceModel;
                viewModel.ShowCorrespondenceDetails(Correspondence);
            }
            catch (Exception)
            {
            }

        }


        private async void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangingEventArgs e)
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