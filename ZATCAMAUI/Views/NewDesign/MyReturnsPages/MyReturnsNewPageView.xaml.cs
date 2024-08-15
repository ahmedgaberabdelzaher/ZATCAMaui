using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;

namespace ZATCAMAUI.Views.NewDesign.MyReturnsPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignMyReturnsNewPageView : ContentPage
    {
        GAZTNewDesignMyReturnsNewPageViewModel viewModel;
        public GAZTNewDesignMyReturnsNewPageView(int Index)
        {
            InitializeComponent();

            viewModel = App.Locator.GAZTNewDesignMyReturnsNewPageView;
            BindingContext = viewModel;
            viewModel.Index = Index;
            viewModel.PopulateReturnTypeList();
            viewModel.PopulateDataInChips();
            viewModel.SelectedChipFilterItem = null;
            ListView_Returns.ItemTapped += (sender, e) =>
            {
                MyReturnsResult SelectedItem = (MyReturnsResult)e.Item;
                viewModel.SelectedListItem = SelectedItem;
                viewModel.PopulateData();
                if (e.Item == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;

            };
        }

        protected async override void OnAppearing()
        {

            try
            {
                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                {
                    viewModel.PickerModel = arg;
                    viewModel.updatePicker();
                });

                viewModel.IsLoading = true;
                VATDeclarationAttachmentPageViewModel.isToBeFilled = true;

                await viewModel.OnPageLoad();
                viewModel.PopulateDataInChips();

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
                    viewModel.SelectedTaxTypeForFilter = viewModel.TaxTypeForFilter.Where(x => x.statementFilter == "02").FirstOrDefault();
                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                }
                if (viewModel.Index == 6)
                {

                    viewModel.SelectedTaxTypeForFilter = viewModel.TaxTypeForFilter.Where(x => x.statementFilter == "06").FirstOrDefault();

                    viewModel.SelectedChipFilterItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where<ChipModel>(x => x.TemplateType.Equals("UnSubmitted")).FirstOrDefault();
                }

                viewModel.IsLoading = false;
            }
            catch (Exception)
            {



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
                    ChipGroup_statusFilter.SelectedChipTextColor = (Color)App.Current.Resources["Error"];
                    ChipGroup_statusFilter.SelectedChipBackground = (Color)App.Current.Resources["ErrorBg"];


                }
                else if (selectedReturntype.Text == AppResources.OverDue)
                {
                    ChipGroup_statusFilter.SelectedChipTextColor = (Color)App.Current.Resources["Error"];
                    ChipGroup_statusFilter.SelectedChipBackground = (Color)App.Current.Resources["ErrorBg"];

                }
                else if (selectedReturntype.Text == AppResources.Submitted)
                {
                    ChipGroup_statusFilter.SelectedChipTextColor = (Color)App.Current.Resources["Success"];
                    ChipGroup_statusFilter.SelectedChipBackground = (Color)App.Current.Resources["SuccessBg"];

                }
            }
            catch (Exception)
            {


            }

        }

        private void payNow_Tapped(object sender, EventArgs e)
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