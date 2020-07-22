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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class InternationalCodeSearchPage : PopupPage
    {
        InternationalCodeSearchPageViewModel viewModel;

        public InternationalCodeSearchPage()
        {
            InitializeComponent();

            viewModel = App.Locator.InternationalCodeSearchPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;

            viewModel.onPageLoad();

            SetLTR();
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

            if (searchPhrase.Length > 0)
            {
                viewModel.MobileCodes = new ObservableCollection<InternationalMobileData>(viewModel.MobileCodes.Where(name => name.Landx.ToLower().Contains(searchPhrase.ToLower())));
            }
            else
            {
                viewModel.refreshList();
            }

        }
       
        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as InternationalMobileData;
            MessagingCenter.Send(this, "SelectedItem", dataItem.Telefto.ToString());
            PopupNavigation.Instance.PopAsync();

            // viewModel._navigationService.GoBack();
        }
        private void Close_Tapped(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PopAsync();

        }
    }
}
