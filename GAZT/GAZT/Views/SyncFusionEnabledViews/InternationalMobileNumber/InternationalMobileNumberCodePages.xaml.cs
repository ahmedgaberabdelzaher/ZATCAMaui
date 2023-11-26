using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.InternationalMobileNumber;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.SyncFusionEnabledViews.InternationalMobileNumber
{
    [Preserve(AllMembers = true)]
    public partial class InternationalMobileNumberCodePages : ContentPage
    {

        InternationalMobileNumberCodePagesViewModel viewModel;

        public InternationalMobileNumberCodePages()
        {
            InitializeComponent();
            viewModel = App.Locator.InternationalMobileNumberCodePages;
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            ChangeAeroIcon();
            SetLTR();

            viewModel.onPageLoad();

        }
        private void SetLTR()
        {
    
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                searchBar.FlowDirection = FlowDirection.RightToLeft;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;

            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                searchBar.FlowDirection = FlowDirection.LeftToRight;

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
            try { 
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
            catch (Exception)
            {
                
                
            }
        }
        private  void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try { 
             var dataItem = e.Item as InternationalMobileData;
            MessagingCenter.Send(this, "SelectedItem", dataItem.Telefto.ToString());
           

            viewModel._navigationService.GoBack();
            }
            catch (Exception)
            {
                
                
            }
        }
    }
}
