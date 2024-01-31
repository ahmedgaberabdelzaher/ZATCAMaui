using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.VATDeRegistration;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{
   
    public partial class ZakatRegistrationDetailsListPageView : ContentPage
    {
        ZakatRegistrationDetailsListPageViewModel viewModel;

        public ZakatRegistrationDetailsListPageView()
        {
            try
            {
                InitializeComponent();
                ChangeAeroIcon();
                NavigationPage.SetBackButtonTitle(this, "");
                SetLTR();

                viewModel = App.Locator.ZakatRegistrationDetailsListPageView;
                On<iOS>().SetUseSafeArea(true);
                BindingContext = viewModel;
                viewModel.PopulateZakatRegListData();
            }
            catch (Exception)
            {
                viewModel.HandleExceptipon();
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;
            }
            catch (Exception)
            {

            }


        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
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

        public async void registrationDetailsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            var selectedLv = sender as SfListView;
            try
            {

                if (selectedLv.SelectedItem == null) return;

                ZakatDeregistrationDetailsListModel selectedItem = (ZakatDeregistrationDetailsListModel)selectedLv.SelectedItem;

                if (selectedItem.ZDTitle == AppResources.DBSMTaxpayerDetails)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Tax Payer Details eService");
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationTaxPayerDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else if (selectedItem.ZDTitle == AppResources.DBSMOutlets)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Outlet Details eService");
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationOutletsDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else if (selectedItem.ZDTitle == AppResources.DBSMFinancialDetails)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Financial Details eService");
                    viewModel._navigationService.NavigateTo(App.ZakatRegistrationFinancialDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                }
                else if (selectedItem.ZDTitle == AppResources.DBSMVATRegistrationDetails)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VatRegistrationTile_Tapped", "VAT Registration eService");
                    viewModel._navigationService.NavigateTo(App.VATRegistrationDisplayDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                }
                else if (((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.DBSMAmend || ((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.TPUpdate)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Amendment/Update eService");
                    App.ZAKATType = ((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.DBSMAmend ? PageExecutionType.Amend : PageExecutionType.Update;
                    viewModel.ZAKATAmendOrUpdateClicked();
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else if (selectedItem.ZDTitle == AppResources.DBSMAmendmentOfVATRegistration)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATAmend_Tapped", "VAT Amendment eService");
                    App.VATType = PageExecutionType.Amend;
                    viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                    /*
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Financial Details eService");
                    //viewModel._navigationService.NavigateTo(App.ZakatRegistrationFinancialDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                    */

                }
                else if (selectedItem.ZDTitle == AppResources.VatReactivationDashboardTitle)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("VATServicesPageView", "VATReactivation_Tapped", "VAT Reactivation eService");
                    App.VATType = PageExecutionType.Reactivation;
                    viewModel._navigationService.NavigateTo(App.VATAmendReactivationPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                  

                }
               
                else if (selectedItem.ZDTitle == AppResources.DBSMVATDeregistration)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATDeregistrationDetails_Tapped", "VAT Deregistration eService");
                    //viewModel._navigationService.NavigateTo(App.VATDeregistrationInstructionsPage);
                    bool IsInstructionChecked = false;
                    try
                    {
                        VATDeRegistrationDetails vATDeRegistrationDetails = await VatRegistrationWebServiceManager.GAZTGetVATDeRegistrationData();
                        IsInstructionChecked = vATDeRegistrationDetails?.d?.Agreeflg == true;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        //await Task.Run(() =>
                        //{

                        //});
                       MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                        });
                        return;
                    }


                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage(IsInstructionChecked));
                    });
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }

                else
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "TIN Deregistration eService");

                    await Task.Run(() =>
                    {
                        App.DisplayProgressView();
                    });

                    viewModel.GetNewTinDeregistrationDataCliked();
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }

                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch (Exception)
            {


            }
            finally
            {
                selectedLv.SelectedItem = null;
            }
        }
    }
}
