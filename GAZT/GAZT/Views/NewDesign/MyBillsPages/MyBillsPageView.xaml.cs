using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;

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
        {//MyBillsPageViewModel
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignMyBillsPageView;
            this.BindingContext = viewModel;
            viewModel.onPageLoad(billInfo);
            viewModel.PopulateReturnTypeList();
            viewModel.PopulateDataInChips();

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
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
            ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
            TaxTypePicker.SelectedItem = selectedReturntype;//Fbnum
            viewModel.SelectedReturnTypeForFilter = selectedReturntype;
        }

        private void chipgroup_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
          //  ChipModel SelectedChipFilterItem
                   ChipModel selectedReturntype = (ChipModel)e.AddedItem;
            ChipGroup_statusFilter.SelectedItem = selectedReturntype;//Fbnum
            viewModel.SelectedChipFilterItem = selectedReturntype;
        }
    }
}