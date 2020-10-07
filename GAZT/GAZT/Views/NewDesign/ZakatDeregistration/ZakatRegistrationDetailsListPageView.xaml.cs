using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    public partial class ZakatRegistrationDetailsListPageView : ContentPage
    {
        ZakatRegistrationDetailsListPageViewModel viewModel;

        public ZakatRegistrationDetailsListPageView()
        {
            InitializeComponent();
            SetLTR();

            viewModel = App.Locator.ZakatRegistrationDetailsListPageView;
            ChangeAeroIcon();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.PopulateZakatRegListData();
            ChangeArrowDirection();
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

        public void ChangeArrowDirection()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {

                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }


        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        public async void registrationDetailsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            try
            {
                var selectedLv = sender as SfListView;
                ZakatDeregistrationDetailsListModel selectedItem = (ZakatDeregistrationDetailsListModel)selectedLv.SelectedItem;

                if (selectedItem.ZDTitle == AppResources.ZZTaxPayerDetails)
                {
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationTaxPayerDetails);
                }
                else if (selectedItem.ZDTitle == AppResources.TinDeregistrationRegistrationOutlets)
                {
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationOutletsDetails);
                }
                else if (selectedItem.ZDTitle == AppResources.ZZZZVATREFinancialDetails)
                {
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationFinancialDetails);
                }
                else if (((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.ZZAmend || ((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.TPUpdate)
                {
                    App.ZAKATType = ((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.ZZAmend ? Enums.PageExecutionType.Amend : Enums.PageExecutionType.Update;
                    viewModel.ZAKATAmendOrUpdateClicked();
                }
                else
                {
                    await Task.Run(() =>
                    {
                        App.DisplayProgressView();
                    });

                    viewModel.GetNewTinDeregistrationDataCliked();
                }

                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
