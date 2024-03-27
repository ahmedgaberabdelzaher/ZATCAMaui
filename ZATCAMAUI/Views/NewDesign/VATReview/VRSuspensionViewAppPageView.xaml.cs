using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VRSuspensionViewAppPageView : ContentPage
    {
        private VatReviewViewModel viewModel;
        public VRSuspensionViewAppPageView()
        {

            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");

            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
            viewModel = App.Locator.VatReviewView;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

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

        private void VRVSItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                viewModel.OpenAttachment(e.DataItem as Attachment);
            }
            catch (Exception)
            {


            }
        }
    }
}