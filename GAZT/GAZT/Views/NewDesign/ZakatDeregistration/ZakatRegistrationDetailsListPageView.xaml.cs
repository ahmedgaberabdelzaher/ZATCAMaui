using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Enums;
using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.VATDeRegistration;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    [Preserve(AllMembers = true)]
    public partial class ZakatRegistrationDetailsListPageView : ContentPage
    {
        ZakatRegistrationDetailsListPageViewModel viewModel;

        public ZakatRegistrationDetailsListPageView()
        {
            try
            {
                InitializeComponent();
                ChangeAeroIcon();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                SetLTR();

                viewModel = App.Locator.ZakatRegistrationDetailsListPageView;
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                this.BindingContext = viewModel;
                viewModel.PopulateZakatRegListData();
            }
            catch (Exception ex)
            {
                viewModel.HandleExceptipon(ex);
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;
            }
            catch (Exception e)
            {

            }


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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }

        }

        public async void registrationDetailsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
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
                    App.ZAKATType = ((ZakatDeregistrationDetailsListModel)e.AddedItems[0]).ZDTitle == AppResources.DBSMAmend ? Enums.PageExecutionType.Amend : Enums.PageExecutionType.Update;
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
                    /*
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Financial Details eService");
                    //viewModel._navigationService.NavigateTo(App.ZakatRegistrationFinancialDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                    */

                }
                /*else if (selectedItem.ZDTitle == AppResources.Registrations)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ZakatRegistrationDetailsListPageView", "RegistrationDetailsListView_SelectionChanged", "Establishment Registration Financial Details eService");
                    //viewModel._navigationService.NavigateTo(App.ZakatRegistrationFinancialDetails);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                } */
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                        });
                        return;
                    }


                    Device.BeginInvokeOnMainThread(() =>
                    {
                        PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage(IsInstructionChecked));
                    });
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                    /*Device.BeginInvokeOnMainThread(() =>
                    {
                        PopupNavigation.Instance.PushAsync(new VATDeregistrationInstructionsPage());
                    });*/
                    //AppDynamics.Agent.Instrumentation.EndCall(callTracker);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
            finally
            {
                selectedLv.SelectedItem = null;
            }
        }
    }
}
