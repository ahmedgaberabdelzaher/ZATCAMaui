using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class InternationalCodeSearchPage : PopupPage
    {
        InternationalCodeSearchPageViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public InternationalCodeSearchPage(ObservableCollection<InternationalMobileData> countryCodeData)
        {
            InitializeComponent();

            viewModel = App.Locator.InternationalCodeSearchPage;
            this.BindingContext =  viewModel;

            this.mobileData = countryCodeData;
            viewModel.MobileCodes = this.mobileData;
            viewModel.MobileCodesAllValues = this.mobileData;

            SetLTR();
        }
        protected override void OnAppearing()

        {
            base.OnAppearing();

            viewModel.onPageLoad();
        }
        private void SetLTR()
        {

            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;

            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;

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
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchPhrase = e.NewTextValue.Trim();

            try
            {
                if (searchPhrase.Length > 0)
                {
                    viewModel.MobileCodes = new ObservableCollection<InternationalMobileData>(viewModel.MobileCodesAllValues.Where(name => (name.Landx.ToLower().Contains(searchPhrase.ToLower())) || (name.Telefto.ToLower().Contains(searchPhrase.ToLower()))));
                }
                else
                {
                    viewModel.refreshList();
                    viewModel.MobileCodes = this.mobileData;
                }
            }
            catch(Exception)
            {
                
            }
        }
       
        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try { 
            var dataItem = e.Item as InternationalMobileData;
            MessagingCenter.Send(this, "SelectedItem", dataItem.Telefto.ToString());

            MessagingCenter.Send(this, "SelectedCountryCode", dataItem.Land1.ToString());
            PopupNavigation.Instance.PopAsync();
            }
            catch (Exception)
            {
                
                
            }
        }
        private void Close_Tapped(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PopAsync();

        }
    }
}
