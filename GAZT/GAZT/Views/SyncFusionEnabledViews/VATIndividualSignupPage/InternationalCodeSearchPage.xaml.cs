using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using EGAZT.Models;
using Xamarin.Forms;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    public partial class InternationalCodeSearchPage : ContentPage
    {
        public InternationalCodeSearchPage()
        {
            InitializeComponent();
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
               // viewModel.MobileCodes = new ObservableCollection<InternationalMobileData>(viewModel.MobileCodes.Where(name => name.Landx.ToLower().Contains(searchPhrase.ToLower())));
            }
            else
            {
               // viewModel.refreshList();
            }

        }
        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as InternationalMobileData;
            MessagingCenter.Send(this, "SelectedItem", dataItem.Telefto.ToString());


            //viewModel._navigationService.GoBack();
        }
    
}
}
