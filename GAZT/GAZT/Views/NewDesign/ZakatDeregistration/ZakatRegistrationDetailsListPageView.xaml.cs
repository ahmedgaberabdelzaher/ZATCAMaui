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
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Tax Payer Details eService");
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationTaxPayerDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else if (selectedItem.ZDTitle == AppResources.TinDeregistrationRegistrationOutlets)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Outlet Details eService");
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationOutletsDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else if (selectedItem.ZDTitle == AppResources.ZZZZVATREFinancialDetails)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Financial Details eService");
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationFinancialDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                }
                else if (((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.ZZAmend || ((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.TPUpdate)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Amendment/Update eService");
                    App.ZAKATType = ((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.ZZAmend ? Enums.PageExecutionType.Amend : Enums.PageExecutionType.Update;
                    viewModel.ZAKATAmendOrUpdateClicked();
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                //else
                //{
                //    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "TIN Deregistration eService");

                //    await Task.Run(() =>
                //    {
                //        App.DisplayProgressView();
                //    });

                //    viewModel.GetNewTinDeregistrationDataCliked();
                //    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                //}

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
