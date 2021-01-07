using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
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

            }

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
           //     ImageBackArrow.Rotation = 0;

    
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
              //  viewModel.RotationForImageInArabic = 180;
 
             //   ImageBackArrow.Rotation = 180;

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
                //  viewModel.SetAllCorrespondancedata();
                ;
            }
            catch (Exception ex)
            {
                viewModel.IsLoading = true;
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

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

            }
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            //CorrespondanceDownPicker.IsOpen = true;
        }

        //private void CorrespondanceDownPicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
        //        CorrespondanceDownPicker.SelectedItem = selectedReturntype;//Fbnum
        //        viewModel.SelectedDropdownItem = selectedReturntype;

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
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
                   // new ReturnTypes {Id = "04",TaxType = AppResources.ZZWithholding},
            };
                viewModel.TaxTypeListForDropDown = new List<ReturnTypes>();
                viewModel.TaxTypeListForDropDown = ReturnTypesList;


            }
            catch (Exception ex)
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
            catch (Exception ex)
            {

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
            //List<ReturnTypes>  selectedReturntype = (ReturnTypes)e.NewValue;
          
            //ChipGroup_statusFilter.SelectedItem = selectedReturntype;
            //viewModel.SelectedChipFilterItem = selectedReturntype;
        }

        private void btn_TaxTypeClicked(object sender, EventArgs e)
        {
            TaxTypeDownPicker.IsOpen = true;
        }
    }
}