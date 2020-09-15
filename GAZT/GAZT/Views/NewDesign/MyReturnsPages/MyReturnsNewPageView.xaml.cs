using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.MyReturnsNewPages
{
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
            ListView_Returns.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)e.Item;
                viewModel.SelectedListItem = SelectedItem;
               
                if (e.Item == null)
                {
                    return;
                } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            };

          //  App.HideProgressView();
        }

        protected async override void OnAppearing()
        { 
            try
            {
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
            //viewModel.SelectedFormBindleFbnumPrev = selectedfbnum;
            //viewModel.TxtFBnum = selectedfbnum.Fbnum;
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
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    viewModel.IsLoading = false;
            //});

        }
    }
}