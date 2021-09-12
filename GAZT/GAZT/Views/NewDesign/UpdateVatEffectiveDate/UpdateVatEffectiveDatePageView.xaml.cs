using EGAZT.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using SearchBar= Xamarin.Forms.SearchBar;

namespace EGAZT.Views.NewDesign.UpdateVatEffectiveDate
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateVatEffectiveDatePageView : ContentPage
    {
        private UpdateVatEffectiveDateViewModel viewModel;
        private SearchBar searchBar;
        public UpdateVatEffectiveDatePageView()
        {
            InitializeComponent();
            ChangeAeroIcon();

            SetLTR();

            viewModel = App.Locator.UpdateVatEffectiveDateView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
          
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            viewModel.GetAllVatEffectiveDateLogs();
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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

        private void searchButtonTapped(object sender, EventArgs e)
        {
            viewModel.IsSearchButtonVisible = false;
            viewModel.IsCloseButtonVisible = true;
        }

        private void filterButtonTapped(object sender, EventArgs e)
        {
            viewModel.FiltersClicked();
        }

        private void CloseSearchButton_Tapped(object sender, EventArgs e)
        {
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
            viewModel.SearchText = "";

            viewModel.CopiedVatLogs = viewModel.VatLogs;
            if (searchBar != null)
            {

                searchBar.Text = "";
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            searchBar = (sender as Xamarin.Forms.SearchBar);


            var keyword = e.NewTextValue;
            //if (searchBarField.Text.Length >= 1)
            //{
                try
                {
                    viewModel.SearchText = searchBar.Text;
                     viewModel.FilterWithReferenceNumber();
                }
                catch (Exception ex)
                {

                }
            //}
        }

        private void btn_Clicked(object sender, EventArgs e)
        {

        }
    }
}