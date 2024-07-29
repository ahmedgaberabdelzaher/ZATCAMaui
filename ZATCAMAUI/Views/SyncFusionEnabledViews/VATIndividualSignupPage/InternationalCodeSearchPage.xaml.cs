using Mopups.Pages;
using Mopups.Services;
using System.Collections.ObjectModel;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class InternationalCodeSearchPage : PopupPage
    {
        InternationalCodeSearchPageViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public InternationalCodeSearchPage(ObservableCollection<InternationalMobileData> countryCodeData)
        {
            InitializeComponent();

            viewModel = App.Locator.InternationalCodeSearchPage;
            this.BindingContext = viewModel;

            mobileData = countryCodeData;
            viewModel.MobileCodes = mobileData;
            viewModel.MobileCodesAllValues = mobileData;

        }
        protected override void OnAppearing()

        {
            base.OnAppearing();

            viewModel.onPageLoad();
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }

        private void Close_Tapped(object sender, EventArgs e)
        {

            MopupService.Instance.PopAsync();

        }

        void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
        {
            try
            {
                var dataItem = e.Parameter as InternationalMobileData;
                MessagingCenter.Send(this, "SelectedItem", dataItem.Telefto.ToString());

                MessagingCenter.Send(this, "SelectedCountryCode", dataItem.Land1.ToString());
                MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {

            }
           
        }

        void searchEntry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
        {

            try
            {
                if (e != null)
                {
                    
                    var value = e.NewTextValue.Trim().ToLower();

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        viewModel.refreshList();
                        viewModel.MobileCodes = mobileData;
                    }
                    else
                    {
                        var result = viewModel.MobileCodesAllValues.Where(s => s.Landx.ToLower().Contains(value));
                        viewModel.MobileCodes = new ObservableCollection<InternationalMobileData>(result);
                    }
                }
            }
            catch (Exception)
            {
                searchEntry.Text = string.Empty;
            }

           
        }
    }
}
