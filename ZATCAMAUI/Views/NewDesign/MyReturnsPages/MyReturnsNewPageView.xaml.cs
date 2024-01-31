using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.MyReturnsPages
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
            BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            On<iOS>().SetUseSafeArea(true);
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel.Index = Index;
            viewModel.PopulateReturnTypeList();
            viewModel.PopulateDataInChips();
            // viewModel.SelectedReturnTypeForFilter = viewModel.ReturnTypeForFilter.FirstOrDefault();
            //TaxTypePicker.SelectedItem = viewModel.ReturnTypeForFilter.FirstOrDefault();
            viewModel.SelectedChipFilterItem = null;
            ListView_Returns.ItemTapped += (sender, e) =>
            {
                try
                {
                    MyReturnsResult SelectedItem = (MyReturnsResult)e.Item;
                    viewModel.SelectedListItem = SelectedItem;

                    if (e.Item == null)
                    {
                        return;
                    } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
                }
                catch (Exception)
                {


                }
            };
        }

        protected async override void OnAppearing()
        {

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;


            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
                viewModel.updatePicker();
            });

            try
            {
                VATDeclarationAttachmentPageViewModel.isToBeFilled = true;
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await viewModel.OnPageLoad();


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
                    // TaxTypePicker.SelectedItem = (ReturnTypes)viewModel.ReturnTypeForFilter.Where(x => x.Id == "01").FirstOrDefault();
                    // viewModel.SelectedTaxTypeForFilter = (ReturnTypes)TaxTypePicker.SelectedItem;

                    viewModel.SelectedTaxTypeForFilter = viewModel.TaxTypeForFilter.Where(x => x.StatementFilter == "02").FirstOrDefault();
                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                }
                if (viewModel.Index == 6)
                {
                    // TaxTypePicker.SelectedItem = (ReturnTypes)viewModel.ReturnTypeForFilter.Where(x => x.Id == "02").FirstOrDefault();
                    //viewModel.SelectedTaxTypeForFilter = (ReturnTypes)TaxTypePicker.SelectedItem;

                    viewModel.SelectedTaxTypeForFilter = viewModel.TaxTypeForFilter.Where(x => x.StatementFilter == "06").FirstOrDefault();

                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                }

                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {



            }


        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {

                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                FlowDirection = FlowDirection.RightToLeft;

            }
        }
        public void ChangeAeroIcon()
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


        private void btn_Clicked(object sender, EventArgs e)
        {

            viewModel.showPickerDialog();
        }

        private void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangedEventArgs e)
        {
            try
            {
                ChipModel selectedReturntype = (ChipModel)e.AddedItem;
                ChipGroup_statusFilter.SelectedItem = selectedReturntype;//Fbnum
                viewModel.SelectedChipFilterItem = selectedReturntype;

                if (selectedReturntype.Text == AppResources.UnSubmitted)
                {
                    ChipGroup_statusFilter.SelectedChipTextColor = (Color)Application.Current.Resources["Error"];
                    ChipGroup_statusFilter.SelectedChipBackgroundColor = (Color)Application.Current.Resources["ErrorBg"];


                }
                else if (selectedReturntype.Text == AppResources.OverDue)
                {
                    ChipGroup_statusFilter.SelectedChipTextColor = (Color)Application.Current.Resources["Error"];
                    ChipGroup_statusFilter.SelectedChipBackgroundColor = (Color)Application.Current.Resources["ErrorBg"];

                }
                else if (selectedReturntype.Text == AppResources.Submitted)
                {
                    ChipGroup_statusFilter.SelectedChipTextColor = (Color)Application.Current.Resources["Success"];
                    ChipGroup_statusFilter.SelectedChipBackgroundColor = (Color)Application.Current.Resources["SuccessBg"];

                }
            }
            catch (Exception)
            {


            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void payNow_Tapped(object sender, EventArgs e)
        {
            try
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)viewModel.ListToDisplay[0];
            }
            catch (Exception)
            {


            }


        }
    }
}