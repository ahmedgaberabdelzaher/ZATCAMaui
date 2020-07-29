using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.MyReturnsNewPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignMyReturnsNewPageView : ContentPage
    {
        GAZTNewDesignMyReturnsNewPageViewModel viewModel;
        public GAZTNewDesignMyReturnsNewPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignMyReturnsNewPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
           viewModel.OnPageLoad();
            viewModel.PopulateReturnTypeList();
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
            //viewModel.SelectedFormBindleFbnumPrev = selectedfbnum;
            //viewModel.TxtFBnum = selectedfbnum.Fbnum;
        }
    }
}