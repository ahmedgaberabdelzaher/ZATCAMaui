using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.InternationalMobileNumber;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.InternationalMobileNumber
{
    public partial class InternationalMobileNumberCodePages : ContentPage
    {

        InternationalMobileNumberCodePagesViewModel viewModel;

        public InternationalMobileNumberCodePages()
        {
            InitializeComponent();
            viewModel = App.Locator.InternationalMobileNumberCodePages;
            BindingContext = viewModel;
            On<iOS>().SetUseSafeArea(true);

            ChangeAeroIcon();

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
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var searchPhrase = e.NewTextValue.Trim();

                if (searchPhrase.Length > 0)
                {
                    viewModel.MobileCodes = new ObservableCollection<InternationalMobileData>(viewModel.MobileCodes.
                        Where(name => ( name.Landx.ToLower().Contains(searchPhrase.ToLower()) ) 
                        ||  (name.Telefto.ToLower().Contains(searchPhrase.ToLower())) ));
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
        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
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
